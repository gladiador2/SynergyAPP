using CBA_app.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using CBA_app.Services;
using CBA_app.Models;

using CBA_app.Views.Login;
using System.Diagnostics;
using CBA_app.Request;
using CommunityToolkit.Maui.Alerts;
using CBA_app.Views;

namespace CBA_app.Services
{

    public class ValidarPeticion : ObservableObject, IRequest
    {
        private readonly HttpClient _client;
        private readonly ConnectivityService _connectivityService;
        private INavigation Navigation => App.Current.MainPage.Navigation;

        public ValidarPeticion()
        {
            _client = new HttpClient();
            _connectivityService = new ConnectivityService();
        }

        public async Task<JsonNode> EjecutarPeticionSesionRest(Dictionary<string, object> variables, string hash, string urlApi)
            => await EjecutarPeticion(variables, hash, urlApi, SesionesClass.SesionRest);

        public async Task<JsonNode> EjecutarPeticionSesionMovile(Dictionary<string, object> variables, string hash, string urlApi)
            => await EjecutarPeticion(variables, hash, urlApi, SesionesClass.SesionMovile);

        public async Task<JsonNode> EjecutarPeticion(Dictionary<string, object> variables, string hash, string urlApi, object sesion, bool reemplazarDecimales = true)
        {
            try
            {
                var jsonData = BuildJsonData(variables, hash, sesion);
                var json = JsonConvert.SerializeObject(jsonData);

                if (reemplazarDecimales)
                    json = json.Replace(".0", string.Empty);

                return await requestWithPromise(json, urlApi);
            }
            catch (SesionException) { throw; }
            catch (Exception ex)
            {
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }

        private Dictionary<string, object> BuildJsonData(Dictionary<string, object> variables, string hash, object sesion)
        {
            var jsonData = new Dictionary<string, object> { { "sesion", sesion } };
            if (variables != null)
                foreach (var campo in variables)
                    jsonData.Add(campo.Key, campo.Value);
            if (!string.IsNullOrEmpty(hash))
                jsonData.Add("hash", hash);
            return jsonData;
        }

        private async Task<bool> VerificarConexion()
        {
            try
            {
                if (_connectivityService.IsConnectedToInternet())
                {
                    if (!await _connectivityService.PingUrlAsync())
                    {
                        await Navigation.PushAsync(new SinInternet("No se pudo conectar a /n" + ConstantesApp.url));
                        return false;
                    }
                    return true;
                }
                await Navigation.PushAsync(new SinInternet());
                return false;
            }
            catch (Exception ex)
            {
                await DisplayMensajes.DisplayErrorAlert(ex.Message);
                return false;
            }
        }

        public async Task<JsonNode> requestWithPromise(string jsonData, string urlApi)
        {
            try
            {
                if (!await VerificarConexion())
                    return null;

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var timeoutTask = Task.Delay(ConstantesApp.TIEMPO_ESPERA);
                var responseTask = _client.PostAsync(ConstantesApp.URL_API + urlApi, content);
                var completedTask = await Task.WhenAny(responseTask, timeoutTask);

                if (completedTask == responseTask)
                {
                    var response = await responseTask;
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return await ProcesarRespuesta(response, responseBody, urlApi);
                }
                else
                {
                    await ManejarTimeout();
                    return null;
                }
            }
            catch (SesionException ex)
            {
                await DisplayMensajes.MostrarMensaje(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }

        public async Task<JsonNode> RequestSinTimeout(string jsonData, string urlApi)
        {
            try
            {
                // Si quieres verificar la conexión, descomenta la siguiente línea:
                // if (!await VerificarConexion()) return null;

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(ConstantesApp.URL_API + urlApi, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                return await ProcesarRespuesta(response, responseBody, urlApi);
            }
            catch (SesionException ex)
            {
                await DisplayMensajes.MostrarMensaje(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }

        private async Task<JsonNode> ProcesarRespuesta(HttpResponseMessage response, string responseBody, string urlApi)
        {
            using var doc = JsonDocument.Parse(responseBody);
            var nodos = JsonNode.Parse(responseBody);
            var root = doc.RootElement;

            if (response.IsSuccessStatusCode)
            {
                // Manejo de respuestas tipo array o string
                if (root.ValueKind == JsonValueKind.Array || root.ValueKind == JsonValueKind.String)
                    return nodos;

                // Si existe el nodo 'd'
                if (root.TryGetProperty(ConstantesApp.EstructuraJSON.Nodos.d, out _))
                    return nodos[ConstantesApp.EstructuraJSON.Nodos.d];

                // Extraer mensaje, error y código
                string mensaje = GetJsonProperty(root, ConstantesApp.EstructuraJSON.Nodos.mensaje);
                string error = GetJsonProperty(root, ConstantesApp.EstructuraJSON.Nodos.error);
                long codigo = GetJsonPropertyLong(root, ConstantesApp.EstructuraJSON.Nodos.codigo);

                if (root.TryGetProperty(ConstantesApp.EstructuraJSON.Nodos.datos, out _))
                {
                    if (!string.IsNullOrEmpty(mensaje))
                        await DisplayMensajes.MostrarMensajeExito(mensaje);
                    return nodos[ConstantesApp.EstructuraJSON.Nodos.datos];
                }
                
                // --- NUEVO: Manejo explícito de error aunque no haya "datos" ---
                if (!string.IsNullOrEmpty(error) && error == Definiciones.SiNo.Si)
                {
                    //440 session finalizada
                    //461 session invalida
                    if (codigo == ConstantesApp.ExcepcionesRestError.NO_AUTENTICADO
                        || codigo == ConstantesApp.ExcepcionesRestError.TOKEN_INVALIDO
                        || codigo == ConstantesApp.ExcepcionesRestError.TOKEN_EXPIRADO
                        || codigo == ConstantesApp.ExcepcionesRestError.TOKEN_REQUERIDO)
                    {
                        if (Preferences.ContainsKey(nameof(App.UserDetails)))
                        {
                            Preferences.Remove(nameof(App.UserDetails));
                        }
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                        throw new SesionException(mensaje);
                        return null;
                    }
                    await DisplayMensajes.MostrarMensaje(mensaje);
                    return null;
                    
                }
                if (!string.IsNullOrEmpty(mensaje))
                {
                    await DisplayMensajes.MostrarMensajeExito(mensaje);
                    return null;
                }
                // -------------------------------------------------------------

                return null;
            }
            else
            {
                string mensaje = GetJsonProperty(root, "Message");
                if (string.IsNullOrEmpty(mensaje))
                    await DisplayMensajes.DisplayErrorAlert("Ocurrió un error al obtener los datos. " + urlApi);
                else
                    await DisplayMensajes.DisplayErrorAlert(mensaje + " " + urlApi);
                return null;
            }
        }

        private string GetJsonProperty(JsonElement root, string property)
            => root.TryGetProperty(property, out var prop) ? prop.GetString() : string.Empty;

        private long GetJsonPropertyLong(JsonElement root, string property)
            => root.TryGetProperty(property, out var prop) ? prop.GetInt64() : 0;

        private async Task<JsonNode> ManejarErrorSesion(string mensaje, long codigo)
        {
            if (codigo == ConstantesApp.ExcepcionesRestError.NO_AUTENTICADO
                || codigo == ConstantesApp.ExcepcionesRestError.TOKEN_INVALIDO
                || codigo == ConstantesApp.ExcepcionesRestError.TOKEN_EXPIRADO
                || codigo == ConstantesApp.ExcepcionesRestError.TOKEN_REQUERIDO)
            {
                if (Preferences.ContainsKey(nameof(App.UserDetails)))
                    Preferences.Remove(nameof(App.UserDetails));
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                throw new SesionException(mensaje);
            }
            await DisplayMensajes.MostrarMensaje(mensaje);
            return null;
        }

        private async Task ManejarTimeout()
        {
            var resultado = await App.Current.MainPage.DisplayAlert("Advertencia", "Se está tardando demasiado. ¿Desea seguir esperando?", "Sí", "No");
            if (!resultado)
            {
                _client.CancelPendingRequests();
                await App.Current.MainPage.DisplayAlert("Advertencia", "La solicitud ha sido cancelada.", "Aceptar");
            }
        }
    }

}
