using System;

namespace PiedraPapelTijera
{
    /// <summary>
    /// Representa un participante de la partida (humano o CPU).
    /// </summary>
    public class Jugador
    {
        public string Nombre { get; set; }
        public int Puntos { get; set; }
        public bool EsCpu { get; set; }

        private static Random random = new Random();

        public Jugador(string nombre, bool esCpu)
        {
            Nombre = nombre;
            EsCpu = esCpu;
            Puntos = 0;
        }

        /// <summary>
        /// Obtiene la jugada elegida por el jugador:
        /// 1 = Piedra, 2 = Papel, 3 = Tijera
        /// </summary>
        public int ElegirJugada()
        {
            if (EsCpu)
            {
                // Elección aleatoria de la CPU (1, 2 o 3)
                return random.Next(1, 4);
            }

            // Elección del usuario humano con validación de entradas
            while (true)
            {
                Console.WriteLine("\nElige tu jugada:");
                Console.WriteLine("1. Piedra");
                Console.WriteLine("2. Papel");
                Console.WriteLine("3. Tijera");
                Console.Write("Introduce tu opción (1-3): ");

                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out int opcion) && opcion >= 1 && opcion <= 3)
                {
                    return opcion;
                }

                Console.WriteLine("[Error] Opción no válida. Por favor, introduce 1, 2 o 3.");
            }
        }

        /// <summary>
        /// Convierte el número de jugada en texto legible.
        /// </summary>
        public static string ObtenerNombreJugada(int jugada)
        {
            return jugada switch
            {
                1 => "Piedra",
                2 => "Papel",
                3 => "Tijera",
                _ => "Desconocido"
            };
        }
    }
}
