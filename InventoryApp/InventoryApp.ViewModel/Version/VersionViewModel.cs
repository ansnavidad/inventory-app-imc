using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model;
using InventoryApp.DAL.Recibo;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Windows;
using System.Diagnostics;

namespace InventoryApp.ViewModel.Version
{
    public class VersionViewModel : ViewModelBase
    {
        #region propiedades

        public static bool IsRunning = false;
        bool _jobDone;
        string _message;
        System.Timers.Timer t;

        //servidor
        string routeService = @"http://10.50.0.131:8080/Services/Broadcast.svc";
        string basicAuthUser = "Administrator";
        string basicAuthPass = "Passw0rd1!";
        string nameService = "GetVersion";

        public bool JobDone
        {
            get { return _jobDone; }
            set
            {
                if (value != _jobDone)
                {
                    this._jobDone = value;
                    OnPropertyChanged("JobDone");
                }
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                if (value != _message)
                {
                    this._message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        #endregion

        public VersionViewModel()
        {
            //basicAuthUser = "Administrator";
            //basicAuthPass = "Passw0rd1!";

            this.Message = "CHECANDO VERSIÓN";
            this._jobDone = false;
            t = new System.Timers.Timer(100);
            t.Enabled = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(NewVersion);
        }

        public void NewVersion(Object sender, System.Timers.ElapsedEventArgs args)
        {
            this.t.Enabled = false;
            ((System.Timers.Timer)sender).Stop();

            string localV = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()[0].ToString();
            localV += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()[2].ToString();
            localV += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()[4].ToString();
            localV += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()[6].ToString();
            
            int serverVersion = CallServiceGetVersion();
            int localVersion = Int32.Parse(localV);

            if (serverVersion > localVersion)
            {
                this.Message = "Instalando nueva versión";
                this.JobDone = true;
                VersionViewModel.IsRunning = false;

                if (System.Windows.MessageBox.Show("Se detectó una nueva actualización de la aplicación, desea actualizar ahora?", "Actualización automática", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    this.actualiceAPP();
                }
            }
            else {

                this.Message = "No hay nuevas versiones.";
                this.JobDone = true;
                VersionViewModel.IsRunning = false;
            }
        }

        private void actualiceAPP()
        {            
            try
            {
                string instalacion = System.Configuration.ConfigurationManager.AppSettings["CarpetaInstalacion"];
                string proceso = System.Configuration.ConfigurationManager.AppSettings["NombreProcesoActualizacion"];

                if (!instalacion.EndsWith("\\"))
                    instalacion += "\\";

                System.Diagnostics.Process.Start(instalacion + proceso);
                //this.Close();
                Application.Current.Shutdown();
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex2)
            {                
                System.Windows.MessageBox.Show("Error tratando de iniciar el proceso automático de actualización de la aplicación - " + ex2.Message);
            }
        }

        public int CallServiceGetVersion()
        {
            #region Variables
            
            UpDateVersionDataMapper dataMapper = new UpDateVersionDataMapper();
            int ActualVersion;

            #endregion
            
            #region Métodos

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
                
                Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                List<UPDATE> list;
                list = dataMapper.GetDeserializeUpDate(resx["GetVersionResult"]);
                
                ActualVersion = 0;
                
                if (list != null)
                    foreach (UPDATE item in list)
                        ActualVersion = item.VERSION;

                return ActualVersion;
            }
            catch (Exception)
            {
                throw;
            }
            #endregion
        }

        public void start()
        {
            this.JobDone = false;
            this.OnPropertyChanged("JobDone");
            VersionViewModel.IsRunning = true;
            t.Start();
        }
    }
}
