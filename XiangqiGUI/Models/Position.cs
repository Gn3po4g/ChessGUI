namespace XiangqiGUI.Models;

public record Position(int Row, int Col)
{
    public Position() : this(0, 0)
    {
    }

    public static Position operator +(Position position, (int dx, int dy) d)
    {
        return new Position(position.Row + d.dx, position.Col + d.dy);
    }

    public Position Half((int dx, int dy) d)
    {
        return new Position(Row + d.dx / 2, Col + d.dy / 2);
    }

    public bool IsOnBoard() => Row is >= 0 and < 10 && Col is >= 0 and < 9;

    public bool IsOnOwnSide(PieceColor color) =>
        (color == PieceColor.Red ? Row is >= 5 and < 10 : Row is >= 0 and < 5)
        && Col is >= 0 and < 9;

    public bool IsOnOwnHome(PieceColor color) =>
        (color == PieceColor.Red ? Row is >= 7 and < 10 : Row is >= 0 and < 3)
        && Col is >= 3 and < 6;

    // public IEnumerable<Position> Dir(int dx, int dy)
    // {
    //     for (int i = Row + dx, j = Col + dy;
    //          i is >= 0 and < Constant.BoardRow && j is >= 0 and < Constant.BoardCol;
    //          i += dx, j += dy)
    //     {
    //         yield return new Position(i, j);
    //     }
    // }
}

public record MoveAction(Position From, Position To);