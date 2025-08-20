using CBA_app.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CBA_app.Request
{
    public class RegistroVisitasRequest : ValidarPeticion
    {
        
        #region Get Vehiculo
        public async Task<JsonNode> GetVehiculo(Dictionary<string, object> variables)
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
                jsonData.Add("hash", ConstantesApp.Hashes.LogisticaSeguridadGetVehiculo);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

                JsonNode datos = await requestWithPromise(json, "LogisticaSeguridadGetVehiculo");
                return datos;

            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }

        #endregion Get Vehiculo

        #region get Persona
        public async Task<JsonNode> GetPersona(Dictionary<string, object> variables)
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
                jsonData.Add("hash", ConstantesApp.Hashes.LogisticaSeguridadGetVisitante);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

                JsonNode datos = await requestWithPromise(json, "LogisticaSeguridadGetVisitante");
                return datos;

            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }
        #endregion get Persona

        #region GuardarPersona
        public async Task<JsonNode> GuardarPersona(Dictionary<string, object> variables)
        {
            try
            {
                var jsonData = new Dictionary<string, object>
                {
                    { "sesion",  SesionesClass.SesionRest }
                };
                // Agregar los otros campos al diccionario jsonData
                //foreach (var campo in variables)
                //{
                //    jsonData.Add(campo.Key, campo.Value);
                //}
                jsonData.Add("hash", ConstantesApp.Hashes.LogisticaSeguridadGuardarVisitante);
                jsonData.Add("visitanteDato", variables);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

            JsonNode datos = await requestWithPromise(json, "LogisticaSeguridadGuardarVisitante");
                return datos;

            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }
        #endregion GuardarPersona

        #region GuardarVehiculo
        public async Task<JsonNode> GuardarVehiculo(Dictionary<string, object> variables)
        {
            try
            {
                var jsonData = new Dictionary<string, object>
                {
                    { "sesion",  SesionesClass.SesionRest }
                };
                // Agregar los otros campos al diccionario jsonData
                jsonData.Add("vehiculoDato", variables);
                //foreach (var campo in variables)
                //{
                //    jsonData.Add(campo.Key, campo.Value);
                //}
                jsonData.Add("hash", ConstantesApp.Hashes.LogisticaSeguridadGuardarVehiculo);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

                JsonNode datos = await requestWithPromise(json, "LogisticaSeguridadGuardarVehiculo");
                return datos;

            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }
        #endregion GuardarVehiculo

        #region getListaVisitantes
        public async Task<JsonNode> getListaVisitantes(Dictionary<string, object> variables)
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
                jsonData.Add("hash", ConstantesApp.Hashes.LogisticaSeguridadListadoLlegadaSalida);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

                JsonNode datos = await requestWithPromise(json, "LogisticaSeguridadListadoLlegadaSalida");
                return datos;

            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }
        #endregion getListaVisitantes

        #region RegistrarEntrada
        public async Task<JsonNode> RegistrrarEntrada(Dictionary<string, object> variables)
        {
            try
            {
                var jsonData = new Dictionary<string, object>
                {
                    { "sesion",  SesionesClass.SesionRest }
                };
                // Agregar los otros campos al diccionario jsonData
                //foreach (var campo in variables)
                //{
                //    jsonData.Add(campo.Key, campo.Value);
                //}
                jsonData.Add("hash", ConstantesApp.Hashes.LogisticaSeguridadRegistrarEntrada);
                jsonData.Add("llegadaDato", variables);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

                JsonNode datos = await requestWithPromise(json, "LogisticaSeguridadRegistrarEntrada");
                
                return datos;


            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await App.Current.MainPage.DisplayAlert("Error", $"RegistrrarEntrada: {ex.Message}", "Aceptar");
                return null;
            }
        }
        #endregion Registraentrada

        #region RegistrarSalida
        public async Task<JsonNode> RegistrarSalida(Dictionary<string, object> variables)
        {
            try
            {
                var jsonData = new Dictionary<string, object>
                {
                    { "sesion",  SesionesClass.SesionRest }
                };
                // Agregar los otros campos al diccionario jsonData

                //foreach (var campo in variables)
                //{
                //    jsonData.Add(campo.Key, campo.Value);
                //}
                jsonData.Add("salidaDato", variables);
                jsonData.Add("hash", ConstantesApp.Hashes.LogisticaSeguridadRegistrarSalida);

                // Serializar el objeto JSON a una cadena
                var json = JsonConvert.SerializeObject(jsonData);

                JsonNode datos = await requestWithPromise(json, "LogisticaSeguridadRegistrarSalida");
                return datos;

            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }
        #endregion Registrar
    }
}
