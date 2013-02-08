using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using RestSharp;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model.Login
{
    public class LoginModel : INotifyPropertyChanged
    {
        #region propiedades logeo
        string routeLogin = ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
        string basicAuthUser = "Administrator";
        string basicAuthPass = "Passw0rd1!";
        string nameService = "GetLogin";
        AppUsuario dataMapper = new AppUsuario();
        #endregion

        #region Fields
        private string _userName;
        private string _password;
        private bool _login;
        #endregion

        #region Props
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
                    }
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Password"));
                    }
                }
            }
        }

        public bool Login
        {
            get
            {
                return _login;
            }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Login"));
                    }
                }
            }
        }
        #endregion

        // consumir servicio
        public void CallServiceGetLoginUser()
        {
            
            #region metodos
            
            //madamos a llamar el metodo que serializa list de pocos
            string dataUser =  GetJonUser();

            if (!String.IsNullOrEmpty(dataUser))
            {
                try
                {
                    var client = new RestClient(routeLogin);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    this._login = bool.Parse(response.Content);
                }
                catch (Exception)
                {
                    this._login = false;
                }
            }
            else
            {
                this._login  = false;
            }
            
            #endregion
        }

        //serializa a json
        public string GetJonUser() 
        {
            string resJson = null;
            USUARIO usuario = new USUARIO() { USUARIO_MAIL = this._userName, USUARIO_PWD = this._password };
            resJson = dataMapper.GetJsonUsuario(usuario);
            if (resJson == null)
                return null;

            return resJson;

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
