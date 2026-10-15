# Technical Specification: Tic-Tac-Toe (Console Edition)

## 1. Overview and Scope

The purpose of this document is to define the architecture, domain model, design contracts, and portability standards for developing a console-based Tic-Tac-Toe game in C# (.NET), applying Object-Oriented Programming (OOP) principles, Single Responsibility Principle (SRP), IDE interoperability, and Spec-Driven Development.

## 2. System Requirements

### 2.1 Functional Requirements (FR)

* **FR-01 (Main Menu):** Interactive navigation to start a game (Human vs Human, Human vs CPU) or exit the application.
* **FR-02 (Board Management):** Representation of a 3x3 grid with clear indexing (1 to 9 or coordinates 0-2).
* **FR-03 (Turn Alternation):** Strict rotation between Player 'X' (default starting player) and Player 'O'.
* **FR-04 (Move Validation):** Verification that the selected cell exists and is currently unoccupied.
* **FR-05 (End Conditions):**
* Win detection by 3 matching symbols horizontally (3 rows), vertically (3 columns), or diagonally (2 diagonals).
* Draw/tie detection when all 9 cells are occupied with no winner.


* **FR-06 (UI Redraw):** Clearing and visual updating of the grid state after each valid action.
* **FR-07 (CPU Mode):** Automatic move generation for the computer-controlled opponent using a decoupled strategy (random or heuristic).
* **FR-08 (Replayability):** Option upon game completion to start a new match or return to the main menu.

### 2.2 Non-Functional Requirements (NFR)

* **NFR-01 (Robustness):** Erroneous user inputs (non-numeric text, out-of-range indices, empty inputs) must not cause unhandled exceptions (`FormatException`, `IndexOutOfRangeException`).
* **NFR-02 (Modularity and OOP):** The `Program` class is limited to bootstrapping the main application loop. Business logic must not reside in global static methods or inside `Main`.
* **NFR-03 (Extensibility):** The player system must support polymorphism to interchange human and CPU implementations without modifying the game engine.
* **NFR-04 (Portability and Environment Interoperability):** The project must be completely agnostic to the editor or development environment (Visual Studio Community, VS Code, Rider, or CLI tools). Compilation and execution rely exclusively on the standard .NET SDK.
* **NFR-05 (Version Control Hygiene):** The Git repository must not track compiled binary artifacts or OS/editor-dependent user configurations.

## 3. Compatibility, Project Structure, and Git

To ensure that the project can be cloned and executed immediately in Visual Studio Community or any other .NET-compatible environment, the following structural guidelines are established:

### 3.1 Repository Structure

```text
TicTacToe/
├── .gitignore
├── README.md
├── TicTacToe.sln               # Standard Visual Studio / .NET solution file
└── src/
    └── TicTacToe/
        ├── TicTacToe.csproj    # .NET SDK project file
        ├── Program.cs
        ├── Logic/
        │   ├── Board.cs
        │   └── Game.cs
        ├── Players/
        │   ├── IPlayer.cs
        │   ├── HumanPlayer.cs
        │   └── CpuPlayer.cs
        └── UI/
            ├── AppMenu.cs
            └── ConsoleUI.cs

```

### 3.2 `.csproj` File Configuration

The project file must adhere to the standard console template of the .NET SDK without proprietary extensions:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>

```

### 3.3 Exclusion Rules (`.gitignore`)

It is mandatory to include a .NET-specific `.gitignore` file in the repository root to exclude:

* Build output directories and binaries: `bin/`, `obj/`
* Visual Studio metadata: `.vs/`, `*.user`, `*.suo`
* Temporary or configuration files from other editors/tools: `.idea/`, `.vscode/`, `.antigravity/`

## 4. Architecture and Domain Model

### 4.1 Component Responsibilities

| Class / Interface | Single Responsibility |
| --- | --- |
| `Program` | Entry point. Initializes the primary orchestrator (`AppMenu`). |
| `AppMenu` | Controls menu flow, captures initial options, and handles console termination. |
| `Game` | Orchestrator of the game lifecycle: turn management, end-of-game checks, and replayability. |
| `Board` | State storage for the cells (3x3), move assignment, and line verification. |
| `IPlayer` (Interface) | Contract defining move acquisition and identity (`Name`, `Symbol`). |
| `HumanPlayer` | Implementation of `IPlayer` that captures cell selection via keyboard using safe input. |
| `CpuPlayer` | Implementation of `IPlayer` designed for algorithmic decision-making. |
| `ConsoleUI` | Pure presentation methods: board rendering, informative messages, and basic capture. |

### 4.2 Logical Class Diagram

```text
+-------------------+
|      Program      |
+-------------------+
          |
          v
+-------------------+
|      AppMenu      |
+-------------------+
          |
          v
+-------------------+          +-------------------+
|       Game        | <>-----> |       Board       |
+-------------------+          +-------------------+
          |                              |
          v                              |
    +-----------+                        |
    |  IPlayer  | <----------------------+
    +-----------+
      ^       ^
      |       |
+-----------+ +-----------+
|   Human   | |    Cpu    |
+-----------+ +-----------+

