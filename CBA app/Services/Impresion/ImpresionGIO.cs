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


namespace CBA_app.Services.Impresion
{
   
    public class ImpresionGIO : ImpresionesServiceBase
    {
        readonly GioRequest request = new();

        #region Propiedades de la clase
        protected Hashtable campos = new Hashtable();

        public bool incluirLogo { get; set; }
        public decimal vCasoId { get; set; }
        private int pageWidth = 260;
        private int pageHeight = 6000;
        private string lineaConGuiones = new string('-', 83);//n guiones
        private int posXInicial = 1;

        private PuertaMovimientoInfoCompl gio;
        private List<ModeloContenedor.Contenedor> datosWS;

        #region estandarizar espacios
        public int espacioEntreLineas = 4;
        public int espacioPequenho = 12;
        public int espacioMediano = 13;
        public int espacioNormal = 17;
        public int espacioGrande = 22;
        public int espacioXL = 27;
        public int espacioXXL = 37;
        #endregion estandarizar espacios

        public int posX, posY; // Variables para controlar la posición de impresión en la página

        public int enmedio = 140;
        #endregion Propiedades de la clase
        public ImpresionGIO(PuertaMovimientoInfoCompl GIO, List<ModeloContenedor.Contenedor> contenedors)
        {
            gio = GIO;
            datosWS = contenedors;
        }

        

        public DataTable ConvertToDataTable(PuertaMovimientoInfoCompl gio)
        {
            DataTable dataTable = new DataTable();

            // Obtener todas las propiedades del objeto
            PropertyInfo[] properties = typeof(PuertaMovimientoInfoCompl).GetProperties();

            // Crear columnas en el DataTable basadas en las propiedades del objeto
            foreach (PropertyInfo property in properties)
            {
                dataTable.Columns.Add(property.Name, property.PropertyType);
            }

            // Crear una nueva fila en el DataTable
            DataRow row = dataTable.NewRow();

            // Asignar los valores de las propiedades del objeto a la fila
            foreach (PropertyInfo property in properties)
            {
                row[property.Name] = property.GetValue(gio) ?? DBNull.Value;
            }

            // Agregar la fila al DataTable
            dataTable.Rows.Add(row);

            return dataTable;
        }




        #region formato a numeros
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


        public static string formatearNumero(int entero, string cadenaDecimales)
        {
            decimal valor = decimal.Parse(entero.ToString());
            if (valor == 0)
                return "0";

            return Math.Round(valor).ToString(cadenaDecimales);
        }
        #endregion formato a numeros

