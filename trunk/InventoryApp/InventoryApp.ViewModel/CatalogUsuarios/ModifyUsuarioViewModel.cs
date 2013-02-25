//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Input;
//using InventoryApp.Model;
//using InventoryApp.Model.Seguridad;
//using System.Collections.ObjectModel;
//using InventoryApp.DAL;
//using InventoryApp.DAL.POCOS;

//namespace InventoryApp.ViewModel.CatalogUsuarios
//{
//    public class ModifyUsuarioViewModel : ViewModelBase
//    {

//        #region Relay Commands

//        public ICommand GuardarRol
//        {
//            get
//            {
//                if (_GuardarRol == null)
//                {
//                    _GuardarRol = new RelayCommand(p => this.AttemptGuardarRol(), p => this.CanAttemptGuardarRol());
//                }
//                return _GuardarRol;
//            }
//        }
//        private RelayCommand _GuardarRol;
//        private void AttemptGuardarRol()
//        {
//            RolActual.UsuariosCollection = new ObservableCollection<Rol.User>();

//            foreach (Usuario u in UsuariosCollection) {

//                if (u.IsChecked)
//                {
//                    Rol.User aux = new Rol.User();
//                    aux.UnidUser = u.UNID_USUARIO;

//                    RolActual.UsuariosCollection.Add(aux);
//                }
//            }

//            MenuAgregar();
//            RolActual.modifyRol();
//        }
//        private bool CanAttemptGuardarRol()
//        {
//            if (!String.IsNullOrEmpty(RolActual.Name) && MenuSelected() && UsuarioSelected())
//                return true;
//            return false;            
//        }
//        public void MenuAgregar() {

//            MenuModel auxMenu = this.MenuViewModel.MenuModel[0];
//            ColaMenuAgregar = new Queue<MenuModel>();
//            RolActual.MenuCollection = new ObservableCollection<Rol.Menu>();

//            ColaMenuAgregar.Enqueue(auxMenu);

//            while (ColaMenuAgregar.Count > 0)
//            {
//                MenuModel nodo = ColaMenuAgregar.Dequeue();
                
//                if (nodo.IsCheck)
//                {
//                    AgregarPadre(nodo.Parent);
//                    Rol.Menu auxMenuRol = new Rol.Menu();
//                    auxMenuRol.MenuName = nodo.MenuName;

//                    RolActual.MenuCollection.Add(auxMenuRol);
//                }
//                foreach (MenuModel mmm in nodo.ChildrenMenu)
//                    ColaMenuAgregar.Enqueue(mmm);                
//            }
//        }
//        public void AgregarPadre(MenuModel m) {

//            if (m == null)
//                return;
//            else
//            {
//                AgregarPadre(m.Parent);

//                Rol.Menu auxMenuRol = new Rol.Menu();
//                auxMenuRol.MenuName = m.MenuName;

//                if (!RolActual.MenuCollection.Contains(auxMenuRol))
//                    RolActual.MenuCollection.Add(auxMenuRol);
//            }
//        }
//        public bool MenuSelected() {

//            MenuModel auxMenu = this.MenuViewModel.MenuModel[0];
//            ColaMenu = new Queue<MenuModel>();

//            if (auxMenu.IsCheck)
//                return true;
//            else {
//                foreach (MenuModel mm in auxMenu.ChildrenMenu)
//                    ColaMenu.Enqueue(mm);
//                while (ColaMenu.Count > 0) {

//                    MenuModel nodo = ColaMenu.Dequeue();
//                    if (nodo.IsCheck)
//                        return true;
//                    else {

//                        foreach (MenuModel mmm in nodo.ChildrenMenu)
//                            ColaMenu.Enqueue(mmm);
//                    }
//                }

//                return false;
//            }
//        }
//        public bool UsuarioSelected() {
            
//            foreach (Usuario u in UsuariosCollection) {

//                if (u.IsChecked)
//                    return true;
//            }
//            return false;
//        }

//        #endregion

//        #region Properties

//        CatalogSeguridadViewModel _catalogSeguridadViewModel;
//        Queue<MenuModel> ColaMenu;
//        Queue<MenuModel> ColaMenuAgregar;
//        Queue<MenuModel> ColaMenuInicial;

