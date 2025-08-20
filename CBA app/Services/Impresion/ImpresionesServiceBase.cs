using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.Json;
using CBA_app.Models;
using CommunityToolkit.Maui.Alerts;
using System.Text.Json.Nodes;
using CBA_app.Request;
using System.Drawing;


namespace CBA_app.Services.Impresion
{
    public class ImpresionesServiceBase
    {
        readonly GioRequest request = new();
        private string lineaConGuiones = new string('-', 83);//n guiones

        public enum PosicionX
        {
            PrincipioDePagina = 0,          // 0
            UnCuartoDePagina = 280 / 4,     // 70
            MediaPagina = 280 / 2,          // 140
            TresCuartosDePagina = 280 * 3 / 4 // 210
        }

        #region EnviarObjetoImpresion
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
        #endregion

        #region agregarL linea
        public void AgregarLineaImpresion(List<ModeloImpresion.Lineaimpresion> lineas, string campo, string fuente, float fuenteTamaño, string color, int posX, int posY, bool negrita, bool esMultilinea, bool alinearDerecha, bool alinearIzquierda, bool centrado)
        {
            var lineaImpresion = new ModeloImpresion.Lineaimpresion
            {
                Campo = campo == string.Empty ? " " : campo,
                fuente = fuente,
                fuenteTamaño = fuenteTamaño,
                color = color,
                posX = posX,
                posY = posY,
                Negrita = negrita,
                esMultilinea = esMultilinea,
                AlinearDerecha = alinearDerecha,
                AlinearIzquierda = alinearIzquierda,
                Centrado = centrado
            };

            lineas.Add(lineaImpresion);
        }
        #endregion

