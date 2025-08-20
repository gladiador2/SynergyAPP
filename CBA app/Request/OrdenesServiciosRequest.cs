using CBA_app.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CBA_app.Request
{
    public class OrdenesServiciosRequest : ValidarPeticion
    {
        public async Task<JsonNode> LogisticaGetPedidosPosicionamientod(Dictionary<string, object> variables)
        {
            return await EjecutarPeticionSesionRest(variables, ConstantesApp.Hashes.LogisticaGetPedidosPosicionamiento, "LogisticaGetPedidosPosicionamiento");
        }

        public async Task<JsonNode> LogisticaGetServicioSubServicio(Dictionary<string, object> variables)
        {
            return await EjecutarPeticionSesionRest(variables, ConstantesApp.Hashes.LogisticaGetServicioSubServicio, "LogisticaGetServicioSubServicio");
        }
        public async Task<JsonNode> LogisticaGetPedidoPosDetalle(Dictionary<string, object> variables)
        {
            return await EjecutarPeticionSesionRest(variables, ConstantesApp.Hashes.LogisticaGetPedidoPosDetalle, "LogisticaGetPedidoPosDetalle");
        }
        public async Task<JsonNode> LogisticaGetPedidoPosInpresion(Dictionary<string, object> variables)
        {
            return await EjecutarPeticionSesionRest(variables, ConstantesApp.Hashes.LogisticaGetPedidoPosInpresion, "LogisticaGetPedidoPosInpresion");
        }
        public async Task<JsonNode> LogisticaModificarPedidoPosDetalle(Dictionary<string, object> variables)
        {
            return await EjecutarPeticionSesionRest(variables, ConstantesApp.Hashes.LogisticaModificarPedidoPosDetalle, "LogisticaModificarPedidoPosDetalle");
        }
        public async Task<JsonNode> LogisticaGetTipoServicio(Dictionary<string, object> variables)
        {
            return await EjecutarPeticionSesionRest(variables, ConstantesApp.Hashes.LogisticaGetTipoServicio, "LogisticaGetTipoServicio");
        }
        public async Task<JsonNode> LogisticaGetActaOS(Dictionary<string, object> variables)
        {
            return await EjecutarPeticionSesionRest(variables, ConstantesApp.Hashes.LogisticaGetActaOS, "LogisticaGetActaOS");
        }
        public async Task<JsonNode> LogisticaUpdateActaOS(Dictionary<string, object> variables)
        {
            return await EjecutarPeticionSesionRest(variables, ConstantesApp.Hashes.LogisticaGetActaOS, "LogisticaUpdateActaOS");
        }


    }
}
