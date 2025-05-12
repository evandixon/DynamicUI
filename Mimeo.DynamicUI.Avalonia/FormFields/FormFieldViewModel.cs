using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimeo.DynamicUI.Avalonia.FormFields
{
    public class FormFieldViewModel : INotifyPropertyChanged
    {
        public FormFieldViewModel(ViewModel viewModel, FormFieldDefinition formFieldDefinition)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            FormFieldDefinition = formFieldDefinition ?? throw new ArgumentNullException(nameof(formFieldDefinition));

            if (ViewModel is INotifyPropertyChanged notifyPropertyChanged)
            {
                notifyPropertyChanged.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public FormFieldDefinition FormFieldDefinition { get; set; }
        public ViewModel ViewModel { get; set; }

        public object? Value
        {
            get => ViewModel.GetValue(FormFieldDefinition);
            set => ViewModel.SetValue(FormFieldDefinition, value);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FormFieldDefinition.PropertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }
    }
}
