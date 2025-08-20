

using CBA_app.Services;

namespace CBA_app.Models.GIO
{
    public class ModeloGio
    {

        public decimal? ID { get; set; }
        public decimal? BASCULA_ID { get; set; }
        public string? TIPO_MOVIMIENTO { get; set; }
        public string? CHAPA_CAMION { get; set; }
        public string? CHAPA_TRACTO { get; set; }
        public string? NUMERO_COMPROBANTE { get; set; }
        public string? IMPORTACION_TERRESTRE { get; set; }
        public string? OBSERVACIONES { get; set; }
        public decimal? CONTENEDOR_ID { get; set; }
        public string? ESTADO_CONTENEDOR { get; set; }
        public decimal? TARA_CAMION { get; set; }
        public decimal? TARA_CONTENEDOR { get; set; }
        public string Conocimiento { get; set; }
        public string? CHOFER_DOCUMENTO { get; set; }
        public string? CHOFER_NOMBRE { get; set; }
        public string? FECHA { get; set; }
        public string? MOVIMIENTO { get; set; }
        public decimal? PESO_BRUTO { get; set; }
        public string? NOTA_REMISION { get; set; }
        public string? CONFIRMADO { get; set; }
        public string? PRECINTO_1 { get; set; }
        public string? PRECINTO_2 { get; set; }
        public string? PRECINTO_3 { get; set; }
        public string? PRECINTO_4 { get; set; }
        public string? PRECINTO_5 { get; set; }
        public string PRECINTO_ELEC_1 { get; set; }
        public string PRECINTO_ELEC_2 { get; set; }
        public string? PRECINTO_VENTILETE { get; set; }
        public decimal? INTERCAMBIO_EQUIPOS_ID { get; set; }
        public decimal? USUARIO_CREADOR_ID { get; set; }
        public decimal? USUARIO_CONFIRMACION_ID { get; set; }
        public string? FECHA_CONFIRMACION { get; set; }
        public string? NRO_CONTENEDOR { get; set; }
        public string? BOOKIN_BL { get; set; }
        public string? Fondo { get; set; }
        public string? Piso { get; set; }
        public string? Techo { get; set; }
        public string? Izquierdo { get; set; }
        public string? Derecho { get; set; }
        public string? Puerta { get; set; }
        public string? Refrigerado { get; set; }
        public string? TIPO_CONTENEDOR { get; set; }
        public decimal? Setpoint { get; set; }
        public decimal Peso_neto { get; set; }
        public decimal? PersonaId { get; set; }
        public string? PersonaNombre { get; set; }
        public decimal? PuertoId { get; set; }
        public string? PuertoNombre { get; set; }

        public string? Operativo { get; set; }
        public decimal? TemperaturaIngreso { get; set; }

        public string? Traxens { get; set; }

        public string Usuario_Confirmacion_Nombre { get; set; }

        public ModeloGio()
        {
            this.ID = -1;
            this.Setpoint = 0;
            this.BASCULA_ID = 0;
            this.TIPO_MOVIMIENTO = string.Empty;
            this.CHAPA_CAMION = string.Empty;
            this.CHAPA_TRACTO = string.Empty;
            this.NUMERO_COMPROBANTE = string.Empty;
            this.IMPORTACION_TERRESTRE = string.Empty;
            this.OBSERVACIONES = string.Empty;
            this.CONTENEDOR_ID = -1;
            this.ESTADO_CONTENEDOR = string.Empty;
            this.TARA_CAMION = 0;
            this.TARA_CONTENEDOR = 0;
            this.Conocimiento = string.Empty;
            this.CHOFER_DOCUMENTO = string.Empty;
            this.CHOFER_NOMBRE = string.Empty;
            this.FECHA = string.Empty;
            this.MOVIMIENTO = string.Empty;
            this.PESO_BRUTO = 0;
            this.Peso_neto = 0;
            this.NOTA_REMISION = string.Empty;
            this.CONFIRMADO = string.Empty;
            this.PRECINTO_1 = string.Empty;
            this.PRECINTO_2 = string.Empty;
            this.PRECINTO_3 = string.Empty;
            this.PRECINTO_4 = string.Empty;
            this.PRECINTO_5 = string.Empty;
            this.PRECINTO_ELEC_1 = string.Empty;
            this.PRECINTO_ELEC_2 = string.Empty;
            this.PRECINTO_VENTILETE = string.Empty;
            this.INTERCAMBIO_EQUIPOS_ID = 0;
            this.USUARIO_CREADOR_ID = 0;
            this.USUARIO_CONFIRMACION_ID = 0;
            this.FECHA_CONFIRMACION = string.Empty;
            this.NRO_CONTENEDOR = string.Empty;
            this.BOOKIN_BL = string.Empty;
            this.Fondo = string.Empty;
            this.Piso = string.Empty;
            this.Techo = string.Empty;
            this.Izquierdo = string.Empty;
            this.Derecho = string.Empty;
            this.Puerta = string.Empty;
            this.Refrigerado = string.Empty;
            this.TIPO_CONTENEDOR = string.Empty;
            this.PersonaId = -1;
            this.PersonaNombre = string.Empty;
            this.PuertoId = -1;
            this.PuertoNombre = string.Empty;
            this.Operativo = string.Empty;
            this.TemperaturaIngreso = 0;
            this.Traxens = "N";
            this.Usuario_Confirmacion_Nombre = string.Empty;
        }
    }

