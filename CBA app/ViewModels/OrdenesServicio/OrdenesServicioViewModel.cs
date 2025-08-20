using CBA_app.Models.OrdenesServicio;
using CBA_app.Views.OrdenServicio;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CBA_app.ViewModels.OrdenesServicio
{
    public partial class OrdenesServicioViewModel :BaseViewModel
    {
        [ObservableProperty]
        private List< ModeloOrdenServicio> _listaOrdenServicio;

        [ObservableProperty]
        private ModeloPedidosPosicionamiento _listaPedidosPosicionamiento;

        [ObservableProperty]
        private bool _isRefreshing;

        public OrdenSevicioPage OrdenesServicioPage  { get; set; }

        public ICommand RefreshCommand { get; }

        public OrdenesServicioViewModel()
        {
            RefreshCommand = new RelayCommand(OnRefresh);
        }
        private async void OnRefresh()
        {
            if (OrdenesServicioPage == null)
                return;

            IsRefreshing = true;

            await OrdenesServicioPage.BusquedaAsync();

            IsRefreshing = false;
        }
    }
}
