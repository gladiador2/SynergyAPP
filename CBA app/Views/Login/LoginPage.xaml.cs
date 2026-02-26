using CBA_app.ViewModels.Startup;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CBA_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using CBA_app.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CommunityToolkit.Maui.Alerts;
using Microsoft.Maui.Animations;
using CBA_app.Request;
using System.Text.RegularExpressions;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using CBA_app.Views.Utiles;

namespace CBA_app.Views.Login;

public partial class LoginPage : ContentPage
{
    private readonly ModeloSession _mSession = new ModeloSession();
    private const string UrlApi = ConstantesApp.URL_API + "api/login";

    public LoginPage(LoginPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        animacionesServices.Rebote(ImageLogoCompanhia);
        // Cargar credenciales almacenadas
        var (usuario, contrasenha) = ObtenerCredenciales();
        if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasenha))
        {
            txtUsuario.Text = usuario;
            txtContraseña.Text = contrasenha;
        }
    }

    #region Commands
    private void OnIngresar(object sender, EventArgs e)
    {
        // Ocultar el teclado
        txtUsuario.Unfocus();
        txtContraseña.Unfocus();
        Login();
    }

    public async void Login()
    {
        if (!string.IsNullOrWhiteSpace(txtUsuario.Text) && !string.IsNullOrWhiteSpace(txtContraseña.Text))
        {
            var session = await Logueo();

            if (session != null)
            {
                await AppConstant.AddFlyoutMenusDetails();
            }
            else
            {
                await DisplayMensajes.MostrarMensajeError("No puede iniciar sesion");
            }
        }
    }
    #endregion

    private async Task<UsuarioModel> Logueo()
    {
        return await AutenticarUsuario(txtUsuario.Text, txtContraseña.Text);
    }

    private async Task<UsuarioModel> AutenticarUsuario(string usuario, string contrasenha)
    {
        try
        {
            var jsonData = new JsonData
            {
                username = usuario,
                password = contrasenha
            };

            var json = JsonConvert.SerializeObject(jsonData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PostAsync(UrlApi, content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var nodos = JsonNode.Parse(responseBody);
                // Verificar si existe el nodo "data"
                if (nodos?["data"] != null)
                {
                    var dataNode = nodos["data"];
                    
                    var mSession = System.Text.Json.JsonSerializer.Deserialize<UsuarioModel>(dataNode.ToJsonString());

                    // Guardar detalles del usuario
                    var userDetailStr = JsonConvert.SerializeObject(mSession);
                    Preferences.Set(nameof(App.UserDetails), userDetailStr);
                    App.UserDetails = mSession;

                    return mSession;
                }
                // Si no existe "data" pero hay "message", mostrar el mensaje
                else if (nodos?["message"] != null)
                {
                    var mensaje = nodos["message"]?.ToString() ?? "Error desconocido";
                    await DisplayMensajes.MostrarMensajeError(mensaje);
                }
                
            }
            return null;
        }
        catch (Exception ex)
        {
            await DisplayMensajes.DisplayErrorAlert(ex.Message);
            return null;
        }
    }

    private async void OnBiometriaClicked(object sender, EventArgs e)
    {
        var result = await CrossFingerprint.Current.IsAvailableAsync();
        if (result)
        {
            var authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Autenticación Biométrica", "Usa tu huella digital para autenticarte"));
            if (authResult.Authenticated)
            {
                var session = await Logueo();

                if (session != null)
                {
                    GuardarCredenciales(txtUsuario.Text, txtContraseña.Text);

                    await AppConstant.AddFlyoutMenusDetails();
                }
                else
                {
                    GuardarCredenciales(txtUsuario.Text, txtContraseña.Text);

                    await AppConstant.AddFlyoutMenusDetails(); 
                    //await DisplayMensajes.MostrarMensajeError("No puede iniciar sesion");
                }
            }
            else
            {
                await DisplayAlert("Error", "Autenticación biométrica fallida", "OK");
            }
        }
        else
        {
            await DisplayAlert("No disponible", "La autenticación biométrica no está disponible en este dispositivo", "OK");
        }
    }
    private void GuardarCredenciales(string usuario, string contrasenha)
    {
        Preferences.Set("Usuario", usuario);
        Preferences.Set("Contrasenha", contrasenha);
    }

    private (string usuario, string contrasenha) ObtenerCredenciales()
    {
        var usuario = Preferences.Get("Usuario", string.Empty);
        var contrasenha = Preferences.Get("Contrasenha", string.Empty);
        return (usuario, contrasenha);
    }

    private void EliminarCredenciales()
    {
        Preferences.Remove("Usuario");
        Preferences.Remove("Contrasenha");
    }



    public class JsonData
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}