    public class Movimiento_rest 
    {
        public decimal ID { get; set; }
        public decimal BASCULA_ID { get; set; }
        public string TIPO_MOVIMIENTO { get; set; }
        public string CHAPA_CAMION { get; set; }
        public string CHAPA_TRACTO { get; set; }
        public string NUMERO_COMPROBANTE { get; set; }
        public string IMPORTACION_TERRESTRE { get; set; }
        public string OBSERVACIONES { get; set; }
        public decimal CONTENEDOR_ID { get; set; }
        public string ESTADO_CONTENEDOR { get; set; }
        public string TIPO_CONTENEDOR { get; set; }
        public decimal TARA_CAMION { get; set; }
        public decimal TARA_CONTENEDOR { get; set; }
        public string CHOFER_DOCUMENTO { get; set; }
        public string CHOFER_NOMBRE { get; set; }
        public string FECHA { get; set; }
        public string MOVIMIENTO { get; set; }
        public decimal PESO_BRUTO { get; set; }
        public decimal PESO_NETO { get; set; }
        public string NOTA_REMISION { get; set; }
        public string CONFIRMADO { get; set; }
        public string PRECINTO_1 { get; set; }
        public string PRECINTO_2 { get; set; }
        public string PRECINTO_3 { get; set; }
        public string PRECINTO_4 { get; set; }
        public string PRECINTO_5 { get; set; }
        public string PRECINTO_VENTILETE { get; set; }
        public decimal INTERCAMBIO_EQUIPOS_ID { get; set; }
        public decimal USUARIO_CREADOR_ID { get; set; }
        public decimal USUARIO_CONFIRMACION_ID { get; set; }
        public string FECHA_CONFIRMACION { get; set; }
        public string NRO_CONTENEDOR { get; set; }
        public string BOOKIN_BL { get; set; }
        public string Fondo { get; set; }
        public string Piso { get; set; }
        public string Techo { get; set; }
        public string Izquierdo { get; set; }
        public string Derecho { get; set; }
        public string Puerta { get; set; }
        public string Refrigerado { get; set; }
        public decimal Setpoint { get; set; }
        public decimal Codigo_Autorizacion { get; set; }
        public decimal Persona_Id { get; set; }
        public decimal Cliente_externo { get; set; }
        public string Persona_Nombre { get; set; }
        public decimal Puerto_Id { get; set; }
        public string Puerto_Nombre { get; set; }
        public string Operativo { get; set; }
        public string Conocimiento { get; set; }
        public string Mercaderia { get; set; }
        public string Clase_Contenedor { get; set; }
        public decimal? Temperatura_Ingreso { get; set; }
        public string Traxens { get; set; }

