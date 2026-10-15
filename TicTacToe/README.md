# TicTacToe (Console Edition)

Console-based Tic-Tac-Toe game developed in C# (.NET 10) following Object-Oriented Programming (OOP) principles, Single Responsibility Principle (SRP), and Spec-Driven Development.

## Features

- **Game Modes**: Human vs Human and Human vs CPU (Heuristic AI).
- **Defensive Input Handling**: Prevents invalid numbers, out-of-bounds indices, and occupied cell selection without crashing.
- **Architecture**: Decoupled domain model (`Board`, `Game`), player abstractions (`IPlayer`, `HumanPlayer`, `CpuPlayer`), and presentation layer (`ConsoleUI`, `AppMenu`).
- **Comprehensive Unit Testing**: High test coverage using xUnit covering board validations, human input reading, AI decision logic, game flow, and menu navigation.

## Project Structure

```text
TicTacToe/
├── .gitignore
├── README.md
├── spec.md
├── TicTacToe.slnx
├── src/
│   └── TicTacToe/
│       ├── Program.cs
│       ├── TicTacToe.csproj
│       ├── Logic/
│       │   ├── Board.cs
│       │   ├── CellSymbol.cs
│       │   ├── Game.cs
│       │   └── GameResult.cs
│       ├── Players/
│       │   ├── IPlayer.cs
│       │   ├── HumanPlayer.cs
│       │   └── CpuPlayer.cs
│       └── UI/
│           ├── AppMenu.cs
│           └── ConsoleUI.cs
└── tests/
    └── TicTacToe.Tests/
        ├── AppMenuTests.cs
        ├── BoardTests.cs
        ├── CpuPlayerTests.cs
        ├── GameTests.cs
        ├── HumanPlayerTests.cs
        └── TicTacToe.Tests.csproj
```

## Running the Application

To run the console game:

```bash
dotnet run --project src/TicTacToe
```

## Running the Tests

To execute the automated xUnit test suite:

```bash
dotnet test
```