        #region alineado derecha
        public void AgregarAlineadoDerecha(List<ModeloImpresion.Lineaimpresion> lineas, string campo, Type Tipofuente, int posX, int posY)
        {
            string fuente = string.Empty;
            float fuenteTamanho = 0;
            bool negrita = false;
            string color = "Black";
            if (Tipofuente == typeof(FuenteBase))
            {
                FuenteBase AplicarFuente = new FuenteBase();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            else if (Tipofuente == typeof(FuenteNegrita))
            {
                FuenteNegrita AplicarFuente = new FuenteNegrita();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            else if (Tipofuente == typeof(FuenteConEspaciado))
            {
                FuenteConEspaciado AplicarFuente = new FuenteConEspaciado();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            AgregarLineaImpresion(lineas, campo, fuente, fuenteTamanho, color, posX, posY, negrita, false, true, false, false);
        }
        #endregion alineado izquierda
        public static List<string> DividirEnPartes(string campo, int palabrasPorParte)
        {
            // Divide el string en palabras
            string[] palabras = campo.Split(' ');

            // Lista para almacenar las partes resultantes
            List<string> partes = new List<string>();

            // Agrupa las palabras en partes de n palabras
            for (int i = 0; i < palabras.Length; i += palabrasPorParte)
            {
                // Toma las siguientes n palabras
                string[] grupo = palabras.Skip(i).Take(palabrasPorParte).ToArray();

                // Une las palabras en el grupo para formar una parte
                string parte = string.Join(" ", grupo);

                // Agrega la parte a la lista de partes
                partes.Add(parte);
            }

            return partes;
        }
        #region multilinea
        public void AgregarMultilinea(List<ModeloImpresion.Lineaimpresion> lineas, string campo, Type Tipofuente, int posX, ref int posY, int pageWidth)
        {
            // Inicialización de variables
            string fuente = string.Empty;
            float fuenteTamanho = 0;
            bool negrita = false;
            string color = "Black";
            pageWidth = pageWidth == 0 ? 280 : pageWidth; // Si el ancho de la página es 0, se establece a 280
            pageWidth = pageWidth - posX - 5;
            // Configuración de la fuente según el tipo proporcionado
            if (Tipofuente == typeof(FuenteBase))
            {
                FuenteBase AplicarFuente = new FuenteBase();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            else if (Tipofuente == typeof(FuenteNegrita))
            {
                FuenteNegrita AplicarFuente = new FuenteNegrita();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            else if (Tipofuente == typeof(FuenteConEspaciado))
            {
                FuenteConEspaciado AplicarFuente = new FuenteConEspaciado();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            //AgregarLineaImpresion(lineas, campo, fuente, fuenteTamanho, color, posX, posY, negrita, false, false, true, false);
            List<string> partes = DividirEnPartes(campo, 5);

            foreach (var linea in partes)
            {
                AgregarLineaImpresion(lineas, linea, fuente, fuenteTamanho, color, posX, posY, negrita, false, false, true, false);
                posY += 10; posX += 10;
            }
            posY -= 10;
            //// Creación de la fuente con las propiedades configuradas
            //Microsoft.Maui.Graphics.Font font = new Microsoft.Maui.Graphics.Font(fuente, (int)fuenteTamanho, FontStyleType.Normal);

            //// Medición del ancho del texto
            //using (Bitmap bmp = new System.Drawing.Bitmap(1, 1))
            //{
            //    using (Graphics g = Graphics.FromImage(bmp))
            //    {
            //        float tamanho = g.MeasureString(campo, font).Width;

            //        // Si el texto cabe en una sola línea
            //        if (tamanho <= pageWidth)
            //        {
            //            AgregarLineaImpresion(lineas, campo, fuente, fuenteTamanho, color, posX, posY, negrita, false, true, false, false);
            //            //posY += 10; // Incrementa la posición Y para la siguiente línea
            //        }
            //        else
            //        {
            //            // Si el texto no cabe en una sola línea, se divide en varias líneas
            //            string[] palabras = campo.Split(' ');
            //            List<string> lineasTexto = new List<string>();
            //            StringBuilder lineaActual = new StringBuilder();

            //            // Construcción de las líneas de texto
            //            foreach (string palabra in palabras)
            //            {
            //                if (g.MeasureString(lineaActual.ToString() + (lineaActual.Length > 0 ? " " : "") + palabra, font).Width > pageWidth)
            //                {
            //                    lineasTexto.Add(lineaActual.ToString());
            //                    lineaActual = new StringBuilder(palabra);
            //                }
            //                else
            //                {
            //                    if (lineaActual.Length > 0)
            //                    {
            //                        lineaActual.Append(" ");
            //                    }
            //                    lineaActual.Append(palabra);
            //                }
            //            }

            //            // Agrega la última línea construida
            //            if (lineaActual.Length > 0)
            //            {
            //                lineasTexto.Add(lineaActual.ToString());
            //            }

            //            // Agrega cada línea de texto a la lista de líneas de impresión
            //            foreach (string linea in lineasTexto)
            //            {
            //                AgregarLineaImpresion(lineas, linea, fuente, fuenteTamanho, color, posX, posY, negrita, false, false, true, false);
            //                posY += 10; // Incrementa la posición Y para la siguiente línea
            //                posX = (int)PosicionX.UnCuartoDePagina;
            //            }
            //        }
            //    }
            //}
        }

        #endregion multilinea

        #region alineado izquierda
        public void AgregarAlineadoIzqquierda(List<ModeloImpresion.Lineaimpresion> lineas, string campo, Type Tipofuente, int posX, int posY)
        {
            string fuente = string.Empty;
            float fuenteTamanho = 0;
            bool negrita = false;
            string color = "Black";
            if (Tipofuente == typeof(FuenteBase))
            {
                FuenteBase AplicarFuente = new FuenteBase();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            else if (Tipofuente == typeof(FuenteNegrita))
            {
                FuenteNegrita AplicarFuente = new FuenteNegrita();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            else if (Tipofuente == typeof(FuenteConEspaciado))
            {
                FuenteConEspaciado AplicarFuente = new FuenteConEspaciado();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            AgregarLineaImpresion(lineas, campo, fuente, fuenteTamanho, color, posX, posY, negrita, false, false, true, false);
        }
        #endregion

        #region alineado centro
        public void AgregarAlineadoCentro(List<ModeloImpresion.Lineaimpresion> lineas, string campo, Type Tipofuente, int posX, int posY)
        {
            string fuente = string.Empty;
            float fuenteTamanho = 0;
            bool negrita = false;
            string color = "Black";
            if (Tipofuente == typeof(FuenteBase))
            {
                FuenteBase AplicarFuente = new FuenteBase();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            else if (Tipofuente == typeof(FuenteNegrita))
            {
                FuenteNegrita AplicarFuente = new FuenteNegrita();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            else if (Tipofuente == typeof(FuenteConEspaciado))
            {
                FuenteConEspaciado AplicarFuente = new FuenteConEspaciado();
                fuenteTamanho = AplicarFuente.FuenteTamaño;
                fuente = AplicarFuente.Fuente;
                negrita = AplicarFuente.Negrita;
            }
            AgregarLineaImpresion(lineas, campo, fuente, fuenteTamanho, color, posX, posY, negrita, false, false, false, true);
        }
        #endregion

        #region linea punteda
        public void AgregarLineaPunteada(List<ModeloImpresion.Lineaimpresion> lineas, int posX, int posY)
        {
            string fuente = string.Empty;
            float fuenteTamanho = 0;
            bool negrita = false;
            string color = "Black";
            FuenteBase AplicarFuente = new FuenteBase();
            fuenteTamanho = AplicarFuente.FuenteTamaño;
            fuente = AplicarFuente.Fuente;
            negrita = AplicarFuente.Negrita;
            AgregarLineaImpresion(lineas, lineaConGuiones, fuente, fuenteTamanho, color, posX, posY, negrita, false, true, false, true);
        }
        #endregion


    }
}
