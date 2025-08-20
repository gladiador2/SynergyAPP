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

public partial class ServicioPage : ContentPage
{
    #region Fields
    //private readonly ServicioViewModel viewModel = new();
    private ModeloOrdenServicio Servicio = new();
    OrdenesServicioViewModel viewModel;
    private readonly OrdenesServiciosRequest request = new();
    private readonly int id_orden_servicio = 0;
    private readonly int id_servicio = 0;

    #endregion

    #region Constructors
    public ServicioPage()
    {
        InitializeComponent();
        viewModel = new OrdenesServicioViewModel();
        BindingContext = viewModel;

    }

    public ServicioPage(int? OrdenServicioId, string contenedor, int? idServicio) : this()
    {
        id_orden_servicio = OrdenServicioId ?? 0;
        id_servicio = idServicio ?? 0;
        // Cambia el título de la página por el valor del contenedor recibido
        this.Title = contenedor;
    }
    #endregion

    #region Lifecycle Methods
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = BuscarAsync();
    }
    #endregion

    #region Private Methods
    private async Task BuscarAsync()
    {
        // Asignar un valor a la propiedad generada:
        this.viewModel.ListaOrdenServicio = new List<ModeloOrdenServicio>();
        // Obtén la lista de servicios
        var servicios = await ObtenerResultadosBusquedaAsync();

        // Para cada servicio, asigna Index y RowColor a cada Detalle
        foreach (var servicio in servicios)
        {
            if (servicio.Detalles != null)
            {
                for (int i = 0; i < servicio.Detalles.Length; i++)
                {
                    servicio.Detalles[i].Index = i;
                    servicio.Detalles[i].RowColor = (i % 2 == 0) ? Color.FromArgb("#F5F5F5") : Colors.White;
                }
            }
        }
        this.viewModel.ListaOrdenServicio = servicios;


        collectionViewServivios.ItemsSource = this.viewModel.ListaOrdenServicio;
    }

    private async Task<List<ModeloOrdenServicio>> ObtenerResultadosBusquedaAsync()
    {
        var jsonData = new Dictionary<string, object>
            {
                { "PedidoPosId", id_orden_servicio.ToString() }
            };
        try
        {

            JsonNode datos = await request.LogisticaGetPedidoPosDetalle(jsonData);
            var resultado = System.Text.Json.JsonSerializer.Deserialize<List<ModeloOrdenServicio>>(datos.ToString());
            return resultado ?? new List<ModeloOrdenServicio>();
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

    //private async Task<List<ModeloOrdenServicio>> GuardarAsync()
    //{
    //    try
    //    {
    //        // Asegúrate de tener el objeto Servicio correctamente inicializado
    //        Servicio = this.viewModel.ListaOrdenServicio.FirstOrDefault();

    //        // Convierte el array de detalles a una lista de diccionarios
    //        var detallesList = Servicio?.Detalles?
    //            .Select(detalle => new Dictionary<string, object>
    //            {
    //            { "id", detalle.id },
    //            { "SubServicio", detalle.SubServicio },
    //            { "cant", detalle.Cant },
    //            { "Articulo", detalle.Articulo },
    //            { "Aprobado", detalle.Aprobado },
    //            { "Observacion", detalle.Observacion }
    //            }).ToList();

    //        var jsonData = new Dictionary<string, object>
    //    {

    //        { "detalles", detallesList }
    //    };



    //        JsonNode datos = await request.LogisticaModificarPedidoPosDetalle(jsonData);
    //        var resultado = System.Text.Json.JsonSerializer.Deserialize<List<ModeloOrdenServicio>>(datos.ToString());
    //        return resultado ?? new List<ModeloOrdenServicio>();
    //    }
    //    catch (System.NullReferenceException)
    //    {
    //        throw;
    //    }
    //    catch (SesionException)
    //    {
    //        throw;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    private async Task<List<ModeloOrdenServicio>> GuardarAsync()
    {
        try
        {
            // Asegúrate de tener el objeto Servicio correctamente inicializado
            Servicio = this.viewModel.ListaOrdenServicio.FirstOrDefault();
            Servicio.PedidoPosId = id_orden_servicio;
            // Serializa el objeto completo a JSON
            var servicioJson = System.Text.Json.JsonSerializer.Serialize(Servicio);

            // Envía el objeto serializado en el diccionario
            var jsonData = new Dictionary<string, object>
        {
           // { "servicio", System.Text.Json.Nodes.JsonNode.Parse(servicioJson) }
           { "servicio", Servicio }

        };

            JsonNode datos = await request.LogisticaModificarPedidoPosDetalle(jsonData);
            var resultado = System.Text.Json.JsonSerializer.Deserialize<List<ModeloOrdenServicio>>(datos.ToString());
            return resultado ?? new List<ModeloOrdenServicio>();
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

    #endregion



    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await this.ScaleTo(0.97, 80, Easing.CubicIn);
        await this.ScaleTo(1, 80, Easing.CubicOut);
        try
        {
            var jsonData = new Dictionary<string, object>
            {
                { "PedidoPosId", id_orden_servicio.ToString() }
            };
            JsonNode datos = await request.LogisticaGetPedidoPosInpresion(jsonData);
            List<PedidoPosicionamientoInforme> ordenServicio = System.Text.Json.JsonSerializer.Deserialize<List<PedidoPosicionamientoInforme>>(datos);

            ImpresionOS impresion = new ImpresionOS(ordenServicio);
            impresion.imprimir();
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void btnGuardar_Clicked(object sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            animacionesServices.Fade(btn); // animación de rebote
        }
        GuardarAsync();
    }
    private void btnEscanearBarcode_Clicked(object sender, EventArgs e)
    {

        // Obtén el ImageButton que disparó el evento
        var imageButton = sender as ImageButton;

        if (imageButton != null)
        {
            // Obtén el contenedor padre (Grid) del ImageButton
            var parentGrid = imageButton.Parent as Microsoft.Maui.Controls.Grid;

            if (parentGrid != null)
            {
                // Encuentra el Entry dentro del mismo Grid
                var entry = parentGrid.Children
                                      .OfType<Entry>()
                                      .FirstOrDefault();

                if (entry != null)
                {
                    var popup = new LectorQrPage(ref entry);
                    this.ShowPopup(popup);
                }

            }
        }
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

    private async void btnActa_Clicked(object sender, EventArgs e)
    {
        
        try
        {
            
            var jsonData = new Dictionary<string, object>
            {
                { "ServicioId", id_servicio.ToString() }
            };
            JsonNode datos = await request.LogisticaGetTipoServicio(jsonData);
            var resultado = System.Text.Json.JsonSerializer.Deserialize<List<TipoServicio>>(datos.ToString());
            ModeloActasOrdenServicio acta = await GetActa(id_orden_servicio);
            TipoServicio tipoServicio = resultado.FirstOrDefault();
            switch (tipoServicio.Nombre)
            {
                case ConstantesApp.TiposServicio.Consolidacion:
                    await Navigation.PushAsync(new ConsolidacionPage(id_orden_servicio,acta));
                    break;
                case ConstantesApp.TiposServicio.Desconsolidacion:
                    await Navigation.PushAsync(new DesconsolidacionPage(id_orden_servicio,acta));
                    break;
                case ConstantesApp.TiposServicio.Verificacion:
                    await Navigation.PushAsync(new VerificacionPage(id_orden_servicio,acta));
                    break;
                default:
                    break;
            }
            

        }
        catch (System.NullReferenceException ex)
        {
            await DisplayAlert("Error", "Ocurrió un error inesperado: " + ex.Message, "Aceptar");
            
        }


    }
    private async Task<ModeloActasOrdenServicio> GetActa(int ordenServicioId)
    {
        try
        {
            var jsonData = new Dictionary<string, object>
            {
                { "ServicioId", ordenServicioId.ToString() }
            };
            JsonNode datos = await request.LogisticaGetActaOS(jsonData);
            var resultado = System.Text.Json.JsonSerializer.Deserialize<List<ModeloActasOrdenServicio>>(datos.ToString());
            return resultado.FirstOrDefault();
        }
        catch (Exception)
        {
            return null;
        }
    }
}

