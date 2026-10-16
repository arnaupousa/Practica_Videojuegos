# Piedra, Papel o Tijeras (C# - Consola)

Implementación orientada a objetos del juego clásico **Piedra, Papel o Tijeras** desarrollada para la práctica de Desarrollo de Interfaces (Curs 2026–2027) por **Nil Rivera**.

---

## 1. Requisitos Cumplidos (MVP y Especificaciones)

- **Usuario contra CPU:** El usuario compite contra la computadora.
- **Elección aleatoria de la CPU:** La CPU genera su jugada de forma impredecible entre Piedra, Papel o Tijera.
- **Sistema al mejor de 5 rondas:** La partida finaliza automáticamente en cuanto un jugador alcanza **3 victorias**.
- **Validación de entradas:** No se permite que el programa falle por introducir texto vacío, letras o números fuera de rango (1, 2 o 3).
- **Marcador acumulado y estado visible:** En cada ronda se muestra claramente el número de ronda, la jugada de cada participante y el marcador actualizado.
- **Flujo de juego:** Inicio con menú principal -> Partida -> Resultado final -> Opción de jugar de nuevo o regresar al menú.
- **Ampliación funcional:** Sistema de **estadísticas acumuladas de sesión** y registro de **rachas de victorias consecutivas** (actual y récord máximo alcanzado).

---

## 2. Estructura de Clases (POO)

El proyecto respeta escrupulosamente el principio de responsabilidad única dividiendo la lógica en 4 clases sencillas y legibles:

| Clase | Archivo | Responsabilidad Principal |
| :--- | :--- | :--- |
| **`Program`** | `Program.cs` | Punto de entrada (`Main`). Muestra el menú principal, coordina el inicio de partidas, consulta de reglas y salida de la aplicación. |
| **`Partida`** | `Partida.cs` | Gestiona el bucle de rondas al mejor de 5, evalúa quién gana cada ronda según las reglas y determina al ganador final de la partida. |
| **`Jugador`** | `Jugador.cs` | Representa a cada participante (usuario o CPU). Contiene el método para obtener la jugada (validada por teclado para humanos, aleatoria para la CPU) y traduce las opciones a texto. |
| **`Estadisticas`** | `Estadisticas.cs` | *(Ampliación funcional)* Registra partidas jugadas, victorias del usuario, victorias de la máquina y controla la racha actual y récord de victorias. |

---

## 3. Reglas y Lógica de Evaluación

- **Piedra (1)** vence a **Tijera (3)**.
- **Papel (2)** vence a **Piedra (1)**.
- **Tijera (3)** vence a **Papel (2)**.
- Si ambos eligen la misma opción, la ronda termina en **Empate** y ninguno suma puntos.
- El primero en sumar **3 puntos** gana la partida.

---

## 4. Cómo Compilar y Ejecutar

Tienes tres formas muy sencillas de arrancarlo:

1. **Doble clic (Recomendada):** Haz doble clic sobre el archivo `iniciar.bat` dentro de la carpeta `PiedraPapelTijera`.
2. **Desde la terminal:**
   ```bash
   cd PiedraPapelTijera
   .\iniciar.bat
   ```
   *(O si tienes el SDK de .NET instalado globalmente: `dotnet run`)*
3. **Desde Visual Studio:** Abre el archivo `PiedraPapelTijera.csproj` con Visual Studio y pulsa **Ctrl + F5** (Iniciar sin depurar).


---

## 5. Documentación de Uso de IA (Para la Entrega y Defensa)

- **Herramienta utilizada:** Google Antigravity / Gemini.
- **Tareas asistidas:** Planificación de la arquitectura modular en C#, diseño sencillo orientado a objetos acorde a los requisitos del enunciado y redacción de la documentación técnica.
- **Comprensión individual:** Nil Rivera comprende íntegramente cada método, estructura de datos y flujo de ejecución para la defensa individual ante el profesor.
