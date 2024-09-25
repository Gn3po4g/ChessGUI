namespace XiangqiGUI.Models;

public record Piece(Position Position, PieceType Type)
{
    public static Piece[] ChessBoard { get; } =
    [
        // Black
        new(new Position(0, 0), PieceType.BlackChariot),
        new(new Position(1, 0), PieceType.BlackHorse),
        new(new Position(2, 0), PieceType.BlackElephant),
        new(new Position(3, 0), PieceType.BlackAdvisor),
        new(new Position(4, 0), PieceType.BlackKing),
        new(new Position(5, 0), PieceType.BlackAdvisor),
        new(new Position(6, 0), PieceType.BlackElephant),
        new(new Position(7, 0), PieceType.BlackHorse),
        new(new Position(8, 0), PieceType.BlackChariot),
        new(new Position(1, 2), PieceType.BlackCannon),
        new(new Position(7, 2), PieceType.BlackCannon),
        new(new Position(0, 3), PieceType.BlackSoldier),
        new(new Position(2, 3), PieceType.BlackSoldier),
        new(new Position(4, 3), PieceType.BlackSoldier),
        new(new Position(6, 3), PieceType.BlackSoldier),
        new(new Position(8, 3), PieceType.BlackSoldier),
        // Red
        new(new Position(0, 9), PieceType.RedChariot),
        new(new Position(1, 9), PieceType.RedHorse),
        new(new Position(2, 9), PieceType.RedElephant),
        new(new Position(3, 9), PieceType.RedAdvisor),
        new(new Position(4, 9), PieceType.RedKing),
        new(new Position(5, 9), PieceType.RedAdvisor),
        new(new Position(6, 9), PieceType.RedElephant),
        new(new Position(7, 9), PieceType.RedHorse),
        new(new Position(8, 9), PieceType.RedChariot),
        new(new Position(1, 7), PieceType.RedCannon),
        new(new Position(7, 7), PieceType.RedCannon),
        new(new Position(0, 6), PieceType.RedSoldier),
        new(new Position(2, 6), PieceType.RedSoldier),
        new(new Position(4, 6), PieceType.RedSoldier),
        new(new Position(6, 6), PieceType.RedSoldier),
        new(new Position(8, 6), PieceType.RedSoldier),
    ];
}

public enum PieceType
{
    BlackAdvisor,
    BlackCannon,
    BlackChariot,
    BlackElephant,
    BlackHorse,
    BlackKing,
    BlackSoldier,
    None,
    RedAdvisor,
    RedCannon,
    RedChariot,
    RedElephant,
    RedHorse,
    RedKing,
    RedSoldier,
}