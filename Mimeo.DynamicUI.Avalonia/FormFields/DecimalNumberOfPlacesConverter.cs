using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimeo.DynamicUI.Avalonia.FormFields
{
    public class DecimalPlacesToFormatConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int decimalPlaces)
            {
                return $"0." + string.Join("", Enumerable.Repeat('0', decimalPlaces));
            }
            return "0";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string format && format.StartsWith("0."))
            {
                int decimalPlaces = format.Length - 2;
                return decimalPlaces;
            }

            return 0;
        }
    }
}
