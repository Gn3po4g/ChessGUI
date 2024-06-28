using ChessGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChessGUI.Models;

public partial class Mark : Piece {
    [ObservableProperty] private bool show;
}