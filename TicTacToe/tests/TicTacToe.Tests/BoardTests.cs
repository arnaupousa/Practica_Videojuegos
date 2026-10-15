using TicTacToe.Logic;
using Xunit;

namespace TicTacToe.Tests;

public class BoardTests
{
    [Fact]
    public void NewBoard_ShouldHaveAllCellsEmpty()
    {
        var board = new Board();

        for (int i = 0; i < Board.BoardSize; i++)
        {
            Assert.True(board.IsCellEmpty(i));
            Assert.Equal(CellSymbol.Empty, board.GetCell(i));
        }

        Assert.False(board.IsFull());
        Assert.False(board.HasWinner(CellSymbol.X));
        Assert.False(board.HasWinner(CellSymbol.O));
        Assert.Equal(9, board.GetAvailableMoves().Count);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(9)]
    [InlineData(100)]
    public void IsCellEmpty_OutOfBounds_ShouldReturnFalse(int position)
    {
        var board = new Board();

        Assert.False(board.IsCellEmpty(position));
    }

    [Fact]
    public void RegisterMove_ValidPosition_ShouldSucceed()
    {
        var board = new Board();

        bool success = board.RegisterMove(4, CellSymbol.X);

        Assert.True(success);
        Assert.False(board.IsCellEmpty(4));
        Assert.Equal(CellSymbol.X, board.GetCell(4));
        Assert.DoesNotContain(4, board.GetAvailableMoves());
    }

    [Fact]
    public void RegisterMove_OccupiedPosition_ShouldReturnFalse()
    {
        var board = new Board();
        board.RegisterMove(0, CellSymbol.X);

        bool secondMove = board.RegisterMove(0, CellSymbol.O);

        Assert.False(secondMove);
        Assert.Equal(CellSymbol.X, board.GetCell(0));
    }

    [Fact]
    public void RegisterMove_EmptySymbol_ShouldReturnFalse()
    {
        var board = new Board();

        bool success = board.RegisterMove(0, CellSymbol.Empty);

        Assert.False(success);
        Assert.True(board.IsCellEmpty(0));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(9)]
    public void RegisterMove_OutOfBounds_ShouldReturnFalse(int position)
    {
        var board = new Board();

        bool success = board.RegisterMove(position, CellSymbol.X);

        Assert.False(success);
    }

    [Theory]
    [InlineData(0, 1, 2)]
    [InlineData(3, 4, 5)]
    [InlineData(6, 7, 8)]
    public void HasWinner_HorizontalWinningRows_ShouldReturnTrue(int c1, int c2, int c3)
    {
        var board = new Board();
        board.RegisterMove(c1, CellSymbol.X);
        board.RegisterMove(c2, CellSymbol.X);
        board.RegisterMove(c3, CellSymbol.X);

        Assert.True(board.HasWinner(CellSymbol.X));
        Assert.False(board.HasWinner(CellSymbol.O));
    }

    [Theory]
    [InlineData(0, 3, 6)]
    [InlineData(1, 4, 7)]
    [InlineData(2, 5, 8)]
    public void HasWinner_VerticalWinningColumns_ShouldReturnTrue(int c1, int c2, int c3)
    {
        var board = new Board();
        board.RegisterMove(c1, CellSymbol.O);
        board.RegisterMove(c2, CellSymbol.O);
        board.RegisterMove(c3, CellSymbol.O);

        Assert.True(board.HasWinner(CellSymbol.O));
        Assert.False(board.HasWinner(CellSymbol.X));
    }

    [Theory]
    [InlineData(0, 4, 8)]
    [InlineData(2, 4, 6)]
    public void HasWinner_DiagonalWinningLines_ShouldReturnTrue(int c1, int c2, int c3)
    {
        var board = new Board();
        board.RegisterMove(c1, CellSymbol.X);
        board.RegisterMove(c2, CellSymbol.X);
        board.RegisterMove(c3, CellSymbol.X);

        Assert.True(board.HasWinner(CellSymbol.X));
        Assert.False(board.HasWinner(CellSymbol.O));
    }

    [Fact]
    public void HasWinner_TwoInARow_ShouldReturnFalse()
    {
        var board = new Board();
        board.RegisterMove(0, CellSymbol.X);
        board.RegisterMove(1, CellSymbol.X);

        Assert.False(board.HasWinner(CellSymbol.X));
    }

    [Fact]
    public void IsFull_WhenAllCellsFilled_ShouldReturnTrue()
    {
        var board = new Board();
        board.RegisterMove(0, CellSymbol.X);
        board.RegisterMove(1, CellSymbol.O);
        board.RegisterMove(2, CellSymbol.X);
        board.RegisterMove(3, CellSymbol.X);
        board.RegisterMove(4, CellSymbol.X);
        board.RegisterMove(5, CellSymbol.O);
        board.RegisterMove(6, CellSymbol.O);
        board.RegisterMove(7, CellSymbol.X);
        board.RegisterMove(8, CellSymbol.O);

        Assert.True(board.IsFull());
        Assert.Empty(board.GetAvailableMoves());
    }

    [Fact]
    public void Reset_ShouldClearAllCells()
    {
        var board = new Board();
        board.RegisterMove(0, CellSymbol.X);
        board.RegisterMove(4, CellSymbol.O);
        board.RegisterMove(8, CellSymbol.X);

        board.Reset();

        for (int i = 0; i < Board.BoardSize; i++)
        {
            Assert.True(board.IsCellEmpty(i));
        }

        Assert.False(board.IsFull());
        Assert.False(board.HasWinner(CellSymbol.X));
        Assert.Equal(9, board.GetAvailableMoves().Count);
    }
}
