using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimeo.DynamicUI.Avalonia.FormFields
{
    public class DecimalPlacesToIncrementConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int decimalPlaces)
            {
                return (decimal)Math.Pow(10, -1 * decimalPlaces);
            }

            return 1;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is decimal increment)
            {
                int decimalPlaces = (int)Math.Round(-1 * Math.Log10((double)increment), 0);
                return decimalPlaces;
            }

            return 0;
        }
    }
}
