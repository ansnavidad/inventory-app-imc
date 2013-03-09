using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model.Seguridad;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.Model;

namespace InventoryApp.ViewModel.CatalogUsuarios
{
    public class CatalogUsuarioViewModel: IPageViewModel
    {
        #region Fields

        private RelayCommand _deleteUsuarioCommand;
        private CatalogUsuarioModel _catalogUsuarioModel;

        #endregion

        #region Properties

        public ICommand DeleteUsuarioCommand
        {
            get
            {
                if (_deleteUsuarioCommand == null)
                {
                    _deleteUsuarioCommand = new RelayCommand(p => this.AttempDeleteUsuario(), p => this.CanAttempDeleteUsuario());
                }
                return _deleteUsuarioCommand;
            }
        }

        public CatalogUsuarioModel CatalogUsuarioModel
        {
            get
            {
                return _catalogUsuarioModel;
            }
            set
            {
                _catalogUsuarioModel = value;
            }
        }
        
        #endregion
        
        #region Constructors

        public CatalogUsuarioViewModel()
        {
            try
            {
                IDataMapper dataMapper = new AppRolDataMapper();
                this._catalogUsuarioModel = new CatalogUsuarioModel(dataMapper);
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        #endregion

        public void loadUser()
        {
            this._catalogUsuarioModel.loadUsuario();
        }
        
        #region Methods

        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteUsuario()
        {
            bool _canDeleteUsuario = false;
            foreach (DeleteUsuarios d in this._catalogUsuarioModel.Usuarios)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteUsuario = true;
                }
            }

            return _canDeleteUsuario;
        }

        public void AttempDeleteUsuario()
        {
            this._catalogUsuarioModel.deleteUsuarios();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogUsuarioModel != null)
            {
                this._catalogUsuarioModel.loadUsuario();
            }
        }

        #endregion  

        /// <summary>
        /// Crea una nueva instancia de ModifyUsuarioViewModel y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyUsuarioViewModel CreateModifyUsuarioViewModel()
        {
            UsuarioModel usuarioModel = new UsuarioModel (new AppUsuario());
            if (this._catalogUsuarioModel != null && this._catalogUsuarioModel.SelectedUsuario != null)
            {
                usuarioModel.UnidUsuario = this._catalogUsuarioModel.SelectedUsuario.UNID_USUARIO;
                usuarioModel.UserMail = this._catalogUsuarioModel.SelectedUsuario.USUARIO_MAIL;
                usuarioModel.Password = this._catalogUsuarioModel.SelectedUsuario.USUARIO_PWD;
                usuarioModel.Rol = this._catalogUsuarioModel.SelectedUsuario.Roles;
            }
            return new ModifyUsuarioViewModel(this, usuarioModel);
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
