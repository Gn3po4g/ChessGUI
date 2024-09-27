using System.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using XiangqiGUI.Models;

namespace XiangqiGUI.ViewModels;

public class GameBoard : IEnumerable<BlockViewModel>
{
    private readonly BlockViewModel[,] _blocks = new BlockViewModel[10, 9];

    public GameBoard(Action<Position, Piece> onClick)
    {
        for (var i = 0; i < _blocks.GetLength(0); i++)
        {
            for (var j = 0; j < _blocks.GetLength(1); j++)
            {
                _blocks[i, j] = new BlockViewModel(new Position(i, j), onClick);
            }
        }
    }

    public BlockViewModel this[Position position] => _blocks[position.Row, position.Col];

    public IEnumerator<BlockViewModel> GetEnumerator()
    {
        return _blocks.OfType<BlockViewModel>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public partial class GameViewModel : ViewModelBase, IRecipient<MoveAction>
{
    public GameBoard Board { get; }

    [ObservableProperty] private IEnumerable<MovePointViewModel> _movePoints = [];

    private PieceColor _whichTurn = PieceColor.Red;

    private Position _lastPosition = new();

    private (Position, Position) _lastMove;

    public GameViewModel()
    {
        Board = new GameBoard(ClickPiece);

        IsActive = true;
    }

    public void Reset()
    {
        foreach (var blockViewModel in Board)
        {
            blockViewModel.Reset();
        }
    }

    private void ClickPiece(Position position, Piece piece)
    {
        if (piece.Color != _whichTurn) return;

        Board[_lastPosition].IsMarked = false;
        Board[position].IsMarked = true;
        _lastPosition = position;

        MovePoints = ValidMoves(position, piece)
            .Select(pos => new MovePointViewModel(position, pos));
    }

    private void Move(Position from, Position to)
    {
        Board[_lastPosition].IsMarked = false;
        _lastMove = (from, to);

        Board[to].PieceInBlock = Board[from].PieceInBlock;
        Board[from].PieceInBlock = null;

        MovePoints = [];
        _whichTurn = _whichTurn == PieceColor.Red ? PieceColor.Black : PieceColor.Red;
    }

    public void Receive(MoveAction message)
    {
        Move(message.From, message.To);
    }


    public IEnumerable<Position> ValidMoves(Position position, Piece piece)
    {
        switch (piece.Type)
        {
            case PieceType.Chariot:
            {
                foreach (var dir in new[] { (-1, 0), (0, 1), (1, 0), (0, -1) })
                {
                    for (var curPos = position + dir; curPos.IsOnBoard(); curPos += dir)
                    {
                        var target = Board[curPos].PieceInBlock;
                        if (target == null)
                        {
                            yield return curPos;
                        }
                        else
                        {
                            if (target.Color != piece.Color)
                            {
                                yield return curPos;
                            }

                            break;
                        }
                    }
                }

                break;
            }
            case PieceType.Horse:
            {
                foreach (var dir in new[] { (-2, -1), (-2, 1), (-1, 2), (1, 2), (2, 1), (2, -1), (1, -2), (-1, -2) })
                {
                    if (!(position + dir).IsOnBoard()) continue;
                    if (Board[position.Half(dir)].PieceInBlock != null) continue;
                    var target = Board[position + dir].PieceInBlock;
                    if (target == null || target.Color != piece.Color)
                    {
                        yield return position + dir;
                    }
                }

                break;
            }
            case PieceType.Elephant:
            {
                foreach (var dir in new[] { (-2, -2), (-2, 2), (-2, 2), (2, 2), (2, 2), (2, -2), (2, -2), (-2, -2) })
                {
                    if (!(position + dir).IsOnOwnSide(piece.Color)) continue;
                    if (Board[position.Half(dir)].PieceInBlock != null) continue;
                    var target = Board[position + dir].PieceInBlock;
                    if (target == null || target.Color != piece.Color)
                    {
                        yield return position + dir;
                    }
                }

                break;
            }
            case PieceType.Advisor:
            {
                foreach (var dir in new[] { (-1, -1), (-1, 1), (1, 1), (1, -1) })
                {
                    if (!(position + dir).IsOnOwnHome(piece.Color)) continue;
                    var target = Board[position + dir].PieceInBlock;
                    if (target == null || target.Color != piece.Color)
                    {
                        yield return position + dir;
                    }
                }

                break;
            }
            case PieceType.King:
            {
                foreach (var dir in new[] { (-1, 0), (0, 1), (1, 0), (0, -1) })
                {
                    if (!(position + dir).IsOnOwnHome(piece.Color)) continue;
                    var target = Board[position + dir].PieceInBlock;
                    if (target == null || target.Color != piece.Color)
                    {
                        yield return position + dir;
                    }
                }

                break;
            }
            case PieceType.Cannon:
            {
                foreach (var dir in new[] { (-1, 0), (0, 1), (1, 0), (0, -1) })
                {
                    for (var curPos = position + dir; curPos.IsOnBoard(); curPos += dir)
                    {
                        if (Board[curPos].PieceInBlock == null)
                        {
                            yield return curPos;
                        }
                        else
                        {
                            for (var targetPos = curPos + dir; targetPos.IsOnBoard(); targetPos += dir)
                            {
                                var target = Board[targetPos].PieceInBlock;
                                if (target == null) continue;
                                if (target.Color != piece.Color)
                                {
                                    yield return targetPos;
                                }

                                break;
                            }

                            break;
                        }
                    }
                }

                break;
            }
            case PieceType.Soldier:
            {
                var forward = piece.Color == PieceColor.Red ? position + (-1, 0) : position + (1, 0);
                if (forward.IsOnBoard())
                {
                    var target = Board[forward].PieceInBlock;
                    if (target == null || target.Color != piece.Color)
                    {
                        yield return forward;
                    }
                }

                if (!position.IsOnOwnSide(piece.Color))
                {
                    var left = position + (0, -1);
                    if (left.IsOnBoard())
                    {
                        var target = Board[left].PieceInBlock;
                        if (target == null || target.Color != piece.Color)
                        {
                            yield return left;
                        }
                    }

                    var right = position + (0, 1);
                    if (right.IsOnBoard())
                    {
                        var target = Board[right].PieceInBlock;
                        if (target == null || target.Color != piece.Color)
                        {
                            yield return right;
                        }
                    }
                }

                break;
            }
            default: yield break;
        }
    }
}