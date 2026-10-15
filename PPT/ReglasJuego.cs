using System;
using System.Collections.Generic;

namespace PPT
{
    /// <summary>
    /// Maneja la lógica de enfrentamientos y reglas de Piedra, Papel, Tijera, Lagarto, Spock.
    /// Basado en las 10 combinaciones canónicas creadas por Sam Kass y popularizadas en The Big Bang Theory.
    /// </summary>
    public static class ReglasJuego
    {
        // Clave: "Atacante->Defensor" -> Verbo de acción
        private static readonly Dictionary<string, string> Interacciones = 
            new Dictionary<string, string>
            {
                { FormarClave(OpcionJugada.Tijera, OpcionJugada.Papel), "corta" },
                { FormarClave(OpcionJugada.Papel, OpcionJugada.Piedra), "cubre" },
                { FormarClave(OpcionJugada.Piedra, OpcionJugada.Lagarto), "aplasta" },
                { FormarClave(OpcionJugada.Lagarto, OpcionJugada.Spock), "envenena" },
                { FormarClave(OpcionJugada.Spock, OpcionJugada.Tijera), "rompe" },
                { FormarClave(OpcionJugada.Tijera, OpcionJugada.Lagarto), "decapita" },
                { FormarClave(OpcionJugada.Lagarto, OpcionJugada.Papel), "devora" },
                { FormarClave(OpcionJugada.Papel, OpcionJugada.Spock), "desautoriza" },
                { FormarClave(OpcionJugada.Spock, OpcionJugada.Piedra), "vaporiza" },
                { FormarClave(OpcionJugada.Piedra, OpcionJugada.Tijera), "aplasta" }
            };

        private static string FormarClave(OpcionJugada atacante, OpcionJugada defensor)
        {
            return atacante.ToString() + "->" + defensor.ToString();
        }

        /// <summary>
        /// Compara las elecciones de dos jugadores y determina el resultado de la ronda con su explicación.
        /// </summary>
        public static ResultadoEvaluacion EvaluarRonda(OpcionJugada eleccionUsuario, OpcionJugada eleccionCpu)
        {
            if (eleccionUsuario == eleccionCpu)
            {
                return new ResultadoEvaluacion(ResultadoRonda.Empate, $"¡Ambos han elegido {eleccionUsuario}! Es un empate.");
            }

            string claveUsuario = FormarClave(eleccionUsuario, eleccionCpu);
            if (Interacciones.TryGetValue(claveUsuario, out string accionUsuario))
            {
                string explicacion = $"{eleccionUsuario} {accionUsuario} a {eleccionCpu}.";
                return new ResultadoEvaluacion(ResultadoRonda.VictoriaUsuario, explicacion);
            }

            string claveCpu = FormarClave(eleccionCpu, eleccionUsuario);
            if (Interacciones.TryGetValue(claveCpu, out string accionCpu))
            {
                string explicacion = $"{eleccionCpu} {accionCpu} a {eleccionUsuario}.";
                return new ResultadoEvaluacion(ResultadoRonda.VictoriaCpu, explicacion);
            }

            return new ResultadoEvaluacion(ResultadoRonda.Empate, "Ronda no concluyente.");
        }

        /// <summary>
        /// Devuelve un listado de cadenas con todas las reglas canónicas del juego.
        /// </summary>
        public static List<string> ObtenerListaReglas()
        {
            var reglas = new List<string>
            {
                "• Tijera corta a Papel",
                "• Papel cubre a Piedra",
                "• Piedra aplasta a Lagarto",
                "• Lagarto envenena a Spock",
                "• Spock rompe a Tijera",
                "• Tijera decapita a Lagarto",
                "• Lagarto devora a Papel",
                "• Papel desautoriza a Spock",
                "• Spock vaporiza a Piedra",
                "• Piedra aplasta a Tijera"
            };
            return reglas;
        }
    }
}
