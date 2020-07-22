using System;
using System.Globalization;
using System.Windows.Data;

namespace DataFusionPlatformPlugin.Converters
{
    public class ObjectToSingleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (float.TryParse(value?.ToString(), out float floatValue))
                return floatValue;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
