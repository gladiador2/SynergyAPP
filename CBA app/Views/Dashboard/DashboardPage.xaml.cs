using CBA_app.Models.Dashboard;
using System.Collections.ObjectModel;

namespace CBA_app.Views.Dashboard
{
    public partial class DashboardPage : ContentPage
    {
        public ObservableCollection<GraficoItem> DatosGrafico { get; set; }

        public DashboardPage()
        {
            InitializeComponent();
            DatosGrafico = new ObservableCollection<GraficoItem>
            {
                new GraficoItem { Categoria = "Cajas dañadas", Valor = 5 },
                new GraficoItem { Categoria = "Cajas abiertas", Valor = 3 },
                new GraficoItem { Categoria = "Muestras retiradas", Valor = 7 }
            };

            BindingContext = this;
        }
    }
}
