using CommunityToolkit.Mvvm.ComponentModel;
using XiangqiGUI.Models;

namespace XiangqiGUI.ViewModels;

public partial class MarkViewModel : ViewModelBase, ICanvasItem
{
    public double CanvasLeft => Position.X * Constant.PieceSize;
    public double CanvasTop => Position.Y * Constant.PieceSize;
    public int ZIndex => 2;

    [ObservableProperty]
    private bool _show;

    [ObservableProperty]
    private Position _position = new();
}