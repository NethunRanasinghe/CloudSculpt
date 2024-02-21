using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Styling;

namespace CloudSculpt.Converters;

public class SettingsThemeNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string selectedTheme) return ThemeVariant.Light;
        if (selectedTheme.Equals("Light"))
        {
            return ThemeVariant.Light;
        }
        
        if (selectedTheme.Equals("Dark"))
        {
            return ThemeVariant.Dark;
        }
        
        if (selectedTheme.Equals("Default"))
        {
            return ThemeVariant.Default;
        }

        return ThemeVariant.Light;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}