
namespace CBA_app.Views.Login;

public partial class PerfilPage : ContentPage
{
	public PerfilPage()
	{
		InitializeComponent();
        BindingContext = App.UserDetails;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //labelUsuario.Text = App.UserDetails.usuario;
        EntryNombre.Text = App.UserDetails.usuario.username;
        EntryApellido.Text = App.UserDetails.usuario.nombre;
        EntryCorreo.Text = App.UserDetails.usuario.email;
        EntrySucursal.Text = "CBA SA";
        //EntryAbreviatura.Text = App.UserDetails.sucursal.abreviatura;

    }
}