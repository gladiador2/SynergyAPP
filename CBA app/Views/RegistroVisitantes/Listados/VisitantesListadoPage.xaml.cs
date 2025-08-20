using CBA_app.ViewModels.RegistroVisitantes;

namespace CBA_app.Views.RegistroVisitantes.Listados;

public partial class VisitantesListadoPage : ContentPage
{   
    ListadoVisitantesViewModel viewModel = new ListadoVisitantesViewModel();
	public VisitantesListadoPage()
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        refrescar();
        //viewModel.GetListaVisitasAsync().Wait();
    }
    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        refrescar();
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        refrescar();
    }

    private void mySwitch_Toggled(object sender, ToggledEventArgs e)
    {
        refrescar();
    }
    async void refrescar() 
    {
        loading.IsVisible = true;
        await viewModel.GetListaVisitasAsync();
        loading.IsVisible = false;
    }
}