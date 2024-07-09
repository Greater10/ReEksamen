using System;
using System.Globalization;
using System.Windows.Data;

namespace DeltaProject.Converters
{
    public class PriorityToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int priorityValue)
            {
                switch (priorityValue)
                {
                    case 1:
                        return "Normal";
                    case 2:
                        return "Haster";
                    case 3:
                        return "Kritisk";
                    default:
                        return "Ukendt";
                }
            }
            return "Ukendt";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                switch (stringValue)
                {
                    case "Normal":
                        return 1;
                    case "Haster":
                        return 2;
                    case "Kritisk":
                        return 3;
                }
            }
            return 0;
        }
    }
}
