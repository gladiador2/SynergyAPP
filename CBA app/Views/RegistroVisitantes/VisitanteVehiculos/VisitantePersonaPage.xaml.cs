using CBA_app.ViewModels.RegistroVisitantes;

namespace CBA_app.Views.RegistroVisitantes.VisitanteVehiculos;

public partial class VisitantePersonaPage : ContentPage
{
    VisitantesEntradaViewModel ViewModel;
    public VisitantePersonaPage()
	{
		InitializeComponent();
	}
    public VisitantePersonaPage(VisitantesEntradaViewModel viewModel)
    {
        this.ViewModel = viewModel;
        this.BindingContext = viewModel;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        txtDocumento.Focus();
    }
    private async void txtDocumento_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            ViewModel.TextoBusqueda = txtDocumento != null ? txtDocumento.Text : ViewModel.TextoBusqueda;
            await ViewModel.GetVisitante();
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("¡Oop algo salio mal!", $"Error: {ex.Message}", "Aceptar");
            
            throw;
        }
        
    }

    private async void btnRegistrarVisita_Clicked(object sender, EventArgs e)
    {
        loading.IsVisible = true;
            await ViewModel.GuardarVisitante();
          
            await ViewModel.RegistrarEntrada();
            ViewModel.TextoBusqueda = string.Empty;
            await ViewModel.GetVisitante();
            await ViewModel.GetListaVisitasAsync();
        loading.IsVisible = false;

    }

    private async void btnFinalizar_Clicked(object sender, EventArgs e)
    {
        ViewModel.finalizar();
        await Navigation.PopToRootAsync();
        
    }
}