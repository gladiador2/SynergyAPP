using CBA_app.Models.OrdenesServicio;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA_app.ViewModels.OrdenesServicio
{
   public partial class ServicioViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ModeloOrdenServicio _servicio;

       
    }
}
