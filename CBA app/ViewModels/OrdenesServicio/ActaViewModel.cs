
using CBA_app.Models.OrdenesServicio;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;


namespace CBA_app.ViewModels.OrdenesServicio
{
  public partial class ActaViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ModeloActasOrdenServicio actaOS;
    }
}
