using System;
using System.Threading;

namespace PPT
{
    /// <summary>
    /// Representa al oponente controlado por la computadora.
    /// Selecciona sus jugadas de manera pseudoaleatoria e impredecible.
    /// </summary>
    public class JugadorCpu : Jugador
    {
        private readonly Random _random;

        public JugadorCpu(string nombre = "CPU (Sheldon)") : base(nombre)
        {
            _random = new Random();
        }

        public override OpcionJugada ElegirJugada()
        {
            // Opciones entre 1 y 5 (Piedra a Spock)
            int valor = _random.Next(1, 6);
            return (OpcionJugada)valor;
        }
    }
}
