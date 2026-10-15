
/// Lógica completa del Ahorcado.
public class Partida
{
    // Lista de 10 palabras
    private List<string> palabras = new List<string>
        {
            "PROGRAMACION", "DESARROLLO", "CONSOLA", "CLASE", "OBJETO",
            "METODO", "VARIABLE", "REPOSITORIO", "GITHUB", "ORDENADOR"
        };

    private int intentosMaximos = 6;

    public void Jugar()
    {
        Random rnd = new Random();
        string palabraSecreta = palabras[rnd.Next(palabras.Count)];
        char[] progreso = new char[palabraSecreta.Length];
        List<char> letrasUsadas = new List<char>();

        // Inicializar el progreso
        for (int i = 0; i < progreso.Length; i++)
        {
            progreso[i] = '_';
        }

        int intentosRestantes = intentosMaximos;
        bool ganado = false;

        while (intentosRestantes > 0 && !ganado)
        {
            Console.Clear();
            Console.WriteLine("=== PARTIDA DE AHORCADO ===");
            Console.WriteLine($"Intentos restantes: {intentosRestantes}");
            Console.WriteLine($"Letras usadas: {string.Join(", ", letrasUsadas)}");
            Console.WriteLine($"Progreso de la palabra: {string.Join(" ", progreso)}");
            Console.Write("\nIntroduce una letra: ");

            string entrada = Console.ReadLine()?.ToUpper().Trim();

            // Validación de entrada
            if (string.IsNullOrEmpty(entrada) || entrada.Length != 1 || !char.IsLetter(entrada[0]))
            {
                Console.WriteLine("\n[Error] Entrada no válida. Debes introducir una única letra. Pulsa una tecla para continuar.");
                Console.ReadKey();
                continue;
            }

            char letra = entrada[0];

            if (letrasUsadas.Contains(letra))
            {
                Console.WriteLine("\n[Aviso] Ya has probado esa letra anteriormente. Pulsa una tecla para continuar.");
                Console.ReadKey();
                continue;
            }

            letrasUsadas.Add(letra);

            // Comprobar si la letra está en la palabra
            if (palabraSecreta.Contains(letra))
            {
                for (int i = 0; i < palabraSecreta.Length; i++)
                {
                    if (palabraSecreta[i] == letra)
                    {
                        progreso[i] = letra;
                    }
                }
            }
            else
            {
                intentosRestantes--;
                Console.WriteLine("\n[¡Fallaste!] La letra no está en la palabra. Pulsa una tecla para continuar.");
                Console.ReadKey();
            }

            // Verificar condición de victoria
            if (!new string(progreso).Contains('_'))
            {
                ganado = true;
            }
        }

        // Pantalla de resultado final
        Console.Clear();
        if (ganado)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("   ¡ENHORABUENA, HAS GANADO!     ");
            Console.WriteLine($"   La palabra era: {palabraSecreta}  ");
            Console.WriteLine("=================================");
        }
        else
        {
            Console.WriteLine("=================================");
            Console.WriteLine("   ¡FIN DE PARTIDA! HAS PERDIDO  ");
            Console.WriteLine($"   La palabra era: {palabraSecreta}  ");
            Console.WriteLine("=================================");
        }

        // Opción de volver a jugar o salir
        Console.Write("\n¿Quieres volver a jugar? (S/N): ");
        string respuesta = Console.ReadLine()?.ToUpper().Trim();
        if (respuesta == "S")
        {
            Jugar();
        }
    }
}