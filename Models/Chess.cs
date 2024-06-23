using CommunityToolkit.Mvvm.ComponentModel;

namespace ChessGUI.Models;

internal enum ChessType
{
    Null,
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

internal partial class Chess : Piece
{
    private readonly int _initX, _initY;
    private readonly ChessType _initType;

    [ObservableProperty] private ChessType _chessType = ChessType.Null;

    public Chess()
    {
    }

    public Chess(int initX, int initY, ChessType type)
    {
        _initX = initX;
        _initY = initY;
        _initType = type;
        ResetPosition();
    }

    public bool IsRed => ChessType switch
    {
        ChessType.RedAdvisor or ChessType.RedBishop or ChessType.RedCannon or ChessType.RedKing
            or ChessType.RedKnight or ChessType.RedPawn or ChessType.RedRook => true,
        _ => false
    };

    public bool IsAlive => ChessType != ChessType.Null;

    public void ResetPosition()
    {
        X = _initX;
        Y = _initY;
        ChessType = _initType;
    }

    public IEnumerable<MovePoint> GetMovePoints(IList<Chess> chesses) => ChessType switch
    {
        ChessType.BlackAdvisor or ChessType.RedAdvisor => AdvisorMovePoints(chesses),
        ChessType.BlackBishop or ChessType.RedBishop => BishopMovePoints(chesses),
        ChessType.BlackCannon or ChessType.RedCannon => CannonMovePoints(chesses),
        ChessType.BlackKing or ChessType.RedKing => KingMovePoints(chesses),
        ChessType.BlackKnight or ChessType.RedKnight => KnightMovePoints(chesses),
        ChessType.BlackPawn or ChessType.RedPawn => PawnMovePoints(chesses),
        ChessType.BlackRook or ChessType.RedRook => RookMovePoints(chesses),
        _ => []
    };

    private IEnumerable<MovePoint> AdvisorMovePoints(IList<Chess> chesses)
    {
        return new[]
        {
            (X - 1, Y - 1), (X - 1, Y + 1), (X + 1, Y - 1), (X + 1, Y + 1)
        }.Where(pos =>
            (IsRed
                ? pos is { Item1: >= 3 and <= 5, Item2: >= 7 and <= 9 }
                : pos is { Item1: >= 3 and <= 5, Item2: >= 0 and <= 2 }
            ) &&
            chesses.SingleOrDefault(chess =>
                chess is { IsAlive: true } &&
                chess.IsRed == IsRed &&
                chess.X == pos.Item1 &&
                chess.Y == pos.Item2) == null
        ).Select(pos => new MovePoint(pos.Item1, pos.Item2));
    }

    private IEnumerable<MovePoint> BishopMovePoints(IList<Chess> chesses)
    {
        return new[]
        {
            (X - 2, Y - 2), (X - 2, Y + 2), (X + 2, Y - 2), (X + 2, Y + 2)
        }.Where(pos =>
            (IsRed
                ? pos is { Item1: >= 0 and <= 8, Item2: >= 5 and <= 9 }
                : pos is { Item1: >= 0 and <= 8, Item2: >= 0 and <= 4 }
            ) &&
            chesses.SingleOrDefault(chess =>
                chess is { IsAlive: true } &&
                chess.IsRed == IsRed &&
                chess.X == pos.Item1 &&
                chess.Y == pos.Item2) == null &&
            chesses.SingleOrDefault(chess =>
                chess is { IsAlive: true } &&
                chess.X == (pos.Item1 + X) / 2 &&
                chess.Y == (pos.Item2 + Y) / 2) == null
        ).Select(pos => new MovePoint(pos.Item1, pos.Item2));
    }

