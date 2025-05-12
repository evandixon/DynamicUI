using Avalonia.Data.Converters;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimeo.DynamicUI.Avalonia
{
    public class LocalizeConverter : IValueConverter
    {
        public LocalizeConverter(IStringLocalizer stringLocalizer)
        {
            this.stringLocalizer = stringLocalizer;
        }

        private readonly IStringLocalizer stringLocalizer;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringLocalizer[stringValue];
            }

            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringLocalizer.GetAllStrings().FirstOrDefault(s => s.Value == stringValue)?.Name ?? value;
            }
            return value;
        }
    }
}
