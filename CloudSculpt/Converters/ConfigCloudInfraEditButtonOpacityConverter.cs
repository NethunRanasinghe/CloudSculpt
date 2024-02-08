using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CloudSculpt.Converters;

public class ConfigCloudInfraEditButtonOpacityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool buttonStatus) return 1;
        if (buttonStatus)
        {
            return 0.7;
        }

        return 1;

    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}