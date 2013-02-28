using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using InventoryApp.Model;
using InventoryApp.Model.Seguridad;
using System.Collections.ObjectModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogUsuarios
{
    public class ModifyUsuarioViewModel : ViewModelBase
    {
        #region Fields
        private UsuarioModel _modiUsuarioModel;
        private RelayCommand _modifyUsuarioCommand;
        private RelayCommand _deleteRolCommand;
        private CatalogUsuarioViewModel _catalogUsuarioViewModel;
        bool _verificarCambioPass = false;
        #endregion

        #region Props

        public UsuarioModel ModiUsuarioModel
        {
            get
            {
                return _modiUsuarioModel;
            }
            set
            {
                _modiUsuarioModel = value;
            }
        }

        public ICommand ModifyUsuarioCommand
        {
            get
            {
                if (_modifyUsuarioCommand == null)
                {
                    _modifyUsuarioCommand = new RelayCommand(p => this.AttempModifyUsuario(), p => this.CanAttempModifyUsuario());
                }
                return _modifyUsuarioCommand;
            }
        }

        public ICommand DeleteRolCommand
        {
            get
            {
                if (_deleteRolCommand == null)
                {
                    _deleteRolCommand = new RelayCommand(p => this.AttemptDeleteRolCommand(), p => this.CanAttemptDeleteRolCommand());
                }
                return _deleteRolCommand;
            }
        }

        public FixupCollection<DeleteRol> DeleteRol
        {
            get { return _DeleteRol; }
            set
            {
                if (_DeleteRol != value)
                {
                    _DeleteRol = value;
                    OnPropertyChanged(DeleteRolPropertyName);
                }
            }
        }
        private FixupCollection<DeleteRol> _DeleteRol;
        public const string DeleteRolPropertyName = "DeleteRol";
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyUsuarioViewModel(CatalogUsuarioViewModel catalogUsuarioViewModel, UsuarioModel selectedUsuarioModel)
        {
            this._modiUsuarioModel = new UsuarioModel(new AppUsuario());
            this._catalogUsuarioViewModel = catalogUsuarioViewModel;
            this._modiUsuarioModel.UnidUsuario = selectedUsuarioModel.UnidUsuario;
            this._modiUsuarioModel.UserMail = selectedUsuarioModel.UserMail;
            this._modiUsuarioModel.Password = selectedUsuarioModel.Password;
            this._modiUsuarioModel.Rol = selectedUsuarioModel.Rol;
            this._DeleteRol = new FixupCollection<DeleteRol>();

        }
        public ModifyUsuarioViewModel() { }
        #endregion

        #region metodos
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyUsuario()
        {
            bool _canAddUsuario = false;

            //valida si se ha echo un cambio en la contraseña
            if (!String.IsNullOrEmpty(this._modiUsuarioModel.NewPassword) || !String.IsNullOrEmpty(this._modiUsuarioModel.ConfirmeNewpassword))
            {
                this._verificarCambioPass = true;
                //valida que las contraseñas sean igual
                if (!this._modiUsuarioModel.NewPassword.Equals(this._modiUsuarioModel.ConfirmeNewpassword))
                {
                    this._modiUsuarioModel.MensajeError = "Las contraseñas no coinciden";
                    
                }
                else if (this._modiUsuarioModel.NewPassword.Length < 8)
                {
                    this._modiUsuarioModel.MensajeError="La longitud mínima de la contraseña es de 8 caracteres";
                    return _canAddUsuario = false;
                }
                else
                {
                    this._modiUsuarioModel.MensajeError = "";
                    return _canAddUsuario = true;
                }

            }
            else
            {
                this._modiUsuarioModel.MensajeError = "";
                this._verificarCambioPass = false;
                return _canAddUsuario = true;
            }

            return _canAddUsuario;
        }

        public void AttempModifyUsuario()
        {
            //valida si hay un elemento a eliminar
            if (this.DeleteRol.Count != 0)
            {
                foreach (var item in this.DeleteRol)
                {
                    if (item.UNID_ROL != 0)
                    {
                        //eliminacion logiga
                        this._modiUsuarioModel.DeleteRelationUsuarioRol(item.UNID_ROL, this._modiUsuarioModel.UnidUsuario);
                    }
                }
            }
            
            //actualiza si hubo cambios
            //valida si el passwor a sido cambiado
            if (this._verificarCambioPass)
                this._modiUsuarioModel.updateUserNewCotraseña();
            else
                this._modiUsuarioModel.updateUserSameCotraseña();


            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogUsuarioViewModel != null)
            {
                this._catalogUsuarioViewModel.loadUser();
            }
        }

        public void AttemptDeleteRolCommand()
        {
            try
            {
                //agrega a un aux los elementos que van a ser eliminados
                var query = (from o in this.ModiUsuarioModel.Rol
                             where o.IsCheckedEliminar == true
                             select o).ToList();
                //valida que no venga vacia la el aux
                if (query.Count != 0)
                {
                    foreach (var item in query)
                    {
                        this.DeleteRol.Add(item);
                    }
                }

                (from o in this.ModiUsuarioModel.Rol
                 where o.IsCheckedEliminar == true
                 select o).ToList().ForEach(o => this.ModiUsuarioModel.Rol.Remove(o));
            }
            catch (Exception)
            {
                ;
            }
        }

        public bool CanAttemptDeleteRolCommand()
        {
            bool canDeleteRol = false;

            if (this.ModiUsuarioModel.Rol != null && this.ModiUsuarioModel.Rol.Count > 0)
            {
                int res = 0;
                try
                {
                    res = (from o in this.ModiUsuarioModel.Rol
                           where o.IsCheckedEliminar == true
                           select o).ToList().Count;
                }
                catch (Exception)
                {
                    res = 0;
                }

                if (res > 0)
                {
                    canDeleteRol = true;
                }
            }

            return canDeleteRol;
        }
        #endregion
    }
}
