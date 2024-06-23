using System.ComponentModel;
using System.Windows.Input;
using ChessGUI.Commands;
using ChessGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChessGUI;

internal partial class MainWindowVm : ObservableObject
{
    public static double BoardWidth => Piece.PieceSize * 9;
    public static double BoardHeight => Piece.PieceSize * 10;

    public BindingList<Chess> Chesses { get; } =
    [
        // Black
        new Chess(3, 0, ChessType.BlackAdvisor), new Chess(5, 0, ChessType.BlackAdvisor),
        new Chess(2, 0, ChessType.BlackBishop), new Chess(6, 0, ChessType.BlackBishop),
        new Chess(1, 0, ChessType.BlackKnight), new Chess(7, 0, ChessType.BlackKnight),
        new Chess(0, 0, ChessType.BlackRook), new Chess(8, 0, ChessType.BlackRook),
        new Chess(4, 0, ChessType.BlackKing), new Chess(0, 3, ChessType.BlackPawn),
        new Chess(2, 3, ChessType.BlackPawn), new Chess(4, 3, ChessType.BlackPawn),
        new Chess(6, 3, ChessType.BlackPawn), new Chess(8, 3, ChessType.BlackPawn),
        new Chess(1, 2, ChessType.BlackCannon), new Chess(7, 2, ChessType.BlackCannon),
        // Red
        new Chess(3, 9, ChessType.RedAdvisor), new Chess(5, 9, ChessType.RedAdvisor),
        new Chess(2, 9, ChessType.RedBishop), new Chess(6, 9, ChessType.RedBishop),
        new Chess(1, 9, ChessType.RedKnight), new Chess(7, 9, ChessType.RedKnight),
        new Chess(0, 9, ChessType.RedRook), new Chess(8, 9, ChessType.RedRook),
        new Chess(4, 9, ChessType.RedKing), new Chess(0, 6, ChessType.RedPawn),
        new Chess(2, 6, ChessType.RedPawn), new Chess(4, 6, ChessType.RedPawn),
        new Chess(6, 6, ChessType.RedPawn), new Chess(8, 6, ChessType.RedPawn),
        new Chess(1, 7, ChessType.RedCannon), new Chess(7, 7, ChessType.RedCannon)
    ];

    private Dictionary<(int, int), Chess> ChessPositions => Chesses
        .Where(chess => chess.IsAlive)
        .ToDictionary(chess => (chess.X, chess.Y));

    [ObservableProperty] private BindingList<MovePoint> _movePoints = [];

    [ObservableProperty] private Chess? _current;
    [ObservableProperty] private Chess? _start;

    private bool _redTurn = true;

    public ICommand ResetCommand => new ResetCommand(Reset);
    public ICommand ClickCommand => new ClickCommand(chess => chess.IsRed ^ !_redTurn, Click);
    public ICommand MoveCommand => new MoveCommand(MoveTo);

    private void Reset()
    {
        foreach (var chess in Chesses)
        {
            chess.ResetPosition();
        }

        Current = null;
        Start = null;
        MovePoints = [];
        _redTurn = true;
    }

    private void Click(Chess chess)
    {
        Start = null;
        Current = chess;
        MovePoints = new BindingList<MovePoint>(chess.GetMovePoints(Chesses).ToList());
    }

    private void MoveTo(MovePoint movePoint)
    {
        if (Current == null) return;
        var des = Chesses.SingleOrDefault(chess =>
            chess is { IsAlive: true } &&
            chess.X == movePoint.X &&
            chess.Y == movePoint.Y, null);
        if (des != null) des.ChessType = ChessType.Null;
        Start = new Chess(Current.X, Current.Y, Current.ChessType);
        Current.X = movePoint.X;
        Current.Y = movePoint.Y;
        MovePoints = [];
        _redTurn ^= true;
    }
}