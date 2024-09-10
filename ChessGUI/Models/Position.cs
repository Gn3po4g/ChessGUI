namespace ChessGUI.Models;

public record Position(int X, int Y)
{
    public Position()
        : this(0, 0) { }

    public double CanvasLeft => X * Constant.PIECE_SIZE;

    public double CanvasTop => Y * Constant.PIECE_SIZE;

    public bool IsOnBoard() => X >= 0 && X < Constant.BOARD_COL && Y >= 0 && Y < Constant.BOARD_ROW;

    public bool IsOnOwnSide(bool isRed) =>
        IsOnBoard() && (isRed ? Y >= Constant.BOARD_ROW / 2 : Y < Constant.BOARD_ROW / 2);

    public bool IsOnOwnHome(bool isRed) =>
        X >= Constant.BOARD_COL / 2 - 1
        && X <= Constant.BOARD_COL / 2 + 1
        && (isRed ? Y >= Constant.BOARD_ROW - 3 && Y < Constant.BOARD_ROW : Y >= 0 && Y < 3);
}
