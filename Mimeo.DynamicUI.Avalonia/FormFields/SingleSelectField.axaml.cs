using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Mimeo.DynamicUI.Avalonia.Controls;
using Mimeo.DynamicUI.Avalonia.FormFields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Mimeo.DynamicUI.Avalonia;

public partial class SingleSelectField : UserControl
{
    public SingleSelectField()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var serviceProvider = (IServiceProvider)this.FindResource(typeof(IServiceProvider))!;
        var stringLocalizer = serviceProvider.GetRequiredService<IStringLocalizer>();
        this.Resources["StringLocalizer"] = new LocalizeConverter(stringLocalizer);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
            
        if (change.Property == DataContextProperty)
        {
            Items = ListFieldDefinition?.Items
                    ?.Select(i => new RadioListItem(ViewModel!.ViewModel, ViewModel.FormFieldDefinition, i.Name, i.Value))
                    .ToList();
        }
    }

    private FormFieldViewModel? ViewModel => DataContext as FormFieldViewModel;
    private SelectFormFieldDefinition? ListFieldDefinition => ViewModel?.FormFieldDefinition as SelectFormFieldDefinition;


    public static readonly StyledProperty<List<RadioListItem>?> ItemsProperty =
        AvaloniaProperty.Register<SingleSelectField, List<RadioListItem>?>(nameof(Items));

    protected List<RadioListItem>? Items
    {
        get => GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public class RadioListItem : ListItem
    {
        public RadioListItem(ViewModel viewModel, FormFieldDefinition formField, string name, string value) : base(name, value)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            FormFieldDefinition = formField ?? throw new ArgumentNullException(nameof(formField));

            if (ViewModel is INotifyPropertyChanged notifyPropertyChanged)
            {
                notifyPropertyChanged.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        public ViewModel ViewModel { get; set; }

        public FormFieldDefinition FormFieldDefinition { get; set; }

        public bool IsSelected
        {
            get => (string?)ViewModel.GetValue(FormFieldDefinition) == Value;
            set
            {
                if (value)
                {
                    ViewModel.SetValue(FormFieldDefinition, Value);
                }
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FormFieldDefinition.PropertyName)
            {
                RaisePropertyChanged(nameof(IsSelected));
            }
        }
    }
}
