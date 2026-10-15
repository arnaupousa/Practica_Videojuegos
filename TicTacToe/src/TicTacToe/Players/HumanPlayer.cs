using TicTacToe.Logic;

namespace TicTacToe.Players;

public class HumanPlayer : IPlayer
{
    private readonly TextReader _reader;
    private readonly TextWriter _writer;

    public string Name { get; }
    public CellSymbol Symbol { get; }

    public HumanPlayer(string name, CellSymbol symbol, TextReader? reader = null, TextWriter? writer = null)
    {
        Name = string.IsNullOrWhiteSpace(name) ? "Player" : name.Trim();
        Symbol = symbol;
        _reader = reader ?? Console.In;
        _writer = writer ?? Console.Out;
    }

    public int GetMove(Board virtualBoard)
    {
        while (true)
        {
            _writer.Write($"{Name} ({Symbol}), enter your move (1-9): ");
            string? rawInput = _reader.ReadLine();

            if (rawInput is null)
            {
                _writer.WriteLine("No input detected. Please try again.");
                continue;
            }

            if (!int.TryParse(rawInput.Trim(), out int choice) || choice < 1 || choice > 9)
            {
                _writer.WriteLine("Invalid input. Please enter a valid number between 1 and 9.");
                continue;
            }

            int position = choice - 1;

            if (!virtualBoard.IsCellEmpty(position))
            {
                _writer.WriteLine("That cell is already occupied. Please choose an empty cell.");
                continue;
            }

            return position;
        }
    }
}
