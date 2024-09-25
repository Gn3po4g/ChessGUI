using XiangqiGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace XiangqiGUI.ViewModels;

public partial class PieceViewModel(Piece pieceData) : ViewModelBase, ICanvasItem
{
    [ObservableProperty] private Position _position = pieceData.Position;
    public PieceType Type => pieceData.Type;

    [ObservableProperty] private bool _isAlive = true;

    public bool IsRed => Type > PieceType.None;

    public double CanvasLeft => Position.X * Constant.PieceSize;
    public double CanvasTop => Position.Y * Constant.PieceSize;
    public int ZIndex => 1;

    public void ResetStatus()
    {
        Position = pieceData.Position;
        IsAlive = true;
    }

    public void Click() => WeakReferenceMessenger.Default.Send(new ClickMessage(this));

    private bool CanMoveTo(Board board, Position position)
    {
        return !board.TryGetValue(position, out var chess) || chess.IsRed != IsRed;
    }

    private Position[][] GetLineMoves() =>
    [
        Enumerable
            .Range(Position.X + 1, Constant.BoardCol - Position.X - 1)
            .Select(x => Position with { X = x })
            .ToArray(),
        Enumerable
            .Range(0, Position.X)
            .Reverse()
            .Select(x => Position with { X = x })
            .ToArray(),
        Enumerable
            .Range(Position.Y + 1, Constant.BoardRow - Position.Y - 1)
            .Select(y => Position with { Y = y })
            .ToArray(),
        Enumerable
            .Range(0, Position.Y)
            .Reverse()
            .Select(y => Position with { Y = y })
            .ToArray(),
    ];

    public Position[] GetMovePoints(Board board)
    {
        return Type switch
        {
            PieceType.BlackChariot or PieceType.RedChariot => ChariotMoves(board),
            PieceType.BlackHorse or PieceType.RedHorse => HorseMoves(board),
            PieceType.BlackElephant or PieceType.RedElephant => ElephantMoves(board),
            PieceType.BlackAdvisor or PieceType.RedAdvisor => AdvisorMoves(board),
            PieceType.BlackKing or PieceType.RedKing => KingMoves(board),
            PieceType.BlackCannon or PieceType.RedCannon => CannonMoves(board),
            PieceType.BlackSoldier or PieceType.RedSoldier => SoldierMoves(board),
            _ => []
        };
    }

    private Position[] AdvisorMoves(Board board)
    {
        var positions = new Position[]
        {
            new(Position.X + 1, Position.Y + 1),
            new(Position.X + 1, Position.Y - 1),
            new(Position.X - 1, Position.Y + 1),
            new(Position.X - 1, Position.Y - 1),
        };
        return positions.Where(pos => pos.IsOnOwnHome(IsRed) && CanMoveTo(board, pos)).ToArray();
    }

    private Position[] ElephantMoves(Board board)
    {
        var positions = new[]
        {
            (new Position(Position.X + 1, Position.Y + 1), new Position(Position.X + 2, Position.Y + 2)),
            (new Position(Position.X + 1, Position.Y - 1), new Position(Position.X + 2, Position.Y - 2)),
            (new Position(Position.X - 1, Position.Y + 1), new Position(Position.X - 2, Position.Y + 2)),
            (new Position(Position.X - 1, Position.Y - 1), new Position(Position.X - 2, Position.Y - 2)),
        };
        return positions
            .Where(pos => !board.ContainsKey(pos.Item1))
            .Select(pos => pos.Item2)
            .Where(pos => pos.IsOnOwnSide(IsRed) && CanMoveTo(board, pos))
            .ToArray();
    }

    private Position[] CannonMoves(Board board)
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

        return [.. res];
    }

    private Position[] KingMoves(Board board)
    {
        var positions = new Position[]
        {
            new(Position.X, Position.Y - 1),
            new(Position.X, Position.Y + 1),
            new(Position.X - 1, Position.Y),
            new(Position.X + 1, Position.Y),
        };
        return positions.Where(pos => pos.IsOnOwnHome(IsRed) && CanMoveTo(board, pos)).ToArray();
    }

    private Position[] HorseMoves(Board board)
    {
        var positions = new (Position, Position[])[]
        {
            (
                new Position(Position.X, Position.Y - 1),
                [new Position(Position.X - 1, Position.Y - 2), new Position(Position.X + 1, Position.Y - 2)]
            ),
            (
                new Position(Position.X, Position.Y + 1),
                [new Position(Position.X - 1, Position.Y + 2), new Position(Position.X + 1, Position.Y + 2)]
            ),
            (
                new Position(Position.X - 1, Position.Y),
                [new Position(Position.X - 2, Position.Y - 1), new Position(Position.X - 2, Position.Y + 1)]
            ),
            (
                new Position(Position.X + 1, Position.Y),
                [new Position(Position.X + 2, Position.Y - 1), new Position(Position.X + 2, Position.Y + 1)]
            ),
        };
        return positions
            .Where(pos => !board.ContainsKey(pos.Item1))
            .SelectMany(pos => pos.Item2)
            .Where(pos => pos.IsOnBoard() && CanMoveTo(board, pos))
            .ToArray();
    }

    private Position[] SoldierMoves(Board board)
    {
        var moves = new List<Position> { new(Position.X, Position.Y - (IsRed ? 1 : -1)) };

        if (Position.IsOnOwnSide(IsRed)) return moves.Where(pos => pos.IsOnBoard() && CanMoveTo(board, pos)).ToArray();

        moves.Add(new Position(Position.X - 1, Position.Y));
        moves.Add(new Position(Position.X + 1, Position.Y));

        return moves.Where(pos => pos.IsOnBoard() && CanMoveTo(board, pos)).ToArray();
    }

    private Position[] ChariotMoves(Board board)
    {
        var res = new List<Position>();
        foreach (var moves in GetLineMoves())
        {
            var emptyPositions = moves.TakeWhile(pos => !board.ContainsKey(pos)).ToList();
            res.AddRange(emptyPositions);
            var target = moves
                .Skip(emptyPositions.Count)
                .FirstOrDefault();
            if (target is not null && CanMoveTo(board, target))
            {
                res.Add(target);
            }
        }

        return [.. res];
    }
}

public sealed class DesignPieceViewModel() : PieceViewModel(Piece.ChessBoard[0]);