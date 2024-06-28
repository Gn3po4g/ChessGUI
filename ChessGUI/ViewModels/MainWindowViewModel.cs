using ChessGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;

namespace ChessGUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    public double BoardWidth => Piece.PieceSize * 9;
    public double BoardHeight => Piece.PieceSize * 10;


    private readonly Mark current = new();
    private readonly Mark start = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Pieces))]
    private MovePoint[] movePoints = [];

    private Chess[] chesses =
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

    public IEnumerable<Piece> Pieces => new Piece[] { current, start }.Concat(chesses).Concat(MovePoints);

    private Dictionary<(int, int), Chess> ChessPositions => chesses
        .Where(chess => chess.IsAlive)
        .ToDictionary(chess => (chess.X, chess.Y));

    private bool redTurn = true;

    [RelayCommand(CanExecute = nameof(CanClick))]
    private void Click(Chess chess) {
        start.Show = false;
        current.Show = true;
        current.X = chess.X;
        current.Y = chess.Y;
        MovePoints = chess.GetMovePoints(ChessPositions).ToArray();
    }

    private bool CanClick(Chess chess) => !(chess.IsRed ^ redTurn);

    [RelayCommand]
    private void MoveTo(MovePoint movePoint) {
        if (ChessPositions.TryGetValue(new(movePoint.X, movePoint.Y), out var chess)) {
            chess.IsAlive = false;
        }
        start.X = current.X;
        start.Y = current.Y;
        start.Show = true;
        chess = ChessPositions[new(current.X, current.Y)];
        chess.X = movePoint.X; 
        chess.Y = movePoint.Y;
        current.X = movePoint.X;
        current.Y = movePoint.Y;
        MovePoints = [];
        redTurn ^= true;
    }
}