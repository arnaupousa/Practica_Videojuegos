using TicTacToe.Logic;
using TicTacToe.Players;

namespace TicTacToe.UI;

public class AppMenu
{
    private readonly TextReader _reader;
    private readonly TextWriter _writer;
    private readonly ConsoleUI _ui;

    public AppMenu(TextReader? reader = null, TextWriter? writer = null, ConsoleUI? ui = null)
    {
        _reader = reader ?? Console.In;
        _writer = writer ?? Console.Out;
        _ui = ui ?? new ConsoleUI(_writer);
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            _writer.WriteLine("=== TIC-TAC-TOE ===");
            _writer.WriteLine("1. Human vs Human");
            _writer.WriteLine("2. Human vs CPU");
            _writer.WriteLine("3. Exit");

            int option = ReadIntInRange(1, 3, "Select an option (1-3): ");

            switch (option)
            {
                case 1:
                    StartGameSession(
                        new HumanPlayer("Player 1", CellSymbol.X, _reader, _writer),
                        new HumanPlayer("Player 2", CellSymbol.O, _reader, _writer));
                    break;

                case 2:
                    StartGameSession(
                        new HumanPlayer("Player 1", CellSymbol.X, _reader, _writer),
                        new CpuPlayer("CPU", CellSymbol.O));
                    break;

                case 3:
                    _writer.WriteLine("Thanks for playing! Goodbye.");
                    running = false;
                    break;
            }
        }
    }

    private void StartGameSession(IPlayer player1, IPlayer player2)
    {
        bool playing = true;
        var game = new Game(player1, player2, null, _ui);

        while (playing)
        {
            game.Execute();

            _writer.WriteLine("Match finished.");
            _writer.WriteLine("1. Rematch");
            _writer.WriteLine("2. Main Menu");

            int choice = ReadIntInRange(1, 2, "Select an option (1-2): ");
            if (choice == 2)
            {
                playing = false;
            }
        }
    }

    private int ReadIntInRange(int min, int max, string prompt)
    {
        while (true)
        {
            _writer.Write(prompt);
            string? rawInput = _reader.ReadLine();

            if (rawInput is null)
            {
                return max;
            }

            if (int.TryParse(rawInput.Trim(), out int val) && val >= min && val <= max)
            {
                return val;
            }

            _writer.WriteLine($"Invalid selection. Please enter a number between {min} and {max}.");
        }
    }
}