    private IEnumerable<MovePoint> CannonMovePoints(IList<Chess> chesses)
    {
        int limit;
        //up
        var up = chesses.Where(chess => chess.IsAlive && chess.X == X && chess.Y < Y)
            .OrderByDescending(chess => chess.Y)
            .Take(2).ToArray();
        limit = up.Length > 0 ? up[0].Y : -1;
        for (var i = Y - 1; i > limit; i--) yield return new MovePoint(X, i);
        if (up.Length > 1 && up[1].IsRed ^ IsRed) yield return new MovePoint(X, up[1].Y);
        //down
        var down = chesses.Where(chess => chess.IsAlive && chess.X == X && chess.Y > Y)
            .OrderBy(chess => chess.Y)
            .Take(2).ToArray();
        limit = down.Length > 0 ? down[0].Y : 10;
        for (var i = Y + 1; i < limit; i++) yield return new MovePoint(X, i);
        if (down.Length > 1 && down[1].IsRed ^ IsRed) yield return new MovePoint(X, down[1].Y);
        //left
        var left = chesses.Where(chess => chess.IsAlive && chess.Y == Y && chess.X < X)
            .OrderByDescending(chess => chess.X)
            .Take(2).ToArray();
        limit = left.Length > 0 ? left[0].X : -1;
        for (var i = X - 1; i > limit; i--) yield return new MovePoint(i, Y);
        if (left.Length > 1 && left[1].IsRed ^ IsRed) yield return new MovePoint(left[1].X, Y);
        //right
        var right = chesses.Where(chess => chess.IsAlive && chess.Y == Y && chess.X > X)
            .OrderBy(chess => chess.X)
            .Take(2).ToArray();
        limit = right.Length > 0 ? right[0].X : 9;
        for (var i = X + 1; i < limit; i++) yield return new MovePoint(i, Y);
        if (right.Length > 1 && right[1].IsRed ^ IsRed) yield return new MovePoint(right[1].X, Y);
    }

    private IEnumerable<MovePoint> KingMovePoints(IList<Chess> chesses)
    {
        return new[]
        {
            (X, Y - 1), (X, Y + 1), (X - 1, Y), (X + 1, Y)
        }.Where(pos =>
            (IsRed
                ? pos is { Item1: >= 3 and <= 5, Item2: >= 7 and <= 9 }
                : pos is { Item1: >= 3 and <= 5, Item2: >= 0 and <= 2 }
            ) &&
            chesses.SingleOrDefault(chess =>
                chess is { IsAlive: true } &&
                chess.IsRed == IsRed &&
                chess.X == pos.Item1 &&
                chess.Y == pos.Item2) == null
        ).Select(pos => new MovePoint(pos.Item1, pos.Item2));
    }

    private IEnumerable<MovePoint> KnightMovePoints(IList<Chess> chesses)
    {
        if (chesses.SingleOrDefault(chess => chess.IsAlive && chess.X == X && chess.Y == Y - 1) == null)
        {
            if (X - 1 >= 0 && Y - 2 >= 0 &&
                chesses.SingleOrDefault(chess =>
                    chess.IsAlive &&
                    chess.X == X - 1 &&
                    chess.Y == Y - 2
                ) == null)
                yield return new MovePoint(X - 1, Y - 2);
            if (X + 1 <= 8 && Y - 2 >= 0 &&
                chesses.SingleOrDefault(chess =>
                    chess.IsAlive &&
                    chess.X == X + 1 &&
                    chess.Y == Y - 2
                ) == null)
                yield return new MovePoint(X + 1, Y - 2);
        }

        if (chesses.SingleOrDefault(chess => chess.IsAlive && chess.X == X && chess.Y == Y + 1) == null)
        {
            if (X - 1 >= 0 && Y + 2 <= 9 &&
                chesses.SingleOrDefault(chess =>
                    chess.IsAlive &&
                    chess.X == X - 1 &&
                    chess.Y == Y + 2
                ) == null)
                yield return new MovePoint(X - 1, Y + 2);
            if (X + 1 <= 8 && Y + 2 <= 9 &&
                chesses.SingleOrDefault(chess =>
                    chess.IsAlive &&
                    chess.X == X + 1 &&
                    chess.Y == Y + 2
                ) == null)
                yield return new MovePoint(X + 1, Y + 2);
        }

        if (chesses.SingleOrDefault(chess => chess.IsAlive && chess.X == X - 1 && chess.Y == Y) == null)
        {
            if (X - 2 >= 0 && Y - 1 >= 0 &&
                chesses.SingleOrDefault(chess =>
                    chess.IsAlive &&
                    chess.X == X - 2 &&
                    chess.Y == Y - 1
                ) == null)
                yield return new MovePoint(X - 2, Y - 1);
            if (X - 2 >= 0 && Y + 1 <= 9 &&
                chesses.SingleOrDefault(chess =>
                    chess.IsAlive &&
                    chess.X == X - 2 &&
                    chess.Y == Y + 1
                ) == null)
                yield return new MovePoint(X - 2, Y + 1);
        }

        if (chesses.SingleOrDefault(chess => chess.IsAlive && chess.X == X + 1 && chess.Y == Y) == null)
        {
            if (X + 2 <= 8 && Y - 1 >= 0 &&
                chesses.SingleOrDefault(chess =>
                    chess.IsAlive &&
                    chess.X == X + 2 &&
                    chess.Y == Y - 1
                ) == null)
                yield return new MovePoint(X + 2, Y - 1);
            if (X + 2 <= 8 && Y + 1 <= 9 &&
                chesses.SingleOrDefault(chess =>
                    chess.IsAlive &&
                    chess.X == X + 2 &&
                    chess.Y == Y + 1
                ) == null)
                yield return new MovePoint(X + 2, Y + 1);
        }
    }

