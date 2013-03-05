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
        public static bool IsRunning = false;
        string _message;
        bool _jobDone;
        System.Timers.Timer t;
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
        private string _cargandoGrid;
        #endregion

        #region propiedades

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Message"));
                    }
                }
            }
        }

        public bool JobDone
        {
            get
            {
                return _jobDone;
            }
            set
            {
                if (_jobDone != value)
                {
                    _jobDone = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("JobDone"));
                    }
                }
            }
        }

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

        public string CargandoGrid
        {
            get
            {
                return _cargandoGrid;
            }
            set
            {
                if (_cargandoGrid != value)
                {
                    _cargandoGrid = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("CargandoGrid"));
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
                   
                    //this.ListLogLoad = this.CallServiceGetListLogLoad(this._batchLoad.ID_BATCH);
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

        public void DownloadData(Object sender, System.Timers.ElapsedEventArgs args)
        {
            this.t.Enabled = false;
            ((System.Timers.Timer)sender).Stop();

            this.ListLogLoad = CallServiceGetListLogLoad(this.BatchLoad.ID_BATCH);    
            CargaItemsViewModel.IsRunning = false;
            this.JobDone = true;
            
        }

        public void start()
        {
            this.JobDone = false;
            CargaItemsViewModel.IsRunning = true;
            t.Enabled = true;
            t.Start();
        }

        // combobox
        public void SetCargaItemsViewModel()
        {
            
            this.Message = "Cargando datos...";
            this._jobDone = false;
            t = new System.Timers.Timer(100);
            t.Enabled = false;

            t.Elapsed += new System.Timers.ElapsedEventHandler(DownloadData);

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
                request.Timeout = 60000;
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
                request.Timeout = 60000;
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
        public FixupCollection<LOG_LOAD_CARGAMOV> CallServiceGetListLogLoad(int idBatch)
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
                request.Timeout = 60000;
                IRestResponse response = client.Execute(request);
                this.CargandoGrid = "";
                if (response.ErrorException!=null)
                {

                    MessageBoxResult result = MessageBox.Show("No hay conexión con el servidor.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                request.Timeout = 60000;
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
