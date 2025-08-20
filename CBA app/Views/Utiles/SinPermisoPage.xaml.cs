namespace CBA_app.Views.Utiles;

public partial class SinPermisoPage : ContentPage
{
    public SinPermisoPage(string mensaje)
    {
        InitializeComponent();
        lblmensaje.Text = mensaje;
    }

    public SinPermisoPage() : this("No tiene permiso")
    {
    }
}