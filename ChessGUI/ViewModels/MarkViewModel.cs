using ChessGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChessGUI.ViewModels;

public partial class MarkViewModel : ViewModelBase, IPiece
{
    [ObservableProperty]
    private bool show;

    [ObservableProperty]
    private Position position = new();
}
