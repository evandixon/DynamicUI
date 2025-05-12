using Avalonia;
using Avalonia.Controls;
using System;

namespace Mimeo.DynamicUI.Avalonia.Controls;

public partial class DateTimePicker : UserControl
{
    public DateTimePicker()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SelectedDateTimeProperty)
        {
            RaisePropertyChanged(SelectedDateProperty, ((DateTime?)change.OldValue)?.Date, (DateTime?)change.NewValue);
            RaisePropertyChanged(SelectedTimeProperty, ((DateTime?)change.OldValue)?.TimeOfDay, ((DateTime?)change.NewValue)?.TimeOfDay);
        }
    }

    public static readonly StyledProperty<DateTime?> SelectedDateTimeProperty =
        AvaloniaProperty.Register<DateTimePicker, DateTime?>(nameof(SelectedDateTime));

    public static readonly DirectProperty<DateTimePicker, DateTimeOffset?> SelectedDateProperty =
        AvaloniaProperty.RegisterDirect<DateTimePicker, DateTimeOffset?>(nameof(SelectedDate),
            o => o.SelectedDate,
            (o, v) => o.SelectedDate = v);

    public static readonly DirectProperty<DateTimePicker, TimeSpan?> SelectedTimeProperty =
        AvaloniaProperty.RegisterDirect<DateTimePicker, TimeSpan?>(nameof(SelectedTime),
            o => o.SelectedTime,
            (o, v) => o.SelectedTime = v);

    public DateTime? SelectedDateTime
    {
        get => GetValue(SelectedDateTimeProperty);
        set => SetValue(SelectedDateTimeProperty, value);
    }

    public DateTimeOffset? SelectedDate
    {
        get => SelectedDateTime?.Date;
        set
        {
            if (value == null)
            {
                SelectedDateTime = null;
                return;
            }

            var oldValue = SelectedDateTime;
            SelectedDateTime = value.Value.Date + (oldValue?.TimeOfDay);
            RaisePropertyChanged(SelectedDateProperty, oldValue?.Date, SelectedDateTime?.Date);
        }
    }

    public TimeSpan? SelectedTime
    {
        get => SelectedDateTime?.TimeOfDay;
        set
        {
            if (value == null)
            {
                SelectedDateTime = null;
                return;
            }

            var oldValue = SelectedDateTime;
            SelectedDateTime = oldValue?.Date + value.Value;
            RaisePropertyChanged(SelectedTimeProperty, oldValue?.TimeOfDay, SelectedDateTime?.TimeOfDay);
        }
    }
}