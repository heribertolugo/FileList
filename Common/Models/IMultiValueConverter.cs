using System;
using System.Globalization;

namespace Common.Models
{
    public interface IMultiValueConverter
    {
        object Convert(object[] value, Type targetType, object parameter, CultureInfo culture);

        object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
    }
}
