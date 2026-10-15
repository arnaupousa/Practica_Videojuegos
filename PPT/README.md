# Piedra, Papel, Tijera, Lagarto, Spock (PPTLS)

**Módulo:** MP 0488 · M7 – Desenvolupament d'interfícies  
**Proyecto:** Práctica 2 · MVP i desenvolupament d'un videojoc en C#  
**Integrante:** Nil Rivera (Rama: `dev_nil.r`)

---

## 1. Descripción del Juego y Alcance (MVP)
Esta versión es una ampliación completa del clásico juego de consola **Piedra, Papel o Tijera**, incorporando las dos opciones canónicas adicionales: **Lagarto** y **Spock** (creadas originalmente por Sam Kass y popularizadas en la serie *The Big Bang Theory*).

### Reglas e Interacciones Canónicas
Cada una de las 5 opciones vence a 2 opciones y pierde contra las otras 2:
- ✂️ **Tijera** corta a 📄 **Papel**
- 📄 **Papel** cubre a 🪨 **Piedra**
- 🪨 **Piedra** aplasta a 🦎 **Lagarto**
- 🦎 **Lagarto** envenena a 🖖 **Spock**
- 🖖 **Spock** rompe a ✂️ **Tijera**
- ✂️ **Tijera** decapita a 🦎 **Lagarto**
- 🦎 **Lagarto** devora a 📄 **Papel**
- 📄 **Papel** desautoriza a 🖖 **Spock**
- 🖖 **Spock** vaporiza a 🪨 **Piedra**
- 🪨 **Piedra** aplasta a ✂️ **Tijera**
- Misma opción contra misma opción resulta en **Empate** (no suma puntos a ningún participante).

### Condición de Victoria
- La partida se juega al **mejor de 5 rondas** (el primer jugador en alcanzar **3 victorias** gana la partida).
- En caso de empate en una ronda, se informa al usuario y el marcador de victorias no se incrementa.
- Al terminar la partida, se muestra el resultado final con felicitación/derrota y se permite al usuario jugar otra partida directa o regresar al menú principal.

---

## 2. Arquitectura Orientada a Objetos (POO)

El proyecto cumple estrictamente con el requisito de no concentrar la lógica en `Program.cs` ni en un único método `Main`, separando responsabilidades en clases modulares:

| Clase / Elemento | Responsabilidad |
| :--- | :--- |
| `Program` | Punto de entrada mínimo (`Main`) que inicializa la aplicación y delega en el menú. |
| `MenuPrincipal` | Muestra el banner, el menú de opciones (Jugar, Reglas, Estadísticas, Salir) y gestiona la navegación. |
| `Partida` | Controla el flujo completo de una partida al mejor de 5, gestionando turnos, combates, marcador y revanchas. |
| `Jugador` | Clase base abstracta con propiedades comunes (`Nombre`, `Victorias`) y método abstracto `ElegirJugada()`. |
| `JugadorHumano` | Subclase de `Jugador` que interactúa con el usuario, solicitando y validando exhaustivamente sus elecciones. |
| `JugadorCpu` | Subclase de `Jugador` que genera elecciones pseudoaleatorias e independientes mediante `Random`. |
| `Marcador` | Registra victorias de usuario, de CPU, empates, ronda actual y evalúa la condición de finalización (3 victorias). |
| `ReglasJuego` | Clase estática con la tabla canónica de las 10 combinaciones y el evaluador de resultados y explicaciones textuales. |
| `ResultadoEvaluacion` | Objeto que encapsula el resultado (`VictoriaUsuario`, `VictoriaCpu`, `Empate`) y la frase explicativa de la acción. |
| `EstadisticasSesion` | **(Ampliación opcional +1 punto)** Mantiene estadísticas acumuladas durante la sesión (partidas totales, victorias, derrotas, rachas y jugadas más frecuentes). |
| `ConsolaHelper` | Proporciona métodos auxiliares seguros (`Limpiar`, `EstablecerTitulo`, `Pausar`) compatibles con cualquier entorno de ejecución. |
| `OpcionJugada` | Enumeración tipada (`Piedra = 1`, `Papel = 2`, `Tijera = 3`, `Lagarto = 4`, `Spock = 5`). |

---

## 3. Validación de Entradas y Control de Errores
- Todas las entradas de usuario en menús y durante el juego se analizan con `int.TryParse` y comprobaciones de cadenas.
- El usuario puede elegir jugada introduciendo el número `1` a `5` o el nombre literal (`piedra`, `papel`, `tijera`, `lagarto`, `spock`).
- Las entradas nulas, vacías o con texto no reconocido muestran un mensaje de error descriptivo en color rojo y vuelven a solicitar la opción sin romper ni cerrar la aplicación.
- Se capturan excepciones globales en `Program.cs` y en operaciones de consola para evitar cierres inesperados.

---

## 4. Compilación y Ejecución

### Opción A: Con Visual Studio o MSBuild
```powershell
MSBuild.exe PPT\PPT.csproj
```
El ejecutable se genera en `PPT\bin\Debug\PPT.exe`.

### Opción B: Con el compilador C# (Roslyn csc)
```powershell
csc -out:PPT.exe PPT\*.cs
.\PPT.exe
```

---

## 5. Documentación de Uso de IA (Requisito de Entrega)
- **Herramienta utilizada:** Asistente de IA en Antigravity IDE.
- **Tareas realizadas con apoyo de IA:**
  - Diseño de la estructura de clases orientada a objetos (POO) siguiendo los requisitos del enunciado.
  - Implementación de la tabla de reglas canónicas para las 10 combinaciones de RPSLS.
  - Creación del sistema de estadísticas de sesión con rachas acumuladas.
  - Revisión y pruebas de compilación para compatibilidad de consola y .NET.
- **Aportación y decisiones tomadas:**
  - Se estructuró el código en clases separadas según las buenas prácticas exigidas por el profesor Rubén Gasque.
  - Se validó el correcto funcionamiento en consola y la modularización en la rama de desarrollo `dev_nil.r`.
