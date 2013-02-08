using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model.Login;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Login
{
    public class LoginViewModel
    {
        #region Fields
        private RelayCommand _validarLoginCommand;
        private LoginModel _loginModel;
        #endregion

        //Exponer las propiedades
        #region Props

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

        public LoginModel LoginModel
        {
            get
            {
                return _loginModel;
            }
            set
            {
                _loginModel = value;
            }
        }
        #endregion

        #region metodos
        public bool CanAttempValidar()
        {
            bool _canDeleteUnidad = true;
            
            

            return _canDeleteUnidad;
        }

        public void AttempValidar()
        {
            
        }
        #endregion
    }
}
