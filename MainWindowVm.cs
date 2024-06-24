using System.Media;
using ChessGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChessGUI;

internal partial class MainWindowVm : ObservableObject {
    public static double BoardWidth => Piece.PieceSize * 9;
    public static double BoardHeight => Piece.PieceSize * 10;

    private readonly IEnumerable<Chess> _chesses =
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

    private Dictionary<(int, int), Chess> ChessPositions => _chesses
        .Where(chess => chess.IsAlive)
        .ToDictionary(chess => (chess.X, chess.Y));

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Pieces))]
    private IEnumerable<MovePoint> _movePoints = [];

    public IEnumerable<Piece> Pieces => _chesses.Cast<Piece>().Concat(MovePoints);

    [ObservableProperty] private Piece? _current;
    [ObservableProperty] private Piece? _start;

    private bool _redTurn = true;

    [RelayCommand]
    private void Reset() {
        foreach (var chess in _chesses) {
            chess.ResetPosition();
        }
        Current = null;
        Start = null;
        MovePoints = [];
        _redTurn = true;
    }

    [RelayCommand(CanExecute = nameof(CanClick))]
    private void Click(Chess chess) {
        Start = null;
        Current = chess;
        MovePoints = chess.GetMovePoints(ChessPositions);
        new SoundPlayer("Resources/capture.wav").Play();
    }

    private bool CanClick(Chess chess) => !(chess.IsRed ^ _redTurn);

    [RelayCommand]
    private void MoveTo(MovePoint movePoint) {
        if (Current == null) return;
        if (ChessPositions.TryGetValue(new(movePoint.X, movePoint.Y), out var chess)) {
            chess.IsAlive = false;
        }
        Start = new Piece { X = Current.X, Y = Current.Y };
        Current.X = movePoint.X;
        Current.Y = movePoint.Y;
        MovePoints = [];
        _redTurn ^= true;
        new SoundPlayer("Resources/move.wav").Play();
    }
}