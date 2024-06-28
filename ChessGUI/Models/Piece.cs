using CommunityToolkit.Mvvm.ComponentModel;

namespace ChessGUI.Models;

public partial class Piece : ObservableObject {
    public static double PieceSize => 60;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanvasTop))]
    private int _x;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanvasLeft))]
    private int _y;

    public double CanvasTop => X * PieceSize;
    public double CanvasLeft => Y * PieceSize;
}