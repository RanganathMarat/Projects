using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace DataBindingDemo.Convertors
{
    class NameCodeMultiValueConvertor:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in values)
            {
                sb.Append(s).Append(' ');
            }
            return sb.ToString().Trim();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as string).Split(' ');
        }
    }
}
