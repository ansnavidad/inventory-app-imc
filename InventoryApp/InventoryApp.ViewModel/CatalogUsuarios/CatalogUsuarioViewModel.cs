using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model.Seguridad;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogUsuarios
{
    public class CatalogUsuarioViewModel : ViewModelBase, IPageViewModel
    {
        #region Relay Commands



        #endregion

        #region Properties
        
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
        
        #endregion
        
        #region Constructors

        public CatalogUsuarioViewModel()
        {
            UsuariosCollection = new ObservableCollection<Usuario>();
            UsuariosCollection = GetUsers();
        }

        #endregion
        
        #region Methods

        public ObservableCollection<Usuario> GetUsers()
        {
            AppUsuario dm = new AppUsuario();
            ObservableCollection<Usuario> res = new ObservableCollection<Usuario>();

            List<USUARIO> lu = (List<USUARIO>)dm.getElementsCatalog();

            foreach (USUARIO u in lu)
            {
                Usuario aux = new Usuario(u);
                res.Add(aux);
            }

            return res;
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

        #endregion      
    }
}
