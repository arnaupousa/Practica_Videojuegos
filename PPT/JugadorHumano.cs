using System;

namespace PPT
{
    /// <summary>
    /// Representa al jugador humano controlado por el usuario desde la consola.
    /// Valida exhaustivamente las entradas para garantizar la robustez del programa.
    /// </summary>
    public class JugadorHumano : Jugador
    {
        public JugadorHumano(string nombre = "Jugador") : base(nombre)
        {
        }

        public override OpcionJugada ElegirJugada()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n--- Elige tu jugada ---");
                Console.ResetColor();
                Console.WriteLine(" [1] 🪨  Piedra");
                Console.WriteLine(" [2] 📄  Papel");
                Console.WriteLine(" [3] ✂️   Tijera");
                Console.WriteLine(" [4] 🦎  Lagarto");
                Console.WriteLine(" [5] 🖖  Spock");
                Console.Write("\nIntroduce tu opción (1-5 o nombre): ");

                string entrada = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(entrada))
                {
                    MostrarError("Entrada vacía. Por favor, introduce un número del 1 al 5 o el nombre de la jugada.");
                    continue;
                }

                // Validación por número
                if (int.TryParse(entrada, out int opcionNumero) && opcionNumero >= 1 && opcionNumero <= 5)
                {
                    return (OpcionJugada)opcionNumero;
                }

                // Validación por nombre textual
                switch (entrada)
                {
                    case "piedra":
                    case "1":
                        return OpcionJugada.Piedra;
                    case "papel":
                    case "2":
                        return OpcionJugada.Papel;
                    case "tijera":
                    case "tijeras":
                    case "3":
                        return OpcionJugada.Tijera;
                    case "lagarto":
                    case "4":
                        return OpcionJugada.Lagarto;
                    case "spock":
                    case "spok":
                    case "5":
                        return OpcionJugada.Spock;
                    default:
                        MostrarError($"'{entrada}' no es una opción válida. Debe ser 1-5 o Piedra/Papel/Tijera/Lagarto/Spock.");
                        break;
                }
            }
        }

        private void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Error] {mensaje}");
            Console.ResetColor();
        }
    }
}
