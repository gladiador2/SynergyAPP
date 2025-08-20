using CBA_app.Models.OrdenesServicio;
using CBA_app.Request;
using CBA_app.Services;
using CBA_app.ViewModels.OrdenesServicio;
using System.Text.Json.Nodes;

namespace CBA_app.Views.OrdenServicio;

public partial class ConsolidacionPage : ContentPage
{
    private readonly int _ordenServicioId;
    ActaViewModel viewModel;
    private readonly OrdenesServiciosRequest request = new();

    public ConsolidacionPage()
    {
        InitializeComponent();
    }

    public ConsolidacionPage(int ordenServicioId, ModeloActasOrdenServicio acta) : this()
    {
        _ordenServicioId = ordenServicioId;
        viewModel = new ActaViewModel();
        viewModel.ActaOS = acta;
        BindingContext = viewModel.ActaOS;
    }

   
    private void Expander_ExpandedChanged(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {
        if (sender is CommunityToolkit.Maui.Views.Expander expander)
        {
            if (expander.Header is VisualElement headerElement)
            {
                animacionesServices.Fade(headerElement);
            }
        }
    }
}
