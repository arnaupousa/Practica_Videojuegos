namespace PPT
{
    /// <summary>
    /// Clase base abstracta que representa a un participante en el juego.
    /// Aplica principios de POO (abstracción y polimorfismo).
    /// </summary>
    public abstract class Jugador
    {
        public string Nombre { get; protected set; }
        public int Victorias { get; private set; }

        protected Jugador(string nombre)
        {
            Nombre = nombre;
            Victorias = 0;
        }

        /// <summary>
        /// Método polimórfico para obtener la jugada del participante.
        /// </summary>
        public abstract OpcionJugada ElegirJugada();

        /// <summary>
        /// Incrementa en uno el número de victorias del jugador en la partida actual.
        /// </summary>
        public void SumarVictoria()
        {
            Victorias++;
        }

        /// <summary>
        /// Reinicia el contador de victorias para una nueva partida.
        /// </summary>
        public void ReiniciarVictorias()
        {
            Victorias = 0;
        }
    }
}
