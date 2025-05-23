using System;
using System.Collections.Generic;

namespace Mimeo.DynamicUI.Avalonia.FormFields;


public class SimpleViewModel<T> : ViewModel
{
    public SimpleViewModel(ListFieldDefinition<T> stackFieldDefinition, Func<T> getter, Action<T> setter)
    {
        this.stackFieldDefinition = stackFieldDefinition ?? throw new ArgumentNullException(nameof(stackFieldDefinition));
        this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        this.setter = setter ?? throw new ArgumentNullException(nameof(setter));
    }

    private readonly ListFieldDefinition<T> stackFieldDefinition;
    private readonly Func<T> getter;
    private readonly Action<T> setter;

    public T Value { get => getter(); set => setter(value); }

    protected override IEnumerable<FormFieldDefinition> GetEditFormFields()
    {
        var formField = FormField(stackFieldDefinition.GetItemFormFieldType(), () => Value);
        formField.LanguageKey = stackFieldDefinition.LanguageKey + "_header";
        yield return formField;
    }
}