using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ChessGUI.ViewModels;
using System.Globalization;

namespace ChessGUI.Converters;

public class ChessTypeToImage : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is ChessType type && Enum.IsDefined(typeof(ChessType), type)) {
            return new Bitmap(AssetLoader.Open(new Uri($"avares://ChessGUI/Assets/{type}.png")));
        }
        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}
