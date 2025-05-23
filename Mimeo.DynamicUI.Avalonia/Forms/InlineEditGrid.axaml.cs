using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Styling;
using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Mimeo.DynamicUI.Avalonia.FormFields;

namespace Mimeo.DynamicUI.Avalonia.Forms;

public partial class InlineEditGrid : UserControl
{
    public InlineEditGrid()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        var serviceProvider = (IServiceProvider)this.FindResource(typeof(IServiceProvider))!;
        var stringLocalizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        _localizeConverter = new LocalizeConverter(stringLocalizer);
        _isLocalizationDefinedConverter = new IsLocalizationDefinedController(stringLocalizer);
        this.Resources["StringLocalizer"] = _localizeConverter;
        this.Resources["IsLocalizationDefinedController"] = _isLocalizationDefinedConverter;
        _formFieldDefinitionConverter = new FormFieldDefinitionViewModelConverter();
    }

    private LocalizeConverter? _localizeConverter;
    private IsLocalizationDefinedController? _isLocalizationDefinedConverter;
    private FormFieldDefinitionViewModelConverter? _formFieldDefinitionConverter;

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == DataContextProperty
            || change.Property == NewItemCreatorProperty)
        {
            RegenerateColumns();
            gridSource.Clear();
            if (ViewModels != null)
            {
                gridSource.AddRange(ViewModels);
            }
        }
    }
    
    public static readonly StyledProperty<Func<ViewModel>?> NewItemCreatorProperty =
        AvaloniaProperty.Register<InlineEditGrid, Func<ViewModel>?>(nameof(NewItemCreator));
    
    public static readonly StyledProperty<FlatTreeDataGridSource<ViewModel>?> FlatTreeDataGridSourceProperty =
        AvaloniaProperty.Register<InlineEditGrid, FlatTreeDataGridSource<ViewModel>?>(nameof(FlatTreeDataGridSource));

    public Func<ViewModel>? NewItemCreator
    {
        get => GetValue(NewItemCreatorProperty);
        set => SetValue(NewItemCreatorProperty, value);
    }

    public FlatTreeDataGridSource<ViewModel>? FlatTreeDataGridSource
    {
        get => GetValue(FlatTreeDataGridSourceProperty);
        set => SetValue(FlatTreeDataGridSourceProperty, value);
    }

    protected IEnumerable<ViewModel>? ViewModels
    {
        get
        {
            if (DataContext is null)
            {
                return null;
            }

            var contextType = DataContext.GetType();
            if (contextType.IsGenericType 
                && contextType.GetGenericTypeDefinition() == typeof(List<>)
                && contextType.GetGenericArguments()[0].IsAssignableTo(typeof(ViewModel)))
            {
                return ((IEnumerable)DataContext).Cast<ViewModel>();
            }

            return null;
        }
    }

    private ObservableCollection<ViewModel> gridSource = new();
    
    private void RegenerateColumns()
    {
        FlatTreeDataGridSource = new(gridSource);
        
        var item = NewItemCreator!.Invoke();
        if (item == null)
        {
            //TheDataGrid.Columns.Clear();
            return;
        }
        var columns = new List<DataGridColumn>();
        foreach (var formField in item.GetListForm().Values)
        {
            var headerKey = formField.LanguageKey;
            var header = _localizeConverter?.Convert(headerKey, typeof(string), null, CultureInfo.CurrentCulture);
            //var column = new TextColumn<ViewModel, TValue>();
            var template = new FuncDataTemplate<ViewModel>((v, n) => new DynamicField
            {
                ViewModel = v,
                FormField = formField
            });
            var column = new TemplateColumn<ViewModel>(header, template);
            FlatTreeDataGridSource.Columns.Add(column);
        }
    }
}