using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ChessGUI.Models;
using System;
using System.Globalization;

namespace ChessGUI.Converters;

public class ChessTypeConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is ChessType type) {
            var typeStr = type switch {
                ChessType.BlackAdvisor => "black_advisor",
                ChessType.BlackBishop => "black_bishop",
                ChessType.BlackCannon => "black_cannon",
                ChessType.BlackKing => "black_king",
                ChessType.BlackKnight => "black_knight",
                ChessType.BlackPawn => "black_pawn",
                ChessType.BlackRook => "black_rook",
                ChessType.RedAdvisor => "red_advisor",
                ChessType.RedBishop => "red_bishop",
                ChessType.RedCannon => "red_cannon",
                ChessType.RedKing => "red_king",
                ChessType.RedKnight => "red_knight",
                ChessType.RedPawn => "red_pawn",
                ChessType.RedRook => "red_rook",
                _ => throw new InvalidCastException()
            };
            return new Bitmap(AssetLoader.Open(new Uri($"avares://ChessGUI/Assets/{typeStr}.png")));
        }
        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}
