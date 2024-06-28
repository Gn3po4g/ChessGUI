using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace ChessGUI.Models;
public enum ChessType {
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

public partial class Chess : Piece {
    private readonly int initX, initY;

    [ObservableProperty] private ChessType chessType;

    [ObservableProperty] private bool isAlive;

    public bool IsRed => ChessType >= ChessType.RedAdvisor;

    public Chess(int initX, int initY, ChessType type) {
        this.initX = initX;
        this.initY = initY;
        ChessType = type;
        ResetPosition();
    }

    public void ResetPosition() {
        X = initX;
        Y = initY;
        IsAlive = true;
    }

    public IEnumerable<MovePoint> GetMovePoints(Dictionary<(int, int), Chess> chessPositions) => ChessType switch {
        ChessType.BlackAdvisor or ChessType.RedAdvisor => AdvisorMovePoints(chessPositions),
        ChessType.BlackBishop or ChessType.RedBishop => BishopMovePoints(chessPositions),
        ChessType.BlackCannon or ChessType.RedCannon => CannonMovePoints(chessPositions),
        ChessType.BlackKing or ChessType.RedKing => KingMovePoints(chessPositions),
        ChessType.BlackKnight or ChessType.RedKnight => KnightMovePoints(chessPositions),
        ChessType.BlackPawn or ChessType.RedPawn => PawnMovePoints(chessPositions),
        ChessType.BlackRook or ChessType.RedRook => RookMovePoints(chessPositions),
        _ => []
    };
    private IEnumerable<MovePoint> AdvisorMovePoints(Dictionary<(int, int), Chess> chessPositions) {
        return new[]
        {
            (X - 1, Y - 1), (X - 1, Y + 1), (X + 1, Y - 1), (X + 1, Y + 1)
        }.Where(pos =>
            (IsRed
                ? pos is { Item1: >= 3 and <= 5, Item2: >= 7 and <= 9 }
                : pos is { Item1: >= 3 and <= 5, Item2: >= 0 and <= 2 }
            ) && (
                !chessPositions.TryGetValue(pos, out var chess) ||
                chess.IsRed ^ IsRed
            )
        ).Select(pos => new MovePoint(pos));
    }

    private IEnumerable<MovePoint> BishopMovePoints(Dictionary<(int, int), Chess> chessPositions) {
        return new[]
        {
            (X - 2, Y - 2), (X - 2, Y + 2), (X + 2, Y - 2), (X + 2, Y + 2)
        }.Where(pos =>
            (IsRed
                ? pos is { Item1: >= 0 and <= 8, Item2: >= 5 and <= 9 }
                : pos is { Item1: >= 0 and <= 8, Item2: >= 0 and <= 4 }
            ) && !chessPositions.ContainsKey(new((pos.Item1 + X) / 2, (pos.Item2 + Y) / 2)) &&
            (
                !chessPositions.TryGetValue(pos, out var chess) ||
                chess.IsRed ^ IsRed
            )
        ).Select(pos => new MovePoint(pos));
    }

    private IEnumerable<MovePoint> CannonMovePoints(Dictionary<(int, int), Chess> chessPositions) {
        //up
        for (var i = Y - 1; i >= 0; i--) {
            if (!chessPositions.ContainsKey(new(X, i))) {
                yield return new MovePoint(X, i);
            } else {
                for (var j = i - 1; j >= 0; j--) {
                    if (chessPositions.TryGetValue(new(X, j), out var chess) &&
                        chess.IsRed ^ IsRed) {
                        yield return new MovePoint(X, j);
                        break;
                    }
                }
                break;
            }
        }
        //down
        for (var i = Y + 1; i <= 9; i++) {
            if (!chessPositions.ContainsKey(new(X, i))) {
                yield return new MovePoint(X, i);
            } else {
                for (var j = i + 1; j <= 9; j++) {
                    if (chessPositions.TryGetValue(new(X, j), out var chess) &&
                        chess.IsRed ^ IsRed) {
                        yield return new MovePoint(X, j);
                        break;
                    }
                }
                break;
            }
        }
        //left
        for (var i = X - 1; i >= 0; i--) {
            if (!chessPositions.ContainsKey(new(i, Y))) {
                yield return new MovePoint(i, Y);
            } else {
                for (var j = i - 1; j >= 0; j--) {
                    if (chessPositions.TryGetValue(new(j, Y), out var chess) &&
                        chess.IsRed ^ IsRed) {
                        yield return new MovePoint(j, Y);
                        break;
                    }
                }
                break;
            }
        }
        //right
        for (var i = X + 1; i <= 8; i++) {
            if (!chessPositions.ContainsKey(new(i, Y))) {
                yield return new MovePoint(i, Y);
            } else {
                for (var j = i + 1; j <= 8; j++) {
                    if (chessPositions.TryGetValue(new(j, Y), out var chess) &&
                        chess.IsRed ^ IsRed) {
                        yield return new MovePoint(j, Y);
                        break;
                    }
                }
                break;
            }
        }
    }

    private IEnumerable<MovePoint> KingMovePoints(Dictionary<(int, int), Chess> chessPositions) {
        return new[]
        {
            (X, Y - 1), (X, Y + 1), (X - 1, Y), (X + 1, Y)
        }.Where(pos =>
            (IsRed
                ? pos is { Item1: >= 3 and <= 5, Item2: >= 7 and <= 9 }
                : pos is { Item1: >= 3 and <= 5, Item2: >= 0 and <= 2 }
            ) && (
                !chessPositions.TryGetValue(pos, out var chess) ||
                chess.IsRed ^ IsRed
            )
        ).Select(pos => new MovePoint(pos));
    }

