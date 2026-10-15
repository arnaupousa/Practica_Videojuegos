using System;
using System.Collections.Generic;

namespace PPT
{
    /// <summary>
    /// Ampliación funcional: Registro de estadísticas y rachas acumuladas durante la sesión.
    /// Permite persistencia de datos durante la ejecución del programa.
    /// </summary>
    public class EstadisticasSesion
    {
        public int PartidasTotales { get; private set; }
        public int VictoriasUsuario { get; private set; }
        public int VictoriasCpu { get; private set; }
        public int RachaActual { get; private set; }
        public int MejorRacha { get; private set; }
        public int RondasTotales { get; private set; }

        private readonly Dictionary<OpcionJugada, int> _conteoJugadasUsuario = new Dictionary<OpcionJugada, int>
        {
            { OpcionJugada.Piedra, 0 },
            { OpcionJugada.Papel, 0 },
            { OpcionJugada.Tijera, 0 },
            { OpcionJugada.Lagarto, 0 },
            { OpcionJugada.Spock, 0 }
        };

        public void RegistrarJugadaUsuario(OpcionJugada jugada)
        {
            _conteoJugadasUsuario[jugada]++;
            RondasTotales++;
        }

        public void RegistrarFinPartida(bool usuarioGano)
        {
            PartidasTotales++;
            if (usuarioGano)
            {
                VictoriasUsuario++;
                RachaActual++;
                if (RachaActual > MejorRacha)
                {
                    MejorRacha = RachaActual;
                }
            }
            else
            {
                VictoriasCpu++;
                RachaActual = 0;
            }
        }

        public void MostrarEstadisticas()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================================================");
            Console.WriteLine("            ESTADÍSTICAS GLOBALES DE SESIÓN           ");
            Console.WriteLine("======================================================");
            Console.ResetColor();

            Console.WriteLine($" Partidas jugadas : {PartidasTotales}");
            Console.WriteLine($" Victorias Jugador: {VictoriasUsuario} ({(PartidasTotales > 0 ? (VictoriasUsuario * 100.0 / PartidasTotales).ToString("0.0") : "0")}%)");
            Console.WriteLine($" Victorias CPU    : {VictoriasCpu} ({(PartidasTotales > 0 ? (VictoriasCpu * 100.0 / PartidasTotales).ToString("0.0") : "0")}%)");
            Console.WriteLine($" Racha actual     : {RachaActual} partida(s)");
            Console.WriteLine($" Mejor racha      : {MejorRacha} partida(s)");
            Console.WriteLine($" Rondas totales   : {RondasTotales}");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- Uso de jugadas por el jugador ---");
            Console.ResetColor();
            foreach (var item in _conteoJugadasUsuario)
            {
                int porcentaje = RondasTotales > 0 ? (item.Value * 100 / RondasTotales) : 0;
                Console.WriteLine($" • {item.Key.ToString().PadRight(8)}: {item.Value} veces ({porcentaje}%)");
            }

            Console.WriteLine("\nPresiona cualquier tecla para volver al menú...");
            Console.ReadKey(true);
        }
    }
}
