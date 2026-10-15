using TicTacToe.Logic;

namespace TicTacToe.Players;

public class CpuPlayer : IPlayer
{
    private readonly Random _random;

    public string Name { get; }
    public CellSymbol Symbol { get; }

    public CpuPlayer(string name = "CPU", CellSymbol symbol = CellSymbol.O, Random? random = null)
    {
        Name = string.IsNullOrWhiteSpace(name) ? "CPU" : name.Trim();
        Symbol = symbol;
        _random = random ?? new Random();
    }

    public int GetMove(Board virtualBoard)
    {
        var available = virtualBoard.GetAvailableMoves();
        if (available.Count == 0)
        {
            return -1;
        }

        foreach (int move in available)
        {
            if (virtualBoard.WouldWin(move, Symbol))
            {
                return move;
            }
        }

        CellSymbol opponent = (Symbol == CellSymbol.X) ? CellSymbol.O : CellSymbol.X;
        foreach (int move in available)
        {
            if (virtualBoard.WouldWin(move, opponent))
            {
                return move;
            }
        }

        if (virtualBoard.IsCellEmpty(4))
        {
            return 4;
        }

        int[] corners = [0, 2, 6, 8];
        var availableCorners = available.Where(corners.Contains).ToList();
        if (availableCorners.Count > 0)
        {
            return availableCorners[_random.Next(availableCorners.Count)];
        }

        return available[_random.Next(available.Count)];
    }
}
