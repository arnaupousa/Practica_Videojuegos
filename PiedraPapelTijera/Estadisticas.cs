using System;

namespace PiedraPapelTijera
{
    /// <summary>
    /// Ampliación funcional: Gestiona las estadísticas de la sesión y las rachas de victorias.
    /// </summary>
    public class Estadisticas
    {
        public int PartidasJugadas { get; private set; }
        public int VictoriasUsuario { get; private set; }
        public int VictoriasCpu { get; private set; }
        public int RachaActual { get; private set; }
        public int RachaMaxima { get; private set; }

        public Estadisticas()
        {
            PartidasJugadas = 0;
            VictoriasUsuario = 0;
            VictoriasCpu = 0;
            RachaActual = 0;
            RachaMaxima = 0;
        }

        /// <summary>
        /// Registra el resultado de una partida finalizada.
        /// </summary>
        public void RegistrarResultado(bool ganoUsuario)
        {
            PartidasJugadas++;

            if (ganoUsuario)
            {
                VictoriasUsuario++;
                RachaActual++;
                if (RachaActual > RachaMaxima)
                {
                    RachaMaxima = RachaActual;
                }
            }
            else
            {
                VictoriasCpu++;
                RachaActual = 0;
            }
        }

        /// <summary>
        /// Muestra el resumen de estadísticas acumuladas por pantalla.
        /// </summary>
        public void Mostrar()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("        ESTADÍSTICAS Y RACHAS           ");
            Console.WriteLine("========================================");
            Console.WriteLine($"Partidas totales jugadas: {PartidasJugadas}");
            Console.WriteLine($"Victorias del usuario:    {VictoriasUsuario}");
            Console.WriteLine($"Victorias de la CPU:      {VictoriasCpu}");
            Console.WriteLine($"Racha actual de victorias:{RachaActual}");
            Console.WriteLine($"Racha máxima conseguida:  {RachaMaxima}");
            Console.WriteLine("========================================");
            Console.WriteLine("\nPulsa cualquier tecla para volver al menú...");
            Console.ReadKey();
        }
    }
}
