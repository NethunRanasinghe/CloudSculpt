using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CloudSculpt.Converters;

public class HalfConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return width / 2;
        }

        return 100;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}