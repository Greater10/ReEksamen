using System;
using System.Globalization;
using System.Windows.Data;

namespace DeltaProject.Converters
{
    public class BooleanToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? "Ja" : "Nej";
            }
            return "Nej";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue.Equals("Ja", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
