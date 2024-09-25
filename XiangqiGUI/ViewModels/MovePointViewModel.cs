using CommunityToolkit.Mvvm.Messaging;
using XiangqiGUI.Models;

namespace XiangqiGUI.ViewModels;

public class MovePointViewModel(Position position) : ViewModelBase, ICanvasItem
{
    public double CanvasLeft => position.X * Constant.PieceSize;
    public double CanvasTop => position.Y * Constant.PieceSize;
    public int ZIndex => 3;
    
    public Position Position => position;

    public void MoveTo() => WeakReferenceMessenger.Default.Send(new ClickMessage(this));
}