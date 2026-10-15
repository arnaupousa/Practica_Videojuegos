using System;

namespace PPT
{
    /// <summary>
    /// Gestiona la visualización del menú principal, la navegación y las pantallas informativas.
    /// Separa la presentación de la lógica del juego.
    /// </summary>
    public class MenuPrincipal
    {
        private readonly JugadorHumano _humano;
        private readonly JugadorCpu _cpu;
        private readonly EstadisticasSesion _estadisticas;

        public MenuPrincipal()
        {
            _humano = new JugadorHumano("Jugador");
            _cpu = new JugadorCpu("CPU Sheldon");
            _estadisticas = new EstadisticasSesion();
        }

        public void Iniciar()
        {
            ConsolaHelper.EstablecerTitulo("Piedra, Papel, Tijera, Lagarto, Spock - C# Console");
            bool salir = false;

            while (!salir)
            {
                MostrarBanner();
                MostrarOpciones();

                Console.Write("\nSelecciona una opción (1-4): ");
                string opcion = Console.ReadLine()?.Trim();

                if (opcion == null)
                {
                    // Si el stream de entrada finaliza (ej. EOF en piping), salimos limpiamente
                    break;
                }

                switch (opcion)
                {
                    case "1":
                        // Iniciar nueva partida
                        Partida partida = new Partida(_humano, _cpu, _estadisticas);
                        partida.Jugar();
                        break;

                    case "2":
                        // Ver reglas del juego
                        MostrarPantallaReglas();
                        break;

                    case "3":
                        // Ver estadísticas
                        _estadisticas.MostrarEstadisticas();
                        break;

                    case "4":
                        // Salir
                        salir = true;
                        MostrarDespedida();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[Error] Opción no válida. Debes introducir un número del 1 al 4.");
                        Console.ResetColor();
                        ConsolaHelper.Pausar("Presiona una tecla para continuar...");
                        break;
                }
            }
        }

        private void MostrarBanner()
        {
            ConsolaHelper.Limpiar();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"  ========================================================");
            Console.WriteLine(@"     PIEDRA - PAPEL - TIJERA - LAGARTO - SPOCK (PPTLS)   ");
            Console.WriteLine(@"  ========================================================");
            Console.ResetColor();
            Console.WriteLine("  Versión extendida con reglas canónicas y estadísticas.");
            Console.WriteLine();
        }

        private void MostrarOpciones()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  [1] Jugar partida (Al mejor de 5 rondas)");
            Console.WriteLine("  [2] Consultar reglas e interacciones");
            Console.WriteLine("  [3] Ver estadísticas de la sesión");
            Console.WriteLine("  [4] Salir");
            Console.ResetColor();
        }

        private void MostrarPantallaReglas()
        {
            ConsolaHelper.Limpiar();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================================");
            Console.WriteLine("             REGLAS CANÓNICAS DEL JUEGO                 ");
            Console.WriteLine("========================================================");
            Console.ResetColor();

            Console.WriteLine("\nEl juego es una ampliación del clásico Piedra, Papel o Tijera.");
            Console.WriteLine("Cada opción vence exactamente a otras 2 y pierde contra 2:\n");

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var regla in ReglasJuego.ObtenerListaReglas())
            {
                Console.WriteLine($"  {regla}");
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nCondición de victoria de la partida:");
            Console.WriteLine("• Se juega al mejor de 5 rondas.");
            Console.WriteLine("• El primero en conseguir 3 victorias gana la partida.");
            Console.WriteLine("• Las rondas empatadas no otorgan puntos a ningún jugador.");
            Console.ResetColor();

            ConsolaHelper.Pausar("\nPresiona cualquier tecla para volver al menú principal...");
        }

        private void MostrarDespedida()
        {
            ConsolaHelper.Limpiar();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("========================================================");
            Console.WriteLine("   ¡Gracias por jugar a Piedra, Papel, Tijera, Spock!   ");
            Console.WriteLine("                    ¡Hasta la próxima!                  ");
            Console.WriteLine("========================================================");
            Console.ResetColor();
        }
    }
}
