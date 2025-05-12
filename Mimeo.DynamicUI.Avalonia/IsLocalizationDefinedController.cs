using Avalonia.Data.Converters;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;

namespace Mimeo.DynamicUI.Avalonia
{
    public class IsLocalizationDefinedController : IValueConverter
    {
        public IsLocalizationDefinedController(IStringLocalizer stringLocalizer)
        {
            this.stringLocalizer = stringLocalizer;
        }

        private readonly IStringLocalizer stringLocalizer;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                var localized = stringLocalizer[stringValue];
                return stringValue != localized;
            }
            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
