using CBA_app.Services;
using CommunityToolkit.Maui.Alerts;

namespace CBA_app.Views;

public partial class SinInternet : ContentPage
{
	public SinInternet()
	{
		InitializeComponent();
	}
    public SinInternet(string mensaje) : this()
    {
        lblPrincipal.Text = mensaje;
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        ConnectivityService connectivity = new ConnectivityService();
		if (connectivity.IsConnectedToInternet())
			{
				await Navigation.PopAsync();
			}
		else
			{
				await Toast.Make("No pudo conectarse", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
			}
    }
}