# Documentación del Proyecto: Tres en Raya (Tic-Tac-Toe)

## 1. Juego Desarrollado y Funcionalidades Implementadas

El proyecto consiste en una versión robusta y desacoplada del clásico juego **Tres en Raya** (*Tic-Tac-Toe*) para consola, desarrollado en **C# (.NET 10)** bajo la metodología **Spec-Driven Development (SDD)** y los principios del diseño orientado a objetos (**OOP** y **SRP**).

### Funcionalidades Implementadas:
* **FR-01 (Menú Principal):** Navegación interactiva que permite elegir entre partidas Humano contra Humano, Humano contra CPU o Salir del juego.
* **FR-02 (Gestión del Tablero):** Representación bidimensional 3x3 (mapeada internamente en un vector plano de 9 posiciones) con índices numéricos claros del 1 al 9 para celdas disponibles y marcas `X` / `O` para celdas ocupadas.
* **FR-03 (Alternancia de Turnos):** Rotación estricta y predecible entre jugadores, iniciando por defecto el Jugador 'X' y respondiendo el Jugador 'O'.
* **FR-04 (Validación Defensiva de Movimientos):** Verificación continua de que las casillas elegidas existan dentro del rango permitido (1 a 9) y no se encuentren previamente ocupadas.
* **FR-05 (Detección de Fin de Partida):**
  * Detección de victoria inmediata comprobando las 8 combinaciones ganadoras (3 filas, 3 columnas y 2 diagonales).
  * Detección de empate (tablas) cuando las 9 casillas quedan ocupadas sin que exista un ganador.
* **FR-06 (Redibujado y Limpieza de Consola):** Limpieza visual de la pantalla (`Console.Clear`) y actualización del tablero tras cada turno válido, con tolerancia a entornos donde la salida estándar esté redirigida o en ejecución de tests automatizados.
* **FR-07 (Modo CPU Inteligente):** Generación automática e instantánea de movimientos por parte de la máquina sin bloquear ni requerir entradas de teclado.
* **FR-08 (Revancha y Rejugabilidad):** Al finalizar cada partida se ofrece un submenú para jugar una revancha inmediata (reiniciando el tablero en limpio) o volver al menú principal.
* **NFR-01 (Robustez Absoluta):** Manejo defensivo de entradas con `int.TryParse`, evitando excepciones no controladas ante entradas vacías, caracteres alfabéticos o números fuera de rango.

---

## 2. Estructura de Clases Final y Responsabilidades

La arquitectura sigue una separación rigurosa de responsabilidades (SRP) dividida en lógica de dominio, jugadores y presentación:

```text
TicTacToe/
├── Program.cs
├── Logic/
│   ├── CellSymbol.cs
│   ├── Board.cs
│   ├── GameResult.cs
│   └── Game.cs
├── Players/
│   ├── IPlayer.cs
│   ├── HumanPlayer.cs
│   └── CpuPlayer.cs
└── UI/
    ├── ConsoleUI.cs
    └── AppMenu.cs
```

### Tabla de Responsabilidades

