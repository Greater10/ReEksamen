using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DeltaProject.Converters
{
    public class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int priority)
            {
                switch (priority)
                {
                    case 1:
                        return new SolidColorBrush(Color.FromRgb(144, 238, 144)); // LightGreen
                    case 2:
                        return new SolidColorBrush(Color.FromRgb(255, 255, 224)); // LightYellow
                    case 3:
                        return new SolidColorBrush(Color.FromRgb(255, 182, 193)); // LightCoral
                }
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
