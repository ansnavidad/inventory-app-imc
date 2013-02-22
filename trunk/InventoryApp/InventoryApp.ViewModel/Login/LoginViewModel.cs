using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model.Login;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using InventoryApp.DAL.POCOS;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Timers;
using System.Windows.Media.Animation;

namespace InventoryApp.ViewModel.Login
{
    public class LoginViewModel : ViewModelBase
    {
        
       #region Relay Commands

       private RelayCommand _validarLoginCommand;
        public ICommand ValidarLoginCommand
        {
            get
            {
                if (_validarLoginCommand == null)
                {
                    _validarLoginCommand = new RelayCommand(p => this.AttempValidar(), p => this.CanAttempValidar());
                }
                return _validarLoginCommand;
            }
        }
        public bool CanAttempValidar()
        {
            if (String.IsNullOrEmpty(LoginModel.Usuario.USUARIO_MAIL) || String.IsNullOrEmpty(LoginModel.Usuario.USUARIO_PWD))
                return false;

            if (!this.LoginModel.EmailValidador())
            {
                this.LoginModel.MensajeError = "Email no valido";
                return false;
            }
            else
            {
                this.LoginModel.MensajeError = "";
                return true;
            }
        }
        public void AttempValidar()
        {
            this.LoginModel.CallServiceGetLoginUser();
            if (!this.LoginModel.Login)
                if (!this.LoginModel.GetLoginUser())
                {
                    MessageBoxResult result = MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
        }

        private RelayCommand _enviarCorreoCommand;
        public ICommand EnviarCorreoCommand
        {
            get
            {
                if (_enviarCorreoCommand == null)
                {
                    _enviarCorreoCommand = new RelayCommand(p => this.AttempEnviarCorreo(), p => this.CanAttempEnviarCorreo());
                }
                return _enviarCorreoCommand;
            }
        }
        public bool CanAttempEnviarCorreo()
        {
            if (String.IsNullOrEmpty(LoginModel.UserRecuperar))
                return false;
            return true;
        }
        public void AttempEnviarCorreo()
        {
            this.LoginModel.ValidaRecuperarEmail();
            if (LoginModel.LoginPass == null)
            {
                MessageBoxResult result = MessageBox.Show("El usuario que ingresó no existe en la base de datos del sistema", "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            else if (LoginModel.LoginPass == true)
            {
                MessageBoxResult result = MessageBox.Show("Se le enviará su contraseña a su correo electrónico", "Informe", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Favor de conectarse a la red del servidor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                

        }

        private RelayCommand _registrarCommand;
        public ICommand RegistrarCommand
        {
            get
            {
                if (_registrarCommand == null)
                {
                    _registrarCommand = new RelayCommand(p => this.AttempRegistrar(), p => this.CanAttempRegistrar());
                }
                return _registrarCommand;
            }
        }
        public bool CanAttempRegistrar()
        {
            
            if (String.IsNullOrEmpty(this.LoginModel.UserRegristro) || String.IsNullOrEmpty(this.LoginModel.UserRegistroPass1) || String.IsNullOrEmpty(this.LoginModel.UserRegistroPass2))
                return false;
            return true;
        }
        public void AttempRegistrar()
        {
            if (!this.LoginModel.EmailValidadorRegistro()) 
            {    
                MessageBoxResult result = MessageBox.Show("Ingrese como usuario un email válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool aux = true;

            foreach (USUARIO u in this.LoginModel.UsuariosCollection) {

                if (this.LoginModel.UserRegristro.Equals(u.USUARIO_MAIL)) 
                {
                    MessageBoxResult result = MessageBox.Show("Ya existe el usuario que acaba de ingresar en el sistema", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            if (!this.LoginModel.UserRegistroPass1.Equals(this.LoginModel.UserRegistroPass2))
            {
                MessageBoxResult result = MessageBox.Show("Las contraseñas no coinciden", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (this.LoginModel.UserRegistroPass1.Length < 8)
            {
                MessageBoxResult result = MessageBox.Show("La longitud mínima de la contraseña es de 8 caracteres", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show(this.LoginModel.CallServiceGetNewUser());
            this.LoginModel.UserRegristro = "";
            this.LoginModel.UserRegistroPass1 = "";
            this.LoginModel.UserRegistroPass2 = "";
        }        

        #endregion
        
        #region Properties

        public LoginModel LoginModel
        {
            get
            {
                return _loginModel;
            }
            set
            {
                if (_loginModel != value)
                {
                    _loginModel = value;
                    OnPropertyChanged("LoginModel");
                }
            }
        }
        private LoginModel _loginModel;


        #endregion

        #region Constructors

        public LoginViewModel() {

            _loginModel = new LoginModel();
                       
        }

        #endregion

        #region login imagen
        public static bool IsRunning = false;
        System.Timers.Timer t;

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
        private string _message;

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
        private bool _jobDone;

        public void start()
        {
            this.JobDone = false;
            this.OnPropertyChanged("JobDone");
            LoginViewModel.IsRunning = true;
            t.Start();
        }

        public void LoadLoginData(Object sender, ElapsedEventArgs args)
        {
            this.t.Enabled = false;
            ((System.Timers.Timer)sender).Stop();
            bool res = true;

            this.Message = "Espere un momento...";
        }

        #endregion
        #region metodos de imagen
        Storyboard _ImgSync;

       
        #endregion
    }    
}
