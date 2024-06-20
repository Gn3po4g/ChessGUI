using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ChessGUI.Common
{
    public class Bool2VisibilityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is true ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is Visibility.Visible;
        }
    }

    public class Type2ImageConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return new BitmapImage();
            var type = value.ToString() ?? "";
            var ret = Application.Current.Resources[type];
            if (ret == null) return new BitmapImage();
            return (BitmapImage)ret;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            foreach (var item in Application.Current.Resources.Cast<DictionaryEntry>()
                         .Where(item => value != null && value.Equals(item.Value)))
            {
                return item.Key;
            }

            return Model.Type.None;
        }
    }
}