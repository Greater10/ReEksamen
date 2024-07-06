using System;
using System.Globalization;
using System.Windows.Data;

namespace DeltaProject.Converters
{
    public class TestTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool hasTest = (bool)value;
            string icon = parameter as string;

            if (hasTest)
            {
                return icon;
            }
            else
            {
                return null; // Or a default/placeholder character if necessary
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
