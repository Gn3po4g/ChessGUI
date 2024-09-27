using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using XiangqiGUI.Models;

namespace XiangqiGUI.Converters;

public class PieceToImage : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Piece piece)
        {
            return new Bitmap(AssetLoader.Open(new Uri($"avares://XiangQiGUI/Assets/{piece.Color}{piece.Type}.png")));
        }

        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}