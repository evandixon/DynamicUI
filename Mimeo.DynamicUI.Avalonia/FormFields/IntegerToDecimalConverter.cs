using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Mimeo.DynamicUI.Avalonia.FormFields
{
    public class IntegerToDecimalConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return (decimal)intValue;
            }

            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                return (int)decimalValue;
            }
            return value;
        }
    }
}
