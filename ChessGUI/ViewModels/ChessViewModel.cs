using System.Linq;
using ChessGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChessGUI.ViewModels;

public enum ChessType
{
    BlackAdvisor,
    BlackBishop,
    BlackCannon,
    BlackKing,
    BlackKnight,
    BlackPawn,
    BlackRook,
    RedAdvisor,
    RedBishop,
    RedCannon,
    RedKing,
    RedKnight,
    RedPawn,
    RedRook,
}

public partial class ChessViewModel : ViewModelBase, IPiece
{
    public ChessType Type { get; init; }

    private readonly Position initPos;

    [ObservableProperty]
    private Position position;

    [ObservableProperty]
    private bool isAlive = true;

    public ChessViewModel(int initX, int initY, ChessType type)
    {
        Type = type;
        initPos = new Position(initX, initY);
        Position = initPos;
    }

    public bool IsRed => Type >= ChessType.RedAdvisor;

    public void ResetStatus()
    {
        Position = initPos;
        IsAlive = true;
    }

    public bool CanMoveTo(Board board, Position position)
    {
        return !board.TryGetValue(position, out var chess) || chess.IsRed != IsRed;
    }

    private List<Position>[] GetLineMoves() =>
        [
            Enumerable
                .Range(Position.X + 1, Constant.BOARD_COL - Position.X - 1)
                .Select(x => new Position(x, Position.Y))
                .ToList(),
            Enumerable
                .Range(0, Position.X)
                .Reverse()
                .Select(x => new Position(x, Position.Y))
                .ToList(),
            Enumerable
                .Range(Position.Y + 1, Constant.BOARD_ROW - Position.Y - 1)
                .Select(y => new Position(Position.X, y))
                .ToList(),
            Enumerable
                .Range(0, Position.Y)
                .Reverse()
                .Select(y => new Position(Position.X, y))
                .ToList(),
        ];

    public List<Position> GetMovePoints(Board board)
    {
        return Type switch
        {
            ChessType.BlackAdvisor or ChessType.RedAdvisor => AdvisorMoves(board),
            ChessType.BlackBishop or ChessType.RedBishop => BishopMoves(board),
            ChessType.BlackCannon or ChessType.RedCannon => CannonMoves(board),
            ChessType.BlackKing or ChessType.RedKing => KingMoves(board),
            ChessType.BlackKnight or ChessType.RedKnight => KnightMoves(board),
            ChessType.BlackPawn or ChessType.RedPawn => PawnMoves(board),
            ChessType.BlackRook or ChessType.RedRook => RookMoves(board),
            _ => [],
        };
    }

    private List<Position> AdvisorMoves(Board board)
    {
        var positions = new Position[]
        {
            new(Position.X + 1, Position.Y + 1),
            new(Position.X + 1, Position.Y - 1),
            new(Position.X - 1, Position.Y + 1),
            new(Position.X - 1, Position.Y - 1),
        };
        return positions.Where(pos => pos.IsOnOwnHome(IsRed) && CanMoveTo(board, pos)).ToList();
    }

    private List<Position> BishopMoves(Board board)
    {
        var positions = new (Position, Position)[]
        {
            (new(Position.X + 1, Position.Y + 1), new(Position.X + 2, Position.Y + 2)),
            (new(Position.X + 1, Position.Y - 1), new(Position.X + 2, Position.Y - 2)),
            (new(Position.X - 1, Position.Y + 1), new(Position.X - 2, Position.Y + 2)),
            (new(Position.X - 1, Position.Y - 1), new(Position.X - 2, Position.Y - 2)),
        };
        return positions
            .Where(pos => !board.ContainsKey(pos.Item1))
            .Select(pos => pos.Item2)
            .Where(pos => pos.IsOnOwnSide(IsRed) && CanMoveTo(board, pos))
            .ToList();
    }

    private List<Position> CannonMoves(Board board)
    {
        var res = new List<Position>();
        foreach (var moves in GetLineMoves())
        {
            var emptyPositions = moves.TakeWhile(pos => !board.ContainsKey(pos)).ToList();
            res.AddRange(emptyPositions);
            var target = moves
                .Skip(emptyPositions.Count)
                .Where(board.ContainsKey)
                .Skip(1)
                .FirstOrDefault();
            if (target is not null && CanMoveTo(board, target))
            {
                res.Add(target);
            }
        }
        return res;
    }

    private List<Position> KingMoves(Board board)
    {
        var positions = new Position[]
        {
            new(Position.X, Position.Y - 1),
            new(Position.X, Position.Y + 1),
            new(Position.X - 1, Position.Y),
            new(Position.X + 1, Position.Y),
        };
        return positions.Where(pos => pos.IsOnOwnHome(IsRed) && CanMoveTo(board, pos)).ToList();
    }

    private List<Position> KnightMoves(Board board)
    {
        var positions = new (Position, Position[])[]
        {
            (
                new(Position.X, Position.Y - 1),
                [new(Position.X - 1, Position.Y - 2), new(Position.X + 1, Position.Y - 2)]
            ),
            (
                new(Position.X, Position.Y + 1),
                [new(Position.X - 1, Position.Y + 2), new(Position.X + 1, Position.Y + 2)]
            ),
            (
                new(Position.X - 1, Position.Y),
                [new(Position.X - 2, Position.Y - 1), new(Position.X - 2, Position.Y + 1)]
            ),
            (
                new(Position.X + 1, Position.Y),
                [new(Position.X + 2, Position.Y - 1), new(Position.X + 2, Position.Y + 1)]
            ),
        };
        return positions
            .Where(pos => !board.ContainsKey(pos.Item1))
            .SelectMany(pos => pos.Item2)
            .Where(pos => pos.IsOnBoard() && CanMoveTo(board, pos))
            .ToList();
    }

    private List<Position> PawnMoves(Board board)
    {
        var moves = new List<Position>() { new(Position.X, Position.Y - (IsRed ? 1 : -1)) };

        if (!Position.IsOnOwnSide(IsRed))
        {
            moves.Add(new(Position.X - 1, Position.Y));
            moves.Add(new(Position.X + 1, Position.Y));
        }
        return moves.Where(pos => pos.IsOnBoard() && CanMoveTo(board, pos)).ToList();
    }

    private List<Position> RookMoves(Board board)
    {
        return GetLineMoves()
            .SelectMany(moves =>
                moves.TakeWhile(pos => !board.ContainsKey(pos) || CanMoveTo(board, pos))
            )
            .ToList();
    }
}

public sealed class DesignChessViewModel : ChessViewModel
{
    public DesignChessViewModel()
        : base(0, 0, ChessType.BlackKing) { }
}
