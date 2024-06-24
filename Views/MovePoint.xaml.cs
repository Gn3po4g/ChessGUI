using System.Windows;
using CommunityToolkit.Mvvm.Input;

namespace ChessGUI.Views;

public partial class MovePoint
{
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(IRelayCommand), typeof(MovePoint));

    public IRelayCommand Command
    {
        get => (IRelayCommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    public MovePoint()
    {
        InitializeComponent();
    }
}