using CBA_app.ViewModels.RegistroVisitantes;

namespace CBA_app.Views.RegistroVisitantes.VisitanteVehiculos;

public partial class VisitantesVehiculosPage : ContentPage
{
    VisitantesEntradaViewModel ViewModel;

    public VisitantesVehiculosPage()
	{
		InitializeComponent();
	}
    public VisitantesVehiculosPage(VisitantesEntradaViewModel viewModel)
    {
        InitializeComponent();
        this.ViewModel = viewModel;
        this.BindingContext = viewModel;
        buscarVehiculo();
    }
        
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await  ViewModel.GuardarVehiculo();
        await Navigation.PushAsync(new VisitantePersonaPage(ViewModel));
    }

    private async void txtNumeroChapa_TextChanged(object sender, TextChangedEventArgs e)
    {
        await buscarVehiculo();
    }
    public async Task buscarVehiculo()
    {
        try
        {
            ViewModel.TextoBusquedaVehiculo = txtNumeroChapa != null || txtNumeroChapa.Text != string.Empty ? txtNumeroChapa.Text : ViewModel.TextoBusquedaVehiculo;
            await ViewModel.GetVeiculo();
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("¡Oop algo salio mal!", $"Error: {ex.Message}", "Aceptar");

            throw;
        }
    }
}