using TicTacToe.UI;
using Xunit;

namespace TicTacToe.Tests;

public class AppMenuTests
{
    [Fact]
    public void Run_Option3_ExitsCleanly()
    {
        var reader = new StringReader("3\n");
        var writer = new StringWriter();
        var menu = new AppMenu(reader, writer);

        menu.Run();

        string output = writer.ToString();
        Assert.Contains("=== TIC-TAC-TOE ===", output);
        Assert.Contains("Thanks for playing! Goodbye.", output);
    }

    [Fact]
    public void Run_InvalidOption_RepromptsUntilValid()
    {
        var inputSequence = string.Join("\n", "abc", "-1", "4", "3") + "\n";
        var reader = new StringReader(inputSequence);
        var writer = new StringWriter();
        var menu = new AppMenu(reader, writer);

        menu.Run();

        string output = writer.ToString();
        Assert.Contains("Invalid selection", output);
        Assert.Contains("Thanks for playing! Goodbye.", output);
    }

    [Fact]
    public void Run_Option1_HumanVsHuman_CompletesMatchAndReturnsToMenu()
    {
        // 1: Human vs Human
        // Game moves (P1 top row win): 1, 4, 2, 5, 3
        // Post match: 2 (Main Menu)
        // Main menu: 3 (Exit)
        var inputSequence = string.Join("\n", "1", "1", "4", "2", "5", "3", "2", "3") + "\n";
        var reader = new StringReader(inputSequence);
        var writer = new StringWriter();
        var menu = new AppMenu(reader, writer);

        menu.Run();

        string output = writer.ToString();
        Assert.Contains("Player 1 (X) wins the game!", output);
        Assert.Contains("Match finished.", output);
        Assert.Contains("Thanks for playing! Goodbye.", output);
    }
}
