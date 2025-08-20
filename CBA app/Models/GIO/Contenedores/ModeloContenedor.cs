using Newtonsoft.Json;

namespace CBA_app.Models.GIO.Contenedores
{
    public class ModeloContenedor
    {

        public class Rootobject
        {
            public string d { get; set; }
        }

        

        public class Contenedor
        {
            public string EN_PREDIO { get; set; }
            public string CLASE { get; set; }
            public decimal ID { get; set; }
            public decimal AGENCIA_ID { get; set; }
            public decimal PESO_MAXIMO { get; set; }
            public string NUMERO { get; set; }
            public string ESTADO { get; set; }
            public decimal TIPO_ID { get; set; }
            public decimal LINEA_ID { get; set; }
            public string tipo_contenedor { get; set; }
            public string agencia { get; set; }
            [JsonProperty("linea")]
            public string linea { get; set; }
            public decimal TARA { get; set; }
            public decimal CAPACIDAD { get; set; }
            public string TIPO_DESCRIPCION { get; set; }



            public Contenedor()
            {
                EN_PREDIO = "N";
                CLASE = string.Empty;
                ///ID se inicializa con -1 porque al momento de "crear" un nuevo contenedor el -1 indica al sistema 
                ///que es un nuevo contenedor.
                ID = -1;
                AGENCIA_ID = 0;
                PESO_MAXIMO = 0;
                NUMERO = string.Empty;
                ESTADO = string.Empty;
                TIPO_ID = 0;
                LINEA_ID = 0;
                TARA = 0;
                CAPACIDAD = 0;
                TIPO_DESCRIPCION = string.Empty;
            }
        }

        public class ConfiguracionTara
        {
            public decimal ID { get; set; }
            public decimal TARA { get; set; }
            public string CHAPA_CAMION { get; set; }
            public string CHAPA_CARRETA { get; set; }
            public string CHOFER_DOCUMENTO { get; set; }
            public string CHOFER_NOMBRE { get; set; }

            public ConfiguracionTara()
            {
                ID = -1;
                TARA = 0;
                CHAPA_CAMION = string.Empty;
                CHAPA_CARRETA = string.Empty;
                CHOFER_DOCUMENTO = string.Empty;
                CHOFER_NOMBRE = string.Empty;

            }
        }

        public class Linea
        {
            public string objeto_nombre { get; set; }
            public float id { get; set; }
            public int orden { get; set; }
            public float ID { get; set; }
            public string CODIGO { get; set; }
            public string ESTADO { get; set; }
            public float AGENCIA_ID { get; set; }
            public string AGENCIA_NOMBRE { get; set; }
            public float PERSONA_ID { get; set; }
            public string PERSONA_CODIGO { get; set; }
            public string PERSONA_NOMBRE { get; set; }
        }



        public class Tipo
        {
            public string objeto_nombre { get; set; }
            public float id { get; set; }
            public int orden { get; set; }
            public float ID { get; set; }
            public string DESCRIPCION { get; set; }
            public string ESTADO { get; set; }
            public string CLASIFICACION { get; set; }
            public float TAMANO { get; set; }
            public float TARA { get; set; }
            public string CODIGO { get; set; }
            public string CODIGO_EDI { get; set; }
        }



        public class Agencia
        {
            public string CODIGO { get; set; }
            public string ID { get; set; }
            public string NOMBRE { get; set; }
        }
        public class ObjetoContenedor
        {
            public decimal ID { get; set; }
            public string EN_PREDIO { get; set; }
            public string CLASE { get; set; }
            public decimal AGENCIA_ID { get; set; }
            public decimal PESO_MAXIMO { get; set; }
            public string NUMERO { get; set; }
            public string ESTADO { get; set; }
            public decimal TIPO_ID { get; set; }
            public string TIPO_DESCRIPCION { get; set; }
            public decimal LINEA_ID { get; set; }
            public decimal TARA { get; set; }
            public decimal CAPACIDAD { get; set; }
        }

    }

    public class ConfiguracionTara
    {
        public decimal ID { get; set; }
        public decimal TARA { get; set; }
        public string CHAPA_CAMION { get; set; }
        public string CHAPA_CARRETA { get; set; }
        public string CHOFER_DOCUMENTO { get; set; }
        public string CHOFER_NOMBRE { get; set; }


    }





}
