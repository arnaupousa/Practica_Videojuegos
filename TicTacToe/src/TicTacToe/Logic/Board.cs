namespace TicTacToe.Logic;

public class Board
{
    public const int BoardSize = 9;

    private readonly CellSymbol[] _cells = new CellSymbol[BoardSize];

    private static readonly int[][] WinningCombinations =
    [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6]
    ];

    public Board()
    {
        Reset();
    }

    public CellSymbol GetCell(int position)
    {
        if (position < 0 || position >= BoardSize)
        {
            throw new ArgumentOutOfRangeException(nameof(position), $"Position must be between 0 and {BoardSize - 1}.");
        }

        return _cells[position];
    }

    public bool IsCellEmpty(int position)
    {
        if (position < 0 || position >= BoardSize)
        {
            return false;
        }

        return _cells[position] == CellSymbol.Empty;
    }

    public bool RegisterMove(int position, CellSymbol symbol)
    {
        if (symbol == CellSymbol.Empty || !IsCellEmpty(position))
        {
            return false;
        }

        _cells[position] = symbol;
        return true;
    }

    public bool HasWinner(CellSymbol symbol)
    {
        if (symbol == CellSymbol.Empty)
        {
            return false;
        }

        foreach (var combo in WinningCombinations)
        {
            if (_cells[combo[0]] == symbol &&
                _cells[combo[1]] == symbol &&
                _cells[combo[2]] == symbol)
            {
                return true;
            }
        }

        return false;
    }

    public bool WouldWin(int position, CellSymbol symbol)
    {
        if (!IsCellEmpty(position) || symbol == CellSymbol.Empty)
        {
            return false;
        }

        _cells[position] = symbol;
        bool won = HasWinner(symbol);
        _cells[position] = CellSymbol.Empty;
        return won;
    }

    public bool IsFull()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            if (_cells[i] == CellSymbol.Empty)
            {
                return false;
            }
        }

        return true;
    }

    public void Reset()
    {
        Array.Clear(_cells, 0, BoardSize);
    }

    public IReadOnlyList<int> GetAvailableMoves()
    {
        var available = new List<int>();
        for (int i = 0; i < BoardSize; i++)
        {
            if (_cells[i] == CellSymbol.Empty)
            {
                available.Add(i);
            }
        }

        return available;
    }
}
