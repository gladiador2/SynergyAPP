using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA_app.Services
{
    public interface IDisplayService
    {
        Task DisplayErrorAlert(Exception ex);
        Task MostrarAlerta(string mensaje);
        Task MostrarMensajeError(string mensaje);
        Task MostrarMensajeExito(string mensaje);
    }

    public static class DisplayMensajes
    {
        private static Page _mainPage = App.Current.MainPage;


        // Método para mostrar una alerta de error
        public static async Task DisplayErrorAlert(Exception ex)
        {
            
            await _mainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "Aceptar");
        }
        public static async Task DisplayErrorAlert(string ex)
        {

            await _mainPage.DisplayAlert("Error", $"{ex}", "Aceptar");
        }

        // Método para mostrar una alerta
        public static async Task MostrarAlerta(string mensaje)
        {
            await Toast.Make(mensaje, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }
        public static async Task MostrarMensaje(string mensaje)
        {
            await _mainPage.DisplayAlert(string.Empty, mensaje, "OK");
        }


        // Método para mostrar un mensaje de error
        public static async Task MostrarMensajeError(string mensaje)
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                if (_mainPage == null)
                    throw new InvalidOperationException("MainPage no ha sido inicializada.");

                await _mainPage.DisplayAlert("Error", mensaje, "OK");
            }
            else
            {
                await Toast.Make(mensaje, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            }
        }

        // Método para mostrar un mensaje de éxito
        public static async Task MostrarMensajeExito(string mensaje)
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                if (_mainPage == null)
                    throw new InvalidOperationException("MainPage no ha sido inicializada.");

                await _mainPage.DisplayAlert("Éxito", mensaje, "OK");
            }
            else
            {
                await Toast.Make(mensaje, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            }
        }
    }
}

