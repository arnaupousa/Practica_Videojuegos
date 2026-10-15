using System;
using System.Threading;

namespace PPT
{
    /// <summary>
    /// Gestiona el desarrollo completo de una partida entre el Jugador y la CPU.
    /// Modalidad: Al mejor de 5 rondas (primero en alcanzar 3 victorias).
    /// </summary>
    public class Partida
    {
        private readonly JugadorHumano _humano;
        private readonly JugadorCpu _cpu;
        private readonly Marcador _marcador;
        private readonly EstadisticasSesion _estadisticas;

        public Partida(JugadorHumano humano, JugadorCpu cpu, EstadisticasSesion estadisticas, int victoriasRequeridas = 3)
        {
            _humano = humano;
            _cpu = cpu;
            _estadisticas = estadisticas;
            _marcador = new Marcador(victoriasRequeridas);
        }

        /// <summary>
        /// Inicia y controla el bucle principal de la partida.
        /// </summary>
        public void Jugar()
        {
            _marcador.Reiniciar();
            _humano.ReiniciarVictorias();
            _cpu.ReiniciarVictorias();

            while (!_marcador.HayGanador())
            {
                Console.Clear();
                MostrarCabeceraRonda();
                _marcador.MostrarMarcador(_humano.Nombre, _cpu.Nombre);

                // 1. Obtener jugada del usuario con validación
                OpcionJugada jugadaUsuario = _humano.ElegirJugada();
                _estadisticas.RegistrarJugadaUsuario(jugadaUsuario);

                // 2. Obtener jugada aleatoria de la CPU
                OpcionJugada jugadaCpu = _cpu.ElegirJugada();

                // 3. Presentación de jugadas con efectos visuales
                MostrarEnfrentamiento(jugadaUsuario, jugadaCpu);

                // 4. Evaluar según las reglas canónicas
                var (resultado, explicacion) = ReglasJuego.EvaluarRonda(jugadaUsuario, jugadaCpu);

                // 5. Aplicar puntuación y mostrar mensaje
                ProcesarResultadoRonda(resultado, explicacion);

                // 6. Pausa informativa antes de continuar si no ha terminado la partida
                if (!_marcador.HayGanador())
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar a la siguiente ronda...");
                    Console.ReadKey(true);
                }
            }

            // Fin de la partida
            FinalizarPartida();
        }

        private void MostrarCabeceraRonda()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("======================================================");
            Console.WriteLine($"           RONDA {_marcador.RondaActual}  (Primero a {_marcador.PuntosParaGanar} victorias)");
            Console.WriteLine("======================================================");
            Console.ResetColor();
        }

        private void MostrarEnfrentamiento(OpcionJugada usuario, OpcionJugada cpu)
        {
            Console.WriteLine("\n[Combate]");
            Thread.Sleep(300);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($" {_humano.Nombre} lanza: {usuario}");
            Console.ResetColor();

            Console.Write("  VS  ");

            Thread.Sleep(300);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{_cpu.Nombre} lanza: {cpu}");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void ProcesarResultadoRonda(ResultadoRonda resultado, string explicacion)
        {
            switch (resultado)
            {
                case ResultadoRonda.VictoriaUsuario:
                    _marcador.RegistrarVictoriaUsuario();
                    _humano.SumarVictoria();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($" >>> ¡PUNTO PARA {_humano.Nombre.ToUpper()}! <<<");
                    Console.WriteLine($" {explicacion}");
                    Console.ResetColor();
                    break;

                case ResultadoRonda.VictoriaCpu:
                    _marcador.RegistrarVictoriaCpu();
                    _cpu.SumarVictoria();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" >>> ¡PUNTO PARA {_cpu.Nombre.ToUpper()}! <<<");
                    Console.WriteLine($" {explicacion}");
                    Console.ResetColor();
                    break;

                case ResultadoRonda.Empate:
                    _marcador.RegistrarEmpate();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" >>> ¡EMPATE! <<<");
                    Console.WriteLine($" {explicacion}");
                    Console.ResetColor();
                    break;
            }
        }

        private void FinalizarPartida()
        {
            Console.Clear();
            bool usuarioGano = _marcador.PuntosUsuario >= _marcador.PuntosParaGanar;
            _estadisticas.RegistrarFinPartida(usuarioGano);

            Console.WriteLine();
            if (usuarioGano)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("******************************************************");
                Console.WriteLine($"        ¡ENHORABUENA! ¡HAS GANADO LA PARTIDA!        ");
                Console.WriteLine($"           {_humano.Nombre} [{_marcador.PuntosUsuario}] - [{_marcador.PuntosCpu}] {_cpu.Nombre}            ");
                Console.WriteLine("******************************************************");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("******************************************************");
                Console.WriteLine($"       ¡DERROTA! LA CPU HA GANADO LA PARTIDA         ");
                Console.WriteLine($"           {_cpu.Nombre} [{_marcador.PuntosCpu}] - [{_marcador.PuntosUsuario}] {_humano.Nombre}            ");
                Console.WriteLine("******************************************************");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nRondas totales jugadas en la partida: {_marcador.RondaActual - 1}");
            Console.WriteLine($"Empates registrados: {_marcador.Empates}");
            Console.ResetColor();

            PreguntarRevancha();
        }

        private void PreguntarRevancha()
        {
            while (true)
            {
                Console.Write("\n¿Deseas jugar otra partida inmediata? (S/N): ");
                string respuesta = Console.ReadLine()?.Trim().ToUpper();

                if (respuesta == "S" || respuesta == "SI" || respuesta == "SÍ")
                {
                    Jugar();
                    break;
                }
                else if (respuesta == "N" || respuesta == "NO")
                {
                    Console.WriteLine("\nRegresando al menú principal...");
                    Thread.Sleep(800);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Respuesta no reconocida. Escribe 'S' para sí o 'N' para no.");
                    Console.ResetColor();
                }
            }
        }
    }
}
