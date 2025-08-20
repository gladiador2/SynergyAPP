using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA_app.Models.OrdenesServicio
{
    public class ModeloPedidosPosicionamiento

    {
        public int Id { get; set; }
        public int? ContenedorId { get; set; }
        public string ContenedorNumero { get; set; }
        public string ContenedorLinea { get; set; }
        public string ContenedorTipo { get; set; }
        public string Clase { get; set; }
        public DateTime? FechaInsercion { get; set; }
        public DateTime? FechaSolicitada { get; set; }
        public int? DepositoId { get; set; }
        public int? BloqueId { get; set; }
        public string Bloque { get; set; }
        public string BloqueNombre { get; set; }
        public string DepositoNombre { get; set; }
        public int? PersonaId { get; set; }
        public string PersonaCodigo { get; set; }
        public string PersonaNombre { get; set; }
        public int? UsuarioInsercionId { get; set; }
        public string UsuarioSolicitud { get; set; }
        public int? TurnoPedidoId { get; set; }
        public string Turno { get; set; }
        public int? ContenedorUbicacionId { get; set; }
        public string Grupo { get; set; }
        public string Columna { get; set; }
        public string Estiba { get; set; }
        public int? UsuarioPosicionId { get; set; }
        public string UsuarioPosicionamiento { get; set; }
        public DateTime? FechaHoraPosicionamiento { get; set; }
        public int? UsuarioInicioId { get; set; }
        public string UsuarioInicio { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
        public int? UsuarioFinId { get; set; }
        public string UsuarioFin { get; set; }
        public DateTime? FechaHoraFin { get; set; }
        public int? UsuarioRetiroId { get; set; }
        public string UsuarioRetiro { get; set; }
        public DateTime? FechaHoraRetiro { get; set; }
        public string Motivo { get; set; }
        public string Solicitante { get; set; }
        public string Montacargas { get; set; }
        public string Estibadores { get; set; }
        public string Observaciones { get; set; }
        public string Aprobado { get; set; } // CHAR(1), se puede mapear como string de 1 caracter
        public string Despacho { get; set; } // VARCHAR2(50)
        public int? Servicio_id { get; set; }
    }
    public class ModeloSubServicio
    {
        public string Nombre { get; set; }
        public int ArticuloId { get; set; }
        public string Articulo { get; set; }
    }

   

    public class TipoServicio
    {
        public int orden { get; set; }
        public string Nombre { get; set; }
    }

    public class ModeloServicio
    {

        public double id { get; set; }
        public int orden { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ModeloSubServicio> SubServicios { get; set; }
    }




    public partial class ModeloOrdenServicio : ObservableObject
    {
        public float id { get; set; }
        public int orden { get; set; }
        public int Id { get; set; }
        public string Servicio { get; set; }

        public int PedidoPosId { get; set; }
        [ObservableProperty]
        private string diceContener;

        public string PrecintoApertura1 { get; set; }
        public string PrecintoApertura2 { get; set; }
        public string PrecintoApertura3 { get; set; }
        public string PrecintoApertura4 { get; set; }
        public string PrecintoApertura5 { get; set; }

        public string PrecintoCierre1 { get; set; }
        public string PrecintoCierre2 { get; set; }
        public string PrecintoCierre3 { get; set; }
        public string PrecintoCierre4 { get; set; }
        public string PrecintoCierre5 { get; set; }
        public Detalle[] Detalles { get; set; }

    }


    public partial class Detalle : ObservableObject
    {
        public int id { get; set; }
        public string SubServicio { get; set; }
        [ObservableProperty]
        private int cant;
        public string Articulo { get; set; }
        [ObservableProperty]
        private string aprobado;

        public int? Index { get; set; }
        public Color RowColor { get; set; }
        [ObservableProperty]
        private string observacion;

    }


    public partial class ModeloActasOrdenServicio : ObservableObject
    {
        /// <summary>
        /// Identificador único del acta de orden de servicio
        /// </summary>
        public decimal ID { get; set; }

        /// <summary>
        /// Identificador del pedido de posicionamiento asociado
        /// </summary>
        public decimal PEDIDO_POSICIONAMIENTO_ID { get; set; }

        /// <summary>
        /// Tipo de acta del servicio
        /// </summary>
        public string TIPO_ACTA { get; set; }

        /// <summary>
        /// Fecha de emisión del acta
        /// </summary>
        public DateTime? FECHA_EMISION { get; set; }

        /// <summary>
        /// Número del acta
        /// </summary>
        public string NUM_ACTA { get; set; }

        /// <summary>
        /// Número de reporte asociado
        /// </summary>
        public string REPORTE_NUMERO { get; set; }

        /// <summary>
        /// Número de hoja del documento
        /// </summary>
        public string HOJA_NUMERO { get; set; }

        /// <summary>
        /// Fecha de ingreso al sistema
        /// </summary>
        public DateTime? FECHA_INGRESO { get; set; }

        /// <summary>
        /// Hora de inicio del servicio
        /// </summary>
        public string HORA_INICIO { get; set; }

        /// <summary>
        /// Hora de finalización del servicio
        /// </summary>
        public string HORA_FINALIZADO { get; set; }

        /// <summary>
        /// Tipo de carga transportada
        /// </summary>
        public string CARGA_TIPO { get; set; }

        /// <summary>
        /// Tipo de importación del proceso
        /// </summary>
        public string IMPORTACION_TIPO { get; set; }

        /// <summary>
        /// Régimen aduanero aplicado
        /// </summary>
        public string REGIMEN_ADUANERO { get; set; }

        /// <summary>
        /// Número de despacho aduanero
        /// </summary>
        public string DESPACHO_NUMERO { get; set; }

        /// <summary>
        /// Nombre o identificación del consignatario
        /// </summary>
        public string CONSIGNATARIO { get; set; }

        /// <summary>
        /// Número de orden de servicio
        /// </summary>
        public string ORDEN_SERVICIO { get; set; }

        /// <summary>
        /// Canal asignado para el proceso
        /// </summary>
        public string CANAL_ASIGNADO { get; set; }

        /// <summary>
        /// Número de conocimiento de transporte
        /// </summary>
        public string CTO_NUM { get; set; }

        /// <summary>
        /// Número de Bill of Lading (BM)
        /// </summary>
        public string BM { get; set; }

        /// <summary>
        /// Número de registro de entrada
        /// </summary>
        public string REG_ENT_NUM { get; set; }

        /// <summary>
        /// Identificación del contenedor RUA
        /// </summary>
        public string CONTENEDOR_RUA { get; set; }

        /// <summary>
        /// Tamaño del contenedor (20', 40', etc.)
        /// </summary>
        public string TAMANIO_CONTENEDOR { get; set; }

        /// <summary>
        /// Precintos manifiestos del contenedor
        /// </summary>
        public string PRECINTOS_MANIF { get; set; }

        /// <summary>
        /// Cantidad manifestada por tipo de embalaje
        /// </summary>
        public string CANT_MANIF_TIPO_EMBALAJE { get; set; }

        /// <summary>
        /// Peso de mercadería según manifiesto
        /// </summary>
        public string PESO_MERC_MANIF { get; set; }

        /// <summary>
        /// Estado del cierre precintado
        /// </summary>
        public string CIERRE_PRECINTADO { get; set; }

        /// <summary>
        /// Cantidad verificada por tipo de embalaje
        /// </summary>
        public string CANT_VERIF_TIPO_EMBALAJE { get; set; }

        /// <summary>
        /// Descripción de lo que dice contener
        /// </summary>
        public string DICE_CONTENER { get; set; }

        /// <summary>
        /// Descripción de la mercadería
        /// </summary>
        public string MERCADERIA { get; set; }

        /// <summary>
        /// Origen de la mercadería
        /// </summary>
        public string ORIGEN_MERCADERIA { get; set; }

        /// <summary>
        /// Depósito donde se encuentra la mercadería
        /// </summary>
        public string DEPOSITO { get; set; }

        /// <summary>
        /// Metros cuadrados ocupados en depósito
        /// </summary>
        public decimal? M2_DEPOSITO { get; set; }

        /// <summary>
        /// Contenedor del Puerto de Santa Fe
        /// </summary>
        public string CONTENEDOR_PSF { get; set; }

        /// <summary>
        /// Identificación del camión externo
        /// </summary>
        public string CAMION_EXTERNO { get; set; }

        /// <summary>
        /// Información de entrega del camión
        /// </summary>
        public string ENTREGA_CAMION { get; set; }

        /// <summary>
        /// Precinto de apertura del contenedor
        /// </summary>
        public string PRECINTO_APERTURA { get; set; }

        /// <summary>
        /// Precinto de cierre del contenedor
        /// </summary>

        public string PRECINTO_CIERRE { get; set; }
        [ObservableProperty]
        public decimal? cAJAS_DANIADAS;

        public string CAJAS_DANIADAS_APROBADO { get; set; }
        [ObservableProperty]
        public decimal? cAJAS_ABIERTAS;

        public string CAJAS_ABIERTAS_APROBADO { get; set; }
        [ObservableProperty]
        public decimal? rETIRO_MUESTRAS;

        public string RETIRO_MUESTRAS_APROBADO { get; set; }
        [ObservableProperty]
        public decimal? cONTEO_CANTIDAD;

        /// <summary>
        /// Indicador de aprobación del conteo (S/N)
        /// </summary>
        public string CONTEO_APROBADO { get; set; }

        /// <summary>
        /// Área donde se encuentra el contenedor
        /// </summary>
        public string AREA_CONTENEDOR { get; set; }

        /// <summary>
        /// Área donde se encuentra el vehículo
        /// </summary>
        public string AREA_VEHICULO { get; set; }

        /// <summary>
        /// Zona de operación donde se realiza el servicio
        /// </summary>
        public string ZONA_OPERACION { get; set; }

        /// <summary>
        /// Observaciones generales del servicio
        /// </summary>
        public string OBSERVACIONES { get; set; }

        /// <summary>
        /// Apuntador del Puerto de Santa Fe
        /// </summary>
        public string APUNTADOR_PSF { get; set; }

        /// <summary>
        /// Funcionario de la Dirección Nacional de Aduanas
        /// </summary>
        public string FUNCIONARIO_DNA { get; set; }

        /// <summary>
        /// Despachante de aduana responsable
        /// </summary>
        public string DESPACHANTE { get; set; }

        /// <summary>
        /// Responsable de la visturía
        /// </summary>
        public string VISTURIA { get; set; }

        /// <summary>
        /// Personal de resguardo asignado
        /// </summary>
        public string RESGUARDO { get; set; }

        // Nuevas propiedades agregadas
        /// <summary>
        /// Tipo Importación/Exportación: Imp. Fluvial, Imp. Terrestre, Traslado otro PTC, Otro
        /// </summary>
        public string TIPO_IMP_EXP { get; set; }

        /// <summary>
        /// Tipo de Carga: Carga FCL, LCL
        /// </summary>
        public string CARGA_FCL_LCL { get; set; }

        /// <summary>
        /// Régimen Aduanero: IC 03Normal, LCL
        /// </summary>
        public string REGIMEN_IC03_LCL { get; set; }

        /// <summary>
        /// Ingreso Depósito: A, B, C, D, E, F, G
        /// </summary>
        public string INGRESO_DEPOSITO { get; set; }

        /// <summary>
        /// Sobre Carreta Cliente/Transp. en Anden: S o N
        /// </summary>
        public string SOBRE_CARRETA_CLIENTE { get; set; }

        /// <summary>
        /// Sobre Carreta PSF: S o N
        /// </summary>
        public string SOBRE_CARRETA_PSF { get; set; }

        /// <summary>
        /// En Anden: S o N
        /// </summary>
        public string EN_ANDEN { get; set; }

        /// <summary>
        /// En Anden (cntr): S o N
        /// </summary>
        public string EN_ANDEN_CNTR { get; set; }

        /// <summary>
        /// Deposito A-B-C: S o N
        /// </summary>
        public string DEPOSITO_ABC { get; set; }

        /// <summary>
        /// Área Cntr: S o N
        /// </summary>
        public string AREA_CNTR { get; set; }

        /// <summary>
        /// Pre-estiba: S o N
        /// </summary>
        public string PRE_ESTIBA { get; set; }

        /// <summary>
        /// Área Vehic.: S o N
        /// </summary>
        public string AREA_VEHIC { get; set; }

        /// <summary>
        /// Otro: S o N
        /// </summary>
        public string OTRO_CAMPO { get; set; }
    }
    public class PedidoPosicionamientoInforme
    {
        public int Id { get; set; }
        public DateTime FechaHoraPosicionamiento { get; set; }
        public DateTime FechaSolicitada { get; set; }

        // Contenedor
        public string NumeroContenedor { get; set; }
        public string LineaNombre { get; set; }
        public string AgenciaNombre { get; set; }
        public string TipoDescripcion { get; set; }
        public decimal PesoMaximo { get; set; }  // payload
        public decimal Tara { get; set; }

        // Servicio
        public string Servicio { get; set; }
        public string Subservicio { get; set; }

        // Detalle
        public int Cantidad { get; set; }

        // Cliente
        public string Cliente { get; set; } // p.nombre + ' ' + p.apellido

        // Usuario
        public string Usuario { get; set; } // u.nombre + ' ' + u.apellido + ' (' + u.usuario + ')'

        // Campos faltantes según comentarios en el SQL:
        // public string PuertoOrigen { get; set; }
        // public string Resguardo { get; set; }
    }

   

}