| Clase / Interfaz | Responsabilidad Principal |
|---|---|
| `Program` | **Punto de Entrada:** Responsabilidad única de inicializar y arrancar el orquestador principal (`AppMenu`). Carece de lógica de negocio o métodos estáticos globales. |
| `AppMenu` | **Orquestador de Aplicación:** Controla el ciclo de vida del menú en consola, captura de selecciones principales, lanzamiento de sesiones de juego y gestión de revanchas. |
| `Game` | **Motor de Juego:** Coordina el bucle de la partida (ejecución de turnos, alternancia entre jugadores, verificación de victoria/empate y comunicación con la UI). |
| `GameResult` | **Objeto de Resultado:** Encapsula de forma inmutable el desenlace de una partida indicando si hubo empate o qué jugador resultó vencedor. |
| `Board` | **Modelo de Dominio del Tablero:** Mantiene el estado de las 9 casillas, valida posiciones vacías, registra jugadas, simula movimientos potenciales (`WouldWin`), evalúa las 8 líneas ganadoras y reinicia el estado. |
| `CellSymbol` | **Enumeración de Casillas:** Define de forma fuertemente tipada los estados posibles de cada casilla (`Empty = 0`, `X = 1`, `O = 2`). |
| `IPlayer` | **Contrato Polimórfico:** Define las propiedades identificativas (`Name`, `Symbol`) y el método contractual `int GetMove(Board virtualBoard)`. |
| `HumanPlayer` | **Jugador Humano:** Implementación de `IPlayer` que solicita la jugada al usuario mediante consola, aplicando reintentos defensivos ante entradas inválidas o casillas ocupadas. Admite inyección de `TextReader`/`TextWriter`. |
| `CpuPlayer` | **Jugador Automático (IA):** Implementación de `IPlayer` que calcula su siguiente movimiento de manera algorítmica sin intervención de teclado. |
| `ConsoleUI` | **Capa de Presentación:** Métodos puros para dibujar el tablero con formato visual legible, emitir mensajes informativos (victoria, empate, errores) y limpiar la pantalla. |

---

## 3. Reparto Final de Tareas y Registro de Contribuciones

El desarrollo se articuló en 6 fases secuenciales siguiendo la metodología de desarrollo dirigido por especificación (*Spec-Driven Development*), con verificación de integración continua en Git publicando siempre en la rama `dev` antes de sincronizar con `main`:

1. **Fase 1 (Estructura Base y Repositorio):**
   * Configuración de la solución estándar `TicTacToe.sln` y proyectos .NET.
   * Configuración de exclusiones higiénicas en `.gitignore` (`bin/`, `obj/`, metadatos de IDEs).
   * Verificación de la rama de trabajo `dev` y seguimiento de `origin/dev`.

2. **Fase 2 (Modelo de Dominio - Board & CellSymbol):**
   * Creación del enumerado `CellSymbol` y de la clase `Board` con un arreglo lineal de 9 posiciones.
   * Implementación de la matriz precomputada de 8 combinaciones ganadoras (filas, columnas y diagonales).
   * Creación del proyecto de pruebas xUnit (`TicTacToe.Tests`) y elaboración de `BoardTests.cs` (20 tests unitarios cubriendo límites, registros válidos/inválidos, victorias y empates).
   * Commit y push a `origin/dev` y merge en `origin/main`.

3. **Fase 3 (Abstracción de Jugadores - IPlayer & HumanPlayer):**
   * Definición del contrato polimórfico `IPlayer`.
   * Implementación de `HumanPlayer` con bucle de lectura seguro contra entradas erróneas, vacías o repetidas.
   * Batería de pruebas en `HumanPlayerTests.cs` (5 tests unitarios verificando robustez con simulaciones de secuencias de entrada).
   * Commit y push a `origin/dev` y merge en `origin/main`.

4. **Fase 4 (Motor de Partidas 1v1 - Game & ConsoleUI):**
   * Limpieza preventiva de comentarios redundantes y simplificación del código.
   * Implementación del motor `Game` y de la clase `ConsoleUI` para redibujado y avisos de estado.
   * Batería de pruebas en `GameTests.cs` (4 tests simulando victorias de P1, P2, empates y renderizado visual).
   * Commit y push a `origin/dev` y merge en `origin/main`.

5. **Fase 5 (Modo CPU e IA Heurística):**
   * Incorporación del método de simulación no destructiva `WouldWin` en `Board`.
   * Implementación de `CpuPlayer` con el árbol de decisiones heurístico.
   * Batería de pruebas en `CpuPlayerTests.cs` (5 tests comprobando victoria inmediata, bloqueo, control del centro y corners).
   * Commit y push a `origin/dev` y merge en `origin/main`.

