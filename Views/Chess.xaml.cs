using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace ChessGUI.Views;

/// <summary>
/// Chess.xaml 的交互逻辑
/// </summary>
public partial class Chess {
    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Models.Chess), typeof(Chess));
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(IRelayCommand), typeof(Chess));

    public Models.Chess Data {
        get => (Models.Chess)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public IRelayCommand Command {
        get => (IRelayCommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public Chess() {
        InitializeComponent();
    }
}