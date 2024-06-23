using System.Windows.Input;
using ChessGUI.Models;

namespace ChessGUI.Commands;

internal class ClickCommand(Func<Chess, bool> canExecute, Action<Chess> execute) : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) => parameter is Chess chess && canExecute.Invoke(chess);

    public void Execute(object? parameter)
    {
        if (parameter is Chess chess)
        {
            execute.Invoke(chess);
        }
    }
}