        public Movimiento_rest()
        {
            CONFIRMADO = string.Empty;
            BASCULA_ID = Definiciones.Error.Valor.EnteroPositivo;
            
            TIPO_MOVIMIENTO = string.Empty;
            CHAPA_CAMION = string.Empty;
            CHAPA_TRACTO = string.Empty;
            NUMERO_COMPROBANTE = string.Empty;
            IMPORTACION_TERRESTRE = string.Empty;
            OBSERVACIONES = string.Empty;
            CONTENEDOR_ID = Definiciones.Error.Valor.EnteroPositivo;
            ESTADO_CONTENEDOR = string.Empty;
            TIPO_CONTENEDOR = string.Empty;
            TARA_CAMION = Definiciones.Error.Valor.EnteroPositivo;
            TARA_CONTENEDOR = Definiciones.Error.Valor.EnteroPositivo;
            CHOFER_DOCUMENTO = string.Empty;
            CHOFER_NOMBRE = string.Empty;
            MOVIMIENTO = string.Empty;
            PESO_BRUTO = Definiciones.Error.Valor.EnteroPositivo;
            PESO_NETO = Definiciones.Error.Valor.EnteroPositivo;
            NOTA_REMISION = string.Empty;
            CONFIRMADO = string.Empty;
            PRECINTO_1 = string.Empty;
            PRECINTO_2 = string.Empty;
            PRECINTO_3 = string.Empty;
            PRECINTO_4 = string.Empty;
            PRECINTO_5 = string.Empty;
            PRECINTO_VENTILETE = string.Empty;
            INTERCAMBIO_EQUIPOS_ID = Definiciones.Error.Valor.EnteroPositivo;
            USUARIO_CREADOR_ID = Definiciones.Error.Valor.EnteroPositivo;
            USUARIO_CONFIRMACION_ID = Definiciones.Error.Valor.EnteroPositivo;
            NRO_CONTENEDOR = string.Empty;
            BOOKIN_BL = string.Empty;
            Fondo = string.Empty;
            Piso = string.Empty;
            Techo = string.Empty;
            Izquierdo = string.Empty;
            Derecho = string.Empty;
            Puerta = string.Empty;
            Refrigerado = string.Empty;
            Setpoint = Definiciones.Error.Valor.EnteroPositivo;
            Codigo_Autorizacion = Definiciones.Error.Valor.EnteroPositivo;
            Persona_Id = Definiciones.Error.Valor.EnteroPositivo;
            Cliente_externo = Definiciones.Error.Valor.EnteroPositivo;
            Persona_Nombre = string.Empty;
            Puerto_Id = Definiciones.Error.Valor.EnteroPositivo;
            Puerto_Nombre = string.Empty;
            Operativo = string.Empty;
            Conocimiento = string.Empty;
            Mercaderia = string.Empty;
            Clase_Contenedor = string.Empty;
            Temperatura_Ingreso = Definiciones.Error.Valor.EnteroPositivo;
            Traxens = string.Empty;

        }
    }
    public class GioLista
    {
        public string NOMBRE { get; set; }

        public Giomovientos MOVIMIENTO { get; set; }

        public GioLista(string NOMBRE, Giomovientos MOVIMIENTO)
        {
            this.NOMBRE = NOMBRE;

            this.MOVIMIENTO = MOVIMIENTO;
        }
    }

    public class GioLisView
    {
        public string contenedor { get; set; }
        public string Chapa { get; set; }
        public string fecha { get; set; }
        public string comprobante { get; set; }
        public Giomovientos MOVIMIENTO { get; set; }

        public bool BtnConfirmado { get; set; }

        public bool BtnDesconfirmado { get; set; }

        public GioLisView(string contenedor, string chapa,string fecha,string comprobante, Giomovientos MOVIMIENTO , bool Confirmado)
        {
            this.contenedor = contenedor;
            this.Chapa = chapa;
            this.fecha = fecha;
            this.comprobante = comprobante;  
            this.MOVIMIENTO = MOVIMIENTO;
            this.BtnConfirmado = Confirmado;
            this.BtnDesconfirmado = !Confirmado;

        }
        
    }
   
