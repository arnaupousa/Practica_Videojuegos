using TicTacToe.Logic;
using TicTacToe.Players;
using TicTacToe.UI;
using Xunit;

namespace TicTacToe.Tests;

public class GameTests
{
    private class MockPlayer : IPlayer
    {
        private readonly Queue<int> _moves;

        public string Name { get; }
        public CellSymbol Symbol { get; }

        public MockPlayer(string name, CellSymbol symbol, IEnumerable<int> moves)
        {
            Name = name;
            Symbol = symbol;
            _moves = new Queue<int>(moves);
        }

        public int GetMove(Board virtualBoard)
        {
            return _moves.Dequeue();
        }
    }

    [Fact]
    public void Execute_Player1Wins_ReturnsPlayer1AsWinner()
    {
        // Player1 (X): 0, 1, 2 (Top row win)
        // Player2 (O): 3, 4
        var p1 = new MockPlayer("Alice", CellSymbol.X, [0, 1, 2]);
        var p2 = new MockPlayer("Bob", CellSymbol.O, [3, 4]);

        var game = new Game(p1, p2);
        var result = game.Execute();

        Assert.False(result.IsDraw);
        Assert.Same(p1, result.Winner);
        Assert.True(game.Board.HasWinner(CellSymbol.X));
    }

    [Fact]
    public void Execute_Player2Wins_ReturnsPlayer2AsWinner()
    {
        // Player1 (X): 0, 1, 5
        // Player2 (O): 3, 4, ? -> let's do vertical col 1 (index 1, 4, 7):
        // P1 plays: 0, 2, 5
        // P2 plays: 1, 4, 7 (Col 2 win)
        var p1 = new MockPlayer("Alice", CellSymbol.X, [0, 2, 5]);
        var p2 = new MockPlayer("Bob", CellSymbol.O, [1, 4, 7]);

        var game = new Game(p1, p2);
        var result = game.Execute();

        Assert.False(result.IsDraw);
        Assert.Same(p2, result.Winner);
        Assert.True(game.Board.HasWinner(CellSymbol.O));
    }

    [Fact]
    public void Execute_FullBoardNoWinner_ReturnsDraw()
    {
        // X O X
        // X X O
        // O X O
        // P1 (X): 0, 2, 3, 4, 7
        // P2 (O): 1, 5, 6, 8
        var p1 = new MockPlayer("Alice", CellSymbol.X, [0, 2, 3, 4, 7]);
        var p2 = new MockPlayer("Bob", CellSymbol.O, [1, 5, 6, 8]);

        var game = new Game(p1, p2);
        var result = game.Execute();

        Assert.True(result.IsDraw);
        Assert.Null(result.Winner);
        Assert.True(game.Board.IsFull());
    }

    [Fact]
    public void ConsoleUI_DrawBoard_RendersBoardCorrectly()
    {
        var board = new Board();
        board.RegisterMove(0, CellSymbol.X);
        board.RegisterMove(4, CellSymbol.O);

        var writer = new StringWriter();
        var ui = new ConsoleUI(writer);

        ui.DrawBoard(board);
        string output = writer.ToString();

        Assert.Contains("X", output);
        Assert.Contains("O", output);
        Assert.Contains("3", output);
    }
}
