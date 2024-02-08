using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CloudSculpt.Converters;

public class ConfigCloudInfraEditDistroConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var bindingFailed = new ObservableCollection<string> { "Binding Failed !" };
        var notImplemented = new ObservableCollection<string> { "Not Implemented !" };
        
        if (value is not string os) return bindingFailed;
        var osValue = os.ToLower();
        
        if (osValue.Equals("v"))
        {
            var distroList = new ObservableCollection<string>
            {
                "ubuntu 22.04",
                "ubuntu 20.04",
                "debian 12",
                "amazon linux 2023"
            };

            return distroList;
        }

        return notImplemented;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}