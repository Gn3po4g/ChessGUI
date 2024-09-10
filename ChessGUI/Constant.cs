global using Board = System.Collections.Frozen.FrozenDictionary<
    ChessGUI.Models.Position,
    ChessGUI.ViewModels.ChessViewModel
>;

namespace ChessGUI
{
    public class Constant
    {
        public const double PIECE_SIZE = 60;
        public const int BOARD_ROW = 10;
        public const int BOARD_COL = 9;
        public const double BOARD_HEIGHT = BOARD_ROW * PIECE_SIZE;
        public const double BOARD_WIDTH = BOARD_COL * PIECE_SIZE;
    }
}
