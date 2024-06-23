using System.Windows.Input;
using ChessGUI.Models;

namespace ChessGUI.Commands;

internal class MoveCommand(Action<MovePoint> execute) : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
    
    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        if (parameter is MovePoint movePoint)
        {
            execute.Invoke(movePoint);
        }
    }
}