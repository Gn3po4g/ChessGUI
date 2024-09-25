namespace XiangqiGUI.ViewModels;

public interface ICanvasItem
{
    double CanvasLeft { get; }
    double CanvasTop { get; }
    int ZIndex { get; }
}