using System.Windows.Input;

namespace ChessGUI.Command;

internal class ClickCommand(ViewModel.Board board) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        if (parameter is not ViewModel.Chess chess) return false;
        return false;
    }

    public void Execute(object? parameter)
    {
        if (parameter is ViewModel.Chess chess)
        {
            board.Click(chess);
        }
    }
}