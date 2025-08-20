using CBA_app.Models.OrdenesServicio;
using CBA_app.Request;
using CBA_app.Services;
using CBA_app.ViewModels.OrdenesServicio;

using System.Text.Json.Nodes;

namespace CBA_app.Views.OrdenServicio;

public partial class OrdenSevicioPage : ContentPage
{
    #region propiedades privadas
    private SwipeView _previouslyOpenedSwipeView;
    private readonly OrdenesServiciosRequest request = new();
    private readonly OrdenesServicioViewModel viewModel = new();
    #endregion propiedades privadas

    #region constructor
    public OrdenSevicioPage()
    {
        InitializeComponent();
        viewModel.OrdenesServicioPage = this;
        BindingContext = viewModel;
        
    }
    #endregion constructor

    #region eventos
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = BuscarAsync();
    }

    private async void btnFiltros_Clicked(object sender, EventArgs e)
    {
        await animacionesServices.desplegar(FrameFiltros);
    }

    private void OddSwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
    {
        if (_previouslyOpenedSwipeView != null && _previouslyOpenedSwipeView != sender)
        {
            _previouslyOpenedSwipeView.Close();
        }
        _previouslyOpenedSwipeView = sender as SwipeView;
    }

    private async void btnbuscar_Clicked(object sender, EventArgs e)
    {
        await BuscarAsync();
    }

    private async Task BuscarAsync()
    {
        try
        {
            await BusquedaAsync();
        }
        catch (SesionException)
        {
            // Control específico de sesión, puedes mostrar un mensaje si lo deseas
            return;
        }
        catch (System.NullReferenceException)
        {
            // Control específico de referencias nulas
            return;
        }
        catch (Exception ex)
        {
            await DisplayMensajes.DisplayErrorAlert($"{ex.Message} Vuelva a intentarlo");
        }
    }

    private void btnNuevo_Clicked(object sender, EventArgs e)
    {
        // Lógica para nuevo registro
    }

    private async void listResultado_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        var item = e.SelectedItem as ModeloPedidosPosicionamiento;
        SetLoading(true);
        if (item.Servicio_id == null || item.Servicio_id == 0)
        {
            await DisplayMensajes.DisplayErrorAlert($"No tiene un servicio cargado");
            SetLoading(false);
            return;
            
        }
        await Navigation.PushAsync(new ServicioPage(item.Id,item.ContenedorNumero,item.Servicio_id));
        ((ListView)sender).SelectedItem = null;
        SetLoading(false);
    }
    #endregion eventos

    #region Funciones

    public async Task BusquedaAsync()
    {
        try
        {
            SetLoading(true);
            var resultado = await ObtenerResultadosBusquedaAsync();

            if (resultado == null || resultado.Count == 0)
            {
                await DisplayMensajes.MostrarMensajeError("No se encontraron resultados para la fecha: "+ fechaInicioDatePicker.Date.ToString("dd/MM/yyyy"));
                lblRegistros.Text = "No se encontraron resultados para la fecha: "+ fechaInicioDatePicker.Date.ToString("dd / MM / yyyy");
                lblRegistros.IsVisible = true;
            }
            else
            {
                lblRegistros.IsVisible = false;
                await DisplayMensajes.MostrarMensajeError("Se encontraron "+ resultado.Count.ToString() + " registros");
                
            }
                listResultado.ItemsSource = resultado;
        }
        finally
        {
            SetLoading(false);
        }
    }

    public void SetLoading(bool estado)
    {
        loading.IsVisible = estado;
        listResultado.IsVisible = !estado;
    }

    private async Task<List<ModeloPedidosPosicionamiento>> ObtenerResultadosBusquedaAsync()
    {
        try
        {
            var finicio = fechaInicioDatePicker.Date.ToString("dd/MM/yyyy");
            var jsonData = new Dictionary<string, object>
            {
                { "fecha", finicio },
                { "soloVerficado", Definiciones.SiNo.Si }
            };

            JsonNode datos = await request.LogisticaGetPedidosPosicionamientod(jsonData);
            var resultado = System.Text.Json.JsonSerializer.Deserialize<List<ModeloPedidosPosicionamiento>>(datos.ToString());
            return resultado ?? new List<ModeloPedidosPosicionamiento>();
        }
        catch (System.NullReferenceException)
        {
            throw;
        }
        catch (SesionException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}
