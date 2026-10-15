using System;

namespace PPT
{
    /// <summary>
    /// Punto de entrada del videojuego Piedra, Papel, Tijera, Lagarto, Spock.
    /// Su única responsabilidad es inicializar la aplicación y delegar el flujo al menú principal.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Configurar codificación para caracteres especiales y emojis si la consola lo soporta
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                MenuPrincipal menu = new MenuPrincipal();
                menu.Iniciar();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nHa ocurrido un error inesperado en la ejecución: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