    private IEnumerable<MovePoint> KnightMovePoints(Dictionary<(int, int), Chess> chessPositions) {
        //up
        if (!chessPositions.ContainsKey(new(X, Y - 1))) {
            (int, int) p1 = new(X - 1, Y - 2), p2 = new(X + 1, Y - 2);
            if (p1 is { Item1: >= 0, Item2: >= 0 } && (
                !chessPositions.TryGetValue(p1, out var chess) ||
                chess.IsRed ^ IsRed
                )) yield return new MovePoint(p1);
            if (p1 is { Item1: <= 8, Item2: >= 0 } && (
                !chessPositions.TryGetValue(p2, out chess) ||
                chess.IsRed ^ IsRed
                )) yield return new MovePoint(p2);
        }
        //down
        if (!chessPositions.ContainsKey(new(X, Y + 1))) {
            (int, int) p1 = new(X - 1, Y + 2), p2 = new(X + 1, Y + 2);
            if (p1 is { Item1: >= 0, Item2: <= 9 } && (
                !chessPositions.TryGetValue(p1, out var chess) ||
                chess.IsRed ^ IsRed
                )) yield return new MovePoint(p1);
            if (p1 is { Item1: <= 8, Item2: <= 9 } && (
                !chessPositions.TryGetValue(p2, out chess) ||
                chess.IsRed ^ IsRed
                )) yield return new MovePoint(p2);
        }
        //left
        if (!chessPositions.ContainsKey(new(X - 1, Y))) {
            (int, int) p1 = new(X - 2, Y - 1), p2 = new(X - 2, Y + 1);
            if (p1 is { Item1: >= 0, Item2: >= 0 } && (
                !chessPositions.TryGetValue(p1, out var chess) ||
                chess.IsRed ^ IsRed
                )) yield return new MovePoint(p1);
            if (p1 is { Item1: >= 0, Item2: <= 9 } && (
                !chessPositions.TryGetValue(p2, out chess) ||
                chess.IsRed ^ IsRed
                )) yield return new MovePoint(p2);
        }
        //right
        if (!chessPositions.ContainsKey(new(X + 1, Y))) {
            (int, int) p1 = new(X + 2, Y - 1), p2 = new(X + 2, Y + 1);
            if (p1 is { Item1: <= 8, Item2: >= 0 } && (
                !chessPositions.TryGetValue(p1, out var chess) ||
                chess.IsRed ^ IsRed
                )) yield return new MovePoint(p1);
            if (p1 is { Item1: <= 8, Item2: <= 9 } && (
                !chessPositions.TryGetValue(p2, out chess) ||
                chess.IsRed ^ IsRed
                )) yield return new MovePoint(p2);
        }
    }

    private IEnumerable<MovePoint> PawnMovePoints(Dictionary<(int, int), Chess> chessPositions) {
        if (IsRed) {
            (int, int) forward = new(X, Y - 1);

            if (forward is { Item2: >= 0 } && (
                !chessPositions.TryGetValue(forward, out var chess) ||
                chess.IsRed ^ IsRed)) yield return new MovePoint(forward);

            if (Y > 4) yield break;

            (int, int) left = new(X - 1, Y), right = new(X + 1, Y);

            if (left is { Item1: >= 0 } && (
                !chessPositions.TryGetValue(left, out chess) ||
                chess.IsRed ^ IsRed)) yield return new MovePoint(left);

            if (right is { Item1: <= 8 } && (
                !chessPositions.TryGetValue(right, out chess) ||
                chess.IsRed ^ IsRed)) yield return new MovePoint(right);
        } else {
            (int, int) forward = new(X, Y + 1);

            if (forward is { Item2: <= 9 } && (
                !chessPositions.TryGetValue(forward, out var chess) ||
                chess.IsRed ^ IsRed)) yield return new MovePoint(X, Y + 1);

            if (Y < 5) yield break;

            (int, int) left = new(X - 1, Y), right = new(X + 1, Y);

            if (left is { Item1: >= 0 } && (
                !chessPositions.TryGetValue(left, out chess) ||
                chess.IsRed ^ IsRed)) yield return new MovePoint(left);

            if (right is { Item1: <= 8 } && (
                !chessPositions.TryGetValue(right, out chess) ||
                chess.IsRed ^ IsRed)) yield return new MovePoint(right);
        }
    }

    private IEnumerable<MovePoint> RookMovePoints(Dictionary<(int, int), Chess> chessPositions) {
        //up
        for (var i = Y - 1; i >= 0; i--) {
            if (!chessPositions.ContainsKey(new(X, i))) {
                yield return new MovePoint(X, i);
            } else {
                if (chessPositions[new(X, i)].IsRed ^ IsRed) {
                    yield return new MovePoint(X, i);
                }
                break;
            }
        }
        //down
        for (var i = Y + 1; i <= 9; i++) {
            if (!chessPositions.ContainsKey(new(X, i))) {
                yield return new MovePoint(X, i);
            } else {
                if (chessPositions[new(X, i)].IsRed ^ IsRed) {
                    yield return new MovePoint(X, i);
                }
                break;
            }
        }
        //left
        for (var i = X - 1; i >= 0; i--) {
            if (!chessPositions.ContainsKey(new(i, Y))) {
                yield return new MovePoint(i, Y);
            } else {
                if (chessPositions[new(i, Y)].IsRed ^ IsRed) {
                    yield return new MovePoint(i, Y);
                }
                break;
            }
        }
        //right
        for (var i = X + 1; i <= 9; i++) {
            if (!chessPositions.ContainsKey(new(i, Y))) {
                yield return new MovePoint(i, Y);
            } else {
                if (chessPositions[new(i, Y)].IsRed ^ IsRed) {
                    yield return new MovePoint(i, Y);
                }
                break;
            }
        }
    }
}