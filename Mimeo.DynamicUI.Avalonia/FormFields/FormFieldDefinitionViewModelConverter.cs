using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Mimeo.DynamicUI.Avalonia.FormFields
{
    public class FormFieldDefinitionViewModelConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            var formFieldDefinition = values.FirstOrDefault(v => v is FormFieldDefinition) as FormFieldDefinition;
            var viewModel = values.FirstOrDefault(v => v is ViewModel) as ViewModel;

            if (formFieldDefinition == null || viewModel == null)
            {
                return null;
            }

            return new FormFieldViewModel(viewModel, formFieldDefinition);
        }
    }
}
