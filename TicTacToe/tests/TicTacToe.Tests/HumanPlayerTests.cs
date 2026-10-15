using TicTacToe.Logic;
using TicTacToe.Players;
using Xunit;

namespace TicTacToe.Tests;

public class HumanPlayerTests
{
    [Fact]
    public void Properties_ShouldBeInitializedProperly()
    {
        var player = new HumanPlayer("Alice", CellSymbol.X);

        Assert.Equal("Alice", player.Name);
        Assert.Equal(CellSymbol.X, player.Symbol);
    }

    [Fact]
    public void GetMove_ValidSingleInput_ShouldReturnZeroBasedIndex()
    {
        var board = new Board();
        var reader = new StringReader("5\n");
        var writer = new StringWriter();
        var player = new HumanPlayer("Alice", CellSymbol.X, reader, writer);

        int move = player.GetMove(board);

        Assert.Equal(4, move);
        Assert.Contains("enter your move (1-9)", writer.ToString());
    }

    [Fact]
    public void GetMove_NonNumericInput_ShouldRepromptUntilValid()
    {
        var board = new Board();
        var inputSequence = string.Join("\n", "abc", "", "   ", "1") + "\n";
        var reader = new StringReader(inputSequence);
        var writer = new StringWriter();
        var player = new HumanPlayer("Bob", CellSymbol.O, reader, writer);

        int move = player.GetMove(board);

        Assert.Equal(0, move);
        Assert.Contains("Invalid input", writer.ToString());
    }

    [Fact]
    public void GetMove_OutOfRangeInput_ShouldRepromptUntilValid()
    {
        var board = new Board();
        var inputSequence = string.Join("\n", "-1", "0", "10", "99", "9") + "\n";
        var reader = new StringReader(inputSequence);
        var writer = new StringWriter();
        var player = new HumanPlayer("Alice", CellSymbol.X, reader, writer);

        int move = player.GetMove(board);

        Assert.Equal(8, move);
        Assert.Contains("Invalid input", writer.ToString());
    }

    [Fact]
    public void GetMove_OccupiedCell_ShouldRepromptUntilEmptyCellChosen()
    {
        var board = new Board();
        board.RegisterMove(4, CellSymbol.O);

        var inputSequence = string.Join("\n", "5", "3") + "\n";
        var reader = new StringReader(inputSequence);
        var writer = new StringWriter();
        var player = new HumanPlayer("Alice", CellSymbol.X, reader, writer);

        int move = player.GetMove(board);

        Assert.Equal(2, move);
        Assert.Contains("already occupied", writer.ToString());
    }
}
