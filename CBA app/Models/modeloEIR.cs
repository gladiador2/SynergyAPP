using CBA_app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA_app.Models
{
    public class modeloEIR
    {
        #region Clase de Detalles EIR
        public class DetallesEIR
        {
            public decimal IdEIR { get; set; }
            public decimal IdDetalle { get; set; }
            public string Numero { get; set; }
            public string Tipo { get; set; }
            public string Estado { get; set; }
            public decimal Tara { get; set; }
            public decimal Payload { get; set; }
            public decimal SetPoint { get; set; }
            public string Precinto1 { get; set; }
            public string Precinto2 { get; set; }
            public string Precinto3 { get; set; }
            public string Precinto4 { get; set; }
            public string Precinto5 { get; set; }
            public string Ventilete { get; set; }
            public string EIRPrecinto1 { get; set; }
            public string EIRPrecinto2 { get; set; }
            public string EIRPrecinto3 { get; set; }
            public string EIRPrecinto4 { get; set; }
            public string EIRPrecinto5 { get; set; }
            public string EIRVentilete { get; set; }
            public string Fondo { get; set; }
            public string Piso { get; set; }
            public string Techo { get; set; }
            public string Izquierdo { get; set; }
            public string Derecho { get; set; }
            public string Puerta { get; set; }
            public string Refrigerado { get; set; }
            public string Observaciones { get; set; }
            public string Operativo { get; set; }

            public decimal TemperaturaIngreso { get; set; }

            public decimal BuqueColumnaId { get; set; }
            public decimal BuqueColumna { get; set; }
            public decimal BuqueEstibaId { get; set; }
            public decimal BuqueEstiba { get; set; }
            public decimal BuqueBahiaId { get; set; }
            public decimal BuqueBahia { get; set; }
            public string Codigo20 { get; set; }
            public string Codigo40 { get; set; }
            public DetallesEIR()
            {
                IdEIR = Definiciones.Error.Valor.EnteroPositivo;
                IdDetalle = Definiciones.Error.Valor.EnteroPositivo; ;
                Numero = string.Empty;
                Tipo = string.Empty;
                Estado = string.Empty;
                Tara = 0;
                Payload = 0;
                SetPoint = 0;
                Precinto1 = string.Empty;
                Precinto2 = string.Empty;
                Precinto3 = string.Empty;
                Precinto4 = string.Empty;
                Precinto5 = string.Empty;
                Ventilete = string.Empty;
                EIRPrecinto1 = string.Empty;
                EIRPrecinto2 = string.Empty;
                EIRPrecinto3 = string.Empty;
                EIRPrecinto4 = string.Empty;
                EIRPrecinto5 = string.Empty;
                EIRVentilete = string.Empty;
                Fondo = string.Empty;
                Piso = string.Empty;
                Techo = string.Empty;
                Izquierdo = string.Empty;
                Derecho = string.Empty;
                Puerta = string.Empty;
                Refrigerado = string.Empty;
                Observaciones = string.Empty;
                Operativo = Definiciones.SiNo.No;
                TemperaturaIngreso = 0;

                BuqueColumnaId = Definiciones.Error.Valor.EnteroPositivo; ;
                BuqueColumna = Definiciones.Error.Valor.EnteroPositivo; ;
                BuqueEstibaId = Definiciones.Error.Valor.EnteroPositivo; ;
                BuqueEstiba = Definiciones.Error.Valor.EnteroPositivo; ;
                BuqueBahiaId = Definiciones.Error.Valor.EnteroPositivo; ;
                BuqueBahia = Definiciones.Error.Valor.EnteroPositivo; ;
                Codigo20 = string.Empty;
                Codigo40 = string.Empty;

            }

        }
        #endregion Clase de Detalles EIR
    }
}
