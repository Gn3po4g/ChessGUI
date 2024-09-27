namespace XiangqiGUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public GameViewModel GameViewModel { get; } = new();

    public void ResetBoard()
    {
        GameViewModel.Reset();
    }
}