using CBA_app.Models.RegistroVisitantes;
using CBA_app.Request;
using CBA_app.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CBA_app.ViewModels.RegistroVisitantes
{
    
    public partial class SalidaVisitantesViewModel : BaseViewModel
    {
        readonly RegistroVisitasRequest request = new();
        private bool buscarSintext = false;

        [ObservableProperty]
        private List<MoleloListaVisitas.ListaVisitas> _listaVisitas;

        [ObservableProperty]
        private string _textoBusqueda = string.Empty;

        [ObservableProperty]
        private bool _loading;

        [ObservableProperty]
        private bool _mostrarDatos;


        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                Buscar();
                
            }
        }
        public SalidaVisitantesViewModel()
        {
            searchText = string.Empty;
            GetListaVisitas();


        }
        public async void GetListaVisitas() 
        {
            await GetListaVisitasAsync();
        }


        public async Task GetListaVisitasAsync()
        {
            try
            {
                Loading = true;
                MostrarDatos = false;
                var jsonData = new Dictionary<string, object> { };

                jsonData.Add("fecha", string.Empty);
                jsonData.Add("cabeceraId", Definiciones.Error.Valor.IntPositivo);
                jsonData.Add("nroDocumento", searchText);
                jsonData.Add("nroChapa", searchText.ToUpper());
                jsonData.Add("soloEnPredio", true);
                
                JsonNode datos = await request.getListaVisitantes(jsonData);

                if (datos != null)
                {
                    // Deserializar la lista completa de visitas
                    var todasLasVisitas = System.Text.Json.JsonSerializer.Deserialize<List<MoleloListaVisitas.ListaVisitas>>(datos.ToString());

                    if (todasLasVisitas != null)
                    {
                        // Tomar los últimos 5 registros
                        ListaVisitas = todasLasVisitas.TakeLast(5).ToList();

                        // Asignar orden a los últimos 5 registros
                        ListaVisitas.Select((visita, index) => { visita.orden = index; return visita; }).ToList();
                    }
                }
                MostrarDatos = true;
                Loading = false;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                Loading = false;
            }
            finally {  }
        }


        [ICommand]
        async Task Buscar()
        {
            if (searchText != string.Empty || buscarSintext)
            {
                await GetListaVisitasAsync();
                buscarSintext = true;
            }
            
        }
    }
}

