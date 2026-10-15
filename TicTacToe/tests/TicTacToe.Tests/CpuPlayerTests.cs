using TicTacToe.Logic;
using TicTacToe.Players;
using Xunit;

namespace TicTacToe.Tests;

public class CpuPlayerTests
{
    [Fact]
    public void GetMove_ImmediateWinAvailable_TakesWinningMove()
    {
        var board = new Board();
        board.RegisterMove(0, CellSymbol.O);
        board.RegisterMove(1, CellSymbol.O);

        var cpu = new CpuPlayer("CPU", CellSymbol.O);
        int move = cpu.GetMove(board);

        Assert.Equal(2, move);
    }

    [Fact]
    public void GetMove_OpponentWinningMoveAvailable_BlocksOpponent()
    {
        var board = new Board();
        board.RegisterMove(0, CellSymbol.X);
        board.RegisterMove(3, CellSymbol.X);

        var cpu = new CpuPlayer("CPU", CellSymbol.O);
        int move = cpu.GetMove(board);

        Assert.Equal(6, move);
    }

    [Fact]
    public void GetMove_CenterAvailable_TakesCenter()
    {
        var board = new Board();
        board.RegisterMove(0, CellSymbol.X);

        var cpu = new CpuPlayer("CPU", CellSymbol.O);
        int move = cpu.GetMove(board);

        Assert.Equal(4, move);
    }

    [Fact]
    public void GetMove_CenterOccupied_TakesCorner()
    {
        var board = new Board();
        board.RegisterMove(4, CellSymbol.X);

        var cpu = new CpuPlayer("CPU", CellSymbol.O);
        int move = cpu.GetMove(board);

        int[] corners = [0, 2, 6, 8];
        Assert.Contains(move, corners);
    }

    [Fact]
    public void CpuPlayer_PlaysFullGameAgainstCpu_FinishesCleanly()
    {
        var p1 = new CpuPlayer("CPU 1", CellSymbol.X, new Random(42));
        var p2 = new CpuPlayer("CPU 2", CellSymbol.O, new Random(100));

        var game = new Game(p1, p2);
        var result = game.Execute();

        Assert.True(result.IsDraw || result.Winner is not null);
    }
}
