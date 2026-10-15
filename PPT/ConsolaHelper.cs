using System;

namespace PPT
{
    /// <summary>
    /// Proporciona operaciones de consola seguras frente a redirecciones de entrada/salida
    /// y entornos de ejecución automatizados o sin terminal físico.
    /// </summary>
    public static class ConsolaHelper
    {
        public static void Limpiar()
        {
            try
            {
                if (!Console.IsOutputRedirected)
                {
                    Console.Clear();
                }
            }
            catch
            {
                // Ignorar excepción si la consola está redirigida
            }
        }

        public static void EstablecerTitulo(string titulo)
        {
            try
            {
                if (!Console.IsOutputRedirected)
                {
                    Console.Title = titulo;
                }
            }
            catch
            {
                // Ignorar si no hay ventana de consola activa
            }
        }

        public static void Pausar(string mensaje = "\nPresiona cualquier tecla para continuar...")
        {
            Console.WriteLine(mensaje);
            try
            {
                if (!Console.IsInputRedirected)
                {
                    Console.ReadKey(true);
                }
            }
            catch
            {
                // Ignorar si la entrada está redirigida
            }
        }
    }
}
