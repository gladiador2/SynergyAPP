using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA_app.Services
{
    public class Funciones 
    {
        public static string SepararMiles(string numero)
        {
            if (string.IsNullOrEmpty(numero))
                return numero;

            int puntoDecimal = numero.IndexOf('.');
            int inicio = (puntoDecimal >= 0) ? puntoDecimal : numero.Length;
            int contador = 0;
            string resultado = "";

            for (int i = inicio - 1; i >= 0; i--)
            {
                resultado = numero[i] + resultado;
                contador++;

                if (contador == 3 && i > 0)
                {
                    resultado = "." + resultado;
                    contador = 0;
                }
            }

            if (puntoDecimal >= 0)
                resultado += numero.Substring(puntoDecimal);

            return resultado;
        }

        public static string SepararMilesYDecimales(string numero)
        {
            if (string.IsNullOrEmpty(numero))
                return numero;

            decimal valorDecimal;

            if (!decimal.TryParse(numero, out valorDecimal))
                return numero;

            return valorDecimal.ToString("#,##0.00");
        }
        
    }

    
   
}
