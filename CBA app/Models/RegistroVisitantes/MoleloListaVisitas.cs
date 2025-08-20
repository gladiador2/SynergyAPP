

namespace CBA_app.Models.RegistroVisitantes
{
    public class MoleloListaVisitas
    {
        public class ListaVisitas
        {
            private string _objeto_nombre;
            private string _salido;
            private string _vehiculo_chapa;
            private string _vehiculo_marca;
            private string _vehiculo_tipo;

            public string objeto_nombre
            {
                get { return _objeto_nombre ?? string.Empty; }
                set { _objeto_nombre = value; }
            }

            public float id { get; set; }
            public int orden { get; set; }
            public float ID { get; set; }
            public DateTime FECHA_LLEGADA { get; set; }
            public DateTime FECHA_SALIDA { get; set; }

            public string SALIDO
            {
                get { return _salido ?? string.Empty; }
                set { _salido = value; }
            }

            public Visitante[] visitantes { get; set; }
            public float VEHICULO_ID { get; set; }

            public string VEHICULO_CHAPA
            {
                get { return _vehiculo_chapa ?? string.Empty; }
                set { _vehiculo_chapa = value; }
            }

            public string VEHICULO_MARCA
            {
                get { return _vehiculo_marca ?? string.Empty; }
                set { _vehiculo_marca = value; }
            }

            public string VEHICULO_TIPO
            {
                get { return _vehiculo_tipo ?? string.Empty; }
                set { _vehiculo_tipo = value; }
            }
            public string BackgroundColor
            {
                get => orden % 2 == 0 ? "S" : "N";
                set
                {

                }
            }
            public string salida
            {
                get { return FECHA_SALIDA != DateTime.MinValue ? FECHA_SALIDA.ToString() : string.Empty; }
                set {  }
            }
            // Método para obtener el número de visitantes con salidas registradas
            public int CantidadVisitantesConSalidaRegistrada()
            {
                return visitantes.Count(v => v.salido == "S");
            }
            public int CantidadVisitantesEnPredio()
            {
                return visitantes.Count(v => v.salido == "N");
            }
          
          

        }

        public class Visitante
        {
            public float id { get; set; }
            public int orden { get; set; }
            public float ID { get; set; }
            public float VISITANTE_ID { get; set; }
            public DateTime FECHA_LLEGADA { get; set; }
            public float LLEGADA_ID { get; set; }
            public string es_chofer { get; set; }
            public string observacion { get; set; }
            public string visitante_nombre { get; set; }
            public string visitante_documento { get; set; }
            public string visitante_empresa { get; set; }
            public string salido { get; set; }
            public DateTime FECHA_SALIDA { get; set; }

            public TimeSpan hora_entrada
            {
                get { return FECHA_LLEGADA.TimeOfDay; }
            }

            public TimeSpan hora_salida
            {
                get { return FECHA_SALIDA.TimeOfDay; }
            }
            public string BackgroundColor
            {
                get => orden % 2 == 0 ? "S" : "N";
                set
                {

                }
            }
           
        }

    }
}
