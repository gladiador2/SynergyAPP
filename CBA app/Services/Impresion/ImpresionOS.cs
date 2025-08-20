using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CBA_app.Models.GIO;
using CBA_app.Models;
using System.Reflection;
using CBA_app.Request;
using CommunityToolkit.Maui.Alerts;
using CBA_app.Models.GIO.Contenedores;
using CBA_app.Models.OrdenesServicio;

namespace CBA_app.Services.Impresion
{
    /// <summary>
    /// Servicio de impresión para Ordenes de Servicio (OS).
    /// Hereda de ImpresionesServiceBase y provee métodos para formatear e imprimir tickets de OS.
    /// </summary>
    public class ImpresionOS : ImpresionesServiceBase
    {
        #region Campos y Propiedades

        /// <summary>
        /// Solicitudes a servicios GIO.
        /// </summary>
        readonly GioRequest request = new();

        /// <summary>
        /// Lista de informes de posicionamiento de pedidos (Ordenes de Servicio).
        /// </summary>
        protected readonly List<PedidoPosicionamientoInforme> OrdenesServicio = new List<PedidoPosicionamientoInforme>();

        /// <summary>
        /// Indica si se debe incluir el logo en la impresión.
        /// </summary>
        public bool incluirLogo { get; set; }

        /// <summary>
        /// Identificador de caso.
        /// </summary>
        public decimal vCasoId { get; set; }

        private int pageWidth = 260;
        private int pageHeight = 6000;
        private string lineaConGuiones = new string('-', 83); // n guiones
        private int posXInicial = 1;

        #region Espaciados estándar para impresión
        public int espacioEntreLineas = 4;
        public int espacioPequenho = 12;
        public int espacioMediano = 13;
        public int espacioNormal = 17;
        public int espacioGrande = 22;
        public int espacioXL = 27;
        public int espacioXXL = 37;
        #endregion

        /// <summary>
        /// Posiciones de impresión en la página.
        /// </summary>
        public int posX, posY;

        /// <summary>
        /// Valor de posición horizontal centrada.
        /// </summary>
        public int enmedio = 140;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor que recibe la lista de Ordenes de Servicio a imprimir.
        /// </summary>
        /// <param name="OS">Lista de pedidos de posicionamiento.</param>
        public ImpresionOS(List<PedidoPosicionamientoInforme> OS)
        {
            OrdenesServicio = OS;
        }

        #endregion

        #region Métodos de Formateo

        /// <summary>
        /// Formatea un número a string con el formato de decimales especificado.
        /// </summary>
        /// <param name="objeto">Número a formatear.</param>
        /// <param name="cadenaDecimales">Formato de decimales.</param>
        /// <returns>String formateado.</returns>
        public static string formatearNumero(object objeto, string cadenaDecimales)
        {
            try
            {
                decimal valor = Convert.ToDecimal(objeto);
                if (valor == 0)
                    return "0";

                return Math.Round(valor).ToString(cadenaDecimales);
            }
            catch (InvalidCastException)
            {
                return objeto.ToString();
            }
        }

        /// <summary>
        /// Formatea un entero a string con el formato de decimales especificado.
        /// </summary>
        /// <param name="entero">Número entero.</param>
        /// <param name="cadenaDecimales">Formato de decimales.</param>
        /// <returns>String formateado.</returns>
        public static string formatearNumero(int entero, string cadenaDecimales)
        {
            decimal valor = decimal.Parse(entero.ToString());
            if (valor == 0)
                return "0";

            return Math.Round(valor).ToString(cadenaDecimales);
        }

        #endregion

        #region Métodos de Impresión

        /// <summary>
        /// Inicia el proceso de impresión del ticket.
        /// </summary>
        public void imprimir()
        {
            imprimeTicket();
        }