        #region imprimir
        public void imprimir()
        {

            CargarCampos();
            imprimeTicket();
        }
        #endregion imprimir
        public void CargarCampos()
        {
            
        }
        #region imprimeTicket
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
                        width = 100, // Ajusta el tamaño según sea necesario
                        height = 100 // Ajusta el tamaño según sea necesario
                    }
                };
                // Lista para almacenar las líneas de impresión
                var lineas = new List<ModeloImpresion.Lineaimpresion>();
                string campo = string.Empty;
                #region configuracion para Epson TM-U220
                float linea = 0;
                
                string titulo = string.Empty;
               
                int offset = 2;
                AgregarAlineadoCentro(lineas, "Puerto Seguro Fluvial S.A.", typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                AgregarAlineadoCentro(lineas, "RUC:  80067871-0", typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                AgregarAlineadoCentro(lineas, "Ruta Villeta Alberdi km. 4,5", typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                AgregarAlineadoCentro(lineas, "Tel: +595212381627", typeof(FuenteBase), posX, posY); posY += espacioPequenho;


                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                AgregarAlineadoCentro(lineas, "RECIBO DE INTERCAMBIO DE EQUIPO", typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                
                campo = campos["movimiento"].ToString() + " - " + campos["comprobante"].ToString();
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;



                campo = "Fecha y hora: "+ campos["fecha_movimiento"].ToString();
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                if (campos.Contains("cliente"))
                {
                    campo = "Cliente: " + campos["cliente"];
                    AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, 2 * offset); posY += espacioPequenho;
                }

                if (campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GOE ||
                    campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GIF)
                {
                    campo = "Booking: " + campos["booking"];
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                    


                }
                if (campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GIF)
                {
                    if (campos.Contains("puerto_nombre"))
                    {
                        campo = "Puerto Devolución: " + campos["puerto_nombre"];
                        AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                    }
                }
                if (campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GIE ||
                   campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GIF || campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GIF)
                {
                    campo = "Remision: " + campos["remision"];
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                }


                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                //Datos del vehiculo
                campo = "DATOS DEL VEHÍCULO";
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho; AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                campo = "RUA Camión: " + campos["rua_tracto"];
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                if (campos.Contains("rua_carreta"))
                {
                    campo = "RUA Carreta: " + campos["rua_carreta"];
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                }

                campo = "Chófer: " + campos["chofer_doc"] + "-" + campos["chofer_nom"];
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                //Datos del contendor
                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                campo = "DATOS DEL CONTENEDOR";
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                campo = "Número: " + campos["nro_contenedor"];
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Tipo: " + campos["tipo_contenedor"];
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Linea: " + campos["linea"];
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Agencia: " + campos["agencia"];
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                if (campos.Contains("puerto"))
                {
                    if (campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GIE ||
                    campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GIF)
                    {
                        campo = "Puerto Origen: " + campos["puerto"];
                        AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                    }
                    else
                    {
                        campo = "Puerto Devolución: " + campos["puerto"];
                        AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                    }

                }
                if (campos.Contains("devolucion"))
                {
                    campo = "Fecha Devolucion: " + campos["devolucion"];
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                }

                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                campo = string.Empty;
                bool imprmirLinea = false;
                if (campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GIF ||
                    campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GOF ||
                    campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GOE)
                {    //Precintos

                    if (campos.Contains("precinto1") || campos.Contains("precinto2") || campos.Contains("precinto3") ||
                        campos.Contains("precinto4") || campos.Contains("precinto5") || campos.Contains("precinto_ventilete"))
                    {
                        campo = "PRECINTOS";
                        AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho; AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                        imprmirLinea = true;
                    }
                    if (campos.Contains("precinto1"))
                    {
                        campo = "Precinto 1: " + campos["precinto1"].ToString();
                        AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                    }


                    if (campos.Contains("precinto2"))
                    {
                        campo = "Precinto 2: " + campos["precinto2"].ToString();
                        AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                    }

                    if (campos.Contains("precinto3"))
                    {
                        campo = "Precinto 3: " + campos["precinto3"].ToString();
                        AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                    }


                    if (campos.Contains("precinto4"))
                    {
                        campo = "Precinto 4: " + campos["precinto4"].ToString();
                        AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                    }


                    if (campos.Contains("precinto5"))
                    {
                        campo = "Precinto 5: " + campos["precinto5"].ToString();
                        AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                    }

                    if (campos.Contains("precinto_ventilete"))
                    {
                        campo = "Precinto Ventilete: " + campos["precinto_ventilete"].ToString();
                        AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                    }
                }
                if (campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GIF ||
                   campos["movimiento"].ToString() == Definiciones.MovimientosContenedores.GOF)
                {
                    if (imprmirLinea)
                        AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                    //Datos de la báscula
                    campo = "DATOS DE BÁSCULA";
                    AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                    AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                    campo = "Payload Contenedor: ";
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY);
                    campo = campos["payload_contenedor"].ToString();
                    AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                    campo = "Peso Bruto: ";
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY);
                    campo = campos["peso_bruto"].ToString();
                    AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                    campo = "Tara Contenedor: ";
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); 
                    campo = campos["tara_contenedor"].ToString();
                    AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                    campo = "Tara Camión: ";
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); 
                    campo = campos["tara_camion"].ToString();
                    AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                    campo = "Peso Neto: ";
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); 
                    campo = campos["peso_neto"].ToString();
                    AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                }
                else
                {
                    if (imprmirLinea)
                        AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                    campo = "DATOS DE BÁSCULA";
                    AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                   AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                    campo = "Payload Contenedor: ";
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY);
                    campo = campos["payload_contenedor"].ToString();
                    AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                    campo = "Tara Contenedor: ";
                    AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); 
                    campo = campos["tara_contenedor"].ToString();
                    AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                }

                //Observaciones
                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                bool imprimirLinea = false;
                if (campos.Contains("observaciones"))
                {
                    imprimirLinea = true;
                    campo = "Observaciones: " + campos["observaciones"];
                    AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, pageWidth); posY += espacioPequenho;
                }

                if (campos.Contains("eir_fondo"))
                {
                    imprimirLinea = true;
                    campo = "Fondo: " + campos["eir_fondo"].ToString().Replace(",", ", ");
                    AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, pageWidth); posY += espacioPequenho;
                }

                if (campos.Contains("eir_panel_derecho"))
                {
                    imprimirLinea = true;
                    campo = "Panel Derecho: " + campos["eir_panel_derecho"].ToString().Replace(",", ", ");
                    AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, pageWidth); posY += espacioPequenho;
                }

                if (campos.Contains("eir_panel_izquierdo"))
                {
                    imprimirLinea = true;
                    campo = "Panel Izquierdo: " + campos["eir_panel_izquierdo"].ToString().Replace(",", ", ");
                    AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, pageWidth); posY += espacioPequenho;
                }

                if (campos.Contains("eir_piso"))
                {
                    imprimirLinea = true;
                    campo = "Piso: " + campos["eir_piso"].ToString().Replace(",", ", ");
                    AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, pageWidth); posY += espacioPequenho;
                }

                if (campos.Contains("eir_puerta"))
                {
                    string puerta = campos["eir_puerta"].ToString();
                    string[] puertas = puerta.Split('-');
                    if (puertas.Length > 0)
                    {
                        if (!puertas[0].Equals(string.Empty))
                        {
                            imprimirLinea = true;

                            campo = "Puerta Derecha: " + puertas[0].Replace(",", ", ");
                            AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, pageWidth); posY += espacioPequenho;
                        }
                    }

                    if (puertas.Length > 1)
                    {
                        if (!puertas[1].Equals(string.Empty))
                        {
                            imprimirLinea = true;
                            campo = "Puerta Izquierda: " + puertas[1].Replace(",", ", ");
                            AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, pageWidth); posY += espacioPequenho;
                        }
                    }

                }

                if (campos.Contains("refrigerado"))
                {
                    imprimirLinea = true;
                    campo = "Reefer: " + campos["refrigerado"].ToString().Replace(",", ", ");
                    AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, pageWidth); posY += espacioPequenho;
                }
                if (campos.Contains("eir_techo"))
                {
                    imprimirLinea = true;
                    campo = "Techo: " + campos["eir_techo"].ToString().Replace(",", ", ");
                    AgregarMultilinea(lineas, campo, typeof(FuenteBase), posX, ref posY, pageWidth); posY += espacioPequenho;
                }
                if (imprimirLinea)
                    AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;

                //Firmas
                campo = "Firmas y Sellos";
               AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                AgregarLineaPunteada(lineas, posX, posY); posY += espacioXXL + espacioXXL;
                linea += 4;
                campo = "--------------------";
               AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                campo = "GIO";
               AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioXXL+ espacioXXL;
                linea += 6;
                campo = "--------------------";
               AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                campo = "Transportista";
               AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioXXL + espacioXXL;
                //Leyenda
                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                campo = "........**IMPORTANTE**........";
               AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                campo = "NO SE ACEPTAN RECLAMOS POSTERIORES";
               AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                campo = "A LA SALIDA DEL TRANSPORTE DEL";
               AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                campo = "PUERTO, POR LO TANTO SE ACEPTAN";
               AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;
                campo = "LOS DATOS DE ESTE DOCUMENTO";
               AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                campo = "Usuario: " + campos["usuario"].ToString();
                AgregarAlineadoCentro(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                //Copias
                AgregarLineaPunteada(lineas, posX, posY); posY += espacioPequenho;
                campo = "Original: GIO";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Duplicado: Cliente";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                campo = "Triplicado: Porteria";
                AgregarAlineadoIzqquierda(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                //es necesario agregar este campos porque en Windows el driver no resetea automaticamente el cabezal de la impresora
                //linea += 6;
                //AgregarAlineadoDerecha(lineas, campo, typeof(FuenteBase), posX, posY); posY += espacioPequenho;

                #endregion configuracion para Epson LX-350

                // Asignar la lista de líneas de impresión al objeto
                objetoImpresion.lineaImpresion = lineas.ToArray();

                // Enviar el objeto a la API REST
                EnviarObjetoImpresion(objetoImpresion, gio.MOVIMIENTO);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion imprimeTicket

        public async void EnviarObjetoImpresion(ModeloImpresion.ObjetoImpresion objetoImpresion, string movimiento)
        {
            var jsonData = new Dictionary<string, object> { };
            if (movimiento == Definiciones.MovimientosContenedores.GIF || movimiento == Definiciones.MovimientosContenedores.GIE)
                jsonData.Add("id", int.Parse(Definiciones.VariablesDeSistema.IPimpresoraGioEntrada.ToString()));
            else
                jsonData.Add("id", (int)Definiciones.VariablesDeSistema.IPimpresoraGioSalida);

            string IP = await request.GetVariableSistema(jsonData);


            bool exito = await request.imprimir(objetoImpresion, IP);
            if (exito)
            {
                await Toast.Make("Imprimiendo...", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            }
            else
                await Toast.Make("No pudo imprimir", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();

        }
    }
}
