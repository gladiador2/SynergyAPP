using CBA_app.ViewModels.RegistroVisitantes;
using CBA_app.Views.RegistroVisitantes.VisitanteVehiculos;

namespace CBA_app.Views.RegistroVisitantes.Movimientos;

public partial class VisitantesEntradaPage : ContentPage
{
    VisitantesEntradaViewModel viewModel = new VisitantesEntradaViewModel();
	public VisitantesEntradaPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        
        if (mySwitch.IsToggled)
        {
            viewModel.TextoBusquedaVehiculo = txtBuscador.Text;
            txtBuscador.Text = string.Empty;
            var vehiculo = await viewModel.GetVeiculo(); //busca por numero de chapa
            if (vehiculo.ID == 0) // el Vehiculo no existe
            {
                
                await Navigation.PushAsync(new VisitantesVehiculosPage(viewModel));
            }
            else
            {
                
                //guardar la cabecera
                await Navigation.PushAsync(new VisitantePersonaPage(viewModel));
            }
            
        }
        else
        {
            viewModel.TextoBusqueda = txtBuscador.Text;
            txtBuscador.Text = string.Empty;
            await viewModel.GetVisitante();
            //guardar la cabecera
            await Navigation.PushAsync(new VisitantePersonaPage(viewModel));
        }
       
    }
}