using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model.Login;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

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
            bool _validar = false;
          if (String.IsNullOrEmpty(LoginModel.Usuario.USUARIO_MAIL) || String.IsNullOrEmpty(LoginModel.Usuario.USUARIO_PWD))
              _validar = false;

          if (!_validar && !String.IsNullOrEmpty(LoginModel.Usuario.USUARIO_MAIL))
              this.LoginModel.MensajeError = "Email no valido";
          else
              this.LoginModel.MensajeError = "";
          return _validar;
        }
        public void AttempValidar()
        {

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

        #region metodos
        
        #endregion

        #region Constructors

        public LoginViewModel() {

            _loginModel = new LoginModel();
        }

        #endregion


    }    
}
