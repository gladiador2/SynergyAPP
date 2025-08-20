using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CBA_app.Models.Facturacion
{

    public class ModeloJsonAXml
    {
        /// <summary>
        /// Datos estáticos del contribuyente emisor, como RUC, razón social, timbrado, etc.
        /// </summary>
        [JsonPropertyName("params")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Params? _params { get; set; }

        /// <summary>
        /// Datos variables del documento electrónico, como tipo de documento, cliente, ítems, fecha, etc.
        /// </summary>
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Data? data { get; set; }

        /// <summary>
        /// Opciones adicionales de configuración para la generación del XML (decimales, valores por defecto, etc.).
        /// </summary>
        [JsonPropertyName("config")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Config? config { get; set; }
    }

    public class Params
    {
        /// <summary>
        /// Versión del esquema de documento electrónico SIFEN.
        /// </summary>
        public int version { get; set; }

        /// <summary>
        /// RUC del contribuyente emisor.
        /// </summary>
        public string? ruc { get; set; }

        /// <summary>
        /// Razón social del emisor.
        /// </summary>
        public string? razonSocial { get; set; }

        /// <summary>
        /// Nombre de fantasía del emisor.
        /// </summary>
        public string? nombreFantasia { get; set; }

        /// <summary>
        /// Lista de actividades económicas del emisor.
        /// </summary>
        public Actividadeseconomica[]? actividadesEconomicas { get; set; }

        /// <summary>
        /// Número de timbrado vigente.
        /// </summary>
        public string? timbradoNumero { get; set; }

        /// <summary>
        /// Fecha de vigencia del timbrado.
        /// </summary>
        public string? timbradoFecha { get; set; }

        /// <summary>
        /// Tipo de contribuyente según la SET.
        /// </summary>
        public int? tipoContribuyente { get; set; }

        /// <summary>
        /// Tipo de régimen tributario del emisor.
        /// </summary>
        public int? tipoRegimen { get; set; }

        /// <summary>
        /// Lista de establecimientos del emisor.
        /// </summary>
        public Establecimiento[]? establecimientos { get; set; }
    }


    public class Data
    {
        /// <summary>
        /// Tipo de documento electrónico (ej: factura, nota de crédito, etc.).
        /// </summary>
        public int tipoDocumento { get; set; }

        /// <summary>
        /// Código del establecimiento emisor.
        /// </summary>
        public string? establecimiento { get; set; }

        /// <summary>
        /// Código de seguridad aleatorio para el documento.
        /// </summary>
        public string? codigoSeguridadAleatorio { get; set; }

        /// <summary>
        /// Código del punto de expedición.
        /// </summary>
        public string? punto { get; set; }

        /// <summary>
        /// Número del documento electrónico.
        /// </summary>
        public string? numero { get; set; }

        /// <summary>
        /// Descripción general del documento.
        /// </summary>
        public string? descripcion { get; set; }

        /// <summary>
        /// Observaciones adicionales del documento.
        /// </summary>
        public string? observacion { get; set; }

        /// <summary>
        /// Fecha de emisión del documento.
        /// </summary>
        public DateTime? fecha { get; set; }

        /// <summary>
        /// Tipo de emisión (normal, contingencia, etc.).
        /// </summary>
        public int? tipoEmision { get; set; }

        /// <summary>
        /// Tipo de transacción (venta, compra, etc.).
        /// </summary>
        public int? tipoTransaccion { get; set; }

        /// <summary>
        /// Tipo de impuesto aplicado.
        /// </summary>
        public int? tipoImpuesto { get; set; }

        /// <summary>
        /// Moneda utilizada en el documento.
        /// </summary>
        public string? moneda { get; set; }

        /// <summary>
        /// Condición de anticipo global.
        /// </summary>
        public int? condicionAnticipo { get; set; }

        /// <summary>
        /// Condición de tipo de cambio.
        /// </summary>
        public int? condicionTipoCambio { get; set; }

        /// <summary>
        /// Descuento global aplicado al documento.
        /// </summary>
        public int? descuentoGlobal { get; set; }

        /// <summary>
        /// Anticipo global aplicado al documento.
        /// </summary>
        public int? anticipoGlobal { get; set; }

        /// <summary>
        /// Valor de cambio de moneda.
        /// </summary>
        public int? cambio { get; set; }

        /// <summary>
        /// Datos del cliente receptor del documento.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Cliente? cliente { get; set; }

        /// <summary>
        /// Datos del usuario responsable de la emisión.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Usuario? usuario { get; set; }

        /// <summary>
        /// Datos específicos de la factura.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Factura? factura { get; set; }

        /// <summary>
        /// Datos de autofactura si corresponde.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Autofactura? autoFactura { get; set; }

        /// <summary>
        /// Datos de nota de crédito o débito si corresponde.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Notacreditodebito? notaCreditoDebito { get; set; }

        /// <summary>
        /// Datos de remisión si corresponde.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Remision? remision { get; set; }

        /// <summary>
        /// Condiciones de la operación (contado, crédito, etc.).
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Condicion? condicion { get; set; }

        /// <summary>
        /// Lista de ítems o productos incluidos en el documento.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Item[]? items { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Sectorenergiaelectrica? sectorEnergiaElectrica { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Sectorseguros? sectorSeguros { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Sectorsupermercados? sectorSupermercados { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Sectoradicional? sectorAdicional { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Detalletransporte? detalleTransporte { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Complementarios? complementarios { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Documentoasociado? documentoAsociado { get; set; }
    }


    public class Actividadeseconomica
    {
        /// <summary>
        /// Código de la actividad económica registrada por el emisor.
        /// </summary>
        public string? codigo { get; set; }

        /// <summary>
        /// Descripción de la actividad económica.
        /// </summary>
        public string? descripcion { get; set; }
    }

    public class Establecimiento
    {
        /// <summary>
        /// Código identificador del establecimiento del emisor.
        /// </summary>
        public string? codigo { get; set; }

        /// <summary>
        /// Dirección física del establecimiento.
        /// </summary>
        public string? direccion { get; set; }

        /// <summary>
        /// Número de casa o local del establecimiento.
        /// </summary>
        public string? numeroCasa { get; set; }

        /// <summary>
        /// Complemento adicional de la dirección (línea 1).
        /// </summary>
        public string? complementoDireccion1 { get; set; }

        /// <summary>
        /// Complemento adicional de la dirección (línea 2).
        /// </summary>
        public string? complementoDireccion2 { get; set; }

        /// <summary>
        /// Código de departamento según la SET.
        /// </summary>
        public int? departamento { get; set; }

        /// <summary>
        /// Descripción del departamento.
        /// </summary>
        public string? departamentoDescripcion { get; set; }

        /// <summary>
        /// Código de distrito según la SET.
        /// </summary>
        public int? distrito { get; set; }

        /// <summary>
        /// Descripción del distrito.
        /// </summary>
        public string? distritoDescripcion { get; set; }

        /// <summary>
        /// Código de ciudad según la SET.
        /// </summary>
        public int? ciudad { get; set; }

        /// <summary>
        /// Descripción de la ciudad.
        /// </summary>
        public string? ciudadDescripcion { get; set; }

        /// <summary>
        /// Teléfono de contacto del establecimiento.
        /// </summary>
        public string? telefono { get; set; }

        /// <summary>
        /// Correo electrónico del establecimiento.
        /// </summary>
        public string? email { get; set; }

        /// <summary>
        /// Denominación comercial del establecimiento.
        /// </summary>
        public string? denominacion { get; set; }
    }

    public class Cliente
    {
        /// <summary>
        /// Indica si el cliente es contribuyente.
        /// </summary>
        public bool contribuyente { get; set; }

        /// <summary>
        /// RUC del cliente receptor.
        /// </summary>
        public string? ruc { get; set; }

        /// <summary>
        /// Razón social del cliente.
        /// </summary>
        public string? razonSocial { get; set; }

        /// <summary>
        /// Nombre de fantasía del cliente.
        /// </summary>
        public string? nombreFantasia { get; set; }

        /// <summary>
        /// Tipo de operación realizada por el cliente.
        /// </summary>
        public int? tipoOperacion { get; set; }

        /// <summary>
        /// Dirección del cliente.
        /// </summary>
        public string? direccion { get; set; }

        /// <summary>
        /// Número de casa del cliente.
        /// </summary>
        public string? numeroCasa { get; set; }

        /// <summary>
        /// Código de departamento del cliente.
        /// </summary>
        public int? departamento { get; set; }

        /// <summary>
        /// Descripción del departamento.
        /// </summary>
        public string? departamentoDescripcion { get; set; }

        /// <summary>
        /// Código de distrito del cliente.
        /// </summary>
        public int? distrito { get; set; }

        /// <summary>
        /// Descripción del distrito.
        /// </summary>
        public string? distritoDescripcion { get; set; }

        /// <summary>
        /// Código de ciudad del cliente.
        /// </summary>
        public int? ciudad { get; set; }

        /// <summary>
        /// Descripción de la ciudad.
        /// </summary>
        public string? ciudadDescripcion { get; set; }

        /// <summary>
        /// Código de país del cliente.
        /// </summary>
        public string? pais { get; set; }

        /// <summary>
        /// Descripción del país.
        /// </summary>
        public string? paisDescripcion { get; set; }

        /// <summary>
        /// Tipo de contribuyente del cliente.
        /// </summary>
        public int? tipoContribuyente { get; set; }

        /// <summary>
        /// Tipo de documento de identificación del cliente.
        /// </summary>
        public int? documentoTipo { get; set; }

        /// <summary>
        /// Número de documento de identificación del cliente.
        /// </summary>
        public string? documentoNumero { get; set; }

        /// <summary>
        /// Teléfono de contacto del cliente.
        /// </summary>
        public string? telefono { get; set; }

        /// <summary>
        /// Número de celular del cliente.
        /// </summary>
        public string? celular { get; set; }

        /// <summary>
        /// Correo electrónico del cliente.
        /// </summary>
        public string? email { get; set; }

        /// <summary>
        /// Código interno del cliente.
        /// </summary>
        public string? codigo { get; set; }
    }

    public class Usuario
    {
        /// <summary>
        /// Tipo de documento de identificación del usuario.
        /// </summary>
        public int? documentoTipo { get; set; }

        /// <summary>
        /// Número de documento de identificación del usuario.
        /// </summary>
        public string? documentoNumero { get; set; }

        /// <summary>
        /// Nombre completo del usuario responsable.
        /// </summary>
        public string? nombre { get; set; }

        /// <summary>
        /// Cargo o función del usuario.
        /// </summary>
        public string? cargo { get; set; }
    }

    public class Factura
    {
        /// <summary>
        /// Indica la presencia física del documento (presencial, virtual, etc.).
        /// </summary>
        public int? presencia { get; set; }

        /// <summary>
        /// Fecha de envío de la factura.
        /// </summary>
        public string? fechaEnvio { get; set; }

        /// <summary>
        /// Datos de la Dirección Nacional de Contrataciones Públicas (DNCP) si corresponde.
        /// </summary>
        public Dncp? dncp { get; set; }
    }

    public class Autofactura
    {
        /// <summary>
        /// Tipo de vendedor en la autofactura.
        /// </summary>
        public int? tipoVendedor { get; set; }

        /// <summary>
        /// Tipo de documento del vendedor.
        /// </summary>
        public int? documentoTipo { get; set; }

        /// <summary>
        /// Número de documento del vendedor.
        /// </summary>
        public int? documentoNumero { get; set; }

        /// <summary>
        /// Nombre del vendedor.
        /// </summary>
        public string? nombre { get; set; }

        /// <summary>
        /// Dirección del vendedor.
        /// </summary>
        public string? direccion { get; set; }

        /// <summary>
        /// Número de casa del vendedor.
        /// </summary>
        public string? numeroCasa { get; set; }

        /// <summary>
        /// Código de departamento del vendedor.
        /// </summary>
        public int? departamento { get; set; }

        /// <summary>
        /// Descripción del departamento.
        /// </summary>
        public string? departamentoDescripcion { get; set; }

        /// <summary>
        /// Código de distrito del vendedor.
        /// </summary>
        public int? distrito { get; set; }

        /// <summary>
        /// Descripción del distrito.
        /// </summary>
        public string? distritoDescripcion { get; set; }

        /// <summary>
        /// Código de ciudad del vendedor.
        /// </summary>
        public int? ciudad { get; set; }

        /// <summary>
        /// Descripción de la ciudad.
        /// </summary>
        public string? ciudadDescripcion { get; set; }

        /// <summary>
        /// Datos de la transacción asociada a la autofactura.
        /// </summary>
        public Transaccion? transaccion { get; set; }
    }
    public class Dncp
    {
        /// <summary>
        /// Modalidad de la contratación pública.
        /// </summary>
        public string? modalidad { get; set; }

        /// <summary>
        /// Código de la entidad contratante.
        /// </summary>
        public int? entidad { get; set; }

        /// <summary>
        /// Año de la contratación.
        /// </summary>
        public int? año { get; set; }

        /// <summary>
        /// Secuencia de la contratación.
        /// </summary>
        public int? secuencia { get; set; }

        /// <summary>
        /// Fecha de la contratación.
        /// </summary>
        public DateTime? fecha { get; set; }
    }

    public class Notacreditodebito
    {
        /// <summary>
        /// Motivo de la nota de crédito o débito.
        /// </summary>
        public int? motivo { get; set; }
    }

    public class Remision
    {
        /// <summary>
        /// Motivo de la remisión.
        /// </summary>
        public int? motivo { get; set; }

        /// <summary>
        /// Tipo de responsable de la remisión.
        /// </summary>
        public int? tipoResponsable { get; set; }

        /// <summary>
        /// Kilómetros recorridos en la remisión.
        /// </summary>
        public int? kms { get; set; }

        /// <summary>
        /// Fecha de la factura asociada a la remisión.
        /// </summary>
        public string? fechaFactura { get; set; }
    }

    public class Transaccion
    {
        /// <summary>
        /// Lugar de la transacción.
        /// </summary>
        public string? lugar { get; set; }

        /// <summary>
        /// Código de departamento del lugar de la transacción.
        /// </summary>
        public int? departamento { get; set; }

        /// <summary>
        /// Descripción del departamento.
        /// </summary>
        public string? departamentoDescripcion { get; set; }

        /// <summary>
        /// Código de distrito del lugar de la transacción.
        /// </summary>
        public int? distrito { get; set; }

        /// <summary>
        /// Descripción del distrito.
        /// </summary>
        public string? distritoDescripcion { get; set; }

        /// <summary>
        /// Código de ciudad del lugar de la transacción.
        /// </summary>
        public int? ciudad { get; set; }

        /// <summary>
        /// Descripción de la ciudad.
        /// </summary>
        public string? ciudadDescripcion { get; set; }
    }

    public class Condicion
    {
        /// <summary>
        /// Tipo de condición de la operación (contado, crédito, etc.).
        /// </summary>
        public int? tipo { get; set; }

        /// <summary>
        /// Entregas asociadas a la condición.
        /// </summary>
        public Entrega[]? entregas { get; set; }

        /// <summary>
        /// Datos de crédito si la condición es a crédito.
        /// </summary>
        public Credito? credito { get; set; }
    }

    public class Entrega
    {
        /// <summary>
        /// Tipo de entrega (efectivo, cheque, tarjeta, etc.).
        /// </summary>
        public int? tipo { get; set; }

        /// <summary>
        /// Monto de la entrega.
        /// </summary>
        public string? monto { get; set; }

        /// <summary>
        /// Moneda de la entrega.
        /// </summary>
        public string? moneda { get; set; }

        /// <summary>
        /// Valor de cambio aplicado a la entrega.
        /// </summary>
        public int? cambio { get; set; }

        /// <summary>
        /// Información de la tarjeta si corresponde.
        /// </summary>
        public Infotarjeta? infoTarjeta { get; set; }

        /// <summary>
        /// Información del cheque si corresponde.
        /// </summary>
        public Infocheque? infoCheque { get; set; }
    }

    public class Credito
    {
        /// <summary>
        /// Tipo de crédito.
        /// </summary>
        public int? tipo { get; set; }

        /// <summary>
        /// Plazo del crédito.
        /// </summary>
        public string? plazo { get; set; }

        /// <summary>
        /// Cantidad de cuotas del crédito.
        /// </summary>
        public int? cuotas { get; set; }

        /// <summary>
        /// Monto de la entrega inicial.
        /// </summary>
        public int? montoEntrega { get; set; }

        /// <summary>
        /// Información de las cuotas.
        /// </summary>
        public Infocuota[]? infoCuotas { get; set; }
    }

    public class Infotarjeta
    {
        /// <summary>
        /// Tipo de tarjeta utilizada.
        /// </summary>
        public int? tipo { get; set; }

        /// <summary>
        /// Descripción del tipo de tarjeta.
        /// </summary>
        public string? tipoDescripcion { get; set; }

        /// <summary>
        /// Titular de la tarjeta.
        /// </summary>
        public string? titular { get; set; }

        /// <summary>
        /// RUC del titular de la tarjeta.
        /// </summary>
        public string? ruc { get; set; }

        /// <summary>
        /// Razón social del titular de la tarjeta.
        /// </summary>
        public string? razonSocial { get; set; }

        /// <summary>
        /// Medio de pago utilizado.
        /// </summary>
        public int? medioPago { get; set; }

        /// <summary>
        /// Código de autorización de la transacción con tarjeta.
        /// </summary>
        public int? codigoAutorizacion { get; set; }
    }

    public class Infocheque
    {
        /// <summary>
        /// Número del cheque utilizado en la operación.
        /// </summary>
        public string? numeroCheque { get; set; }

        /// <summary>
        /// Banco emisor del cheque.
        /// </summary>
        public string? banco { get; set; }
    }

    public class Infocuota
    {
        /// <summary>
        /// Moneda de la cuota.
        /// </summary>
        public string? moneda { get; set; }

        /// <summary>
        /// Monto de la cuota.
        /// </summary>
        public int? monto { get; set; }

        /// <summary>
        /// Fecha de vencimiento de la cuota.
        /// </summary>
        public string? vencimiento { get; set; }
    }

    public class Item
    {
        /// <summary>
        /// Código del producto o servicio.
        /// </summary>
        public string? codigo { get; set; }

        /// <summary>
        /// Descripción del producto o servicio.
        /// </summary>
        public string? descripcion { get; set; }

        /// <summary>
        /// Observaciones adicionales del ítem.
        /// </summary>
        public string? observacion { get; set; }

        /// <summary>
        /// Código de partida arancelaria si corresponde.
        /// </summary>
        public int? partidaArancelaria { get; set; }

        /// <summary>
        /// Código NCM del producto.
        /// </summary>
        public string? ncm { get; set; }

        /// <summary>
        /// Unidad de medida del ítem.
        /// </summary>
        public int? unidadMedida { get; set; }

        /// <summary>
        /// Cantidad del ítem.
        /// </summary>
        public float? cantidad { get; set; }

        /// <summary>
        /// Precio unitario del ítem.
        /// </summary>
        public int? precioUnitario { get; set; }

        /// <summary>
        /// Valor de cambio aplicado al ítem.
        /// </summary>
        public int? cambio { get; set; }

        /// <summary>
        /// Descuento aplicado al ítem.
        /// </summary>
        public int? descuento { get; set; }

        /// <summary>
        /// Anticipo aplicado al ítem.
        /// </summary>
        public int? anticipo { get; set; }

        /// <summary>
        /// Código de país de origen del ítem.
        /// </summary>
        public string? pais { get; set; }

        /// <summary>
        /// Descripción del país de origen.
        /// </summary>
        public string? paisDescripcion { get; set; }

        /// <summary>
        /// Tolerancia permitida para el ítem.
        /// </summary>
        public int? tolerancia { get; set; }

        /// <summary>
        /// Cantidad de tolerancia permitida.
        /// </summary>
        public int? toleranciaCantidad { get; set; }

        /// <summary>
        /// Porcentaje de tolerancia permitida.
        /// </summary>
        public int? toleranciaPorcentaje { get; set; }

        /// <summary>
        /// CDC de anticipo relacionado al ítem.
        /// </summary>
        public string? cdcAnticipo { get; set; }

        /// <summary>
        /// Datos DNCP específicos del ítem.
        /// </summary>
        public Dncp1? dncp { get; set; }

        /// <summary>
        /// Tipo de IVA aplicado al ítem.
        /// </summary>
        public int? ivaTipo { get; set; }

        /// <summary>
        /// Base imponible de IVA.
        /// </summary>
        public int? ivaBase { get; set; }

        /// <summary>
        /// Monto de IVA aplicado.
        /// </summary>
        public int? iva { get; set; }

        /// <summary>
        /// Lote del producto.
        /// </summary>
        public string? lote { get; set; }

        /// <summary>
        /// Fecha de vencimiento del producto.
        /// </summary>
        public string? vencimiento { get; set; }

        /// <summary>
        /// Número de serie del producto.
        /// </summary>
        public string? numeroSerie { get; set; }

        /// <summary>
        /// Número de pedido relacionado.
        /// </summary>
        public string? numeroPedido { get; set; }

        /// <summary>
        /// Número de seguimiento relacionado.
        /// </summary>
        public string? numeroSeguimiento { get; set; }

        /// <summary>
        /// Datos del importador si corresponde.
        /// </summary>
        public Importador? importador { get; set; }

        /// <summary>
        /// Registro SENAVE del producto.
        /// </summary>
        public string? registroSenave { get; set; }

        /// <summary>
        /// Registro de entidad comercial del producto.
        /// </summary>
        public string? registroEntidadComercial { get; set; }

        /// <summary>
        /// Datos del sector automotor si corresponde.
        /// </summary>
        public Sectorautomotor? sectorAutomotor { get; set; }
    }
    public class Sectorenergiaelectrica
    {
        /// <summary>
        /// Número de medidor de energía eléctrica.
        /// </summary>
        public string? numeroMedidor { get; set; }

        /// <summary>
        /// Código de actividad de energía eléctrica.
        /// </summary>
        public int? codigoActividad { get; set; }

        /// <summary>
        /// Código de categoría de consumo.
        /// </summary>
        public string? codigoCategoria { get; set; }

        /// <summary>
        /// Lectura anterior del medidor.
        /// </summary>
        public int? lecturaAnterior { get; set; }

        /// <summary>
        /// Lectura actual del medidor.
        /// </summary>
        public int? lecturaActual { get; set; }
    }

    public class Sectorseguros
    {
        /// <summary>
        /// Código de la aseguradora.
        /// </summary>
        public string? codigoAseguradora { get; set; }

        /// <summary>
        /// Código de la póliza.
        /// </summary>
        public string? codigoPoliza { get; set; }

        /// <summary>
        /// Número de la póliza.
        /// </summary>
        public string? numeroPoliza { get; set; }

        /// <summary>
        /// Vigencia de la póliza.
        /// </summary>
        public int? vigencia { get; set; }

        /// <summary>
        /// Unidad de vigencia (días, meses, años).
        /// </summary>
        public string? vigenciaUnidad { get; set; }

        /// <summary>
        /// Fecha de inicio de vigencia.
        /// </summary>
        public string? inicioVigencia { get; set; }

        /// <summary>
        /// Fecha de fin de vigencia.
        /// </summary>
        public string? finVigencia { get; set; }

        /// <summary>
        /// Código interno del ítem asegurado.
        /// </summary>
        public string? codigoInternoItem { get; set; }
    }

    public class Sectorsupermercados
    {
        /// <summary>
        /// Nombre del cajero que realizó la operación.
        /// </summary>
        public string? nombreCajero { get; set; }

        /// <summary>
        /// Monto pagado en efectivo.
        /// </summary>
        public int? efectivo { get; set; }

        /// <summary>
        /// Monto de vuelto entregado.
        /// </summary>
        public int? vuelto { get; set; }

        /// <summary>
        /// Monto de donación realizada.
        /// </summary>
        public int? donacion { get; set; }

        /// <summary>
        /// Descripción de la donación.
        /// </summary>
        public string? donacionDescripcion { get; set; }
    }

    public class Sectoradicional
    {
        /// <summary>
        /// Ciclo de facturación o servicio.
        /// </summary>
        public string? ciclo { get; set; }

        /// <summary>
        /// Fecha de inicio del ciclo.
        /// </summary>
        public string? inicioCiclo { get; set; }

        /// <summary>
        /// Fecha de fin del ciclo.
        /// </summary>
        public string? finCiclo { get; set; }

        /// <summary>
        /// Fecha de vencimiento de pago.
        /// </summary>
        public string? vencimientoPago { get; set; }

        /// <summary>
        /// Número de contrato asociado.
        /// </summary>
        public string? numeroContrato { get; set; }

        /// <summary>
        /// Saldo anterior del contrato o servicio.
        /// </summary>
        public int? saldoAnterior { get; set; }
    }

    public class Detalletransporte
    {
        /// <summary>
        /// Tipo de transporte utilizado.
        /// </summary>
        public int? tipo { get; set; }

        /// <summary>
        /// Modalidad del transporte.
        /// </summary>
        public int? modalidad { get; set; }

        /// <summary>
        /// Tipo de responsable del transporte.
        /// </summary>
        public int? tipoResponsable { get; set; }

        /// <summary>
        /// Condición de negociación del transporte.
        /// </summary>
        public string? condicionNegociacion { get; set; }

        /// <summary>
        /// Número de manifiesto de carga.
        /// </summary>
        public string? numeroManifiesto { get; set; }

        /// <summary>
        /// Número de despacho de importación.
        /// </summary>
        public string? numeroDespachoImportacion { get; set; }

        /// <summary>
        /// Fecha estimada de inicio del traslado.
        /// </summary>
        public string? inicioEstimadoTranslado { get; set; }

        /// <summary>
        /// Fecha estimada de fin del traslado.
        /// </summary>
        public string? finEstimadoTranslado { get; set; }

        /// <summary>
        /// Código de país de destino.
        /// </summary>
        public string? paisDestino { get; set; }

        /// <summary>
        /// Nombre del país de destino.
        /// </summary>
        public string? paisDestinoNombre { get; set; }

        /// <summary>
        /// Datos de salida del transporte.
        /// </summary>
        public Salida? salida { get; set; }

        /// <summary>
        /// Datos de entrega del transporte.
        /// </summary>
        public Entrega1? entrega { get; set; }

        /// <summary>
        /// Datos del vehículo utilizado.
        /// </summary>
        public Vehiculo? vehiculo { get; set; }

        /// <summary>
        /// Datos del transportista responsable.
        /// </summary>
        public Transportista? transportista { get; set; }
    }

    public class Complementarios
    {
        /// <summary>
        /// Número de orden de compra asociada.
        /// </summary>
        public string? ordenCompra { get; set; }

        /// <summary>
        /// Número de orden de venta asociada.
        /// </summary>
        public string? ordenVenta { get; set; }

        /// <summary>
        /// Número de asiento contable asociado.
        /// </summary>
        public string? numeroAsiento { get; set; }

        /// <summary>
        /// Datos de carga complementaria.
        /// </summary>
        public Carga? carga { get; set; }
    }

    public class Documentoasociado
    {
        /// <summary>
        /// Formato del documento asociado.
        /// </summary>
        public int? formato { get; set; }

        /// <summary>
        /// Código CDC del documento asociado.
        /// </summary>
        public string? cdc { get; set; }

        /// <summary>
        /// Tipo de documento asociado.
        /// </summary>
        public int? tipo { get; set; }

        /// <summary>
        /// Número de timbrado del documento asociado.
        /// </summary>
        public string? timbrado { get; set; }

        /// <summary>
        /// Código de establecimiento del documento asociado.
        /// </summary>
        public string? establecimiento { get; set; }

        /// <summary>
        /// Código de punto de expedición del documento asociado.
        /// </summary>
        public string? punto { get; set; }

        /// <summary>
        /// Número del documento asociado.
        /// </summary>
        public string? numero { get; set; }

        /// <summary>
        /// Fecha del documento asociado.
        /// </summary>
        public string? fecha { get; set; }

        /// <summary>
        /// Número de retención asociado.
        /// </summary>
        public string? numeroRetencion { get; set; }

        /// <summary>
        /// Resolución de crédito fiscal asociada.
        /// </summary>
        public string? resolucionCreditoFiscal { get; set; }

        /// <summary>
        /// Tipo de constancia asociada.
        /// </summary>
        public int? constanciaTipo { get; set; }

        /// <summary>
        /// Número de constancia asociada.
        /// </summary>
        public int? constanciaNumero { get; set; }

        /// <summary>
        /// Código de control de constancia asociada.
        /// </summary>
        public string? constanciaControl { get; set; }
    }
    public class Salida
    {
        /// <summary>
        /// Dirección de salida del transporte.
        /// </summary>
        public string? direccion { get; set; }

        /// <summary>
        /// Número de casa en la dirección de salida.
        /// </summary>
        public string? numeroCasa { get; set; }

        /// <summary>
        /// Complemento adicional de la dirección de salida (línea 1).
        /// </summary>
        public string? complementoDireccion1 { get; set; }

        /// <summary>
        /// Complemento adicional de la dirección de salida (línea 2).
        /// </summary>
        public string? complementoDireccion2 { get; set; }

        /// <summary>
        /// Código de departamento de salida.
        /// </summary>
        public int? departamento { get; set; }

        /// <summary>
        /// Descripción del departamento de salida.
        /// </summary>
        public string? departamentoDescripcion { get; set; }

        /// <summary>
        /// Código de distrito de salida.
        /// </summary>
        public int? distrito { get; set; }

        /// <summary>
        /// Descripción del distrito de salida.
        /// </summary>
        public string? distritoDescripcion { get; set; }

        /// <summary>
        /// Código de ciudad de salida.
        /// </summary>
        public int? ciudad { get; set; }

        /// <summary>
        /// Descripción de la ciudad de salida.
        /// </summary>
        public string? ciudadDescripcion { get; set; }

        /// <summary>
        /// Código de país de salida.
        /// </summary>
        public string? pais { get; set; }

        /// <summary>
        /// Descripción del país de salida.
        /// </summary>
        public string? paisDescripcion { get; set; }

        /// <summary>
        /// Teléfono de contacto en la salida.
        /// </summary>
        public string? telefonoContacto { get; set; }
    }

    public class Entrega1
    {
        /// <summary>
        /// Dirección de entrega del transporte.
        /// </summary>
        public string? direccion { get; set; }

        /// <summary>
        /// Número de casa en la dirección de entrega.
        /// </summary>
        public string? numeroCasa { get; set; }

        /// <summary>
        /// Complemento adicional de la dirección de entrega (línea 1).
        /// </summary>
        public string? complementoDireccion1 { get; set; }

        /// <summary>
        /// Complemento adicional de la dirección de entrega (línea 2).
        /// </summary>
        public string? complementoDireccion2 { get; set; }

        /// <summary>
        /// Código de departamento de entrega.
        /// </summary>
        public int? departamento { get; set; }

        /// <summary>
        /// Descripción del departamento de entrega.
        /// </summary>
        public string? departamentoDescripcion { get; set; }

        /// <summary>
        /// Código de distrito de entrega.
        /// </summary>
        public int? distrito { get; set; }

        /// <summary>
        /// Descripción del distrito de entrega.
        /// </summary>
        public string? distritoDescripcion { get; set; }

        /// <summary>
        /// Código de ciudad de entrega.
        /// </summary>
        public int? ciudad { get; set; }

        /// <summary>
        /// Descripción de la ciudad de entrega.
        /// </summary>
        public string? ciudadDescripcion { get; set; }

        /// <summary>
        /// Código de país de entrega.
        /// </summary>
        public string? pais { get; set; }

        /// <summary>
        /// Descripción del país de entrega.
        /// </summary>
        public string? paisDescripcion { get; set; }

        /// <summary>
        /// Teléfono de contacto en la entrega.
        /// </summary>
        public string? telefonoContacto { get; set; }
    }

    public class Vehiculo
    {
        /// <summary>
        /// Tipo de vehículo utilizado en el transporte.
        /// </summary>
        public string? tipo { get; set; }

        /// <summary>
        /// Marca del vehículo.
        /// </summary>
        public string? marca { get; set; }

        /// <summary>
        /// Tipo de documento del vehículo.
        /// </summary>
        public int? documentoTipo { get; set; }

        /// <summary>
        /// Número de documento del vehículo.
        /// </summary>
        public string? documentoNumero { get; set; }

        /// <summary>
        /// Observaciones sobre el vehículo.
        /// </summary>
        public string? obs { get; set; }

        /// <summary>
        /// Número de matrícula del vehículo.
        /// </summary>
        public string? numeroMatricula { get; set; }

        /// <summary>
        /// Número de vuelo si corresponde (transporte aéreo).
        /// </summary>
        public int? numeroVuelo { get; set; }
    }

    public class Transportista
    {
        /// <summary>
        /// Indica si el transportista es contribuyente.
        /// </summary>
        public bool? contribuyente { get; set; }

        /// <summary>
        /// Nombre del transportista.
        /// </summary>
        public string? nombre { get; set; }

        /// <summary>
        /// RUC del transportista.
        /// </summary>
        public string? ruc { get; set; }

        /// <summary>
        /// Tipo de documento del transportista.
        /// </summary>
        public int? documentoTipo { get; set; }

        /// <summary>
        /// Número de documento del transportista.
        /// </summary>
        public string? documentoNumero { get; set; }

        /// <summary>
        /// Dirección del transportista.
        /// </summary>
        public string? direccion { get; set; }

        /// <summary>
        /// Observaciones sobre el transportista.
        /// </summary>
        public int? obs { get; set; }

        /// <summary>
        /// Código de país del transportista.
        /// </summary>
        public string? pais { get; set; }

        /// <summary>
        /// Descripción del país del transportista.
        /// </summary>
        public string? paisDescripcion { get; set; }

        /// <summary>
        /// Datos del chofer responsable.
        /// </summary>
        public Chofer? chofer { get; set; }

        /// <summary>
        /// Datos del agente responsable.
        /// </summary>
        public Agente? agente { get; set; }
    }

    public class Chofer
    {
        /// <summary>
        /// Número de documento del chofer.
        /// </summary>
        public string? documentoNumero { get; set; }

        /// <summary>
        /// Nombre del chofer.
        /// </summary>
        public string? nombre { get; set; }

        /// <summary>
        /// Dirección del chofer.
        /// </summary>
        public string? direccion { get; set; }
    }

    public class Agente
    {
        /// <summary>
        /// Nombre del agente responsable.
        /// </summary>
        public string? nombre { get; set; }

        /// <summary>
        /// RUC del agente responsable.
        /// </summary>
        public string? ruc { get; set; }

        /// <summary>
        /// Dirección del agente responsable.
        /// </summary>
        public string? direccion { get; set; }
    }

    public class Carga
    {
        /// <summary>
        /// Número de orden de compra de la carga.
        /// </summary>
        public string? ordenCompra { get; set; }

        /// <summary>
        /// Número de orden de venta de la carga.
        /// </summary>
        public string? ordenVenta { get; set; }

        /// <summary>
        /// Número de asiento contable de la carga.
        /// </summary>
        public string? numeroAsiento { get; set; }
    }

    public class Dncp1
    {
        /// <summary>
        /// Código de nivel general DNCP.
        /// </summary>
        public string? codigoNivelGeneral { get; set; }

        /// <summary>
        /// Código de nivel específico DNCP.
        /// </summary>
        public string? codigoNivelEspecifico { get; set; }

        /// <summary>
        /// Código GTIN del producto DNCP.
        /// </summary>
        public string? codigoGtinProducto { get; set; }

        /// <summary>
        /// Código de nivel de paquete DNCP.
        /// </summary>
        public string? codigoNivelPaquete { get; set; }
    }

    public class Importador
    {
        /// <summary>
        /// Nombre del importador.
        /// </summary>
        public string? nombre { get; set; }

        /// <summary>
        /// Dirección del importador.
        /// </summary>
        public string? direccion { get; set; }

        /// <summary>
        /// Número de registro del importador.
        /// </summary>
        public string? registroImportador { get; set; }
    }

    public class Sectorautomotor
    {
        /// <summary>
        /// Tipo de vehículo automotor.
        /// </summary>
        public int? tipo { get; set; }

        /// <summary>
        /// Número de chasis del vehículo.
        /// </summary>
        public string? chasis { get; set; }

        /// <summary>
        /// Color del vehículo.
        /// </summary>
        public string? color { get; set; }

        /// <summary>
        /// Potencia del motor del vehículo.
        /// </summary>
        public int? potencia { get; set; }

        /// <summary>
        /// Capacidad del motor (cilindrada).
        /// </summary>
        public int? capacidadMotor { get; set; }

        /// <summary>
        /// Capacidad de pasajeros del vehículo.
        /// </summary>
        public int? capacidadPasajeros { get; set; }

        /// <summary>
        /// Peso bruto del vehículo.
        /// </summary>
        public int? pesoBruto { get; set; }

        /// <summary>
        /// Peso neto del vehículo.
        /// </summary>
        public int? pesoNeto { get; set; }

        /// <summary>
        /// Tipo de combustible utilizado.
        /// </summary>
        public int? tipoCombustible { get; set; }

        /// <summary>
        /// Descripción del tipo de combustible.
        /// </summary>
        public string? tipoCombustibleDescripcion { get; set; }

        /// <summary>
        /// Número de motor del vehículo.
        /// </summary>
        public string? numeroMotor { get; set; }

        /// <summary>
        /// Capacidad de tracción del vehículo.
        /// </summary>
        public float? capacidadTraccion { get; set; }

        /// <summary>
        /// Año de fabricación del vehículo.
        /// </summary>
        public int? año { get; set; }

        /// <summary>
        /// Tipo de vehículo automotor.
        /// </summary>
        public string? tipoVehiculo { get; set; }

        /// <summary>
        /// Cilindrada del motor.
        /// </summary>
        public string? cilindradas { get; set; }
    }
    public class Config
    {
        public bool defaultValues { get; set; }
        public int decimals { get; set; }
        public int taxDecimals { get; set; }
        public int pygDecimals { get; set; }
        public bool userObjectRemove { get; set; }
    }
}