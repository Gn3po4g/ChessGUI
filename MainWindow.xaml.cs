using System.Windows;

namespace ChessGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        // ResetButton.Commands = new ResetCommand((ViewModels.Board)Resources["BoardModel"]);
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