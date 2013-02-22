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
    public static class VersionViewModel
    {
        public static bool IsRunning = false;

        static string routeService = ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
        static string basicAuthUser = "Administrator";
        static string basicAuthPass = "Passw0rd1!";
        static string nameService = "GetVersion";

        public static bool NewVersion()
        {
            string localV = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()[0].ToString();
            localV += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()[2].ToString();
            localV += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()[4].ToString();
            localV += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()[6].ToString();
            
            int serverVersion = CallServiceGetVersion();
            int localVersion = Int32.Parse(localV);

            if (serverVersion > localVersion)
            {
                return true;               
            }
            else {

                return false;
            }
        }        

        public static int CallServiceGetVersion()
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

                if (!response.Content.Equals(""))
                {
                    Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                    List<UPDATE> list;
                    list = dataMapper.GetDeserializeUpDate(resx["GetVersionResult"]);

                    ActualVersion = 0;

                    if (list != null)
                        foreach (UPDATE item in list)
                            ActualVersion = item.VERSION;

                    return ActualVersion;
                }
                else {

                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            #endregion
        }
    }
}
