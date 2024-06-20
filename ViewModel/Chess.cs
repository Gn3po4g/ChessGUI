using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ChessGUI.Command;

namespace ChessGUI.ViewModel;

internal enum ChessColor
{
    Red,Black
}

internal abstract class Chess(int initRow, int initCow, ChessColor color) : INotifyPropertyChanged
{
    private bool _focused;

    private int _row = initRow;
    private int _col = initCow;

    public double Left => Board.ChessSize * _col;
    public double Top => Board.ChessSize * _row;

    public abstract ImageSource ImageSource { get; set; }

    public bool Focused
    {
        get => _focused;
        set
        {
            _focused = value;
            OnTypeChanged(nameof(Focused));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnTypeChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public abstract bool CanEat(Chess other);
}