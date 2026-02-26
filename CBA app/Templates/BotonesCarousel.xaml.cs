using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CBA_app.Templates;

    public partial class BotonesCarousel : ContentView
{
    public ObservableCollection<BotonScroll> Botones { get; set; } = new();

    public BotonesCarousel()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public void AgregarBoton(string Imagen, ICommand comando)
    {
        Botones.Add(new BotonScroll { Imagen = Imagen, Comando = comando });
    }

    public void QuitarUltimoBoton()
    {
        if (Botones.Count > 0)
            Botones.RemoveAt(Botones.Count - 1);
    }
}

public class BotonScroll
{
    public string Imagen { get; set; }
    public ICommand Comando { get; set; }
}