    private IEnumerable<MovePoint> PawnMovePoints(IList<Chess> chesses)
    {
        var chessPositions = chesses
            .Where(chess => chess.IsAlive)
            .ToDictionary(chess => (chess.X, chess.Y));
        if (IsRed)
        {
            if (!chessPositions.TryGetValue(new ValueTuple<int, int>(X, Y - 1), out var forward) ||
                forward.IsRed ^ IsRed)
            {
                if (MovePoint.IsValidMove(X, Y - 1)) yield return new MovePoint(X, Y - 1);
            }

            if (Y > 4) yield break;
            if (!chessPositions.TryGetValue(new ValueTuple<int, int>(X - 1, Y), out var left) ||
                left.IsRed ^ IsRed)
            {
                if (MovePoint.IsValidMove(X - 1, Y)) yield return new MovePoint(X - 1, Y);
            }

            if (!chessPositions.TryGetValue(new ValueTuple<int, int>(X + 1, Y), out var right) ||
                right.IsRed ^ IsRed)
            {
                if (MovePoint.IsValidMove(X + 1, Y)) yield return new MovePoint(X + 1, Y);
            }
        }
        else
        {
            if (!chessPositions.TryGetValue(new ValueTuple<int, int>(X, Y + 1), out var forward) ||
                forward.IsRed ^ IsRed)
            {
                if (MovePoint.IsValidMove(X, Y + 1)) yield return new MovePoint(X, Y + 1);
            }

            if (Y < 5) yield break;
            if (!chessPositions.TryGetValue(new ValueTuple<int, int>(X - 1, Y), out var left) ||
                left.IsRed ^ IsRed)
            {
                if (MovePoint.IsValidMove(X - 1, Y)) yield return new MovePoint(X - 1, Y);
            }

            if (!chessPositions.TryGetValue(new ValueTuple<int, int>(X + 1, Y), out var right) ||
                right.IsRed ^ IsRed)
            {
                if (MovePoint.IsValidMove(X + 1, Y)) yield return new MovePoint(X + 1, Y);
            }
        }
    }

    private IEnumerable<MovePoint> RookMovePoints(IList<Chess> chesses)
    {
        var chessPositions = chesses
            .Where(chess => chess.IsAlive)
            .ToDictionary(chess => (chess.X, chess.Y));
        for (var i = Y - 1; i >= 0; i--)
        {
            if (!chessPositions.ContainsKey(new ValueTuple<int, int>(X, i)))
            {
                yield return new MovePoint(X, i);
            }
            else
            {
                if (chessPositions[new ValueTuple<int, int>(X, i)].IsRed ^ IsRed)
                {
                    yield return new MovePoint(X, i);
                }

                break;
            }
        }

        for (var i = Y + 1; i <= 9; i++)
        {
            if (!chessPositions.ContainsKey(new ValueTuple<int, int>(X, i)))
            {
                yield return new MovePoint(X, i);
            }
            else
            {
                if (chessPositions[new ValueTuple<int, int>(X, i)].IsRed ^ IsRed)
                {
                    yield return new MovePoint(X, i);
                }

                break;
            }
        }

        for (var i = X - 1; i >= 0; i--)
        {
            if (!chessPositions.ContainsKey(new ValueTuple<int, int>(i, Y)))
            {
                yield return new MovePoint(i, Y);
            }
            else
            {
                if (chessPositions[new ValueTuple<int, int>(i, Y)].IsRed ^ IsRed)
                {
                    yield return new MovePoint(i, Y);
                }

                break;
            }
        }

        for (var i = X + 1; i <= 9; i++)
        {
            if (!chessPositions.ContainsKey(new ValueTuple<int, int>(i, Y)))
            {
                yield return new MovePoint(i, Y);
            }
            else
            {
                if (chessPositions[new ValueTuple<int, int>(i, Y)].IsRed ^ IsRed)
                {
                    yield return new MovePoint(i, Y);
                }

                break;
            }
        }
    }
}