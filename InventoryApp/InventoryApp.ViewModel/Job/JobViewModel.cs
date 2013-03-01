using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using System.Windows.Input;
using System.Configuration;

namespace InventoryApp.ViewModel.Job
{
    public class JobViewModel : IPageViewModel, INotifyPropertyChanged
    {
        #region propiedades

        //string routeService = @"http://10.50.0.131:8080/Services/Receiver.svc";
        //string routeBach = @"http://10.50.0.131:8080/Services/Broadcast.svc";
        string routeService = ConfigurationManager.AppSettings["RutaServicioSubida"].ToString();
        string routeBach = ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
        string basicAuthUser = "Administrator";
        string basicAuthPass = "Passw0rd1!";
        string nameService = "ExecuteJob";
        string nameService2 = "GetProcessBach";
        private RelayCommand _actualizarCommand;
        private RelayCommand _jobCommand;
        FixupCollection<PROCESS_BATCH> _proccesBatch;
        #endregion

        public ICommand JobCommand
        {
            get
            {
                if (_jobCommand == null)
                {
                    _jobCommand = new RelayCommand(p => this.AttempJob(), p => this.CanAttempJob());
                }
                return _jobCommand;
            }
        }

        public ICommand ActualizarCommand
        {
            get
            {
                if (_actualizarCommand == null)
                {
                    _actualizarCommand = new RelayCommand(p => this.AttempActualizar(), p => this.CanAttempActualizar());
                }
                return _actualizarCommand;
            }
        }

        public bool CanAttempJob()
        {
            bool aux = true;

            if (ProccesBatch == null)
                return false;

            if (ProccesBatch.Count == 0)
                return false; 

            foreach (PROCESS_BATCH p in ProccesBatch) {

                if (p.IS_DONE == false)
                    aux = false;
            }

            return aux;
        }

        public void AttempJob()
        {
            
        }

        public bool CanAttempActualizar()
        {
            return true;
        }

        public void AttempActualizar()
        {
            ProccesBatch = new FixupCollection<PROCESS_BATCH>();
            List<PROCESS_BATCH> l = new List<PROCESS_BATCH>();
            l = CallServiceGetProcessMatch();
            if (l != null)
            {
                foreach (PROCESS_BATCH p in l)
                {
                    ProccesBatch.Add(p);
                }
            }
        }

        public FixupCollection<PROCESS_BATCH> ProccesBatch
        {
            get
            {
                return _proccesBatch;
            }
            set
            {
                if (_proccesBatch != value)
                {
                    _proccesBatch = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ProccesBatch"));
                    }
                }
            }
        }

        public JobViewModel() {

            ProccesBatch = new FixupCollection<PROCESS_BATCH>();
            List<PROCESS_BATCH> l = new List<PROCESS_BATCH>();
            l = CallServiceGetProcessMatch();
            if (l != null)
            {
                foreach (PROCESS_BATCH p in l)
                {

                    ProccesBatch.Add(p);
                }
            }
        }

        public void CallServiceGetExecuteJob()
        {       
            #region metodos

            try
            {
                var client = new RestClient(routeService);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { });
                IRestResponse response = client.Execute(request);

            }
            catch (Exception)
            {
                throw;
            }
            #endregion
        }

        public List<PROCESS_BATCH> CallServiceGetProcessMatch()
        {
            #region metodos

            try
            {
                var client = new RestClient(routeBach);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameService2;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { });
                IRestResponse response = client.Execute(request);
                ProcessBachDataMapper dataMapper = new ProcessBachDataMapper();

                Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);
                
                List<PROCESS_BATCH> list = new List<PROCESS_BATCH>();

                if (resx != null)
                {
                    list = dataMapper.GetDeserializeProcessBach(resx["GetProcessBachResult"]);
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
            #endregion
        }

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
