using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model.Seguridad;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using InventoryApp.Model;
using System.Collections.ObjectModel;
using InventoryApp.DAL;
using InventoryApp.ViewModel.CatalogUsuarios;
using InventoryApp.ViewModel.Historial;
using System.Windows.Forms;

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
            foreach (Usuario u in UsuariosCollection) {

                if (u.IsChecked)
                {
                    Rol.User aux = new Rol.User();
                    aux.UnidUser = u.UNID_USUARIO;

                    RolActual.UsuariosCollection.Add(aux);
                }
            }

            MenuAgregar();
            RolActual.saveRol();
            this._catalogSeguridadViewModel.RolesCollection = this._catalogSeguridadViewModel.GetRols();
            
            //valida si se iso una intancia de desde usuario
            if (_addUsuarioViewModel!=null)
                AddRolUser();
            
        }
        private bool CanAttemptGuardarRol()
        {
            foreach (ROL rrr in _ExistingRols)
            {

                if (rrr.ROL_NAME.Equals(this.RolActual.Name))
                {
                    if (cccc)
                    {
                        cccc = false;
                        MessageBox.Show("El nombre del Rol ya existe en el sistema, favor de cambiarlo.");
                    }
                    return false;
                }
            }

            if (!String.IsNullOrEmpty(RolActual.Name) && MenuSelected() && UsuarioSelected())
                return true;
            return false;
        }
        public void MenuAgregar() {

            MenuModel auxMenu = this.MenuViewModel.MenuModel[0];
            ColaMenuAgregar = new Queue<MenuModel>();

            ColaMenuAgregar.Enqueue(auxMenu);

            while (ColaMenuAgregar.Count > 0)
            {
                MenuModel nodo = ColaMenuAgregar.Dequeue();
                
                if (nodo.IsCheck)
                {
                    AgregarPadre(nodo.Parent);
                    Rol.Menu auxMenuRol = new Rol.Menu();
                    auxMenuRol.MenuName = nodo.MenuName;

                    RolActual.MenuCollection.Add(auxMenuRol);
                }
                foreach (MenuModel mmm in nodo.ChildrenMenu)
                    ColaMenuAgregar.Enqueue(mmm);                
            }
        }
        public void AgregarPadre(MenuModel m) {

            if (m == null)
                return;
            else
            {
                AgregarPadre(m.Parent);

                Rol.Menu auxMenuRol = new Rol.Menu();
                auxMenuRol.MenuName = m.MenuName;

                if (!RolActual.MenuCollection.Contains(auxMenuRol))
                    RolActual.MenuCollection.Add(auxMenuRol);
            }
        }
        public bool MenuSelected() {

            MenuModel auxMenu = this.MenuViewModel.MenuModel[0];
            ColaMenu = new Queue<MenuModel>();

            if (auxMenu.IsCheck)
                return true;
            else {
                foreach (MenuModel mm in auxMenu.ChildrenMenu)
                    ColaMenu.Enqueue(mm);
                while (ColaMenu.Count > 0) {

                    MenuModel nodo = ColaMenu.Dequeue();
                    if (nodo.IsCheck)
                        return true;
                    else {

                        foreach (MenuModel mmm in nodo.ChildrenMenu)
                            ColaMenu.Enqueue(mmm);
                    }
                }

                return false;
            }
        }
        public bool UsuarioSelected() {
            
            foreach (Usuario u in UsuariosCollection) {

                if (u.IsChecked)
                    return true;
            }
            return false;
        }

        #endregion

        #region Properties

        private bool cccc;
        private USUARIO ActualUser;
        private ObservableCollection<ROL> _ExistingRols;

        CatalogSeguridadViewModel _catalogSeguridadViewModel;
        Queue<MenuModel> ColaMenu;
        Queue<MenuModel> ColaMenuAgregar;

        public Rol RolActual
        {
            get { return _RolActual; }
            set
            {
                if (_RolActual != value)
                {
                    _RolActual = value;
                    cccc = true;
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

        public ObservableCollection<Usuario> UsuariosCollection
        {
            get { return _UsuariosCollection; }
            set
            {
                if (_UsuariosCollection != value)
                {
                    _UsuariosCollection = value;
                    OnPropertyChanged(UsuariosCollectionPropertyName);
                }
            }
        }
        private ObservableCollection<Usuario> _UsuariosCollection;
        public const string UsuariosCollectionPropertyName = "UsuariosCollection";

        //
        private ModifyUsuarioViewModel _addUsuarioViewModel;

        #endregion

        #region Constructors 

        public AddSeguridadViewModel(CatalogSeguridadViewModel catalogSeguridadViewModel)
        {
            this._catalogSeguridadViewModel = catalogSeguridadViewModel;
            this.UsuariosCollection = GetUsers();
            this._MenuViewModel = new MenuViewModel(this._catalogSeguridadViewModel.IsSuperAdmin);
            this._RolActual = new Rol(catalogSeguridadViewModel.ActualUser);
            this.ActualUser = catalogSeguridadViewModel.ActualUser;
            this._ExistingRols = GetExistingRols();
        }

        //sobrecarga para agregar desde  administracion de usuario un nuevo rol
        public AddSeguridadViewModel(ModifyUsuarioViewModel modifyUsuarioViewModel)
        {
            this._catalogSeguridadViewModel = new CatalogSeguridadViewModel(true, modifyUsuarioViewModel.ActualUser);
            this.UsuariosCollection = GetUsers();
            this._MenuViewModel = new MenuViewModel(this._catalogSeguridadViewModel.IsSuperAdmin);
            this._RolActual = new Rol(this.ActualUser);
            //
            this._addUsuarioViewModel = new ModifyUsuarioViewModel();
            this._addUsuarioViewModel = modifyUsuarioViewModel;
            this.ActualUser = modifyUsuarioViewModel.ActualUser;
            this._ExistingRols = GetExistingRols();
        }

        #endregion

        #region Methods
        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(new AlmacenModel(new AlmacenTecnicoDataMapper(), new USUARIO()));
            return historialViewModel;
        }

        public ObservableCollection<Usuario> GetUsers()
        {
            AppUsuario dm = new AppUsuario();
            ObservableCollection<Usuario> res = new ObservableCollection<Usuario>();

            List<USUARIO> lu = (List<USUARIO>)dm.getElementsRolesFiltrado();

            foreach (USUARIO u in lu) {

                Usuario aux = new Usuario(u);

                res.Add(aux);
            }

            return res;
        }

        public ObservableCollection<ROL> GetExistingRols()
        {

            AppRolDataMapper app = new AppRolDataMapper();
            return (ObservableCollection<ROL>)app.getElements();
        }

        #endregion

        #region metodos agregar usuario

        public void AddRolUser()
        {
            int contador = 0;
            foreach (var rolesActual in this._catalogSeguridadViewModel.RolesCollection)
            {
                foreach (var user in rolesActual.UsuariosCollection)
                {
                    if (user.UnidUser == this._addUsuarioViewModel.ModiUsuarioModel.UnidUsuario)
                    {
                        if (contador==0)
                        {
                            AddRol(user.UnidUser);
                            contador += 1;
                        }
                        break;
                    }
                    
                }

            }
            
        }
        public void AddRol(long unidUser)
        {
            AppRolDataMapper dataMapper = new AppRolDataMapper();

            object element = dataMapper.getElementRoles(unidUser);

            FixupCollection<DeleteRol> ic = new FixupCollection<DeleteRol>();
          
            if (element != null)
            {
                if (((List<ROL>)element).Count > 0)
                {
                    foreach (ROL item in (List<ROL>)element)
                    {
                        if (item.UNID_ROL != 1)
                        {
                          DeleteRol aux = new DeleteRol(item);
                          ic.Add(aux);
                        }

                    }
                }
            }

            if (ic.Count!=0)
            {
                foreach (var item in ic.Reverse())
                {
                    this._addUsuarioViewModel.ModiUsuarioModel.Rol.Add(item);
                    break;
                    
                }
                
            }

        }
        #endregion
    }
}
