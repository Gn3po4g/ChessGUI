using ChessGUI.Command;
using System.Windows;

namespace ChessGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    // private readonly Model.Board _board = new();

    public MainWindow()
    {
        InitializeComponent();
        // ChessBoard.DataContext = _board;
        ResetButton.Command = new ResetCommand((ViewModel.Board)Resources["BoardModel"]);
    }

    private void OnLoad(object sender, RoutedEventArgs e)
    {
        //engine = new Engine("D:\\SharkChess\\engine\\pikafish\\pikafish-bmi2.exe", Receive);
    }

    //private void Send(object sender, RoutedEventArgs e) {
    //    engine.Send(message.Text);
    //    //message_received.Text +="a";
    //}
    //private void Receive(string message) {
    //    if (message.StartsWith("bestmove")) {
    //        Dispatcher.Invoke(()=> message_received.Text += message+'\n');
    //    }
    //}
}