using System;

namespace PPT
{
    /// <summary>
    /// Gestiona la puntuación y el progreso de rondas en una partida.
    /// Implementa la regla oficial: primero en llegar a 3 victorias (al mejor de 5).
    /// </summary>
    public class Marcador
    {
        public int PuntosUsuario { get; private set; }
        public int PuntosCpu { get; private set; }
        public int Empates { get; private set; }
        public int RondaActual { get; private set; }
        public int PuntosParaGanar { get; private set; }

        public Marcador(int puntosParaGanar = 3)
        {
            PuntosParaGanar = puntosParaGanar;
            Reiniciar();
        }

        public void RegistrarVictoriaUsuario()
        {
            PuntosUsuario++;
            RondaActual++;
        }

        public void RegistrarVictoriaCpu()
        {
            PuntosCpu++;
            RondaActual++;
        }

        public void RegistrarEmpate()
        {
            Empates++;
            RondaActual++;
        }

        public bool HayGanador()
        {
            return PuntosUsuario >= PuntosParaGanar || PuntosCpu >= PuntosParaGanar;
        }

        public string ObtenerNombreGanador(string nombreUsuario, string nombreCpu)
        {
            if (PuntosUsuario >= PuntosParaGanar) return nombreUsuario;
            if (PuntosCpu >= PuntosParaGanar) return nombreCpu;
            return "Nadie";
        }

        public void Reiniciar()
        {
            PuntosUsuario = 0;
            PuntosCpu = 0;
            Empates = 0;
            RondaActual = 1;
        }

        /// <summary>
        /// Muestra el marcador actual en consola con colores representativos.
        /// </summary>
        public void MostrarMarcador(string nombreUsuario, string nombreCpu)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("+------------------------------------------------------+");
            Console.ResetColor();

            Console.Write("| ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{nombreUsuario}: {PuntosUsuario}".PadRight(20));
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Empates: {Empates}".PadRight(12));
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{nombreCpu}: {PuntosCpu}".PadRight(14));
            Console.ResetColor();

            Console.WriteLine(" |");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("+------------------------------------------------------+");
            Console.ResetColor();
        }
    }
}
