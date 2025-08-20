

using System.Collections.ObjectModel;
using System.Drawing;
using Color = System.Drawing.Color;

namespace CBA_app.Models.ControlOperaciones
{
    public class ModeloControlOperaciones
    {

        public class ListaOperaciones
        {
            public float id { get; set; }
            public int orden { get; set; }
            public string operacion { get; set; }
            public string contenedor_numero { get; set; }
            public string nro_formulario { get; set; }
            public string persona_nombre { get; set; }
            public string prosesado { get; set; }
            public DateTime fecha { get; set; }

            public string fechaString
            {
                get => fecha.ToString();
                set
                {

                }
            }
            public string BackgroundColor
            {
                get => orden % 2 == 0 ? "S" : "N";
                set
                {

                }
            }
            public List<string> Bascula { get; set; } = new List<string> { "Bascula" };
        }

        public class Procesado
        {
            public bool resultado;
        }

        public class Operaciones
        {

            public TimeSpan HoraFin { get; set; }

            public TimeSpan HoraInicio { get; set; }

            public string OPERACION { get; set; }
            public DateTime FECHA { get; set; }
            public DateTime HORA_INICIO { get; set; }
            public DateTime HORA_FIN { get; set; }
            public string NRO_FORMULARIO { get; set; }
            public float CONTENEDOR_ID { get; set; }
            public string CONTENEDOR_NUMERO { get; set; }
            public float LINEA_ID { get; set; }
            public float CANTIDAD { get; set; }
            public string BL_NRO { get; set; }
            public string BOOKING { get; set; }
            public float PERSONA_ID { get; set; }
            public string PERSONA_NOMBRE { get; set; }
            public string CLASIFICACION_ID { get; set; }
            public float ITEM { get; set; }
            public string MERCADERIA_INTERNA { get; set; }
            public string PRECINTO_1 { get; set; }
            public string PRECINTO_2 { get; set; }
            public string PRECINTO_3 { get; set; }
            public string PRECINTO_4 { get; set; }
            public string PRECINTO_5 { get; set; }
            public string PRECINTO_ELECTRONICO1 { get; set; }
            public string PRECINTO_ELECTRONICO2 { get; set; }
            public string ESTADO { get; set; }
            public string PROCESADO { get; set; }
            public string OBSERVACION { get; set; }
            public string VENTILETE { get; set; }
            public float PESO_BRUTO { get; set; }
            public float TARA_CAMION { get; set; }
            public float PESO_NETO { get; set; }
            public float TARA_CONTENEDOR { get; set; }
            public string PISO { get; set; }
            public string FONDO { get; set; }
            public string TECHO { get; set; }
            public string PANEL_DERECHO { get; set; }
            public string PANEL_IZQUIERDO { get; set; }
            public string PUERTA { get; set; }
            public float id { get; set; }
            public int orden { get; set; }


        }
        public class GrupoOperaciones
        {
            // Propiedades de la clase
            public string Operacion { get; set; } // Propiedad que representa la operación del grupo
            public List<ListaOperaciones> Elementos { get; set; } // Lista de elementos asociados a la operación
        }



        public class Basculas
        {
            public Decimal id { get; set; }
            public int orden { get; set; }
            public string nombre { get; set; }
        }
        public class Servicio
        {
            public Decimal id { get; set; }
            public int orden { get; set; }
            public string nombre { get; set; }
        }

        public class hojaServicio
        {
            public decimal ID { get; set; }
            public decimal TEXTO_PREDEFINIDO_ID { get; set; }
            public decimal CONTENEDORES_OPERACIONES_ID { get; set; }
            public decimal Cantidad { get; set; }
            public string TEXTO_DESCRIPCION { get; set; }
            public string OPERACION_DESCRIPCION { get; set; }
            public decimal id { get; set; }
            public int orden { get; set; }
        }


    }
}
