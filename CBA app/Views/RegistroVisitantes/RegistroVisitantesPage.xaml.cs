using CBA_app.ViewModels.RegistroVisitantes;

namespace CBA_app.Views.RegistroVisitantes;

public partial class RegistroVisitantesPage : ContentPage
{
    
    public RegistroVisitantesPage()
    {
        InitializeComponent();
       
        
    }
    protected override void OnAppearing()
    {
        
        base.OnAppearing();

    }
    private async void btnEntrada_Clicked(object sender, EventArgs e)
    {
        loading.IsVisible = true;
        await Navigation.PushAsync(new RegistroVisitantes.Movimientos.VisitantesEntradaPage());
        //await Shell.Current.GoToAsync($"//{nameof(RegistroVisitantes.Movimientos.VisitantesEntradaPage)}");
        loading.IsVisible = false;
    }

    private async void btnListado_Clicked(object sender, EventArgs e)
    {
        loading.IsVisible = true;


        await Navigation.PushAsync(new RegistroVisitantes.Listados.VisitantesListadoPage());
        loading.IsVisible = false;
    }

    private async void btnSalida_Clicked(object sender, EventArgs e)
    {
        loading.IsVisible = true;
        await Navigation.PushAsync(new RegistroVisitantes.Movimientos.VisitantesSalidaPage());

        loading.IsVisible = false;
    }


   
}