using CBA_app.Models.RegistroVisitantes;
using CBA_app.Request;
using CBA_app.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CBA_app.ViewModels.RegistroVisitantes
{
    
    public partial class ListadoVisitantesViewModel : BaseViewModel
    {
        readonly RegistroVisitasRequest request = new RegistroVisitasRequest();

        [ObservableProperty]
        private List<MoleloListaVisitas.ListaVisitas> _listaVisitas;

        [ObservableProperty]
        private string _nroDocumento;

        [ObservableProperty]
        private string _nroChapa;

        [ObservableProperty]
        private int _cabecraID;


        [ObservableProperty]
        private DateTime _fechaBusqueda;

        [ObservableProperty]
        private bool _soloEnPredio;

        public ListadoVisitantesViewModel()
        {
            inicializarFiltros();
            
        }
        void inicializarFiltros()
        {
            NroChapa = string.Empty;
            NroDocumento = string.Empty;
            CabecraID = Definiciones.Error.Valor.IntPositivo;
            FechaBusqueda = DateTime.Now;
        }
    
        public async Task GetListaVisitasAsync()
        {
            try
            {
                
                var jsonData = new Dictionary<string, object> { };

                jsonData.Add("fecha", FechaBusqueda);
                jsonData.Add("cabeceraId", CabecraID);
                jsonData.Add("nroDocumento", NroDocumento);
                jsonData.Add("nroChapa", NroChapa);
                jsonData.Add("soloEnPredio", SoloEnPredio);
                
                JsonNode datos = await request.getListaVisitantes(jsonData);

                if (datos != null)
                {
                   ListaVisitas = System.Text.Json.JsonSerializer.Deserialize<List<MoleloListaVisitas.ListaVisitas>>(datos.ToString());

                    
                }
                
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                
            }
        }
        public async Task<List<MoleloListaVisitas.ListaVisitas>> GetTotalVisitasAsync()
        {
            try
            {

                var jsonData = new Dictionary<string, object> { };

                jsonData.Add("fecha", string.Empty);
                jsonData.Add("cabeceraId", string.Empty);
                jsonData.Add("nroDocumento", string.Empty);
                jsonData.Add("nroChapa", string.Empty);
                jsonData.Add("soloEnPredio", false);
                
                JsonNode datos = await request.getListaVisitantes(jsonData);

                if (datos != null)
                {
                  var  ListatotalVisitas = System.Text.Json.JsonSerializer.Deserialize<List<MoleloListaVisitas.ListaVisitas>>(datos.ToString());
                    return ListatotalVisitas;

                }
                return null;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }
    }
}
