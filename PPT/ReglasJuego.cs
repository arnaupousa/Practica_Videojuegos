using System;
using System.Collections.Generic;

namespace PPT
{
    /// <summary>
    /// Maneja la lógica de enfrentamientos y reglas de Piedra, Papel, Tijera, Lagarto, Spock.
    /// </summary>
    public static class ReglasJuego
    {
        // Clave: (JugadaAtacante, JugadaDefensora) -> Verbo de acción
        private static readonly Dictionary<(OpcionJugada, OpcionJugada), string> Interacciones = 
            new Dictionary<(OpcionJugada, OpcionJugada), string>
            {
                { (OpcionJugada.Tijera, OpcionJugada.Papel), "corta" },
                { (OpcionJugada.Papel, OpcionJugada.Piedra), "cubre" },
                { (OpcionJugada.Piedra, OpcionJugada.Lagarto), "aplasta" },
                { (OpcionJugada.Lagarto, OpcionJugada.Spock), "envenena" },
                { (OpcionJugada.Spock, OpcionJugada.Tijera), "rompe" },
                { (OpcionJugada.Tijera, OpcionJugada.Lagarto), "decapita" },
                { (OpcionJugada.Lagarto, OpcionJugada.Papel), "devora" },
                { (OpcionJugada.Papel, OpcionJugada.Spock), "desautoriza" },
                { (OpcionJugada.Spock, OpcionJugada.Piedra), "vaporiza" },
                { (OpcionJugada.Piedra, OpcionJugada.Tijera), "aplasta" }
            };

        /// <summary>
        /// Compara las elecciones de dos jugadores y determina el resultado de la ronda con su explicación.
        /// </summary>
        public static (ResultadoRonda resultado, string explicacion) EvaluarRonda(OpcionJugada eleccionUsuario, OpcionJugada eleccionCpu)
        {
            if (eleccionUsuario == eleccionCpu)
            {
                return (ResultadoRonda.Empate, $"¡Ambos han elegido {eleccionUsuario}! Es un empate.");
            }

            if (Interacciones.TryGetValue((eleccionUsuario, eleccionCpu), out string accionUsuario))
            {
                string explicacion = $"{eleccionUsuario} {accionUsuario} a {eleccionCpu}.";
                return (ResultadoRonda.VictoriaUsuario, explicacion);
            }

            if (Interacciones.TryGetValue((eleccionCpu, eleccionUsuario), out string accionCpu))
            {
                string explicacion = $"{eleccionCpu} {accionCpu} a {eleccionUsuario}.";
                return (ResultadoRonda.VictoriaCpu, explicacion);
            }

            return (ResultadoRonda.Empate, "Ronda no concluyente.");
        }

        /// <summary>
        /// Devuelve un texto formateado con todas las reglas canónicas del juego.
        /// </summary>
        public static List<string> ObtenerListaReglas()
        {
            var reglas = new List<string>();
            foreach (var kvp in Interacciones)
            {
                reglas.Add($"• {kvp.Key.Item1} {kvp.Value} a {kvp.Key.Item2}");
            }
            return reglas;
        }
    }
}
