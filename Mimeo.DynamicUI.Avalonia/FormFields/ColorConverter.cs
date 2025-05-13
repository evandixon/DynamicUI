using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Mimeo.DynamicUI.Avalonia.FormFields;

public class ColorConverter : IValueConverter
{
    private static readonly Regex hexRegex = new(@"\#([0-9A-Z]{2})([0-9A-Z]{2})([0-9A-Z]{2})", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly Regex rgbRegex = new(@"rgb\(([0-9]{1,3}),\s?([0-9]{1,3}),\s?([0-9]{1,3})\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string stringValue)
        {
            var hexMatch = hexRegex.Match(stringValue);
            if (hexMatch.Success)
            {
                var r = byte.Parse(hexMatch.Groups[1].Value, NumberStyles.HexNumber);
                var g = byte.Parse(hexMatch.Groups[2].Value, NumberStyles.HexNumber);
                var b = byte.Parse(hexMatch.Groups[3].Value, NumberStyles.HexNumber);
                return new Color(255, r, g, b);
            }
            
            var rgbMatch = rgbRegex.Match(stringValue);
            if (rgbMatch.Success)
            {
                var r = byte.Parse(rgbMatch.Groups[1].Value);
                var g = byte.Parse(rgbMatch.Groups[2].Value);
                var b = byte.Parse(rgbMatch.Groups[3].Value);
                return new Color(255, r, g, b);
            }
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        return value;
    }
}