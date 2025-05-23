using Mimeo.DynamicUI.Extensions;
using System.Linq.Expressions;

namespace Mimeo.DynamicUI
{
    public interface IListFieldDefinition
    {
        object? CreateNewItem();
    }

    public class ListFieldDefinition<T> : FormFieldDefinition, IListFieldDefinition
    {
        public ListFieldDefinition(Expression<Func<List<T>>> @for)
            : base(FormFieldType.List, LinqExtensions.Cast<List<T>, object?>(@for))
        {
        }

        public FormFieldType? ItemFormFieldType { get; set; }

        public Func<T>? NewItemCreator { get; set; }

        [Obsolete("Differentiate using FormFieldType instead")]
        public ListFieldPresentationMode PresentationMode
        {
            get => _presentationMode;
            set
            {
                _presentationMode = value;
                if (_presentationMode == ListFieldPresentationMode.Table)
                {
                    this.Type = FormFieldType.Table;
                }
                else if (_presentationMode == ListFieldPresentationMode.SectionList)
                {
                    this.Type = FormFieldType.SectionList;
                }
                else if (_presentationMode == ListFieldPresentationMode.ReorderableSectionList)
                {
                    this.Type = FormFieldType.ReorderableSectionList;
                }
            }
        }

        private ListFieldPresentationMode _presentationMode = ListFieldPresentationMode.Table;

        /// <summary>
        /// For use with <see cref="PresentationMode"/> equal to <see cref="ListFieldPresentationMode.SectionList"/> or <see cref="ListFieldPresentationMode.ReorderableSectionList"/>,
        /// a form field that displays as the header of the section
        /// </summary>
        public Func<T, FormFieldDefinition>? HeaderField { get; set; }

        public FormFieldType GetItemFormFieldType()
        {
            return ItemFormFieldType ?? DetermineFormFieldType(typeof(T));
        }

        public T CreateNewItem()
        {
            if (NewItemCreator != null)
            {
                return NewItemCreator();
            }

            if (typeof(T) == typeof(string))
            {
                return (T)(object)string.Empty;
            }

            return Activator.CreateInstance<T>();
        }

        object? IListFieldDefinition.CreateNewItem() => CreateNewItem();
    }

    [Obsolete("Differentiate using FormFieldType instead")]
    public enum ListFieldPresentationMode
    {
        Table,
        SectionList,
        ReorderableSectionList
    }
}
