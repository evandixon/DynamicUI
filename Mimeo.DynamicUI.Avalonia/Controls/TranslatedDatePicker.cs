using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Mimeo.DynamicUI.Avalonia.FormFields;
using System;

namespace Mimeo.DynamicUI.Avalonia.Controls
{
    public class TranslatedDatePicker : DatePicker
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            var serviceProvider = (IServiceProvider)this.FindResource(typeof(IServiceProvider))!;
            dateTimeConverter = serviceProvider.GetRequiredService<IDateTimeConverter>();
            UpdateDisplayValue();
        }

        private IDateTimeConverter? dateTimeConverter;

        protected override Type StyleKeyOverride => typeof(DatePicker);

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == ValueProperty)
            {
                UpdateDisplayValue();
            }
            else if (change.Property == SelectedDateProperty)
            {
                UpdateRawValue();
            }
        }

        public static readonly DirectProperty<TranslatedDatePicker, object?> ValueProperty =
            AvaloniaProperty.RegisterDirect<TranslatedDatePicker, object?>(
                nameof(Value),
                o => o.Value,
                (o, v) => o.Value = v);

        public object? Value
        {
            get => _value;
            set => SetAndRaise(ValueProperty, ref _value, value);
        }
        private object? _value;

        private DateTime? DateTime
        {
            get
            {
                if (Value is DateTime dt)
                {
                    return dt;
                }
                else if (Value is DateTimeOffset dto)
                {
                    return dto.UtcDateTime;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (DateTimeDefinition?.PropertyType == typeof(DateTimeOffset))
                {
                    Value = new DateTimeOffset(value.GetValueOrDefault());
                }
                else if (DateTimeDefinition?.PropertyType == typeof(DateTimeOffset?))
                {
                    Value = value.HasValue ? new DateTimeOffset(value.Value) : (DateTimeOffset?)null;
                }
                else if (DateTimeDefinition?.PropertyType == typeof(DateTime))
                {
                    Value = value.GetValueOrDefault();
                }
                else
                {
                    Value = value;
                }
            }
        }

        private DateTimeFieldDefinition? DateTimeDefinition => (DataContext as FormFieldViewModel)?.FormFieldDefinition as DateTimeFieldDefinition;

        public static readonly DirectProperty<TranslatedDatePicker, DateDisplayMode> DisplayModeProperty =
            AvaloniaProperty.RegisterDirect<TranslatedDatePicker, DateDisplayMode>(
                nameof(DisplayMode),
                o => o.DisplayMode,
                (o, v) => o.DisplayMode = v);

        public DateDisplayMode DisplayMode { get; set; }

        private void UpdateDisplayValue()
        {
            if (dateTimeConverter == null || DateTime == null || DateTime.Value == default)
            {
                return;
            }

            SelectedDate = dateTimeConverter.UtcToDisplay(DateTime.Value, DisplayMode).Date;
        }

        private void UpdateRawValue()
        {
            if (dateTimeConverter == null)
            {
                return;
            }

            var selectedDate = SelectedDate;
            if (!selectedDate.HasValue)
            {
                Value = null;
                return;
            }

            Value = dateTimeConverter.DisplayToUtc(selectedDate.Value.DateTime.Date, DisplayMode);
        }
    }
}
