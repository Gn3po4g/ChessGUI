global using Board = System.Collections.Frozen.FrozenDictionary<
    XiangqiGUI.Models.Position,
    XiangqiGUI.ViewModels.PieceViewModel>;
using System.Collections.Frozen;
using XiangqiGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace XiangqiGUI.ViewModels;

public record ClickMessage(ViewModelBase ViewModel);

public partial class MainWindowViewModel : ViewModelBase, IRecipient<ClickMessage>
{
    private readonly MarkViewModel _current = new();

    private readonly MarkViewModel _start = new();

    private readonly PieceViewModel[] _chesses = Piece.ChessBoard.Select(chess => new PieceViewModel(chess)).ToArray();

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(Pieces))]
    private MovePointViewModel[] _movePoints = [];

    public ICanvasItem[] Pieces => [_start, _current, .._chesses, ..MovePoints];

    public MainWindowViewModel()
    {
        IsActive = true;
    }

    private Board ChessBoard => _chesses
        .Where(chess => chess.IsAlive)
        .ToDictionary(chess => chess.Position)
        .ToFrozenDictionary();

    private bool _redTurn = true;

    private void Click(PieceViewModel piece)
    {
        if (piece.IsRed != _redTurn) return;
        _start.Show = false;
        _current.Position = piece.Position;
        _current.Show = true;
        MovePoints = piece
            .GetMovePoints(ChessBoard)
            .Select(point => new MovePointViewModel(point))
            .ToArray();
    }

    private void MoveTo(MovePointViewModel movePoint)
    {
        _start.Position = _current.Position;
        _start.Show = true;
        if (ChessBoard.TryGetValue(movePoint.Position, out var chess))
        {
            chess.IsAlive = false;
        }

        ChessBoard[_current.Position].Position = movePoint.Position;
        _current.Position = movePoint.Position;
        MovePoints = [];
        _redTurn = !_redTurn;
    }

    public void Receive(ClickMessage message)
    {
        switch (message.ViewModel)
        {
            case PieceViewModel chess:
                Click(chess);
                break;
            case MovePointViewModel movePoint:
                MoveTo(movePoint);
                break;
        }
    }

    public void ResetBoard()
    {
        Array.ForEach(_chesses, chess => chess.ResetStatus());
        _redTurn = true;
        _start.Show = false;
        _current.Show = false;
        MovePoints = [];
    }
}