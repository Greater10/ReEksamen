using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

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
                return icon; // Still returning the icon, but the binding will use a different color
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TestTypeToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool hasTest = (bool)value;
            return hasTest ? (Brush)new SolidColorBrush(Color.FromRgb(45, 45, 45)) : (Brush)new SolidColorBrush(Color.FromRgb(153, 153, 153));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
