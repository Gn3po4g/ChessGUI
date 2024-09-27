using CommunityToolkit.Mvvm.ComponentModel;
using XiangqiGUI.Models;

namespace XiangqiGUI.ViewModels;

public partial class BlockViewModel(Position position, Action<Position, Piece> onClick) : ViewModelBase
{
    private readonly Piece? _defaultPiece = Piece.Board.GetValueOrDefault(position);
    [ObservableProperty] private Piece? _pieceInBlock;

    [ObservableProperty] private bool _isMarked;

    public BlockViewModel() : this(new Position(9, 4), (_, _) => { })
    {
        PieceInBlock = _defaultPiece;
        IsMarked = true;
    }

    // public void ResetStatus()
    // {
    //     Position = pieceData.Position;
    //     IsAlive = true;
    // }
    public void Reset()
    {
        PieceInBlock = _defaultPiece;
    }

    public void Click()
    {
        if (PieceInBlock == null) return;
        onClick(position, PieceInBlock);
    }
}