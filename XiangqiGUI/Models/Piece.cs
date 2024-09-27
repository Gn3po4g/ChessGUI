namespace XiangqiGUI.Models;

public record Piece(PieceType Type, PieceColor Color)
{
    public static Dictionary<Position, Piece> Board { get; } = new()
    {
        // Black
        [new Position(0, 0)] = new Piece(PieceType.Chariot, PieceColor.Black),
        [new Position(0, 1)] = new Piece(PieceType.Horse, PieceColor.Black),
        [new Position(0, 2)] = new Piece(PieceType.Elephant, PieceColor.Black),
        [new Position(0, 3)] = new Piece(PieceType.Advisor, PieceColor.Black),
        [new Position(0, 4)] = new Piece(PieceType.King, PieceColor.Black),
        [new Position(0, 5)] = new Piece(PieceType.Advisor, PieceColor.Black),
        [new Position(0, 6)] = new Piece(PieceType.Elephant, PieceColor.Black),
        [new Position(0, 7)] = new Piece(PieceType.Horse, PieceColor.Black),
        [new Position(0, 8)] = new Piece(PieceType.Chariot, PieceColor.Black),
        [new Position(2, 1)] = new Piece(PieceType.Cannon, PieceColor.Black),
        [new Position(2, 7)] = new Piece(PieceType.Cannon, PieceColor.Black),
        [new Position(3, 0)] = new Piece(PieceType.Soldier, PieceColor.Black),
        [new Position(3, 2)] = new Piece(PieceType.Soldier, PieceColor.Black),
        [new Position(3, 4)] = new Piece(PieceType.Soldier, PieceColor.Black),
        [new Position(3, 6)] = new Piece(PieceType.Soldier, PieceColor.Black),
        [new Position(3, 8)] = new Piece(PieceType.Soldier, PieceColor.Black),
        // Red
        [new Position(9, 0)] = new Piece(PieceType.Chariot, PieceColor.Red),
        [new Position(9, 1)] = new Piece(PieceType.Horse, PieceColor.Red),
        [new Position(9, 2)] = new Piece(PieceType.Elephant, PieceColor.Red),
        [new Position(9, 3)] = new Piece(PieceType.Advisor, PieceColor.Red),
        [new Position(9, 4)] = new Piece(PieceType.King, PieceColor.Red),
        [new Position(9, 5)] = new Piece(PieceType.Advisor, PieceColor.Red),
        [new Position(9, 6)] = new Piece(PieceType.Elephant, PieceColor.Red),
        [new Position(9, 7)] = new Piece(PieceType.Horse, PieceColor.Red),
        [new Position(9, 8)] = new Piece(PieceType.Chariot, PieceColor.Red),
        [new Position(7, 1)] = new Piece(PieceType.Cannon, PieceColor.Red),
        [new Position(7, 7)] = new Piece(PieceType.Cannon, PieceColor.Red),
        [new Position(6, 0)] = new Piece(PieceType.Soldier, PieceColor.Red),
        [new Position(6, 2)] = new Piece(PieceType.Soldier, PieceColor.Red),
        [new Position(6, 4)] = new Piece(PieceType.Soldier, PieceColor.Red),
        [new Position(6, 6)] = new Piece(PieceType.Soldier, PieceColor.Red),
        [new Position(6, 8)] = new Piece(PieceType.Soldier, PieceColor.Red),
    };
}

public enum PieceType
{
    King,
    Advisor,
    Elephant,
    Horse,
    Chariot,
    Cannon,
    Soldier
}

public enum PieceColor
{
    Red,
    Black
}