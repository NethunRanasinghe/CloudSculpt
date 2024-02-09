using System;
using System.Globalization;
using Avalonia.Data.Converters;
using CloudSculpt.Views.Elements;

namespace CloudSculpt.Converters;

public class ConfigCloudInfraEditParamViewConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var vmConfigParam = new ConfigCloudInfraEditVm();
        var containerConfigContainer = new ConfigCloudInfraEditContainer();
        var containerConfigKube = new ConfigCloudInfraEditKube();
        
        // TODO :: Return an Error Page
        if (value is not string os) return vmConfigParam;
        var osValue = os.ToLower();
        
        if (osValue.Equals("v"))
        {
            return vmConfigParam;
        }
        
        if (osValue.Equals("d"))
        {
            return containerConfigContainer;
        }

        if (osValue.Equals("k"))
        {
            return containerConfigKube;
        }

        return vmConfigParam;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}