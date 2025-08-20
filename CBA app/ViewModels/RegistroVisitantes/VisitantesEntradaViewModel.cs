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


    public partial class VisitantesEntradaViewModel : BaseViewModel
    {
        readonly RegistroVisitasRequest request = new();
        #region propiedades


        [ObservableProperty]
        private string _textoBusqueda;

        [ObservableProperty]
        private string _textoBusquedaVehiculo;

        #region Vehiculo
        [ObservableProperty]
        public bool _loading;

        [ObservableProperty]
        public List<MoleloListaVisitas.Visitante> _listaVisitantes;

        [ObservableProperty]
        public int _vehiculoID = Definiciones.Error.Valor.IntPositivo;
        [ObservableProperty]
        public string _vehiculoCHAPA = string.Empty;
        [ObservableProperty]
        public string _vehiculoMARCA;
        [ObservableProperty]
        public string _vehiculoTIPO;
        [ObservableProperty]
        public string _vehiculoOBSERVACIONES;
        [ObservableProperty]
        public string _vehiculoESTADO;
        [ObservableProperty]
        public string _vehiculoCamion;
        [ObservableProperty]
        public string _vehiculoCarreta;
        [ObservableProperty]
        public string _vehiculoContenedor;
        [ObservableProperty]
        public string _vehiculoMicDta;
        #endregion Vehiculo

        #region persona
        [ObservableProperty]
        public string _personaobjeto_nombre;

        [ObservableProperty]
        public int _personaid = Definiciones.Error.Valor.IntPositivo;

        [ObservableProperty]
        public int _personaorden;

        [ObservableProperty]
        public int _personaID;

        [ObservableProperty]
        public string _personaNOMBRE;

        [ObservableProperty]
        public string _personaNRO_DOCUMENTO;

        [ObservableProperty]
        public string _personaEMPRESA;

        [ObservableProperty]
        public string _personaESTADO;

        #endregion persona

        #region LLegada
        [ObservableProperty]
        public int _llegadaID = Definiciones.Error.Valor.IntPositivo;

        [ObservableProperty]
        public decimal _llegadaVEHICULO_ID =Definiciones.Error.Valor.EnteroPositivo;

        [ObservableProperty]
        public decimal _llegadaVISITANTE_ID = Definiciones.Error.Valor.EnteroPositivo;

        [ObservableProperty]
        public int _llegadaDETALLE_ID = Definiciones.Error.Valor.IntPositivo;

        [ObservableProperty]
        public string _llegadaES_CHOFER;

        [ObservableProperty]
        public ModeloRegistroEntrada.Entrada _modeloEntrada;
        #endregion LLegada

        #endregion propiedades
        public VisitantesEntradaViewModel()
        {
                
        }
        public async Task<ModeloRegistroEntrada.ModeloVehiculo> GetVeiculo()
        {
            try
            {
                ModeloRegistroEntrada.ModeloVehiculo vehiculo = new ModeloRegistroEntrada.ModeloVehiculo();
                var jsonData = new Dictionary<string, object> { };
                
                jsonData.Add("chapa_nro", TextoBusquedaVehiculo == null ? string.Empty : TextoBusquedaVehiculo);

               
                JsonNode datos = await request.GetVehiculo(jsonData);

                if (datos != null)
                {
                    var ListVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ModeloRegistroEntrada.ModeloVehiculo>>(datos.ToString());

                    if (ListVehiculos.Count > 0)
                    {
                        vehiculo = ListVehiculos.First();
                        VehiculoID = (int)vehiculo.id;
                        VehiculoCHAPA = vehiculo.CHAPA;
                        VehiculoMARCA = vehiculo.MARCA;
                        VehiculoTIPO = vehiculo.TIPO;
                        VehiculoOBSERVACIONES = vehiculo.OBSERVACIONES;
                        VehiculoESTADO = vehiculo.ESTADO;
                        VehiculoCamion = vehiculo.CAMION;
                        VehiculoCarreta = vehiculo.CARRETA;
                        VehiculoContenedor = vehiculo.CONTENEDOR;
                        VehiculoMicDta = vehiculo.MICDTA;
                        LlegadaID = Definiciones.Error.Valor.IntPositivo;
                    }
                    else
                    {
                        
                        VehiculoID = (int)vehiculo.id;
                        VehiculoCHAPA = TextoBusquedaVehiculo;
                        VehiculoMARCA = string.Empty;
                        VehiculoTIPO = String.Empty;
                        VehiculoOBSERVACIONES = string.Empty;
                        VehiculoESTADO = string.Empty;
                        VehiculoCamion = string.Empty;
                        VehiculoCarreta = string.Empty;
                        VehiculoContenedor = string.Empty;
                        VehiculoMicDta = string.Empty;
                        LlegadaID = Definiciones.Error.Valor.IntPositivo;
                    }
                }
                return vehiculo;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }
        public async Task<ModeloRegistroEntrada.ModeloPersonas> GetVisitante()
        {
            try
            {
                ModeloRegistroEntrada.ModeloPersonas persona = new ModeloRegistroEntrada.ModeloPersonas();
                var jsonData = new Dictionary<string, object> { };

                jsonData.Add("nro_documento", TextoBusqueda == null ? string.Empty : TextoBusqueda);

                JsonNode datos = await request.GetPersona(jsonData);

                if (datos != null)
                {
                    var ListPersona = System.Text.Json.JsonSerializer.Deserialize<List<ModeloRegistroEntrada.ModeloPersonas>>(datos.ToString());

                    if (ListPersona.Count > 0)
                    {
                        persona = ListPersona.First();
                        PersonaID = (int)persona.ID;
                        PersonaNRO_DOCUMENTO = persona.NRO_DOCUMENTO;
                        PersonaEMPRESA = persona.EMPRESA;
                        PersonaNOMBRE = persona.NOMBRE;
                        PersonaESTADO = persona.ESTADO;

                    }
                    else
                    {
                        PersonaID = Definiciones.Error.Valor.IntPositivo;
                        PersonaNRO_DOCUMENTO = TextoBusqueda;
                        PersonaEMPRESA = string.Empty;
                        PersonaNOMBRE = string.Empty;
                        PersonaESTADO = string.Empty;
                    }
                }
                return persona;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }

        public async Task<ModeloRegistroEntrada.ModeloPersonas> GuardarVisitante()
        {
            try
            {
                ModeloRegistroEntrada.ModeloPersonas persona = new ModeloRegistroEntrada.ModeloPersonas();
                var jsonData = new Dictionary<string, object> { };
                jsonData.Add("ID", PersonaID) ;
                jsonData.Add("NOMBRE", PersonaNOMBRE);
                jsonData.Add("NRO_DOCUMENTO", PersonaNRO_DOCUMENTO);
                jsonData.Add("EMPRESA", PersonaEMPRESA);
                jsonData.Add("ESTADO",PersonaESTADO);
                JsonNode datos = await request.GuardarPersona(jsonData);

                if (datos != null)
                {
                    var ListPersona = System.Text.Json.JsonSerializer.Deserialize<List<ModeloRegistroEntrada.ModeloPersonas>>(datos.ToString());

                    if (ListPersona.Count > 0)
                    {
                        persona = ListPersona.First();
                        PersonaID = (int)persona.ID;
                        PersonaNRO_DOCUMENTO = persona.NRO_DOCUMENTO;
                        PersonaEMPRESA = persona.EMPRESA;
                        PersonaNOMBRE = persona.NOMBRE;
                        PersonaESTADO = persona.ESTADO;

                    }
                    else
                    {
                        PersonaID = 0;
                        PersonaEMPRESA = string.Empty;
                        PersonaNOMBRE = string.Empty;
                        PersonaESTADO = string.Empty;
                    }
                }
                return persona;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }

        public async Task<ModeloRegistroEntrada.ModeloVehiculo> GuardarVehiculo()
        {
            try
            {
                ModeloRegistroEntrada.ModeloVehiculo vehiculo = new ModeloRegistroEntrada.ModeloVehiculo();
                var jsonData = new Dictionary<string, object> { };

                jsonData.Add("ID", VehiculoID == 0 ? Definiciones.Error.Valor.EnteroPositivo : VehiculoID);
                jsonData.Add("CHAPA", VehiculoCHAPA);
                jsonData.Add("MARCA", VehiculoMARCA);
                jsonData.Add("TIPO", VehiculoTIPO);
                jsonData.Add("OBSERVACIONES", VehiculoMARCA);
                jsonData.Add("CAMION", VehiculoCamion);
                jsonData.Add("CARRETA", VehiculoCarreta);
                jsonData.Add("CONTENEDOR", VehiculoContenedor);
                jsonData.Add("MICDTA", VehiculoMicDta);
                jsonData.Add("ESTADO", "A");
                JsonNode datos = await request.GuardarVehiculo(jsonData);

                if (datos != null)
                {
                    var ListVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ModeloRegistroEntrada.ModeloVehiculo>>(datos.ToString());

                    if (ListVehiculos.Count > 0)
                    {
                        vehiculo = ListVehiculos.First();
                        VehiculoID = (int)vehiculo.id;
                        VehiculoCHAPA = vehiculo.CHAPA;
                        VehiculoMARCA = vehiculo.MARCA;
                        VehiculoTIPO = vehiculo.TIPO;
                        VehiculoOBSERVACIONES = vehiculo.OBSERVACIONES;
                        VehiculoCamion = vehiculo.CAMION;
                        VehiculoCarreta = vehiculo.CARRETA;
                        VehiculoContenedor = vehiculo.CONTENEDOR;
                        VehiculoMicDta = vehiculo.MICDTA;
                        VehiculoESTADO = vehiculo.ESTADO;
                    }
                }
                return vehiculo;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
            }
        }

        public async Task<ModeloRegistroEntrada.Entrada> RegistrarEntrada()
        {
            try
            {
                
                var jsonData = new Dictionary<string, object> { };

                jsonData.Add("LLEGADA_ID", LlegadaID.ToString());
                jsonData.Add("VEHICULO_ID", VehiculoID == 0 ? Definiciones.Error.Valor.IntPositivo : int.Parse(VehiculoID.ToString()));
                jsonData.Add("VISITANTE_ID",PersonaID);
                jsonData.Add("DETALLE_ID", LlegadaDETALLE_ID);
                jsonData.Add("ES_CHOFER", LlegadaID == Definiciones.Error.Valor.IntPositivo  ? Definiciones.SiNo.Si : Definiciones.SiNo.No);
                
                JsonNode datos = await request.RegistrrarEntrada(jsonData);
                if (datos != null) 
                {
                    var LisModeloEntrada = System.Text.Json.JsonSerializer.Deserialize<List<ModeloRegistroEntrada.Entrada>>(datos.ToString());
                    ModeloEntrada = LisModeloEntrada.First();
                    LlegadaID = int.Parse(ModeloEntrada.LLEGADA_ID.ToString());
                }
               
                
                return ModeloEntrada;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);
                return null;
                
            }
        }
        public void finalizar() 
        {
            TextoBusqueda = string.Empty;
            TextoBusquedaVehiculo = string.Empty;
            LlegadaID = Definiciones.Error.Valor.IntPositivo;
            GetVeiculo();
            

        }
        public async Task GetListaVisitasAsync()
        {
            try
            {
                Loading = true;
                var jsonData = new Dictionary<string, object> { };

                jsonData.Add("fecha", string.Empty);
                jsonData.Add("cabeceraId", LlegadaID);
                jsonData.Add("nroDocumento", string.Empty);
                jsonData.Add("nroChapa", string.Empty);
                jsonData.Add("soloEnPredio", false);
                JsonNode datos = await request.getListaVisitantes(jsonData);

                if (datos != null)
                {
                   List<MoleloListaVisitas.ListaVisitas> ListaVisitas = System.Text.Json.JsonSerializer.Deserialize<List<MoleloListaVisitas.ListaVisitas>>(datos.ToString());
                    MoleloListaVisitas.ListaVisitas visita = ListaVisitas.First();
                    MoleloListaVisitas.Visitante[] visitantesArray = visita.visitantes;
                    ListaVisitantes = visitantesArray.ToList();

                }

            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en un DisplayAlert
                await DisplayMensajes.DisplayErrorAlert(ex);

            }
            finally { Loading = false; }
        }

    }
}