6. **Fase 6 (Menú Principal, Ciclo de Vida y README):**
   * Desarrollo de `AppMenu` y adaptación de `Program.cs` como punto de entrada mínimo.
   * Creación del archivo de especificación de uso `README.md`.
   * Pruebas de integración del menú en `AppMenuTests.cs` completando 37 tests unitarios en total con 100% de éxito.
   * Reemplazo de `.slnx` por la solución clásica `.sln` para garantizar compatibilidad universal con Visual Studio Community.
   * Commit y push final a `origin/dev` y sincronización con `origin/main`.

---

## 4. Ampliaciones Opcionales Realizadas: CPU Heurística

Como ampliación técnica avanzada, en lugar de una CPU que elige casillas de forma puramente aleatoria (Nivel 1), se implementó una **estrategia heurística multicapa (Nivel 2)** en `CpuPlayer.GetMove`:

### Jerarquía de Decisiones Algorítmicas:
1. **Ganancia Inmediata (Victoria):**
   * La CPU itera sobre las casillas libres y consulta `virtualBoard.WouldWin(pos, Symbol)`. Si colocar su ficha en alguna posición completa una fila, columna o diagonal, toma inmediatamente esa casilla para ganar la partida.
2. **Bloqueo Inmediato (Defensa):**
   * Si no puede ganar en ese turno, evalúa si el oponente ganaría en su siguiente turno mediante `virtualBoard.WouldWin(pos, opponentSymbol)`. Si detecta una amenaza inminente, ocupa dicha casilla para neutralizar la victoria rival.
3. **Control del Centro (Posición Estratégica):**
   * Si no hay victorias ni bloqueos urgentes, comprueba si la casilla central (índice `4`) está disponible. El centro es la posición geométrica con mayor valor estratégico al formar parte de 4 de las 8 líneas ganadoras.
4. **Dominio de Esquinas:**
   * Si el centro está tomado, prioriza aleatoriamente entre las 4 esquinas disponibles (`0, 2, 6, 8`), cada una de las cuales participa en 3 combinaciones ganadoras.
5. **Selección Residual de Bordes:**
   * En caso de no haber esquinas libres, selecciona una de las casillas laterales restantes (`1, 3, 5, 7`).

Esta heurística garantiza un adversario desafiante, coherente y tácticamente sólido.

---

## 5. Documentación del Uso de IA

Durante todo el ciclo de desarrollo se utilizó un modelo de inteligencia artificial como asistente de ingeniería de software en modalidad de programación en pareja (*pair programming*):

### Metodología Aplicada:
1. **Desarrollo Guiado por Especificaciones (Spec-Driven Development):**
   * Antes de generar código fuente, se analizaron en profundidad los contratos, invariantes y responsabilidades estipulados en `spec.md`.
   * Se consensuó previamente el modelo de datos (un vector 1D de tamaño 9 indexado de 0 a 8) y el tipado fuerte mediante el enum `CellSymbol`, exponiendo las ventajas de diseño antes de escribir una sola línea.
2. **Desarrollo Guiado por Pruebas (TDD) e Integración Continua:**
   * Cada componente creado vino acompañado de su correspondiente suite de pruebas unitarias con **xUnit** (`BoardTests`, `HumanPlayerTests`, `CpuPlayerTests`, `GameTests`, `AppMenuTests`), alcanzando 37 tests automatizados ejecutados localmente mediante el SDK de .NET.
3. **Inyección de Dependencias y Testabilidad:**
   * La IA diseñó `HumanPlayer`, `ConsoleUI` y `AppMenu` recibiendo abstracciones `TextReader` y `TextWriter`, lo que permitió someter las interacciones de consola a pruebas unitarias automatizadas sin bloquear la terminal.
4. **Control de Versiones y Flujo de Trabajo en Git:**
   * En cumplimiento con las directrices del usuario, cada hito se validó, compiló, testeó, confirmó con commit descriptivo y se subió primeramente a la rama remota `origin/dev`, procediendo posteriormente al merge rápido sobre `origin/main`.
5. **Refactorización y Limpieza de Código:**
   * Tras la validación de las primeras fases, se eliminaron los comentarios superfluos o redundantes para mantener una base de código limpia, autodocumentada y profesional.
