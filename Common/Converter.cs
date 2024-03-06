using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ChessGUI.Common {
    public class Bool2VisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            Visibility? v = value as Visibility?;
            if (v != null && v == Visibility.Visible) {
                return true;
            } else {
                return false;
            }
        }
    }
    public class Type2ImageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (BitmapImage)Application.Current.Resources[value.ToString()];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            foreach (DictionaryEntry item in Application.Current.Resources) {
                if (value.Equals(item.Value)) { return item.Key; }
            }
            return Model.Type.none;
        }
    }
}
