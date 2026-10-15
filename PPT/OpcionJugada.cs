namespace PPT
{
    /// <summary>
    /// Opciones disponibles en el juego Piedra, Papel, Tijera, Lagarto, Spock.
    /// </summary>
    public enum OpcionJugada
    {
        Piedra = 1,
        Papel = 2,
        Tijera = 3,
        Lagarto = 4,
        Spock = 5
    }

    /// <summary>
    /// Posibles resultados de una ronda individual.
    /// </summary>
    public enum ResultadoRonda
    {
        VictoriaUsuario,
        VictoriaCpu,
        Empate
    }
}
