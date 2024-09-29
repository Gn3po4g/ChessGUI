using CommunityToolkit.Mvvm.ComponentModel;
using XiangqiGUI.Models;

namespace XiangqiGUI.ViewModels;

public partial class BlockViewModel(Position position, Action<Position, Piece>? onClick) : ViewModelBase
{
    private readonly Piece? _defaultPiece = Piece.Board.GetValueOrDefault(position);

    [ObservableProperty] private Piece? _pieceInBlock;

    [ObservableProperty] private bool _isMarked;

    [ObservableProperty] private bool _isFrom;

    [ObservableProperty] private bool _isTo;

    public BlockViewModel() : this(new Position(9, 4), null)
    {
        PieceInBlock = _defaultPiece;
        IsMarked = true;
        IsFrom = true;
        IsTo = true;
    }

    public void Reset()
    {
        PieceInBlock = _defaultPiece;
        IsMarked = false;
        IsFrom = false;
        IsTo = false;
    }

    public void Click()
    {
        if (PieceInBlock == null) return;
        onClick?.Invoke(position, PieceInBlock);
    }
}