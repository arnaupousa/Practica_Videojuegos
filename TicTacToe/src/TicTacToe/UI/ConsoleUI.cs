using TicTacToe.Logic;
using TicTacToe.Players;

namespace TicTacToe.UI;

public class ConsoleUI
{
    private readonly TextWriter _writer;

    public ConsoleUI(TextWriter? writer = null)
    {
        _writer = writer ?? Console.Out;
    }

    public virtual void ClearScreen()
    {
        try
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
        }
        catch
        {
        }
    }

    public virtual void DrawBoard(Board board, bool clear = true)
    {
        if (clear)
        {
            ClearScreen();
        }

        _writer.WriteLine();
        for (int row = 0; row < 3; row++)
        {
            int baseIndex = row * 3;
            string c1 = FormatCell(board.GetCell(baseIndex), baseIndex + 1);
            string c2 = FormatCell(board.GetCell(baseIndex + 1), baseIndex + 2);
            string c3 = FormatCell(board.GetCell(baseIndex + 2), baseIndex + 3);

            _writer.WriteLine($" {c1} | {c2} | {c3} ");
            if (row < 2)
            {
                _writer.WriteLine("---+---+---");
            }
        }
        _writer.WriteLine();
    }

    public virtual void ShowWinner(IPlayer winner)
    {
        _writer.WriteLine($"Congratulations! {winner.Name} ({winner.Symbol}) wins the game!");
    }

    public virtual void ShowDraw()
    {
        _writer.WriteLine("The game ended in a draw! No more empty cells.");
    }

    public virtual void ShowError(string message)
    {
        _writer.WriteLine($"Error: {message}");
    }

    private static string FormatCell(CellSymbol symbol, int index)
    {
        return symbol switch
        {
            CellSymbol.X => "X",
            CellSymbol.O => "O",
            _ => index.ToString()
        };
    }
}
