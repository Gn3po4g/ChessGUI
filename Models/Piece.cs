using CommunityToolkit.Mvvm.ComponentModel;

namespace ChessGUI.Models;

public partial class Piece : ObservableObject
{
    public static double PieceSize => 60;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(XofCanvas))]
    private int _x;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(YofCanvas))]
    private int _y;

    public double XofCanvas => X * PieceSize;
    public double YofCanvas => Y * PieceSize;
}