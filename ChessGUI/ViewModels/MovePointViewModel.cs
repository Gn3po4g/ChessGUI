using ChessGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChessGUI.ViewModels;

public partial class MovePointViewModel(Position position) : ViewModelBase, IPiece
{
    [ObservableProperty]
    private Position position = position;
}
