using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Mimeo.DynamicUI.Avalonia.FormFields;

public partial class TableField : UserControl
{
    public TableField()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == DataContextProperty)
        {
            TheInlineEditGrid.NewItemCreator = () => ListFieldDefinition?.CreateNewItem() as ViewModel ?? new ViewModel();
        }
    }

    private IListFieldDefinition? ListFieldDefinition => (DataContext as FormFieldViewModel)?.FormFieldDefinition as IListFieldDefinition;
}