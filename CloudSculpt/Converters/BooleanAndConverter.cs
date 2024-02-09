using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace CloudSculpt.Converters;

public class BooleanAndConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        return values.OfType<IConvertible>().All(System.Convert.ToBoolean);
    }
}