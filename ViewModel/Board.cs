using ChessGUI.Command;
using System.Windows.Input;

namespace ChessGUI.Model;

internal class Board
{
    public static int Row => 10;
    public static int Column => 9;
    public static double ChessSize => 60;
    public static double BoardWidth => ChessSize * Column;
    public static double BoardHeight => ChessSize * Row;

    private readonly Chess[,] _board = new Chess[Row, Column];
    public IEnumerable<Chess> ChessList => _board.Cast<Chess>();

    private Chess? _focused, _start, _end;

    public bool RedTurn { get; private set; } = true;
    public Chess? Focused
    {
        get => _focused;
        set
        {
            if (_focused != null)
            {
                _focused.Focused = false;
            }

            _focused = value;
            if (_focused != null)
            {
                _focused.Focused = true;
            }
        }
    }

    public Chess? Start
    {
        get => _start;
        set
        {
            if (_start != null)
            {
                _start.Focused = false;
            }

            _start = value;
            if (_start != null)
            {
                _start.Focused = true;
            }
        }
    }

    public Chess? End
    {
        get => _end;
        set
        {
            if (_end != null)
            {
                _end.Focused = false;
            }

            _end = value;
            if (_end != null)
            {
                _end.Focused = true;
            }
        }
    }

    public Board()
    {
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Column; j++)
            {
                _board[i, j] = new Chess(i, j)
                {
                    Type = _default[i, j]
                };
            }
        }
    }


    public void ResetBoard()
    {
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Column; j++)
            {
                _board[i, j].Type = _default[i, j];
            }
        }
    }

    public ICommand ClickCommand => new ClickCommand(this);

    public void Click(Chess chess)
    {
        if (Focused != null) return;
        if (chess.Type != Type.None)
        {
            Focused = chess;
        }
        //} else {
        //    Move(Focused, clicked);
        //    Focused = null;
        //    Start = Focused;
        //    End = clicked;
        //TheBoard[row, col].Focused ^= true;
    }

    public void Move(Chess from, Chess to)
    {
        to.Type = from.Type;
        from.Type = Type.None;
    }

    private readonly Type[,] _default =
    {
        { Type.Br, Type.Bn, Type.Bb, Type.Ba, Type.Bk, Type.Ba, Type.Bb, Type.Bn, Type.Br },
        { Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None },
        { Type.None, Type.Bc, Type.None, Type.None, Type.None, Type.None, Type.None, Type.Bc, Type.None },
        { Type.Bp, Type.None, Type.Bp, Type.None, Type.Bp, Type.None, Type.Bp, Type.None, Type.Bp },
        { Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None },
        { Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None },
        { Type.Rp, Type.None, Type.Rp, Type.None, Type.Rp, Type.None, Type.Rp, Type.None, Type.Rp },
        { Type.None, Type.Rc, Type.None, Type.None, Type.None, Type.None, Type.None, Type.Rc, Type.None },
        { Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None, Type.None },
        { Type.Rr, Type.Rn, Type.Rb, Type.Ra, Type.Rk, Type.Ra, Type.Rb, Type.Rn, Type.Rr },
    };
}