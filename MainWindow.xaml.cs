using ChessGUI.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ChessGUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e) {
            //engine = new Engine("D:\\SharkChess\\engine\\pikafish\\pikafish-bmi2.exe", Receive);

        }

        private void Reset(object sender, RoutedEventArgs e) {
            Board.ResetBoard();
        }

        private void Clear(object sender, RoutedEventArgs e) {
            Board.ClearBoard();
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
}