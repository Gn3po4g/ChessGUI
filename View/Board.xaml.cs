using ChessGUI.Common;
using System.Windows.Controls;

namespace ChessGUI.View {
    /// <summary>
    /// Board.xaml 的交互逻辑
    /// </summary>
    public partial class Board : UserControl {
        private readonly Model.Board board = new();
        public Board() {
            InitializeComponent();
            DataContext = board;
        }

        public void ResetBoard() { board.ResetBoard(); }

        public void ClearBoard() { board.ClearBoard(); }

        public void ButtonClicked(object sender, ClickEventArgs e) {
            board.Click(e.Row, e.Column);
        }
    }
}