```

## 5. Design Contracts (Conceptual Definition)

### 5.1 `Board` Entity

* **State:** Two-dimensional array or one-dimensional array of size 9 (`char` or `enum CellSymbol { Empty, X, O }`).
* **Operations:**
* `bool IsCellEmpty(int position)`
* `bool RegisterMove(int position, CellSymbol symbol)`
* `bool HasWinner(CellSymbol symbol)`
* `bool IsFull()`
* `void Reset()`



### 5.2 `IPlayer` Interface

* **Properties:** `Name` (string), `Symbol` (CellSymbol).
* **Method:** `int GetMove(Board virtualBoard)`
* Allows a human player to request input from the user and the CPU to query board state to determine its move.



### 5.3 `Game` Orchestrator

* **Dependencies:** `Board` instance, two references of type `IPlayer` (`player1`, `player2`), reference to `ConsoleUI`.
* **Primary Method:** `GameResult Execute()`
* Runs the turn cycle until `HasWinner` or `IsFull` returns true.



## 6. Algorithms and Business Logic (Pseudocode)

### 6.1 Win Verification (`Board`)

Definition of the 8 winning combinations using 0-based indexing:

$$\text{Rows: } [0,1,2], [3,4,5], [6,7,8]$$

$$\text{Columns: } [0,3,6], [1,4,7], [2,5,8]$$

$$\text{Diagonals: } [0,4,8], [2,4,6]$$

```text
ALGORITHM HasWinner(symbol):
    combinations = [
        [0, 1, 2], [3, 4, 5], [6, 7, 8],
        [0, 3, 6], [1, 4, 7], [2, 5, 8],
        [0, 4, 8], [2, 4, 6]
    ]

    FOR EACH comb IN combinations:
        IF cells[comb[0]] == symbol AND 
           cells[comb[1]] == symbol AND 
           cells[comb[2]] == symbol THEN:
            RETURN True
            
    RETURN False

```

### 6.2 Game Lifecycle Loop (`Game.Execute`)

```text
ALGORITHM StartGameLoop:
    currentPlayer = player1
    gameOver = False

    WHILE NOT gameOver:
        ConsoleUI.DrawBoard(board)
        position = currentPlayer.GetMove(board)

        IF board.IsCellEmpty(position):
            board.RegisterMove(position, currentPlayer.Symbol)

            IF board.HasWinner(currentPlayer.Symbol):
                ConsoleUI.DrawBoard(board)
                ConsoleUI.ShowWinner(currentPlayer)
                gameOver = True
            ELSE IF board.IsFull():
                ConsoleUI.DrawBoard(board)
                ConsoleUI.ShowDraw()
                gameOver = True
            ELSE:
                // Alternate turn
                currentPlayer = (currentPlayer == player1) ? player2 : player1
        ELSE:
            ConsoleUI.ShowError("Cell occupied, please choose another.")

```

### 6.3 AI Strategy for `CpuPlayer`

* **Level 1 (Basic):** Detection of empty cells by filtering the array, followed by random selection using `Random`.
* **Level 2 (Intermediate / Heuristic):**
1. If the CPU has an immediate winning move, take it.
2. If the opponent has an immediate winning move on their next turn, block it.
3. If the center is available, take it.
4. Otherwise, pick a random available corner or edge.



## 7. Defensive Input Strategy

To prevent failures from non-numeric or empty inputs:

* Strict use of `int.TryParse(Console.ReadLine(), out int value)`.
* Numeric range validation (`value >= 1 && value <= 9`).
* Conversion to zero-based array index (`index = value - 1`).

```text
FUNCTION ReadIntInRange(minimum, maximum) -> Integer:
    REPEAT:
        rawInput = ReadConsoleLine()
        IF IsInteger(rawInput, result) AND result >= minimum AND result <= maximum THEN:
            RETURN result
        ELSE:
            ShowMessage("Invalid input. Please try again.")
    UNTIL a valid value is obtained

```

## 8. Acceptance Criteria (Testing Checklist)

| ID | Scenario | Input / Action | Expected Result |
| --- | --- | --- | --- |
| **TC-01** | Non-numeric input | Input: `"abc"`, `""`, `" "` | The system displays a warning and requests input again without crashing. |
| **TC-02** | Out-of-range input | Input: `0`, `10`, `-3` | Range error message and immediate re-prompt. |
| **TC-03** | Repeated cell | Select an occupied cell | Does not overwrite; requests an available cell. |
| **TC-04** | Horizontal/vertical win | Complete any row or column | Finishes the game, proclaims the winner, and blocks further moves. |
| **TC-05** | Diagonal win | Complete main or anti-diagonal | Finishes the game and declares the winner. |
| **TC-06** | Draw | 9 cells filled without a 3-in-a-row | Finishes with a standard draw message. |
| **TC-07** | CPU turn | Player 1 turn ends | The CPU plays immediately in a valid cell without keyboard prompts. |
| **TC-08** | Post-game flow | Game finished | Prompt: `1. Rematch`, `2. Main Menu`. The board resets cleanly upon restart. |
| **TC-09** | IDE Interoperability | Clone repo and open `.sln` in VS Community | Compiles and executes with `dotnet run` or `F5` without configuration errors. |

## 9. Recommended Implementation Phases

1. **Phase 1 (Base Structure and Repository):** Generate solution and project via CLI (`dotnet new sln`, `dotnet new console`), and configure `.gitignore`.
2. **Phase 2 (Domain Model - Board):** Implement the isolated `Board` class along with its logical validation tests and winning combinations.
3. **Phase 3 (Player Abstraction):** Create the `IPlayer` interface and implement `HumanPlayer` with robust input validation.
4. **Phase 4 (1v1 Game Engine):** Build the `Game` class to coordinate the game loop between two human players.
5. **Phase 5 (CPU Mode):** Implement `CpuPlayer` using the `IPlayer` interface and integrate it into pre-game setup.
6. **Phase 6 (UI and Menus):** Implement `AppMenu` and the `ConsoleUI` class with screen clearing (`Console.Clear`) and visual board formatting.