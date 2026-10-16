using System;

namespace PiedraPapelTijera
{
    /// <summary>
    /// Gestiona la partida de Piedra, Papel o Tijeras al mejor de 5 (gana el primero en llegar a 3).
    /// </summary>
    public class Partida
    {
        private Jugador usuario;
        private Jugador cpu;
        private const int VictoriasObjetivo = 3;

        public Partida()
        {
            usuario = new Jugador("Usuario", esCpu: false);
            cpu = new Jugador("CPU", esCpu: true);
        }

        /// <summary>
        /// Ejecuta el bucle de la partida hasta que alguien alcance las 3 victorias.
        /// Devuelve true si el usuario ha ganado la partida, false si ha ganado la CPU.
        /// </summary>
        public bool Jugar()
        {
            int numeroRonda = 1;

            while (usuario.Puntos < VictoriasObjetivo && cpu.Puntos < VictoriasObjetivo)
            {
                Console.Clear();
                MostrarEncabezado(numeroRonda);

                int jugadaUsuario = usuario.ElegirJugada();
                int jugadaCpu = cpu.ElegirJugada();

                Console.WriteLine("\n----------------------------------------");
                Console.WriteLine($"Tú elegiste:    {Jugador.ObtenerNombreJugada(jugadaUsuario)}");
                Console.WriteLine($"La CPU eligió:  {Jugador.ObtenerNombreJugada(jugadaCpu)}");
                Console.WriteLine("----------------------------------------");

                int resultadoRonda = DeterminarGanadorRonda(jugadaUsuario, jugadaCpu);

                if (resultadoRonda == 0)
                {
                    Console.WriteLine("¡Empate en esta ronda! Ninguno suma puntos.");
                }
                else if (resultadoRonda == 1)
                {
                    usuario.Puntos++;
                    Console.WriteLine($"¡Punto para {usuario.Nombre}!");
                }
                else
                {
                    cpu.Puntos++;
                    Console.WriteLine($"¡Punto para {cpu.Nombre}!");
                }

                MostrarMarcador();
                numeroRonda++;

                Console.WriteLine("\nPulsa cualquier tecla para pasar a la siguiente ronda...");
                Console.ReadKey();
            }

            return MostrarResultadoFinal();
        }

        /// <summary>
        /// Muestra el encabezado con la ronda actual y el objetivo.
        /// </summary>
        private void MostrarEncabezado(int ronda)
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"         RONDA {ronda} (Al mejor de 5)");
            Console.WriteLine($"  Primer jugador en llegar a {VictoriasObjetivo} victorias gana");
            Console.WriteLine("========================================");
            MostrarMarcador();
        }

        /// <summary>
        /// Muestra el marcador actual de la partida.
        /// </summary>
        private void MostrarMarcador()
        {
            Console.WriteLine($"\nMARCADOR ACTUAL -> {usuario.Nombre}: {usuario.Puntos} | {cpu.Nombre}: {cpu.Puntos}");
        }

        /// <summary>
        /// Evalúa la jugada: 1 = Piedra, 2 = Papel, 3 = Tijera.
        /// Devuelve 0 en caso de empate, 1 si gana el usuario, 2 si gana la CPU.
        /// </summary>
        private int DeterminarGanadorRonda(int jugadaUsuario, int jugadaCpu)
        {
            if (jugadaUsuario == jugadaCpu)
            {
                return 0; // Empate
            }

            // Piedra (1) vence a Tijera (3)
            // Papel (2) vence a Piedra (1)
            // Tijera (3) vence a Papel (2)
            if ((jugadaUsuario == 1 && jugadaCpu == 3) ||
                (jugadaUsuario == 2 && jugadaCpu == 1) ||
                (jugadaUsuario == 3 && jugadaCpu == 2))
            {
                return 1; // Gana Usuario
            }

            return 2; // Gana CPU
        }

        /// <summary>
        /// Muestra la pantalla final con el vencedor del juego.
        /// </summary>
        private bool MostrarResultadoFinal()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("           PARTIDA FINALIZADA           ");
            Console.WriteLine("========================================");
            Console.WriteLine($"Marcador final: {usuario.Nombre} {usuario.Puntos} - {cpu.Puntos} {cpu.Nombre}");

            bool ganoUsuario = usuario.Puntos >= VictoriasObjetivo;

            if (ganoUsuario)
            {
                Console.WriteLine("\n¡ENHORABUENA! ¡HAS GANADO LA PARTIDA!");
            }
            else
            {
                Console.WriteLine("\n¡HAS PERDIDO! LA CPU HA GANADO LA PARTIDA.");
            }

            Console.WriteLine("========================================");
            return ganoUsuario;
        }
    }
}
