namespace XiangqiGUI.Models;

public record Position(int X, int Y)
{
    public Position() : this(0, 0)
    {
    }

    public bool IsOnBoard() => X is >= 0 and < Constant.BoardCol && Y is >= 0 and < Constant.BoardRow;

    public bool IsOnOwnSide(bool isRed) =>
        IsOnBoard() && (isRed ? Y >= Constant.BoardRow / 2 : Y < Constant.BoardRow / 2);

    public bool IsOnOwnHome(bool isRed) =>
        X is >= Constant.BoardCol / 2 - 1 and <= Constant.BoardCol / 2 + 1
        && (isRed ? Y is >= Constant.BoardRow - 3 and < Constant.BoardRow : Y is >= 0 and < 3);
}