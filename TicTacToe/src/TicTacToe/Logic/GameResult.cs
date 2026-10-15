using TicTacToe.Players;

namespace TicTacToe.Logic;

public class GameResult
{
    public IPlayer? Winner { get; }
    public bool IsDraw => Winner is null;

    public GameResult(IPlayer? winner)
    {
        Winner = winner;
    }
}
