using System.Windows;
using System.Windows.Input;

namespace ChessGUI.Views;

/// <summary>
/// Chess.xaml 的交互逻辑
/// </summary>
public partial class Chess
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(Chess));
    
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    public Chess()
    {
        InitializeComponent();
    }
}