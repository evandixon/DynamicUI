namespace Mimeo.DynamicUI
{
    public enum FormFieldType
    {
        Hidden = 0,
        Text,
        Combobox,
        Checkbox,
        SingleSelect,
        SingleSelectDropdown,
        SingleSelectDataSourceDropdown,
        MultiSelect,
        MultiSelectDropdown,
        MultiSelectDataSourceDropdown,
        Date,
        Time,
        DateTime,
        Color,
        Integer,
        Decimal,
        [Obsolete("Use Table, SectionList, or ReorderableSectionList instead")]
        List,
        Table,
        SectionList,
        ReorderableSectionList,
        Nullable,
        Guid,
        Section,
        Custom
    }
}
