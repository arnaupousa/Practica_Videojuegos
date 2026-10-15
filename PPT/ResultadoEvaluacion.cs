namespace PPT
{
    /// <summary>
    /// Encapsula el resultado de una evaluación de ronda junto con la explicación de la acción.
    /// Compatible universalmente sin dependencias externas de tuplas.
    /// </summary>
    public class ResultadoEvaluacion
    {
        public ResultadoRonda Resultado { get; private set; }
        public string Explicacion { get; private set; }

        public ResultadoEvaluacion(ResultadoRonda resultado, string explicacion)
        {
            Resultado = resultado;
            Explicacion = explicacion;
        }
    }
}
