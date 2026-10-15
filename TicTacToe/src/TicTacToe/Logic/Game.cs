using TicTacToe.Players;
using TicTacToe.UI;

namespace TicTacToe.Logic;

public class Game
{
    private readonly ConsoleUI? _ui;

    public Board Board { get; }
    public IPlayer Player1 { get; }
    public IPlayer Player2 { get; }
    public IPlayer CurrentPlayer { get; private set; }

    public Game(IPlayer player1, IPlayer player2, Board? board = null, ConsoleUI? ui = null)
    {
        Player1 = player1 ?? throw new ArgumentNullException(nameof(player1));
        Player2 = player2 ?? throw new ArgumentNullException(nameof(player2));
        Board = board ?? new Board();
        _ui = ui;
        CurrentPlayer = Player1;
    }

    public GameResult Execute()
    {
        Board.Reset();
        CurrentPlayer = Player1;

        while (true)
        {
            _ui?.DrawBoard(Board);
            int position = CurrentPlayer.GetMove(Board);

            if (Board.IsCellEmpty(position))
            {
                Board.RegisterMove(position, CurrentPlayer.Symbol);

                if (Board.HasWinner(CurrentPlayer.Symbol))
                {
                    _ui?.DrawBoard(Board);
                    _ui?.ShowWinner(CurrentPlayer);
                    return new GameResult(CurrentPlayer);
                }

                if (Board.IsFull())
                {
                    _ui?.DrawBoard(Board);
                    _ui?.ShowDraw();
                    return new GameResult(null);
                }

                CurrentPlayer = (CurrentPlayer == Player1) ? Player2 : Player1;
            }
            else
            {
                _ui?.ShowError("Cell occupied, please choose another.");
            }
        }
    }
}