//        public Rol RolActual
//        {
//            get { return _RolActual; }
//            set
//            {
//                if (_RolActual != value)
//                {
//                    _RolActual = value;
//                    OnPropertyChanged(RolActualPropertyName);
//                }
//            }
//        }
//        protected Rol _RolActual;
//        public const string RolActualPropertyName = "RolActual";

//        public MenuViewModel MenuViewModel
//        {
//            get { return _MenuViewModel; }
//            set
//            {
//                if (_MenuViewModel != value)
//                {
//                    _MenuViewModel = value;
//                    OnPropertyChanged(MenuViewModelPropertyName);
//                }
//            }
//        }
//        private MenuViewModel _MenuViewModel;
//        public const string MenuViewModelPropertyName = "MenuViewModel";

//        public ObservableCollection<Usuario> UsuariosCollection
//        {
//            get { return _UsuariosCollection; }
//            set
//            {
//                if (_UsuariosCollection != value)
//                {
//                    _UsuariosCollection = value;
//                    OnPropertyChanged(UsuariosCollectionPropertyName);
//                }
//            }
//        }
//        private ObservableCollection<Usuario> _UsuariosCollection;
//        public const string UsuariosCollectionPropertyName = "UsuariosCollection";

//        #endregion

//        #region Constructors 

//        public ModifySeguridadViewModel(CatalogSeguridadViewModel catalogSeguridadViewModel)
//        {
//            this._catalogSeguridadViewModel = catalogSeguridadViewModel;
//            this.RolActual = this._catalogSeguridadViewModel.SelectedRol;

//            this.MenuViewModel = new MenuViewModel(this._catalogSeguridadViewModel.IsSuperAdmin);
//            this.CargaMenuInicial();

//            this.UsuariosCollection = new ObservableCollection<Usuario>();
//            this.UsuariosCollection = this.GetUsers();
//            this.CargaUsuariosInicial();
//        }

//        #endregion

//        #region Methods

//        public ObservableCollection<Usuario> GetUsers()
//        {
//            AppUsuario dm = new AppUsuario();
//            ObservableCollection<Usuario> res = new ObservableCollection<Usuario>();

//            List<USUARIO> lu = (List<USUARIO>)dm.getElementsRolesFiltrado();

//            foreach (USUARIO u in lu) {

//                Usuario aux = new Usuario(u);

//                res.Add(aux);
//            }

//            return res;
//        }

//        public void CargaMenuInicial() {

//            foreach (Rol.Menu m in this.RolActual.MenuCollection) {

//                BuscaNombre(m.MenuName);        
//            }
//        }

//        public void BuscaNombre(string s) {

//            MenuModel auxMenu = this.MenuViewModel.MenuModel[0];
//            ColaMenuInicial = new Queue<MenuModel>();

//            if (auxMenu.MenuName.Equals(s))
//            {
//                auxMenu.IsCheck = true;
//                foreach (MenuModel Mmodel in auxMenu.ChildrenMenu) {

//                    Mmodel.IsCheck = false;
//                }
//                return;
//            }
//            else
//            {
//                foreach (MenuModel mm in auxMenu.ChildrenMenu)
//                    ColaMenuInicial.Enqueue(mm);
//                while (ColaMenuInicial.Count > 0)
//                {
//                    MenuModel nodo = ColaMenuInicial.Dequeue();
//                    if (nodo.MenuName.Equals(s))
//                    {
//                        nodo.IsCheck = true;
//                        foreach (MenuModel Mmodel in nodo.ChildrenMenu)
//                        {

//                            Mmodel.IsCheck = false;
//                        }
//                        return;
//                    }
//                    else
//                    {
//                        foreach (MenuModel mmm in nodo.ChildrenMenu)
//                            ColaMenuInicial.Enqueue(mmm);
//                    }
//                }
//            }
//        }

//        public void CargaUsuariosInicial() {

//            foreach (Rol.User u in this.RolActual.UsuariosCollection) {

//                foreach (Usuario U in this.UsuariosCollection) {

//                    if (U.UNID_USUARIO == u.UnidUser)
//                        U.IsChecked = true;
//                }
//            }
//        }

//        #endregion        
//    }
//}
