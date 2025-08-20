using CBA_app.Models.RegistroVisitantes;
using CBA_app.Request;
using CBA_app.Services;
using CBA_app.ViewModels.RegistroVisitantes;
using System.Text.Json.Nodes;

namespace CBA_app.Views.RegistroVisitantes.Movimientos;

public partial class VisitantesSalidaPage : ContentPage
{
   SalidaVisitantesViewModel viewModel = new SalidaVisitantesViewModel();
    public VisitantesSalidaPage()
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
     //   viewModel.GetListaVisitasAsync().Wait();
    }
    private void btnBuscador_Clicked(object sender, EventArgs e)
    {
        PanelsearchOperaciones.IsVisible = !PanelsearchOperaciones.IsVisible;
        FrameSeach.IsVisible = !FrameSeach.IsVisible;
        
        searchOperaciones.Focus();
    }

    private async void searchOperaciones_TextChanged(object sender, TextChangedEventArgs e)
    {
       
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //loading.IsVisible = !loading.IsVisible;
        Button btn = (Button)sender;
        btn.IsVisible = false;
        MoleloListaVisitas.Visitante modelo = (MoleloListaVisitas.Visitante)btn.BindingContext;
        int id = (int)modelo.id;
        await EliminarDetalle(id);
       // loading.IsVisible = !loading.IsVisible;
    }
    private async Task EliminarDetalle(int id) 
    {
        try
        {

            var jsonData = new Dictionary<string, object> { };

            jsonData.Add("LLEGADA_ID", Definiciones.Error.Valor.IntPositivo);
            jsonData.Add("DETALLE_ID", id);
            jsonData.Add("soloVehiculo", false);
            RegistroVisitasRequest request = new RegistroVisitasRequest();
            JsonNode datos = await request.RegistrarSalida(jsonData);

            if (datos != null)
            {
                await viewModel.GetListaVisitasAsync();
 
            }

        }
        catch (Exception ex)
        {
            // Mostrar mensaje de error en un DisplayAlert
            await DisplayMensajes.DisplayErrorAlert(ex);
        }
    }
    private async Task EliminarCabecera(int id)
    {
        try
        {
            var jsonData = new Dictionary<string, object> { };

            jsonData.Add("LLEGADA_ID", id);
            jsonData.Add("DETALLE_ID", Definiciones.Error.Valor.IntPositivo );
            jsonData.Add("soloVehiculo", false);
            RegistroVisitasRequest request = new RegistroVisitasRequest();
            JsonNode datos = await request.RegistrarSalida(jsonData);

            if (datos != null)
            {
               await viewModel.GetListaVisitasAsync();
                // ListaVisitas = System.Text.Json.JsonSerializer.Deserialize<List<MoleloListaVisitas.ListaVisitas>>(datos.ToString());
            }
        }
        catch (Exception ex)
        {
            // Mostrar mensaje de error en un DisplayAlert
            await DisplayMensajes.DisplayErrorAlert(ex);

        }
    }
    private async Task EliminarSoloauto(int id)
    {
        try
        {

            var jsonData = new Dictionary<string, object> { };

            jsonData.Add("LLEGADA_ID", id);
            jsonData.Add("DETALLE_ID", Definiciones.Error.Valor.IntPositivo);
            jsonData.Add("soloVehiculo", true);
            RegistroVisitasRequest request = new RegistroVisitasRequest();
            JsonNode datos = await request.RegistrarSalida(jsonData);

            if (datos != null)
            {
                await viewModel.GetListaVisitasAsync();
                // ListaVisitas = System.Text.Json.JsonSerializer.Deserialize<List<MoleloListaVisitas.ListaVisitas>>(datos.ToString());
            }

        }
        catch (Exception ex)
        {
            // Mostrar mensaje de error en un DisplayAlert
            await App.Current.MainPage.DisplayAlert("Error", $"Ocurri� un error: {ex.Message}", "Aceptar");
        }
    }

    private async void btnRegistrarSalida_Clicked(object sender, EventArgs e)
    {
        loading.IsVisible = true;
        Button btn = (Button)sender;
        MoleloListaVisitas.ListaVisitas modelo = (MoleloListaVisitas.ListaVisitas)btn.BindingContext;
        int id = (int)modelo.id;
        string opcion = await DisplayActionSheet("Desea dar salida a:", null, null, "El auto", "Todos");

        if (opcion == "El auto")
        {
            await EliminarSoloauto(id); //elimina la cabecera y los visitantes que se encuentran adentro
        }

        else if (opcion == "Todos")
        {
            await EliminarCabecera(id); //elimina la cabecera y los visitantes que se encuentran adentro
        }

        loading.IsVisible = false;
    }
}