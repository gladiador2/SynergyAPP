

namespace CBA_app.Views;

public partial class Bienvenida : ContentPage
{
	public Bienvenida()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
      
        if (!FlyoutLateral.IsVisible)
        {
            // Mostrar y animar desde la derecha
            FlyoutLateral.IsVisible = true;
            FlyoutLateral.TranslationX = FlyoutLateral.Width; // Posición fuera de pantalla
            await FlyoutLateral.TranslateTo(0, 0, 250, Easing.CubicOut); // Desliza hacia la vista
            FlyoutLateral.Focus();
        }
        else
        {
            // Animar hacia la derecha y ocultar
            await FlyoutLateral.TranslateTo(FlyoutLateral.Width, 0, 250, Easing.CubicIn);
            FlyoutLateral.IsVisible = false;
        }

        if (FlyoutLateral.IsVisible)
        {
            FlyoutLateral.Focus();
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (FlyoutLateral.IsVisible)
        {
            await FlyoutLateral.TranslateTo(FlyoutLateral.Width, 0, 250, Easing.CubicIn);
            FlyoutLateral.IsVisible = false;
        }
    }
}