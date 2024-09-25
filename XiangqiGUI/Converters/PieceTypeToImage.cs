using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using XiangqiGUI.Models;

namespace XiangqiGUI.Converters;

public class PieceTypeToImage : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is PieceType type && Enum.IsDefined(typeof(PieceType), type))
        {
            return new Bitmap(AssetLoader.Open(new Uri($"avares://XiangQiGUI/Assets/{type}.png")));
        }

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}