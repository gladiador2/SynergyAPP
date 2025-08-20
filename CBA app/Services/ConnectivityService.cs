using System.Net.NetworkInformation;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Networking;
using CBA_app.Views;

namespace CBA_app.Services
{
    public class ConnectivityService
    {
        private readonly HttpClient _httpClient;

        public ConnectivityService()
        {
            _httpClient = new HttpClient();
        }

        public bool IsConnectedToInternet()
        {
            return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }

        public async Task<bool> PingUrlAsync()
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3)); // Timeout de 3 segundos
                var response = await _httpClient.GetAsync(ConstantesApp.url, cts.Token);
                return response.IsSuccessStatusCode;
            }
            catch (OperationCanceledException)
            {
                // Timeout alcanzado
                return false;
            }
            catch (HttpRequestException)
            {
                // Error de red (incluye "socket closed")
                return false;
            }
            catch (Exception)
            {
                // Otros errores
                return false;
            }
        }


    }
}
