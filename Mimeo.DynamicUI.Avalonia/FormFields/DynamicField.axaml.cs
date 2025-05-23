using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Mimeo.DynamicUI.Avalonia.FormFields;

public partial class DynamicField : UserControl
{
    public DynamicField()
    {
        InitializeComponent();
    }
    
    public static readonly StyledProperty<ViewModel?> ViewModelProperty = AvaloniaProperty.Register<DynamicField, ViewModel?>(nameof(ViewModel));
    public static readonly StyledProperty<FormFieldDefinition?> FormFieldProperty = AvaloniaProperty.Register<DynamicField, FormFieldDefinition?>(nameof(FormField));

    public ViewModel? ViewModel
    {
        get => GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public FormFieldDefinition? FormField
    {
        get => GetValue(FormFieldProperty);
        set => SetValue(FormFieldProperty, value);
    }
}