    public class Giomovientos
    {
        public string FECHA { get; set; }
        public string NUMERO_COMPROBANTE { get; set; }
        public string CHAPA_CAMION { get; set; }
        public string CONFIRMADO { get; set; }
        public string ID { get; set; }
        public string CONTENEDOR_NUMERO { get; set; }
    }



    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public float ID { get; set; }
        public string TIPO_MOVIMIENTO { get; set; }
        public string CHAPA_CAMION { get; set; }
        public string CHAPA_TRACTO { get; set; }
        public string MOVIMIENTO { get; set; }
        public float CONTENEDOR_ID { get; set; }
        public string CONTENEDOR_NUMERO { get; set; }
        public float CONTENEDOR_TARA { get; set; }
        public float PERSONA_ID { get; set; }
        public string PERSONA_DOCUMENTO { get; set; }
        public string PERSONA_CODIGO { get; set; }
        public string PERSONA_NOMBRE { get; set; }
        public float PUERTO_ID { get; set; }
        public string PUERTO_NOMBRE { get; set; }
        public string PUERTO_ABREVIATURA { get; set; }
        public string NUMERO_COMPROBANTE { get; set; }
        public string IMPORTACION_TERRESTRE { get; set; }
        public string ESTADO_CONTENEDOR { get; set; }
        public float TARA_CAMION { get; set; }
        public string CHOFER_DOCUMENTO { get; set; }
        public string CHOFER_NOMBRE { get; set; }
        public DateTime FECHA { get; set; }
        public float PESO_BRUTO { get; set; }
        public float TARA_CONTENEDOR { get; set; }
        public float PESO_NETO { get; set; }
        public string NOTA_REMISION { get; set; }
        public string CONFIRMADO { get; set; }
        public string PRECINTO_1 { get; set; }
        public string PRECINTO_2 { get; set; }
        public string PRECINTO_3 { get; set; }
        public string PRECINTO_ELECTRONICO_1 { get; set; }
        public float USUARIO_CREADOR_ID { get; set; }
        public string USUARIO_CREADOR_NOMBRE { get; set; }
        public float USUARIO_CONFIRMACION_ID { get; set; }
        public string USUARIO_CONFIRMACION_NOMBRE { get; set; }
        public DateTime FECHA_CONFIRMACION { get; set; }
        public string BOOKING_BL { get; set; }
        public string EIR_TECHO { get; set; }
        public string EIR_PANEL_DERECHO { get; set; }
        public string EIR_PANEL_IZQUIERDO { get; set; }
        public string EIR_PUERTA { get; set; }
        public float SET_POINT { get; set; }
        public string MERCADERIAS { get; set; }
        public string PESO_MANUAL { get; set; }
        public string OPERATIVO { get; set; }
        public string TRAXENS { get; set; }
        public string INTERNO { get; set; }
    }


    public class PuertaMovimientoInfoCompl
    {
        public float ID { get; set; }
        public float BASCULA_ID { get; set; }
        public string BASCULA_NOMBRE { get; set; }
        public string TIPO_MOVIMIENTO { get; set; }
        public string CHAPA_CAMION { get; set; }
        public string CHAPA_TRACTO { get; set; }
        public string MOVIMIENTO { get; set; }
        public float CONTENEDOR_ID { get; set; }
        public string CONTENEDOR_NUMERO { get; set; }
        public float CONTENEDOR_TARA { get; set; }
        public float PERSONA_ID { get; set; }
        public string PERSONA_DOCUMENTO { get; set; }
        public string PERSONA_CODIGO { get; set; }
        public string PERSONA_NOMBRE { get; set; }
        public float PUERTO_ID { get; set; }
        public string PUERTO_NOMBRE { get; set; }
        public string PUERTO_ABREVIATURA { get; set; }
        public string NUMERO_COMPROBANTE { get; set; }
        public string IMPORTACION_TERRESTRE { get; set; }
        public string OBSERVACIONES { get; set; }
        public string ESTADO_CONTENEDOR { get; set; }
        public float TARA_CAMION { get; set; }
        public string CHOFER_DOCUMENTO { get; set; }
        public string CHOFER_NOMBRE { get; set; }
        public DateTime FECHA { get; set; }
        public float PESO_BRUTO { get; set; }
        public float TARA_CONTENEDOR { get; set; }
        public float PESO_NETO { get; set; }
        public string NOTA_REMISION { get; set; }
        public string CONFIRMADO { get; set; }
        public string PRECINTO_1 { get; set; }
        public string PRECINTO_2 { get; set; }
        public string PRECINTO_3 { get; set; }
        public string PRECINTO_4 { get; set; }
        public string PRECINTO_5 { get; set; }
        public string PRECINTO_ELECTRONICO_1 { get; set; }
        public string PRECINTO_ELECTRONICO_2 { get; set; }
        public string PRECINTO_VENTILETE { get; set; }
        public float INTERCAMBIO_EQUIPOS_ID { get; set; }
        public float USUARIO_CREADOR_ID { get; set; }
        public string USUARIO_CREADOR_NOMBRE { get; set; }
        public float USUARIO_CONFIRMACION_ID { get; set; }
        public string USUARIO_CONFIRMACION_NOMBRE { get; set; }
        public DateTime FECHA_CONFIRMACION { get; set; }
        public string BOOKING_BL { get; set; }
        public string EIR_PISO { get; set; }
        public string EIR_FONDO { get; set; }
        public string EIR_TECHO { get; set; }
        public string EIR_PANEL_DERECHO { get; set; }
        public string EIR_PANEL_IZQUIERDO { get; set; }
        public string EIR_PUERTA { get; set; }
        public string EIR_REFRIGERADO { get; set; }
        public float EQUIPO_AUTORIZADO_DET_ID { get; set; }
        public string NRO_AUTORIZACION { get; set; }
        public string CODIGO_AUTORIZACION { get; set; }
        public float SET_POINT { get; set; }
        public string CONTENDOR_CLASE { get; set; }
        public string MERCADERIAS { get; set; }
        public string DANADO { get; set; }
        public string DANO_INFORMADO { get; set; }
        public string RECHAZADO { get; set; }
        public string CONOCIMIENTO { get; set; }
        public string PESO_MANUAL { get; set; }
        public float TEMPERATURA_INGRESO { get; set; }
        public string OPERATIVO { get; set; }
        public string TRAXENS { get; set; }
        public string INTERNO { get; set; }
        public string CLIENTE_EXTERNO { get; set; }
    }

}

