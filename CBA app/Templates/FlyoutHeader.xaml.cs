using CBA_app.Views.Login;

namespace CBA_app.Templates;

public partial class FlyoutHeader : ContentView
{
	public FlyoutHeader()
	{
		InitializeComponent();

        if (App.UserDetails != null)
        {
            lblUserName.Text = "  " + App.UserDetails.usuario.nombre  ;
           
        }
    }

   

    private async void ImagePerfil_Clicked(object sender, EventArgs e)
    {
        var nav = new PerfilPage();
        nav.Title = "Perfil";
        await Navigation.PushAsync(nav);

        // Cierra el Flyout
        Shell.Current.FlyoutIsPresented = false;


    }
}