using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace InventoryApp.ViewModel.Job
{
    public class JobViewModel: IPageViewModel
    {
        #region propiedades
        //servidor
        //imc
        string routeService = @"http://192.168.0.116:2020/Services/Receiver.svc";
        string prueba= @"http://localhost:8082/Services/Receiver.svc";
        string basicAuthUser = "Administrator";
        string basicAuthPass = "Passw0rd1!";
        //string basicAuthUser = "ISAAC";
        //string basicAuthPass = "isaac";
        string nameService = "ExecuteJob";
        #endregion

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
    }
}
