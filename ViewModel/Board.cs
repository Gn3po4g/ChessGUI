using ChessGUI.Command;
using System.Windows.Input;

namespace ChessGUI.ViewModel;

internal class Board
{
    public static double ChessSize => 60;
    public static double BoardWidth => ChessSize * 9;
    public static double BoardHeight => ChessSize * 10;

    private readonly Chess[] _board =
    [
        new Advisor(0, 3, ChessColor.Black), new Advisor(0, 5, ChessColor.Black),
        new Advisor(9, 3, ChessColor.Red), new Advisor(9, 5, ChessColor.Red),
        new Bishop(0, 2, ChessColor.Black), new Bishop(0, 6, ChessColor.Black),
        new Bishop(9, 2, ChessColor.Red), new Bishop(9, 6, ChessColor.Red),
        new Cannon(2, 1, ChessColor.Black), new Cannon(2, 7, ChessColor.Black),
        new Cannon(7, 1, ChessColor.Red), new Cannon(7, 7, ChessColor.Red),
        new King(0, 4, ChessColor.Black), new King(9, 4, ChessColor.Red),
        new Knight(0, 1, ChessColor.Black), new Knight(0, 7, ChessColor.Black),
        new Knight(9, 1, ChessColor.Red), new Knight(9, 7, ChessColor.Red),
        new Pawn(3, 0, ChessColor.Black), new Pawn(3, 2, ChessColor.Black),
        new Pawn(3, 4, ChessColor.Black), new Pawn(3, 6, ChessColor.Black),
        new Pawn(3, 8, ChessColor.Black), new Pawn(6, 0, ChessColor.Red),
        new Pawn(6, 2, ChessColor.Red), new Pawn(6, 4, ChessColor.Red),
        new Pawn(6, 6, ChessColor.Red), new Pawn(6, 8, ChessColor.Red),
        new Rook(0, 0, ChessColor.Black), new Rook(0, 8, ChessColor.Black),
        new Rook(9, 0, ChessColor.Red), new Rook(9, 8, ChessColor.Red)
    ];

    public IEnumerable<Chess> ChessList => _board;

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


    public void ResetBoard()
    {
    }

    public ICommand ClickCommand => new ClickCommand(this);

    public void Click(Chess chess)
    {
    }
}