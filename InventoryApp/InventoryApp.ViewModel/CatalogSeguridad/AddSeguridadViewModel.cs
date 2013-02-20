using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model.Seguridad;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using InventoryApp.Model;

namespace InventoryApp.ViewModel.CatalogSeguridad
{
    public class AddSeguridadViewModel : ViewModelBase
    {
        #region Relay Commands

        public ICommand GuardarRol
        {
            get
            {
                if (_GuardarRol == null)
                {
                    _GuardarRol = new RelayCommand(p => this.AttemptGuardarRol(), p => this.CanAttemptGuardarRol());
                }
                return _GuardarRol;
            }
        }
        private RelayCommand _GuardarRol;
        private void AttemptGuardarRol()
        {
        }
        private bool CanAttemptGuardarRol()
        {
            if (RolActual.MenuCollection.Count > 0 && RolActual.UsuariosCollection.Count > 0 && !String.IsNullOrEmpty(RolActual.Name))
                return true;
            return false;
        }

        public ICommand DeleteUsuario
        {
            get
            {
                if (_DeleteUsuario == null)
                {
                    _DeleteUsuario = new RelayCommand(p => this.AttemptDeleteUsuario(), p => this.CanAttemptDeleteUsuario());
                }
                return _DeleteUsuario;
            }
        }
        private RelayCommand _DeleteUsuario;
        private void AttemptDeleteUsuario()
        {
        }
        private bool CanAttemptDeleteUsuario()
        {
            if (this.RolActual.UsuariosCollection.Count > 0)
                return true;
            return false;
        }

        #endregion

        #region Properties

        CatalogSeguridadViewModel _catalogSeguridadViewModel;
        Queue<Rol.Menu> ColaMenu;

        public Rol RolActual
        {
            get { return _RolActual; }
            set
            {
                if (_RolActual != value)
                {
                    _RolActual = value;
                    OnPropertyChanged(RolActualPropertyName);
                }
            }
        }
        protected Rol _RolActual;
        public const string RolActualPropertyName = "RolActual";

        public MenuViewModel MenuViewModel
        {
            get { return _MenuViewModel; }
            set
            {
                if (_MenuViewModel != value)
                {
                    _MenuViewModel = value;
                    OnPropertyChanged(MenuViewModelPropertyName);
                }
            }
        }
        private MenuViewModel _MenuViewModel;
        public const string MenuViewModelPropertyName = "MenuViewModel";        

        #endregion

        #region Constructors 

        public AddSeguridadViewModel(CatalogSeguridadViewModel catalogSeguridadViewModel)
        {
            this._catalogSeguridadViewModel = catalogSeguridadViewModel;
            this._MenuViewModel = new MenuViewModel();
            this._RolActual = new Rol();
        }

        #endregion

        #region Methods
        #endregion        
    }
}
