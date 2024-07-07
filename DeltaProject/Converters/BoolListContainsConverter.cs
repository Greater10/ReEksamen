using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace DeltaProject.Converters
{
    public class BoolListContainsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is IList list && values[1] is string item)
            {
                return list.Contains(item);
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var result = new object[2];
            if (value is bool isChecked && targetTypes.Length == 2)
            {
                result[0] = Binding.DoNothing;
                result[1] = Binding.DoNothing;

                if (targetTypes[0] == typeof(IList) && targetTypes[1] == typeof(string))
                {
                    result[0] = new Action<IList, string>((list, item) =>
                    {
                        if (isChecked)
                        {
                            if (!list.Contains(item))
                            {
                                list.Add(item);
                            }
                        }
                        else
                        {
                            if (list.Contains(item))
                            {
                                list.Remove(item);
                            }
                        }
                    });
                    result[1] = parameter; // item
                }
            }
            return result;
        }
    }
}
