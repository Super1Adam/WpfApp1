using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1.Models
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            return ((DateTime?)value)?.ToString("yyyy-MM-dd"); // 格式化为日期字符串
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value as string)) return null;
            if (DateTime.TryParse(value as string, out DateTime result))
            {
                return result;
            }
            return null;
        }
    }
}