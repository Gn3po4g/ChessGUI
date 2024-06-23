using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ChessGUI.Models;

namespace ChessGUI
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

    public class ChessType2ImageBrushConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var res = Application.Current.Resources;

            if (value is not ChessType chessType || !res.Contains(chessType.ToString()))
            {
                return new ImageBrush();
            }

            return (ImageBrush)res[chessType.ToString()]!;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}