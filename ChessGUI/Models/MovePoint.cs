namespace ChessGUI.Models;

public class MovePoint : Piece {
    public MovePoint(int x, int y) {
        X = x;
        Y = y;
    }

    public MovePoint((int, int) pos) {
        X = pos.Item1;
        Y = pos.Item2;
    }
}