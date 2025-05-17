// 文件路径：WpfApp1/Views/ImagePathToVisibilityConverter.cs

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp1.Views
{
    public class ImagePathToVisibilityConverter : IValueConverter // 👈 必须是 public
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            return string.IsNullOrWhiteSpace(path) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
