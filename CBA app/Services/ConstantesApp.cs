using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA_app.Services
{
    public static class ConstantesApp
    {
        
        public const string URL_API = "http://10.10.1.125:3000/";
        

        public static readonly string url = new Uri(URL_API).GetLeftPart(UriPartial.Authority);

        public const int TIEMPO_ESPERA = 5000;
        public const string RUTA_LOGS = "logs/";
        public const string FORMATO_FECHA = "yyyy-MM-dd HH:mm:ss";

        #region Hashes
        public static class Hashes
        {
            public const string EntidadesLogin = "a81e6b49-b6b8-4f8d-8b2b-1dce7611196d";

            // Articulos
            public const string ArticulosFamiliasBuscar = "bf9d5089-dfef-413f-92b4-3f0e52de8011";
            public const string ArticulosFamiliasGuardar = "b9880ced-fcca-4c3f-befa-2427d1eb3c55";
            public const string ArticulosFamiliasInactivar = "37f62c07-bb97-4050-8d8e-5cfedf0255e0";
            public const string ArticulosGruposBuscar = "d79ac858-d175-486a-a874-64b27df5dafe";
            public const string ArticulosGruposGuardar = "b85cbfcc-c0d4-4b7c-9ad3-759954d6930d";
            public const string ArticulosGruposInactivar = "7f936e7e-c647-4f1d-87e1-05f267eb1770";
            public const string ArticulosSubgruposBuscar = "9c8955bc-cb31-45ec-8b7a-2b713c8eb3ab";
            public const string ArticulosSubgruposGuardar = "7af9f93e-bd1f-4055-a52b-1b05a4974817";
            public const string ArticulosSubgruposInactivar = "2873fda3-6100-4cc8-a8d1-f694f88c1687";

            // Logistica
            public const string LogisticaGetDepositosPorUsuario = "90ff5c1c-7cf4-4426-a1ef-1def52f778b9";
            public const string LogisticaGetContenedoresPorDeposito = "9aa73723-dad2-4761-8ea4-ebb0e287ea98";
            public const string LogisticaCambiarEstadoContenedor = "e500ba76-c048-4dc4-9176-01b06bf5df78";
            public const string Logisticagetlineas = "a21337f5-7118-4b3f-84ce-bcf5ef42a593";
            public const string LogisticaGetLineas = "0b82af24-c0b1-4b29-848f-14d5ef0e6729";
            public const string LogisticaGetPuertos = "507b11c3-99ab-4e74-a04e-7ffa102a1493";
            public const string LogisticaEliminarMovimiento = "69ba73bb-6f4f-4ebd-80ac-0b1608c0d422";
            public const string LogisticaGuardarMovimiento = "75b6cebb-afe8-4af2-8792-c2d4520d239c";
            public const string LogisticaGetMovimientoPuertaPorId = "118801dd-f5f5-4583-98cf-e2ff48b5a272";
            public const string LogisticaGetListadoControlOperaciones = "693187fa-d1d6-4490-8f66-d49d8d4772fe";
            public const string LogisticaGetOperacionPorId = "e60ee38b-71ff-4b12-9270-41940839cf89";
            public const string LogisticaProsesarOperacionesConetenedores = "3add8079-8f85-4996-996d-530fde0ca359";
            public const string LogisticaGuardarOperacion = "c740e515-d620-49f0-add7-7b314a42bfe7";
            public const string LogisticaGetPesoDesdeBascula = "7bb23fc7-047b-4ec3-808b-cb8632789f6e";
            public const string LogisticaGetBasculas = "a07e11c3-99ab-4e74-a04e-7ff0805a1493";
            public const string LogisticaSeguridadRegistrarEntrada = "a1efa082-9530-4585-80eb-ee6444085267";
            public const string LogisticaGetHojaServicioConcepto = "a1efa082-9530-4585-80eb-ee6444085268";
            public const string LogisticaGuardarHojaServicio = "6ae85107-1958-490c-a3d3-954aedcb4097";
            public const string LogisticaGetHojaServicioListado = "3569999d-60fd-4626-810d-a714379b7ff3";
            public const string LogisticaBorrarServicioPorId = "8d7b8b9c-7a9a-476b-bc87-c00fddd22bac";
            public const string LogisticaGetIngresoImportTerrestre = "7bc62aba-f473-44f1-bb69-033ec205adf4";
            public const string LogisticaGetConfiguracionesTara = "3640afd6-86f5-4459-82ef-ac8f78511169";
            public const string LogisticaGetContenedorPorNumero = "5dbac52c-02cd-4f63-aca9-cc8a23444183";
            public const string LogisticaGetMovimientosGIO = "0bd88dbc-bed0-4fe7-b0ab-d730ed6b92b4";
            public const string LogisticaGetNombreChofer = "daf8af81-03a3-412d-bcf6-ee36ea6fc7d0";
            public const string LogisticaGetMovimientoPuerta = "118801dd-f5f5-4583-98cf-e2ff48b5a272";
            public const string LogisticaGetTiposContenedores = "a21337f5-7118-4b3f-84ce-bcf5ef42a593";
            public const string LogisticaGetAgencias = "a06392a0-345a-11ed-a261-0242ac120002";
            public const string LogisticaGuardarContenedor = "0333d9cc-4859-4e91-b254-6eb55a8733c1";
            public const string LogisticaGuardarTaraCamion = "614a7de7-a802-42ca-9555-f0645d3c0a8d";
            //Ordenes de servicio
            public const string LogisticaGetPedidosPosicionamiento = "882a6988-de88-40d6-8c66-334552e1cbce";
            public const string LogisticaGetServicioSubServicio = "041c8355-6e2d-4744-8d37-7dd949d88b57";
            public const string LogisticaGetPedidoPosDetalle = "0ea6ece3-5a3d-4174-9095-02b207b89000";
            public const string LogisticaModificarPedidoPosDetalle = "85eaeb66-9090-41f8-90c4-858769647467";
            public const string LogisticaGetPedidoPosInpresion = "3d399b9a-b4c6-4c07-8439-9333cd17a9cf";
            public const string LogisticaGetTipoServicio = "acefe432-d5fe-4870-82c3-10d542b108de";
            public const string LogisticaGetActaOS = "3dc22a2b-1b61-4c23-8fae-2b4471c5d02c";
            public const string LogisticaUpdateActaOS = "69e54357-149c-460d-9847-7a75818a0d33";


            //registroentrada
            public const string LogisticaSeguridadGetVehiculo = "161eff22-5ab1-4bec-932f-f0d5323fae6e";
            public const string LogisticaSeguridadGetVisitante = "43dcda34-63bf-4641-a4ac-b790fd6d6a78";
            public const string LogisticaSeguridadGuardarVisitante = "cee5b9de-abef-4f31-8f34-8a2329284ec5";
            public const string LogisticaSeguridadGuardarVehiculo = "7199af07-3c87-4923-b246-04bb5ef4d84e";
            public const string LogisticaSeguridadListadoLlegadaSalida = "2a377784-3bae-47e0-bcfa-bceaa126bcbf";
            public const string LogisticaSeguridadRegistrarSalida = "f5d28a17-3cba-44ae-b625-88db697b74de";

            // Entidades
            public const string entidadesPersonasBuscar = "2b1b5f1c-eaf1-4e4a-bf43-8ddd41ee0028";
            public const string EntidadesFichaGet = "e21bb562-9dd1-49c5-8041-0a8d382390fa";
            public const string EntidadesVehiculoGetPorId = "cebc073c-5bf6-40aa-82fd-786f6292c9d1";
            //Dashboard
            public const string DashboardLogisticaGetStockContenedoresPorFecha = "877b878a-3263-4c51-8a31-105246775932";
            public const string DashboardLogisticaGetMovimientosGIOPorFecha = "dceaff6e-9f7f-433d-bdd8-51c5a283b7fc";
            public const string DashboardLogisticaGetFacturacionPorFecha = "2df6a01e-235c-4111-9d78-a326662a81e2";
            public const string dashboardLogisticaGetMovimientosMuellePorFecha = "8c17a07f-d330-4db4-8c32-fdcd4d297138";

            // Datos Aduanas
            public const string LogisticaDatosAduanas = "59f84d61-8381-4346-ae40-94acdb52c9a6";

            // Datos PE por Linea
            public const string LogisticaGetLineasPorUsuario = "f927f4f1-2b2d-461f-a78c-62ba7791a34e";
            public const string LogisticaGetPreembarquesSinIntercambioPorLinea = "32d7d995-54c3-473a-b063-f1d2170d0e63";
            public const string LogisticaGetPreembarquesInformePorLinea = "b9c2ee24-b9ff-403a-bf1a-48cd86823188";

        }
        #endregion 
        public static class SiNoDefiniciones
        {
            public const string SI = "S";
            public const string NO = "N";
        }

        public static class EstructuraJSON
        {
            public static class Nodos
            {
                public const string mensaje = "mensaje";
                public const string error = "error";
                public const string codigo = "codigo";
                public const string datos = "datos";
                public const string d = "d";

            }
        }
        public static class TiposServicio
        {
            public const string Verificacion = "Verificación";
            public const string Consolidacion = "Consolidación";
            public const string Desconsolidacion = "Desconsolidación";
        }
        public static class SiNoCombo
        {
            public static readonly List<object> Values = new List<object>
        {
            new { value = SiNoDefiniciones.SI, label = "Sí" },
            new { value = SiNoDefiniciones.NO, label = "No" }
        };
        }

        public static class LogisticaPredioEstadoReparacion
        {
            public static readonly List<object> Values = new List<object>
        {
            new { value = "N/A", label = "N/A" },
            new { value = "PENDIENTE", label = "Pendiente" },
            new { value = "EN_REPARACION", label = "En reparación" },
            new { value = "REPARADO", label = "Reparado" }
        };
        }

        public static class LogisticaClaseContenedor
        {
            public static readonly List<object> Values = new List<object>
        {
            new { value = "A", label = "A" },
            new { value = "B", label = "B" },
            new { value = "C", label = "C" },
            new { value = "D", label = "D" }
        };
        }

        public const int LengthOfTextSearch = 3;

        public static class ExcepcionesRestError
        {
            public const int NO_AUTENTICADO = 401;
            public const int TOKEN_EXPIRADO = 440;
            public const int TOKEN_REQUERIDO = 460;
            public const int TOKEN_INVALIDO = 461;
            public const int ERROR_CLIENTE = 462;
        }
    }
    public class SesionException : Exception
    {
        public SesionException() { }

        public SesionException(string message) : base(message) { }

        public SesionException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
