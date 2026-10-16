using System;

namespace PiedraPapelTijera
{
    /// <summary>
    /// Punto de entrada del programa. Coordina el menú principal y el flujo general de la aplicación.
    /// </summary>
    public class Program
    {
        private static Estadisticas estadisticas = new Estadisticas();

        public static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("      PIEDRA, PAPEL O TIJERAS           ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Jugar partida (Al mejor de 5)");
                Console.WriteLine("2. Ver estadísticas y rachas");
                Console.WriteLine("3. Ver reglas del juego");
                Console.WriteLine("4. Salir");
                Console.WriteLine("========================================");
                Console.Write("Selecciona una opción (1-4): ");

                string opcion = Console.ReadLine()?.Trim();

                switch (opcion)
                {
                    case "1":
                        IniciarBuclePartidas();
                        break;
                    case "2":
                        estadisticas.Mostrar();
                        break;
                    case "3":
                        MostrarReglas();
                        break;
                    case "4":
                        salir = true;
                        Console.WriteLine("\n¡Gracias por jugar! Hasta pronto.");
                        break;
                    default:
                        Console.WriteLine("\n[Error] Opción no válida. Introduce un número del 1 al 4.");
                        Console.WriteLine("Pulsa cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Inicia una partida y permite jugar varias seguidas o regresar al menú principal.
        /// </summary>
        private static void IniciarBuclePartidas()
        {
            bool jugarOtra = true;

            while (jugarOtra)
            {
                Partida partida = new Partida();
                bool ganoUsuario = partida.Jugar();
                estadisticas.RegistrarResultado(ganoUsuario);

                Console.Write("\n¿Quieres jugar otra partida inmediatamente? (S/N): ");
                string respuesta = Console.ReadLine()?.Trim().ToUpper();

                if (respuesta != "S")
                {
                    jugarOtra = false;
                }
            }
        }

        /// <summary>
        /// Muestra las reglas básicas del juego y las condiciones de victoria.
        /// </summary>
        private static void MostrarReglas()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("            REGLAS DEL JUEGO            ");
            Console.WriteLine("========================================");
            Console.WriteLine("• Piedra vence a Tijera (la rompe).");
            Console.WriteLine("• Papel vence a Piedra (la envuelve).");
            Console.WriteLine("• Tijera vence a Papel (lo corta).");
            Console.WriteLine("• Si ambos eligen la misma opción, es empate.");
            Console.WriteLine("\nFormato de la partida:");
            Console.WriteLine("• Se juega al mejor de 5 rondas.");
            Console.WriteLine("• El primer jugador en acumular 3 victorias gana la partida.");
            Console.WriteLine("========================================");
            Console.WriteLine("\nPulsa cualquier tecla para volver al menú...");
            Console.ReadKey();
        }
    }
}
