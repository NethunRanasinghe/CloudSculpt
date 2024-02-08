using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CloudSculpt.Converters;

public class ConfigCloudInfraEditOsTypeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var bindingFailed = new ObservableCollection<string> { "Binding Failed !" };
        var invalidValue = new ObservableCollection<string> { "Invalid Value !" };
        
        if (value is not string os) return bindingFailed;
        var osValue = os.ToLower();
        
        if (osValue.Equals("v"))
        {
            var osList = new ObservableCollection<string>
            {
                "linux",
                "windows"
            };

            return osList;
        }

        if (osValue.Equals("d") || osValue.Equals("k"))
        {
            var osList = new ObservableCollection<string>
            {
                "linux"
            };

            return osList;
        }
        
        return invalidValue;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}