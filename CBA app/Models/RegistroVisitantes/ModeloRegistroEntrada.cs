

namespace CBA_app.Models.RegistroVisitantes
{
    public class ModeloRegistroEntrada
    {
        public class ModeloVehiculo
        {
            public string objeto_nombre { get; set; }
            public float id { get; set; }
            public int orden { get; set; }
            public float ID { get; set; }
            public string CHAPA { get; set; }
            public string MARCA { get; set; }
            public string TIPO { get; set; }
            public string OBSERVACIONES { get; set; }
            public string ESTADO { get; set; }
            public string CAMION { get; set; }
            public string CARRETA { get; set; }
            public string CONTENEDOR { get; set; }
            public string MICDTA { get; set; }
        }




        public class ModeloPersonas
        {
            public string objeto_nombre { get; set; }
            public decimal id { get; set; }
            public int orden { get; set; }
            public decimal ID { get; set; }
            public string NOMBRE { get; set; }
            public string NRO_DOCUMENTO { get; set; }
            public string EMPRESA { get; set; }
            public string ESTADO { get; set; }
        }




        public class Entrada
        {
            public int orden { get; set; }
            public float LLEGADA_ID { get; set; }
            public float VEHICULO_ID { get; set; }
            public float VISITANTE_ID { get; set; }
            public float DETALLE_ID { get; set; }
            public string ES_CHOFER { get; set; }
        }

    }
}

