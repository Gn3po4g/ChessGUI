namespace ChessGUI.Models;

internal partial class MovePoint : Piece
{
    public MovePoint()
    {
    }

    public MovePoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static bool IsValidMove(int x, int y) => x is >= 0 and <= 8 && y is >= 0 and <= 9;
}