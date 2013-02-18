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
using System.Collections.ObjectModel;

namespace InventoryApp.Model.Login
{
    public class LoginModel : ModelBase
    {
        #region Propiedades logeo
        string routeLogin = ConfigurationManager.AppSettings["RutaServicioDescarga"].ToString();
        string basicAuthUser = "Administrator";
        string basicAuthPass = "Passw0rd1!";
        string nameService = "GetLogin";
        string nameServiceRecuver = "GetRecoverPassword";
        string nameServiceNewUser = "GetRegisterUser";
        string nameServiceListUser = "GetValidateNotExistUser";
        long recuperar;
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

        public bool? LoginPass
        {
            get
            {
                return _loginPass;
            }
            set
            {
                if (_loginPass != value)
                {
                    _loginPass = value;
                    OnPropertyChanged("LoginPass");
                }
            }
        }
        private bool? _loginPass;
        
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

        public List<USUARIO> UsuariosCollection
        {
            get
            {
                return _UsuariosCollection;
            }
            set
            {
                if (_UsuariosCollection != value)
                {
                    _UsuariosCollection = value;
                    OnPropertyChanged(UsuariosCollectionPropertyName);
                }
            }
        }
        private List<USUARIO> _UsuariosCollection;
        public const string UsuariosCollectionPropertyName = "UsuariosCollection";

        #endregion

        #region Service
        // consumir servicio login
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
                    
                    Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                    bool list;
                    list = dataMapper.GetDeserializeUsuarioBool(resx["GetLoginResult"]);
                    if (list != false)
                        this.Login = list;
                    else
                        this.Login = list;

                }
                catch (Exception)
                {
                    this.Login = false;
                }
            }
            else
            {
                this.Login = false;
            }
            
            #endregion
        }

        // consumir servicio recupera contraseña
        public bool CallServiceGetRecoverPass()
        {
            #region metodos
            bool resService = false;
            if (recuperar !=0)
            {
                try
                {
                    var client = new RestClient(routeLogin);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameServiceRecuver;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { dataUser = recuperar });
                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                    bool list;
                    list = dataMapper.GetDeserializeUsuarioBool(resx["GetRecoverPasswordResult"]);
                    if (list != false)
                        resService = list;
                    else
                        resService = list;
                    
                }
                catch (Exception)
                {
                    resService = false;
                }
            }
            else
            {
                resService = false;
            }
            return resService;
            
            #endregion
        }

        // consumir servicio Nuevo usuario
        public string CallServiceGetNewUser()
        {
            string resServer = null;
            #region metodos

            //madamos a llamar el metodo que serializa list de pocos
            string dataUser = GetJonNewUser();

            if (!String.IsNullOrEmpty(dataUser))
            {
                try
                {
                    var client = new RestClient(routeLogin);
                    client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameServiceNewUser;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { dataUser = dataUser });
                    IRestResponse response = client.Execute(request);

                    Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                    object list;
                    list = dataMapper.GetDeserializePocoUsuario(resx["GetRegisterUserResult"]);
                    if (list != null)
                    {
                        //guarda el nuevo registro
                        dataMapper.insertElementSync(list);
                        resServer = "Se ha enviado un correo de confirmación a su dirección de correo electrónico. \nConfirme su registro e intente iniciar sesión en la aplicación";
                    }    
                    else
                        resServer = "Ha ocurrido un error en el servidor, favor de intentar de nuevo";

                }
                catch (Exception)
                {
                    resServer = "Actualmente no hay conexión al servidor, favor de verificarla";
                }
            }
            else
            {
                resServer = "Ha ocurrido un error en el servidor, favor de intentar de nuevo";
            }
            return resServer;
            #endregion
        }

        // consumir servicio para obtener la lista de usurios del servidor
        public List<USUARIO> CallServiceGetListUser()
        {
            List<USUARIO> res = new List<USUARIO>();
            #region metodos
            try
            {
                var client = new RestClient(routeLogin);
                client.Authenticator = new HttpBasicAuthenticator(basicAuthUser, basicAuthPass);
                var request = new RestRequest(Method.POST);
                request.Resource = nameServiceListUser;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                List<USUARIO> list;
                list = dataMapper.GetDeserializeUsuario(resx["GetValidateNotExistUserResult"]);
                if (list.Count != 0)
                    res = list;
                else
                   res = null;

            }
            catch (Exception)
            {
                res = null;
            }
            return res;
            #endregion
        }

        public bool GetLoginUser()
        {
            bool res = false;

            res = dataMapper.GetElementLoginLocal(this._Usuario);
            this.Login = res;
            return res;

        }

        //serializa a json login
        public string GetJonUser() 
        {
            string resJson = null;            
            resJson = dataMapper.GetJsonUsuario(_Usuario);
            if (resJson == null)
                return null;

            return resJson;

        }

        //serializa a json nuevo usuario
        public string GetJonNewUser()
        {
            string resJson = null;
            USUARIO u = new USUARIO();
            u.USUARIO_MAIL = this.UserRegristro;
            u.USUARIO_PWD = this.UserRegistroPass1;
            resJson = dataMapper.GetJsonUsuario(u);
            if (resJson == null)
                return null;

            return resJson;

        }
        #endregion
        
        #region Constructors
        public LoginModel() {

            this._Usuario = new USUARIO();
            this.Login = false;
            this.LoginPass = false;
            this.UserRegristro = "";
            this.UserRecuperar = "";
            this.UserRegistroPass1 = "";
            this.UserRegistroPass2 = "";
            this.MensajeError = "";
            this.recuperar = 0;
            
            this._UsuariosCollection = CallServiceGetListUser();
            if (this._UsuariosCollection == null)
                this._UsuariosCollection = GetUsuarios();
        }
        #endregion

        #region Methods
        public void ValidaRecuperarEmail()
        {
            USUARIO u = new USUARIO();
            u.USUARIO_MAIL = this._UserRecuperar;
            recuperar = dataMapper.GetValidarCorreoLocal(u);
            if (recuperar != 0)
            {
                if(CallServiceGetRecoverPass())
                    this.LoginPass = true;
                else
                    this.LoginPass = false;
            }
            else
                this.LoginPass = null;
           
        }

        public bool EmailValidador()
        {
            bool validar = true;
            if (!Regex.IsMatch(this._Usuario.USUARIO_MAIL, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                validar = false;
            
            return validar;
        }

        public bool EmailValidadorRegistro()
        {
            bool validar = true;
            if (!Regex.IsMatch(this.UserRegristro, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                validar = false;

            return validar;
        }

        public List<USUARIO> GetUsuarios()
        {
            List<USUARIO> aux = new List<USUARIO>();
            aux = (List<USUARIO>)dataMapper.getElements();

            return aux;
        }

        #endregion
    }
}