        /// <summary>
        /// Genera y envía el ticket de impresión para la Orden de Servicio.
        /// </summary>
        public void imprimeTicket()
        {
            try
            {
                // Crear una instancia de ObjetoImpresion
                var objetoImpresion = new ModeloImpresion.ObjetoImpresion
                {
                    variablesImpresion = new ModeloImpresion.Variablesimpresion
                    {
                        pageWidth = pageWidth,
                        pageHeight = pageHeight,
                        lineaConGuiones = lineaConGuiones,
                        posXInicial = posXInicial,
                        QR = null,
                        cdc = string.Empty,
                        link = string.Empty
                    },
                    lineaImpresion = new List<ModeloImpresion.Lineaimpresion>().ToArray(),
                    qr = new ModeloImpresion.Qr
                    {
                        link = string.Empty,
                        posX = 1,
                        posY = 1,
                        width = 100,
                        height = 100
                    }
                };

                PedidoPosicionamientoInforme Informe = OrdenesServicio.FirstOrDefault();
                var lineas = new List<ModeloImpresion.Lineaimpresion>();
                string campo = string.Empty;

                #region Configuración para Epson TM-U220

                float linea = 0;
                string titulo = string.Empty;
                int offset = 2;

                // Encabezado
                AgregarAlineadoCentro(lineas, "Puerto Seguro Fluvial S.A.", typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                AgregarAlineadoCentro(lineas, "RUC:  80067871-0", typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                AgregarAlineadoCentro(lineas, "Ruta Villeta Alberdi km. 4,5", typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                AgregarAlineadoCentro(lineas, "Tel: +595212381627", typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                AgregarAlineadoCentro(lineas, "Orden de Servicio", typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Fecha y hora: " + Informe.FechaHoraPosicionamiento;
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                campo = "Cliente: " + Informe.Cliente;
                AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, 2 * offset); posY += espacioPequenho;

                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                // Datos del contenedor
                AgregarAlineadoCentro(lineas, "DATOS DEL CONTENEDOR", typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                campo = "Número: ";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY);
                campo = Informe.NumeroContenedor;
                AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Línea: ";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY);
                campo = Informe.LineaNombre;
                AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Agencia: ";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY);
                campo = Informe.AgenciaNombre;
                AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Tipo: ";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY);
                campo = Informe.TipoDescripcion;
                AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Payload Contenedor: ";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY);
                campo = formatearNumero(Informe.PesoMaximo, "N0") + " kg";
                AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Tara Contenedor: ";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY);
                campo = formatearNumero(Informe.Tara, "N0") + " kg";
                AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                campo = "Servicio: " + Informe.Servicio;
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                campo = "Subservicio";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteNegrita), posX, posY);

                campo = "Cantidad";
                AgregarAlineadoDerecha(lineas, campo, typeof(FuenteNegrita), posX, posY); posY += espacioPequenho;
                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                // Detalle de subservicios
                foreach (var item in OrdenesServicio)
                {
                    campo = item.Subservicio;
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY);

                    campo = item.Cantidad.ToString();
                    AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                }
                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                posY += espacioXXL + espacioXXL;

                // Firmas y copias
                campo = "----------------------------------------";
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                campo = "Cliente";
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioXXL + espacioXXL;
                linea += 4;

                campo = "----------------------------------------";
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                campo = "Resguardo";
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioXXL + espacioXXL;
                linea += 4;

                campo = "----------------------------------------";
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                campo = "Usuario:" + Informe.Usuario;
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioXXL + espacioXXL;
                linea += 4;

                campo = "----------------------------------------";
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                campo = "Firma y Sello";
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                posY += espacioXXL + espacioXXL;
                linea += 4;

                // Copias
                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                campo = "Original: GIO";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Duplicado: Cliente";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Triplicado: Porteria";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                // Es necesario agregar este campo porque en Windows el driver no resetea automáticamente el cabezal de la impresora
                // linea += 6;
                // AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                #endregion

                // Asignar la lista de líneas de impresión al objeto
                objetoImpresion.lineaImpresion = lineas.ToArray();

                // Enviar el objeto a la API REST
                EnviarObjetoImpresion(objetoImpresion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Métodos Auxiliares

        /// <summary>
        /// Envía el objeto de impresión a la API REST y muestra un Toast con el resultado.
        /// </summary>
        /// <param name="objetoImpresion">Objeto de impresión a enviar.</param>
        public async void EnviarObjetoImpresion(ModeloImpresion.ObjetoImpresion objetoImpresion)
        {
            var jsonData = new Dictionary<string, object> { };
            jsonData.Add("id", (int)Definiciones.VariablesDeSistema.IPimpresoraGioSalida);

            string IP = await request.GetVariableSistema(jsonData);

            bool exito = await request.imprimir(objetoImpresion, IP);
            if (exito)
            {
                await Toast.Make("Imprimiendo...", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            }
            else
            {
                await Toast.Make("No pudo imprimir", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            }
        }

        #endregion
    }
}
