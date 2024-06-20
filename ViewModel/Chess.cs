using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ChessGUI.Command;

namespace ChessGUI.Model;

public enum Type
{
    Ba,
    Bb,
    Bc,
    Bk,
    Bn,
    Bp,
    Br,
    None,
    Ra,
    Rb,
    Rc,
    Rk,
    Rn,
    Rp,
    Rr
}

internal class Chess(int row, int col) : INotifyPropertyChanged
{
    private bool _focused;
    private Type _type = Type.None;

    private int Row => row;
    private int Col => col;

    public Type Type
    {
        get => _type;
        set
        {
            _type = value;
            OnTypeChanged(nameof(Type));
        }
    }

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

    public bool IsRed => Type > Type.None;
    
    public bool CanEat(Chess other)
    {
        return Row == other.Row || Col == other.Col;
    }
}