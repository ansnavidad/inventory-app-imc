using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace InventoryApp.ViewModel.Version
{
    class VersionViewModel: IPageViewModel
    {
        #region propiedades
        //servidor
        string routeService = @"http://10.50.0.131:8080/Services/Broadcast.svc";
        string basicAuthUser = "Administrator";
        string basicAuthPass = "Passw0rd1!";
        string nameService = "GetVersion";
        #endregion

        public void CallServiceGetVersion()
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
