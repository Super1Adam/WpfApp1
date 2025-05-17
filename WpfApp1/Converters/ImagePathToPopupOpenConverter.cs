using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1.Converters
{
    public class ImagePathToPopupOpenConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var isMouseOver = values[0] as bool?;
            var imagePath = values[1] as string;

            if (isMouseOver == true && !string.IsNullOrEmpty(imagePath))
                return true;

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
