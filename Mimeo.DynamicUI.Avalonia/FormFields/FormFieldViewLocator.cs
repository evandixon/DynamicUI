using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;

namespace Mimeo.DynamicUI.Avalonia.FormFields
{
    public class FormFieldViewLocator : IDataTemplate
    {
        public IServiceProvider? ServiceProvider { get; set; }

        public Control? Build(object? param)
        {
            if (param is not FormFieldViewModel formFieldDefinition)
            {
                return null;
            }

            switch (formFieldDefinition.FormFieldDefinition.Type)
            {
                case FormFieldType.Text:
                    return new TextField();
                case FormFieldType.Combobox:
                    return new ComboboxField();
                case FormFieldType.Checkbox:
                    return new CheckboxField();
                case FormFieldType.SingleSelect:
                    return new SingleSelectField();
                case FormFieldType.MultiSelect:
                    return new MultiSelectField();
                case FormFieldType.DateTime:
                    return new DateTimeField();
                case FormFieldType.Date:
                    return new DateField();
                case FormFieldType.Time:
                    return new TimeField();
                case FormFieldType.Color:
                    return new ColorField();
                case FormFieldType.Integer:
                    return new IntegerField();
                case FormFieldType.Decimal:
                    return new DecimalField();
                case FormFieldType.Guid:
                    return new GuidField();
                case FormFieldType.Section:
                    return new SectionField();
            }

            return new TextBlock { Text = $"Could not find UI implementation for form field type '{formFieldDefinition.FormFieldDefinition.Type}'." };
        }

        public bool Match(object? data)
        {
            return data is FormFieldViewModel;
        }
    }
}
