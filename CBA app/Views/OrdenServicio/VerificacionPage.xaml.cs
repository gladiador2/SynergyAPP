using CBA_app.Models.OrdenesServicio;
using CBA_app.Request;
using CBA_app.Services;
using CBA_app.ViewModels.OrdenesServicio;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using CBA_app.Services.Impresion;
using System.Globalization;
using CommunityToolkit.Maui.Views;
using CBA_app.Views.LectorQR;
using CommunityToolkit.Maui.Views;

namespace CBA_app.Views.OrdenServicio;

public partial class VerificacionPage : ContentPage
{
    private readonly int _ordenServicioId;
    ActaViewModel viewModel;
    private readonly OrdenesServiciosRequest request = new();

    public VerificacionPage()
    {
        InitializeComponent();
    }

    public VerificacionPage(int ordenServicioId, ModeloActasOrdenServicio acta) : this()
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

    private void btnGuardar_Clicked(object sender, EventArgs e)
    {
          GuardarAsync();
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {

    }
    private async Task<ModeloActasOrdenServicio> GuardarAsync()
    {
        try
        {
          
            var jsonData = new Dictionary<string, object>
        {
           { "Acta", this.viewModel.ActaOS }

        };

            JsonNode datos = await request.LogisticaUpdateActaOS(jsonData);
            var resultado = System.Text.Json.JsonSerializer.Deserialize<List<ModeloActasOrdenServicio>>(datos.ToString());
            this.viewModel.ActaOS = resultado != null ? resultado.FirstOrDefault() : new ModeloActasOrdenServicio();
            return resultado != null ?  resultado.FirstOrDefault() : new ModeloActasOrdenServicio();
        }
        catch (System.NullReferenceException)
        {
            throw;
        }
        catch (SesionException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
