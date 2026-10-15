using TicTacToe.Logic;

namespace TicTacToe.Players;

public interface IPlayer
{
    string Name { get; }
    CellSymbol Symbol { get; }
    int GetMove(Board virtualBoard);
}
