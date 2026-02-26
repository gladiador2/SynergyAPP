using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CBA_app.Templates;

public partial class BotonesCarouselView : ContentView
{
    public ObservableCollection<BotonCarrusel> Botones { get; set; } = new();

    public BotonesCarouselView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public void AgregarBoton(string Imagen, ICommand comando)
    {
        Botones.Add(new BotonCarrusel { Imagen = Imagen, Comando = comando });
    }

    public void QuitarUltimoBoton()
    {
        if (Botones.Count > 0)
            Botones.RemoveAt(Botones.Count - 1);
    }
}

public class BotonCarrusel
{
    public string Imagen { get; set; }
    public ICommand Comando { get; set; }
}
