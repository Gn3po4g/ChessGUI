using System.Collections.Frozen;
using ChessGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChessGUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly MarkViewModel current = new();
    private readonly MarkViewModel start = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Pieces))]
    private List<MovePointViewModel> movePoints = [];

    private readonly ChessViewModel[] chesses =
    [
        // Black
        new ChessViewModel(3, 0, ChessType.BlackAdvisor),
        new ChessViewModel(5, 0, ChessType.BlackAdvisor),
        new ChessViewModel(2, 0, ChessType.BlackBishop),
        new ChessViewModel(6, 0, ChessType.BlackBishop),
        new ChessViewModel(1, 0, ChessType.BlackKnight),
        new ChessViewModel(7, 0, ChessType.BlackKnight),
        new ChessViewModel(0, 0, ChessType.BlackRook),
        new ChessViewModel(8, 0, ChessType.BlackRook),
        new ChessViewModel(4, 0, ChessType.BlackKing),
        new ChessViewModel(0, 3, ChessType.BlackPawn),
        new ChessViewModel(2, 3, ChessType.BlackPawn),
        new ChessViewModel(4, 3, ChessType.BlackPawn),
        new ChessViewModel(6, 3, ChessType.BlackPawn),
        new ChessViewModel(8, 3, ChessType.BlackPawn),
        new ChessViewModel(1, 2, ChessType.BlackCannon),
        new ChessViewModel(7, 2, ChessType.BlackCannon),
        // Red
        new ChessViewModel(3, 9, ChessType.RedAdvisor),
        new ChessViewModel(5, 9, ChessType.RedAdvisor),
        new ChessViewModel(2, 9, ChessType.RedBishop),
        new ChessViewModel(6, 9, ChessType.RedBishop),
        new ChessViewModel(1, 9, ChessType.RedKnight),
        new ChessViewModel(7, 9, ChessType.RedKnight),
        new ChessViewModel(0, 9, ChessType.RedRook),
        new ChessViewModel(8, 9, ChessType.RedRook),
        new ChessViewModel(4, 9, ChessType.RedKing),
        new ChessViewModel(0, 6, ChessType.RedPawn),
        new ChessViewModel(2, 6, ChessType.RedPawn),
        new ChessViewModel(4, 6, ChessType.RedPawn),
        new ChessViewModel(6, 6, ChessType.RedPawn),
        new ChessViewModel(8, 6, ChessType.RedPawn),
        new ChessViewModel(1, 7, ChessType.RedCannon),
        new ChessViewModel(7, 7, ChessType.RedCannon),
    ];

    public List<IPiece> Pieces => [current, start, .. chesses, .. MovePoints];

    private Board ChessBoard =>
        chesses
            .Where(chess => chess.IsAlive)
            .ToDictionary(chess => chess.Position)
            .ToFrozenDictionary();

    private bool redTurn = true;

    [RelayCommand(CanExecute = nameof(CanClick))]
    private void Click(ChessViewModel chess)
    {
        start.Show = false;
        current.Show = true;
        current.Position = chess.Position;
        MovePoints = chess
            .GetMovePoints(ChessBoard)
            .Select(point => new MovePointViewModel(point))
            .ToList();
    }

    private bool CanClick(ChessViewModel chess) => chess.IsRed == redTurn;

    [RelayCommand]
    private void MoveTo(MovePointViewModel movePoint)
    {
        start.Position = current.Position;
        start.Show = true;
        if (ChessBoard.TryGetValue(movePoint.Position, out var chess))
        {
            chess.IsAlive = false;
        }
        ChessBoard[current.Position].Position = movePoint.Position;
        current.Position = movePoint.Position;
        MovePoints = [];
        redTurn = !redTurn;
    }

    [RelayCommand]
    private void ResetBoard()
    {
        Array.ForEach(chesses, chess => chess.ResetStatus());
        redTurn = true;
        start.Show = current.Show = false;
        MovePoints = [];
    }
}
