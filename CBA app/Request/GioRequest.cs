using CBA_app.Models;
using CBA_app.Models.GIO;
using CBA_app.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CBA_app.Request
{
    public class GioRequest : ValidarPeticion
    {
        public async Task<JsonNode> GetVehiculo(Dictionary<string, object> variables)
            => await EjecutarPeticionSesionMovile(
                variables,
                ConstantesApp.Hashes.LogisticaSeguridadGetVehiculo,
                "LogisticaSeguridadGetVehiculo");

        public async Task<string> GetVariableSistema(Dictionary<string, object> variables)
        {
            try
            {
                var jsonData = new Dictionary<string, object>
                {
                 { "sesion",  SesionesClass.SesionRest }
                };
                // Agregar los otros campos al diccionario jsonData
                foreach (var campo in variables)
                {
                    jsonData.Add(campo.Key, campo.Value);
                }


                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

                JsonNode datos = await requestWithPromise(json, "GetVariableSistema");
                return datos.ToString();

            }
            catch (System.Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }

        public async Task<JsonNode> EntidadesFichaGet(Dictionary<string, object> variables)
            => await EjecutarPeticionSesionMovile(
                variables,
                ConstantesApp.Hashes.EntidadesFichaGet,
                "EntidadesFichaGet");

        public async Task<JsonNode> GetPuertasMovimientosPorId(Dictionary<string, object> variables)
            => await EjecutarPeticionSesionRest(
                variables,
                ConstantesApp.Hashes.LogisticaGetOperacionPorId,
                "LogisticaGetPuertasMovimientosPorID");

        public async Task<JsonNode> EntidadesVehiculoGetPorId(Dictionary<string, object> variables)
            => await EjecutarPeticionSesionMovile(
                variables,
                ConstantesApp.Hashes.EntidadesFichaGet,
                "EntidadesVehiculoGetPorId");

        public async Task<JsonNode> LogisticaGetPuertos()
        { 
            try
            {
                var jsonData = new Dictionary<string, object>
                {
                    { "sesion", SesionesClass.SesionMovile }
                };
        // Agregar los otros campos al diccionario jsonData

        jsonData.Add("hash", ConstantesApp.Hashes.LogisticaGetPuertos);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

        JsonNode datos = await requestWithPromise(json, "LogisticaGetPuertos");
                return datos;

            }
            catch (System.Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }

        }

        public async Task<JsonNode> LogisticaLinea()
        {
            try
            {
                var jsonData = new Dictionary<string, object>
                {
                    { "sesion",  SesionesClass.SesionRest }
                };
                // Agregar los otros campos al diccionario jsonData

                jsonData.Add("hash", ConstantesApp.Hashes.LogisticaGetLineas);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

                JsonNode datos = await requestWithPromise(json, "LogisticaGetLineas");
                return datos;

            }
            catch (System.Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }



        }

        public async Task<JsonNode> LogisticaGetAgencias()
        {
            try
            {
                var jsonData = new Dictionary<string, object>
                {
                    { "sesion",  SesionesClass.SesionRest }
                };
                // Agregar los otros campos al diccionario jsonData

                jsonData.Add("hash", ConstantesApp.Hashes.LogisticaGetAgencias);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

                JsonNode datos = await requestWithPromise(json, "LogisticaGetAgencias");
                return datos;

            }
            catch (System.Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }



        }

        public async Task<JsonNode> LogisticaGetTiposContenedores()
        {
            try
            {
                var jsonData = new Dictionary<string, object>
                {
                    { "sesion",  SesionesClass.SesionRest }
                };
                // Agregar los otros campos al diccionario jsonData

                jsonData.Add("hash", ConstantesApp.Hashes.LogisticaGetTiposContenedores);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

                JsonNode datos = await requestWithPromise(json, "LogisticaGetTiposContenedores");
                return datos;

            }
            catch (System.Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }



        }

        public async Task<JsonNode> LogisticaGetMovimientosGIO(Dictionary<string, object> variables)
            => await EjecutarPeticionSesionMovile(
                variables,
                ConstantesApp.Hashes.LogisticaGetMovimientosGIO,
                "LogisticaGetMovimientosGIO");
        public async Task<bool> imprimir(ModeloImpresion.ObjetoImpresion objetoImpresion, string IP)
        {
            try
            {
                var json = JsonConvert.SerializeObject(objetoImpresion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var response = await client.PostAsync("http://" + IP + "/printTicket/", content);
                return response.IsSuccessStatusCode;


            }
            catch (System.Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return false;
            }
        }
        public async Task<JsonNode> LogisticaGetConfiguracionesTara(Dictionary<string, object> variables)
            => await EjecutarPeticionSesionMovile(
                variables,
                ConstantesApp.Hashes.LogisticaGetConfiguracionesTara,
                "LogisticaGetConfiguracionesTara");

        public async Task<JsonNode> LogisticaGetIngresoImportTerrestre(Dictionary<string, object> variables)
            => await EjecutarPeticionSesionMovile(
                variables,
                ConstantesApp.Hashes.EntidadesFichaGet,
                "EntidadesVehiculoGetPorId");

        public async Task<JsonNode> LogisticaGuardarMovimiento(Dictionary<string, object> variables)
            => await EjecutarPeticion(
                variables,
                ConstantesApp.Hashes.LogisticaGuardarMovimiento,
                "LogisticaGuardarMovimientoRest", SesionesClass.SesionRest, true);

        public async Task<JsonNode> LogisticaGuardarTaraCamion(Dictionary<string, object> variables)
            => await EjecutarPeticionSesionMovile(
                variables,
                ConstantesApp.Hashes.LogisticaGuardarTaraCamion,
                "LogisticaGuardarTaraCamion");

        public async Task<JsonNode> LogisticaGuardarContenedor(Dictionary<string, object> variables)
            => await EjecutarPeticionSesionMovile(
                variables,
                ConstantesApp.Hashes.LogisticaGuardarContenedor,
                "LogisticaGuardarContenedor");

        public async Task<JsonNode> LogisticaGetContenedorPorNumero(Dictionary<string, object> variables)
            => await EjecutarPeticionSesionMovile(
                variables,
                ConstantesApp.Hashes.LogisticaGetContenedorPorNumero,
                "LogisticaGetContenedorPorNumero");

        public async Task<JsonNode> LogisticaGetNombreChofer(Dictionary<string, object> variables)
            => await EjecutarPeticion(
                variables,
                ConstantesApp.Hashes.LogisticaGetNombreChofer,
                "LogisticaGetNombreChofer",SesionesClass.SesionMovile,true);

        public async Task<JsonNode> LogisticaGetMovimientoPuerta(Dictionary<string, object> variables)
            => await EjecutarPeticion(
                variables,
                ConstantesApp.Hashes.LogisticaGetMovimientoPuerta,
                "LogisticaGetMovimientoPuerta", SesionesClass.SesionMovile, true);

    }
}
/* => await EjecutarPeticion(
                variables,
                ConstantesApp.Hashes.LogisticaGetNombreChofer,
                "LogisticaGetNombreChofer",SesionesClass.SesionMovile,true);

public async Task<JsonNode> LogisticaGuardarIngresoImportTerrestre(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaGuardarIngresoImportTerrestre";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosIngresoImportacionesTerrestresGuardar();
parametros.hash = "773386f8-724f-4ad7-bae3-0f7e0ccd3599";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));

parametros.item_ingreso = item_ingreso;

var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    //var resultado = (string)JsonConvert.DeserializeObject(jsonrta, typeof(string));
    return jsonrta;
}
catch (System.Exception e)
{
    return e.Message;
}

}
//Reeefer
public async Task<JsonNode> LogisticaGetControlReeferDetalle(Dictionary<string, object> variables)//LogisticaGetControlReeferDetalle //b0dca0a8-7709-4042-9ab6-5d7c6202396f
{


string url = host.rest + "/";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosLogisticaGetControlReeferDetalle();
parametros.hash = "";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));

parametros.control_id = control_id;
parametros.turno_id = turno_id;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    //var resultado = (string)JsonConvert.DeserializeObject(jsonrta, typeof(string));
    return jsonrta;
}
catch (System.Exception e)
{
    return e.Message;
}

}
public async Task<JsonNode> LogisticaControlReeferGuardarLectura(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaControlReeferGuardarLectura";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosControlReeferDetalleGuardar();
parametros.hash = "0d2f3cd0-6cd4-4a17-9439-525c6d71a365";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));

parametros.control_detalle = control_detalle;

var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    //var resultado = (string)JsonConvert.DeserializeObject(jsonrta, typeof(string));
    return jsonrta;
}
catch (System.Exception e)
{
    return e.Message;
}

}
public async Task<JsonNode> LogisticaControlReeferGuardarControl(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaControlReeferGuardarControl";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosControlReeferGuardar();
parametros.hash = "0bd60f1f-ee2c-43cf-8349-a93962eb14f3";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));

parametros.control = control;

var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    //var resultado = (string)JsonConvert.DeserializeObject(jsonrta, typeof(string));
    return jsonrta;
}
catch (System.Exception e)
{
    return e.Message;
}

}
//Vehiculos
public async Task<JsonNode> LogisticaGuardarVehiculo(Dictionary<string, object> variables)
{
string url = host.rest + "/LogisticaGuardarVehiculo";
uri = new Uri(string.Format(url, string.Empty));
var parametros = new ParametrosVehiculosGuardar();
parametros.hash = "dca4f574-2b1f-4ed0-ad4d-5b33434f572a";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.vehiculo = vehiculo;
var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);
content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    return jsonrta;
}
catch (System.Exception e)
{
    return e.Message;
}
}
public async Task<JsonNode> LogisticaGuardarObservacionesVehiculos(List<ObservacionesVehiculos> observaciones, decimal vehiculo_id)
{
string url = host.rest + "/LogisticaGuardarObservacionesVehiculos";
uri = new Uri(string.Format(url, string.Empty));
var parametros = new ParametrosVehiculosObservacionesGuardar();
parametros.hash = "712d5847-fg77-35h6-7d54-33c54sd20d519";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.vehiculo_id = vehiculo_id;
parametros.observaciones = observaciones;
var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);
content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    return jsonrta;
}
catch (System.Exception e)
{
    return e.Message;
}
}


//Desde aqui metodos antes en SOAP
//GIO
public async Task<JsonNode> LogisticaGetMovimientosGIO(string fecha_desde, string fecha_hasta, string movimiento, string confirmado)
{


string url = host.rest + "/LogisticaGetMovimientosGIO";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosMovimientosGIO();
parametros.hash = "0bd88dbc-bed0-4fe7-b0ab-d730ed6b92b4";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));

parametros.fecha_desde = fecha_desde;
parametros.fecha_hasta = fecha_hasta;
parametros.movimiento = movimiento;
parametros.confirmado = confirmado;

var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    //var resultado = (string)JsonConvert.DeserializeObject(jsonrta, typeof(string));
    return jsonrta;
}
catch (System.Exception e)
{
    return e.Message;
}

}





public async Task<JsonNode> LogisticaGetContenedorPorNumero(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaGetContenedorPorNumero";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosContenedorPorNumero();
parametros.hash = "5dbac52c-02cd-4f63-aca9-cc8a23444183";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.nro_contenedor = nro_contenedor;


var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));

    //var resultado = (string)JsonConvert.DeserializeObject(jsonrta, typeof(string));
    if (hash.d == "[]")
    {
        return "NOEXISTE";
    }
    else
    {
        return hash.d;
    }

}
catch (System.Exception e)
{
    return e.Message;
}

}





public async Task<JsonNode> LogisticaGuardarMovimiento(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaGuardarMovimiento";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosGuardarMovimientoPuerta();
parametros.hash = "75b6cebb-afe8-4af2-8792-c2d4520d239c";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.datos = datos;


var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();

    return jsonrta;
    //var resultado = (Resultado)JsonConvert.DeserializeObject(jsonrta, typeof(Resultado));
    //return decimal.Parse(resultado.d);
}
catch (System.Exception e)
{
    Resultado res = new Resultado();
    Estructura dato = new Estructura();
    dato.id = -1;
    res.mensaje = e.Message;
    res.datos.Add(dato);
    return JsonConvert.SerializeObject(res);
}

}


//Predio

public async Task<JsonNode> LogisticaGetDepositoContenedores(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaGetDepositoContenedores";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosDepositoContenedores();
parametros.hash = "aad0c584-e143-4b45-854d-7b354d20d519";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.tipo = tipo;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGetBloquesPorDeposito(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaGetBloquesPorDeposito";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosBloquesPorDeposito();
parametros.hash = "fa6afdc7-6978-4a9d-88a5-a69d10cc98a9";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.depositoId = depositoId;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGetGruposPorBloque(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaGetGruposPorBloque";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosGruposPorBloque();
parametros.hash = "bca014c3-9043-4b21-a0e9-7bec4ebb6078";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.bloqueId = bloqueId;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGuardarContenedorEnUbicacion(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaGuardarContenedorEnUbicacion";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosGuardarContenedorEnUbicacion();
parametros.hash = "29f534d4-880d-413b-8c66-c8303595b412";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.contenedor = contenedor;
parametros.deposito = deposito;
parametros.bloque = bloque;
parametros.grupo = grupo;
parametros.columna = columna;
parametros.estiba = estiba;

var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaEliminarContenedorUbicacion(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaEliminarContenedorUbicacion";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosEliminarContenedorUbicacion();
parametros.hash = "2a0114eb-d2ef-4bd1-909a-049ad4ebd35d";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.ubicacion = ubicacion;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGetUbicacionContenedor(Dictionary<string, object> variables)
{


string url = host.rest + "/LogisticaGetUbicacionContenedor";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosGetUbicacionContenedor();
parametros.hash = "27f15f92-2b36-4c91-8d25-5f8420c618a4";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.contenedor = contenedor;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}


//Muelle

public async Task<JsonNode> LogisticaBuquesContenedoresPendientes()
{


string url = host.rest + "/LogisticaBuquesContenedoresPendientes";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosBuquesContenedoresPendientes();
parametros.hash = "6ebcbf98-5d0e-492d-a4ce-1e1bc3ff58d5";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;

}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaIntercambiosNoFinalizadosPorBuque(Dictionary<string, object> variables)
{
    try
    {
        var jsonData = new Dictionary<string, object>
        {
           { "sesion", SesionesClass.SesionMovile }
        };
        // Agregar los otros campos al diccionario jsonData
        foreach (var campo in variables)
        {
            jsonData.Add(campo.Key, campo.Value);
        }
        jsonData.Add("hash", ConstantesApp.Hashes.LogisticaIntercambiosNoFinalizadosPorBuque);//15872645-ea07-4f42-bdbe-88db2be333c2

        // Serializar el objeto JSON a una cadena
        var json = JsonConvert.SerializeObject(jsonData);

        JsonNode datos = await requestWithPromise(json, "LogisticaIntercambiosNoFinalizadosPorBuque");
        return datos;

    }
    catch (System.Exception ex)
    {
        // Mostrar mensaje de error en un DisplayAlert
        await DisplayMensajes.DisplayErrorAlert(ex);
        return null;
    }
}

public async Task<JsonNode> LogisticaGetMapeoBuques(decimal dato_id, int tipo)
{


string url = host.rest + "/LogisticaGetMapeoBuques";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosMapeoBuques();
parametros.hash = "773386f8-724f-4ad7-bae3-0f7e0ccd3599";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.dato_id = dato_id;
parametros.tipo = tipo;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGetContenedoresIncluidosIntercambio(decimal recepcion_id)
{


string url = host.rest + "/LogisticaContenedoresIncluidosIntercambio";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ContenedoresIncluidosIntercambio();
parametros.hash = "0f3d47ac-405f-4d7c-bdf7-3735a112ceaa";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.recepcion_id = recepcion_id;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGetContenedoresPendientesPorIntercambio(decimal recibo_id)
{


string url = host.rest + "/LogisticaGetContenedoresPendientesPorIntercambio";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ContenedoresPendientesIntercambio();
parametros.hash = "f4ef9611-0009-46a9-8476-22dad60334b0";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.recibo_id = recibo_id;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGetDetallesContenedor(decimal detalle_id, string operacion)
{


string url = host.rest + "/LogisticaGetDetallesContenedor";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosDetallesContenedor();
parametros.hash = "8e356ca7-0e2f-4728-92ba-7fffe3182a73";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.detalle_id = detalle_id;
parametros.operacion = operacion;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGuardaDetallesContenedor(string datos, string operacion)
{


string url = host.rest + "/LogisticaGuardaDetallesContenedor";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosGuardaDetallesContenedor();
parametros.hash = "dce677bc-95cf-4088-8fb3-d3c381b82ecf";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.datos = datos;
parametros.operacion = operacion;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

//Administracion
public async Task<JsonNode> LogisticaGetDatosParaContenedores()
{


string url = host.rest + "/LogisticaGetDatosParaContenedores";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosDatosParaContenedores();
parametros.hash = "d0cdf6fa-0447-4710-ad71-0a17b3d60bfb";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;

}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGetMovimientosPorContenedor(decimal contenedor_id)
{


string url = host.rest + "/LogisticaGetMovimientosPorContenedor";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosMovimientosPorContenedor();
parametros.hash = "50254127-3f34-48f4-aa06-4003a0e19930";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.contenedor_id = contenedor_id;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGuardarContenedor(string datos)
{


string url = host.rest + "/LogisticaGuardarContenedor";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosGuardarContenedor();
parametros.hash = "0333d9cc-4859-4e91-b254-6eb55a8733c1";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.datos = datos;


var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();

    return jsonrta;
    //var resultado = (Resultado)JsonConvert.DeserializeObject(jsonrta, typeof(Resultado));
    //return decimal.Parse(resultado.d);
}
catch (System.Exception e)
{
    Resultado res = new Resultado();
    Estructura dato = new Estructura();
    dato.id = -1;
    res.mensaje = e.Message;
    res.datos.Add(dato);
    return JsonConvert.SerializeObject(res);
}

}

public async Task<JsonNode> LogisticaCambiartEstadoMovimientoContenedor(decimal movimiento_id)
{


string url = host.rest + "/LogisticaCambiartEstadoMovimientoContenedor";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosCambiartEstadoMovimientoContenedor();
parametros.hash = "0ecec252-1e05-45dc-9f6b-207f690370eb";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.movimiento_id = movimiento_id;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    return jsonrta;
}
catch (System.Exception e)
{
    Resultado res = new Resultado();
    Estructura dato = new Estructura();
    dato.id = -1;
    res.mensaje = e.Message;
    res.datos.Add(dato);
    return JsonConvert.SerializeObject(res);
}

}


//Predio - Transferencia de Contenedores
public async Task<JsonNode> LogisticaGetTransferenciaContenedores(string where)
{


string url = host.rest + "/LogisticaGetTransferenciaContenedores";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosGetTransferenciaContenedores();
parametros.hash = "968dc3c2-1589-46ce-9476-8642624e0186";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.where = where;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaGetTransferenciaContenedoresDetalles(string where)
{


string url = host.rest + "/LogisticaGetTransferenciaContenedoresDetalles";
uri = new Uri(string.Format(url, string.Empty));
//Usa los mismos parametros que la busqueda de la cabecera por lo tanto se reutiliza la clase de parametros
var parametros = new ParametrosGetTransferenciaContenedores();
parametros.hash = "4ee2baaf-245c-4943-8217-8a18b8b7c2b5";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.where = where;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}

public async Task<JsonNode> LogisticaTransferenciaContenedoresDetallesGuardar(decimal transferencia_id, decimal contenedor_id)
{


string url = host.rest + "/LogisticaTransferenciaContenedoresDetallesGuardar";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosTransferenciaContenedoresDetallesGuardar();
parametros.hash = "470a2b89-e06b-43a6-9c95-3738446709a4";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.transferencia_id = transferencia_id;
parametros.contenedor_id = contenedor_id;



var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}
public async Task<JsonNode> LogisticaTransferenciaContenedoresDetallesEliminar(decimal transferencia_detalle_id)
{


string url = host.rest + "/LogisticaTransferenciaContenedoresDetallesEliminar";
uri = new Uri(string.Format(url, string.Empty));

var parametros = new ParametrosTransferenciaContenedoresDetallesEliminar();
parametros.hash = "0f9a4c77-3ad6-44e2-aff5-6a66ca7561a3";
var sesionMobileString = await App.iLogin.getSesionMobile();
parametros.sesion = (SesionMobile)JsonConvert.DeserializeObject(sesionMobileString, (typeof(SesionMobile)));
parametros.transferencia_detalle_id = transferencia_detalle_id;


var json = JsonConvert.SerializeObject(parametros);
json = json.Replace(".0", string.Empty);

content = new StringContent(json, Encoding.UTF8, "application/json");
try
{
    response = await client.PostAsync(uri, content);
    var jsonrta = await response.Content.ReadAsStringAsync();
    var hash = (RespuestaRest2)JsonConvert.DeserializeObject(jsonrta, (typeof(RespuestaRest2)));
    return hash.d;
}
catch (System.Exception e)
{
    return e.Message;
}

}
*/

