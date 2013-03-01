using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Configuration;
using RestSharp;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows;
using System.Diagnostics;

namespace InventoryApp.ViewModel.CargaItems
{
    public class CargaItemsViewModel : IPageViewModel, INotifyPropertyChanged
    {
        
        #region campos
        BatchLoadDataMapper dataMapper = new BatchLoadDataMapper();
        string routeBach = ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
        string basicAuthUser = "Administrator";
        string basicAuthPass = "Passw0rd1!";
        string nameServiceValidateProccess = "GetValidateNotExitProcessRunning";
        string nameServiceListBatch = "GetListBatchProcess";
        string nameServiceListLogLoad = "GetListLogLoad";
        string nameServiceLogingItems = "GetLoadingItems";
        private RelayCommand _abrirRutaCommand;
        private RelayCommand _ejecutarCargaCommand;
        private bool? _valideProcess;
        private FixupCollection<BATCH_LOAD_CARGAMOV> _listBatchLoad;
        private FixupCollection<LOG_LOAD_CARGAMOV> _listLogLoad;
        private BATCH_LOAD_CARGAMOV _batchLoad;
        #endregion

        #region propiedades

        public bool? ValideProcess
        {
            get
            {
                return _valideProcess;
            }
            set
            {
                if (_valideProcess != value)
                {
                    _valideProcess = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ValideProcess"));
                    }
                }
            }
        }

        public FixupCollection<BATCH_LOAD_CARGAMOV> ListBatchLoad
        {
            get
            {
                return _listBatchLoad;
            }
            set
            {
                if (_listBatchLoad != value)
                {
                    _listBatchLoad = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ListBatchLoad"));
                    }
                }
            }
        }

        //filtra por idbach
        public BATCH_LOAD_CARGAMOV BatchLoad
        {
            get
            {
                return _batchLoad;
            }
            set
            {
                if (_batchLoad != value)
                {
                    _batchLoad = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("BatchLoad"));
                    }
                    //Cambia cuando a seleccionado el combo
                    this.ListLogLoad = this.CallServiceGetListLogLoad(this._batchLoad.ID_BATCH);
                }
            }
        }

        public FixupCollection<LOG_LOAD_CARGAMOV> ListLogLoad
        {
            get
            {
                return _listLogLoad;
            }
            set
            {
                if (_listLogLoad != value)
                {
                    _listLogLoad = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ListLogLoad"));
                    }
                }
            }
        }

        public ICommand EjecutarCargaCommand
        {
            get
            {
                if (_ejecutarCargaCommand == null)
                {
                    _ejecutarCargaCommand = new RelayCommand(p => this.AttempCargaItems(), p => this.CanAttempCargaItems());
                }
                return _ejecutarCargaCommand;
            }
        }

        public ICommand AbrirRutaCommand
        {
            get
            {
                if (_abrirRutaCommand == null)
                {
                    _abrirRutaCommand = new RelayCommand(p => this.AttempAbrirRuta(), p => this.CanAttempAbrirRuta());
                }
                return _abrirRutaCommand;
            }
        }

        #endregion

        #region metodos
        public bool CanAttempCargaItems()
        {
            bool canValideProcess = false;

            if (this.ValideProcess == true)
                return true;
            else if (this.ValideProcess == null)
                return false;
            else
                canValideProcess = false;
            

            return canValideProcess;
        }

        public void AttempCargaItems()
        {
            try
            {
                CallServiceGetExecuteLogingItems();
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("No hay conexión con el servidor. \n Verifique que está conectado a la red correcta", "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);  
            }
            
        }

        public bool CanAttempAbrirRuta()
        {
            bool canOpenRuta = false;

            if (this.ValideProcess==true)
                return true;
            return canOpenRuta;
        }

        public void AttempAbrirRuta()
        {
            Process.Start(ConfigurationManager.AppSettings["RutaArchivos"].ToString());   
        }

        #endregion

        #region servicios

        /// <summary>
        ///  ejecuta un servicio web para la carga de items
        /// </summary>
        public void CallServiceGetExecuteLogingItems()
        {
            #region metodos

            try
            {
                var client = new RestClient(routeBach);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameServiceLogingItems;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { });
                IRestResponse response = client.Execute(request);

            }
            catch (Exception)
            {
                //validar
                throw;
            }
            #endregion
        }

        /// <summary>
        ///  ejecuta un servicio web y retorna una lista de pocos de la tabla BATCH_LOAD_CARGAMOV
        /// </summary>
        public void CallServiceGetListBatchProcess()
        {
            #region metodos

            try
            {
                var client = new RestClient(routeBach);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameServiceListBatch;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                FixupCollection<BATCH_LOAD_CARGAMOV> list = new FixupCollection<BATCH_LOAD_CARGAMOV>();

                if (resx != null)
                {
                    list = dataMapper.GetDeserializeBatchLoad(resx["GetListBatchProcessResult"]);
                    //asigmanos a la lista 
                    this.ListBatchLoad = list;
                }


                
            }
            catch (Exception)
            {
                throw;
            }
            #endregion
        }

        /// <summary>
        ///  ejecuta un servicio web y retorna una lista de pocos de la tabla LOG_LOAD_CARGAMOV
        /// </summary>
        public FixupCollection<LOG_LOAD_CARGAMOV> CallServiceGetListLogLoad( int idBatch)
        {
            #region metodos

            try
            {
                var client = new RestClient(routeBach);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameServiceListLogLoad;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { idBatch = idBatch});
                IRestResponse response = client.Execute(request);
                if (response.ErrorException!=null)
                {
                    MessageBoxResult result = MessageBox.Show(response.ErrorMessage, "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;
                }

                Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                FixupCollection<LOG_LOAD_CARGAMOV> list = new FixupCollection<LOG_LOAD_CARGAMOV>();

                if (resx != null)
                {
                    list = dataMapper.GetDeserializeLogLoad(resx["GetListLogLoadResult"]);
                }
                return list;
                

            }
            catch (Exception)
            {
                throw;
            }
            #endregion
        }

        /// <summary>
        ///  ejecuta un servicio web y retorna un true o false si hay un proceso corriendo
        /// </summary>
        public void CallServiceGetValidateNotExitProcessRunning()
        {
            #region metodos

            try
            {
                var client = new RestClient(routeBach);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameServiceValidateProccess;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new {});
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                bool list =false;
                list = dataMapper.GetDeserializeBatchLoadBool(resx["GetValidateNotExitProcessRunningResult"]);
                if (list)
                    this.ValideProcess = true;
                else
                {
                    MessageBoxResult result = MessageBox.Show("Actualmente hay unproceso ejecutandose.", "Informe", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.ValideProcess = false;
                }
                    
            }
            catch (Exception)
            {
                throw;
            }
            #endregion
        }

        #endregion

        #region constructores
        public CargaItemsViewModel()
        {
            this._listBatchLoad = new FixupCollection<BATCH_LOAD_CARGAMOV>();
            //this._batchLoad = new BATCH_LOAD_CARGAMOV();
            try
            {
                CallServiceGetValidateNotExitProcessRunning();
                
                CallServiceGetListBatchProcess();
                
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("No hay conexión con el servidor. \n Verifique que está conectado a la red correcta", "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.ValideProcess = null;
            }
            

        }
        #endregion

        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
