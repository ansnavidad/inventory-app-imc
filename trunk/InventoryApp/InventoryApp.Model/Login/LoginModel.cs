using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using RestSharp;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Text.RegularExpressions;

namespace InventoryApp.Model.Login
{
    public class LoginModel : ModelBase
    {
        #region Propiedades logeo
        string routeLogin = ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
        string basicAuthUser = "Administrator";
        string basicAuthPass = "Passw0rd1!";
        string nameService = "GetLogin";
        AppUsuario dataMapper = new AppUsuario();        
        #endregion

        #region Properties

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
                    OnPropertyChanged("Login");
                }
            }
        }
        private bool _login;

        public USUARIO Usuario
        {
            get
            {
                return _Usuario;
            }
            set
            {
                if (EmailValidador())
                {
                    if (_Usuario != value)
                    {
                        _Usuario = value;
                        OnPropertyChanged(UsuarioPropertyName);
                    }
                }
            }
        }
        private USUARIO _Usuario;
        public const string UsuarioPropertyName = "Usuario";
        
        public string UserRegristro
        {
            get
            {
                return _UserRegristro;
            }
            set
            {
                if (_UserRegristro != value)
                {
                    _UserRegristro = value;
                    OnPropertyChanged(UserRegristroPropertyName);
                }
            }
        }
        private string _UserRegristro;
        public const string UserRegristroPropertyName = "UserRegristro";

        public string UserRecuperar
        {
            get
            {
                return _UserRecuperar;
            }
            set
            {
                if (_UserRecuperar != value)
                {
                    _UserRecuperar = value;
                    OnPropertyChanged(UserRecuperarPropertyName);
                }
            }
        }
        private string _UserRecuperar;
        public const string UserRecuperarPropertyName = "UserRecuperar";

        public string UserRegistroPass1
        {
            get
            {
                return _UserRegistroPass1;
            }
            set
            {
                if (_UserRegistroPass1 != value)
                {
                    _UserRegistroPass1 = value;
                    OnPropertyChanged(UserRegistroPass1PropertyName);
                }
            }
        }
        private string _UserRegistroPass1;
        public const string UserRegistroPass1PropertyName = "UserRegistroPass1";

        public string UserRegistroPass2
        {
            get
            {
                return _UserRegistroPass2;
            }
            set
            {
                if (_UserRegistroPass2 != value)
                {
                    _UserRegistroPass2 = value;
                    OnPropertyChanged(UserRegistroPass2PropertyName);
                }
            }
        }
        private string _UserRegistroPass2;
        public const string UserRegistroPass2PropertyName = "UserRegistroPass2";

        public string MensajeError
        {
            get
            {
                return _mensajeError;
            }
            set
            {
                if (_mensajeError != value)
                {
                    _mensajeError = value;
                    OnPropertyChanged(MensajeErrorPropertyName);
                }
            }
        }
        private string _mensajeError;
        public const string MensajeErrorPropertyName = "MensajeError";
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
            resJson = dataMapper.GetJsonUsuario(_Usuario);
            if (resJson == null)
                return null;

            return resJson;

        }

        public LoginModel() {

            this._Usuario = new USUARIO();
            this.Login = false;
            this.UserRegristro = "";
            this.UserRecuperar = "";
            this.UserRegistroPass1 = "";
            this.UserRegistroPass2 = "";
            this.MensajeError = "";
        }

        public bool EmailValidador()
        {
            bool validar = true;
            if (!Regex.IsMatch(this._Usuario.USUARIO_MAIL, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                validar = false;
            
            return validar;
        }
    }
}
