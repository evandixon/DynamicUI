using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Mimeo.DynamicUI.Avalonia.FormFields;
using System;

namespace Mimeo.DynamicUI.Avalonia;

public partial class GuidField : UserControl
{
    public GuidField()
    {
        InitializeComponent();
    }

    protected void OnGuidGenerateClick(object sender, RoutedEventArgs e)
    {
        ValueGuid = Guid.NewGuid();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == DataContextProperty)
        {
            var oldContext = change.OldValue as FormFieldViewModel;
            var newContext = change.NewValue as FormFieldViewModel;
            Guid? oldValue = Guid.TryParse(oldContext?.Value?.ToString(), out var oldValueGuid) ? oldValueGuid : null;
            Guid? newValue = Guid.TryParse(newContext?.Value?.ToString(), out var newValueGuid) ? newValueGuid : null;
            RaisePropertyChanged(ValueGuidProperty, oldValue, newValue);
        }
    }

    public static readonly DirectProperty<GuidField, Guid?> ValueGuidProperty =
        AvaloniaProperty.RegisterDirect<GuidField, Guid?>(
            nameof(ValueGuid),
            o => o.ValueGuid,
            (o, v) => o.ValueGuid = v ?? default);

    public static readonly DirectProperty<GuidField, string?> ValueStringProperty =
        AvaloniaProperty.RegisterDirect<GuidField, string?>(
            nameof(ValueString),
            o => o.ValueString,
            (o, v) => o.ValueString = v);

    private FormFieldViewModel? ViewModel => DataContext as FormFieldViewModel;

    private object? Value
    {
        get => ViewModel?.Value;
        set
        {
            if (ViewModel != null)
            {
                var oldValue = ViewModel.Value;
                ViewModel.Value = value;
            }
        }
    }

    public Guid ValueGuid
    {
        get
        {
            if (Definition == null)
            {
                return default;
            }
            else if (Definition.PropertyType == typeof(Guid))
            {
                return (Guid?)Value ?? default;
            }
            else if (Definition.PropertyType == typeof(string))
            {
                return Guid.TryParse(Value?.ToString(), out var parsedGuid) ? parsedGuid : default;
            }

            throw new Exception("Unable to determine guid property type");
        }
        set
        {
            var oldValue = ValueGuid;
            if (Definition?.PropertyType == typeof(Guid))
            {
                Value = value;
            }
            else if (Definition?.PropertyType == typeof(string))
            {
                Value = value.ToString();
            }
            else
            {
                throw new Exception("Unable to determine guid property type");
            }

            RaisePropertyChanged(ValueGuidProperty, oldValue, value);
            RaisePropertyChanged(ValueStringProperty, oldValue.ToString(), ValueString);
        }
    }

    private string? ValueString
    {
        get => valueString ?? ValueGuid.ToString();
        set
        {
            if (Guid.TryParse(value, out var guid))
            {
                valueString = guid.ToString();
                ValueGuid = guid;
                return;
            }
            else
            {
                // User is in the middle of typing a guid
                // Don't update the raw value, but let them continue typing
                valueString = value;
            }
        }
    }
    private string? valueString;

    private FormFieldDefinition? Definition => (DataContext as FormFieldViewModel)?.FormFieldDefinition;

}