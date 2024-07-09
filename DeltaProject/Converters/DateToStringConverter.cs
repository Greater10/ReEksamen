using System;
using System.Globalization;
using System.Windows.Data;

namespace DeltaProject.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateValue)
            {
                return dateValue.ToString("dd/MM/yyyy", culture);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (DateTime.TryParseExact(stringValue, "dd/MM/yyyy", culture, DateTimeStyles.None, out DateTime dateValue))
                {
                    return dateValue;
                }
            }
            return DateTime.MinValue;
        }
    }
}
