using CommunityToolkit.Mvvm.Messaging;
using XiangqiGUI.Models;

namespace XiangqiGUI.ViewModels;

public class MovePointViewModel(Position from, Position to) : ViewModelBase
{
    public int Row => to.Row;
    public int Col => to.Col;

    public void Move()
    {
        WeakReferenceMessenger.Default.Send(new MoveAction(from, to));
    }
}