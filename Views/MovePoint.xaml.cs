using System.Windows;
using System.Windows.Input;

namespace ChessGUI.Views;

public partial class MovePoint
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(MovePoint));
    
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    public MovePoint()
    {
        InitializeComponent();
    }
}