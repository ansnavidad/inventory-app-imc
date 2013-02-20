using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model.Seguridad;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogSeguridad
{
    public class CatalogSeguridadViewModel : ViewModelBase, IPageViewModel
    {
        #region Relay Commands

        //Borrar Roles
        public ICommand DeleteRoles
        {
            get
            {
                if (_DeleteRoles == null)
                {
                    _DeleteRoles = new RelayCommand(p => this.AttemptDeleteRoles(), p => this.CanAttemptDeleteRoles());
                }
                return _DeleteRoles;
            }
        }
        private RelayCommand _DeleteRoles;
        private void AttemptDeleteRoles()
        {
            foreach (Rol r in RolesCollection)
                if (r.IsChecked) {

                    r.DeleteRol();
                }
            this.RolesCollection = this.GetRols();
        }
        private bool CanAttemptDeleteRoles()
        {
            foreach (Rol r in RolesCollection)
                if (r.IsChecked)
                    return true;
            return false;
        }

        #endregion

        #region Properties

        public ObservableCollection<Rol> RolesCollection
        {
            get { return _RolesCollection; }
            set
            {
                if (_RolesCollection != value)
                {
                    _RolesCollection = value;
                    OnPropertyChanged(RolesCollectionPropertyName);
                }
            }
        }
        protected ObservableCollection<Rol> _RolesCollection;
        public const string RolesCollectionPropertyName = "RolesCollection";

        public Rol SelectedRol
        {
            get { return _SelectedRol; }
            set
            {
                if (_SelectedRol != value)
                {
                    _SelectedRol = value;
                    OnPropertyChanged(SelectedRolPropertyName);
                }
            }
        }
        protected Rol _SelectedRol;
        public const string SelectedRolPropertyName = "SelectedRol";

        #endregion
        
        #region Constructors

        public CatalogSeguridadViewModel() {

            this.RolesCollection = this.GetRols();
        }

        #endregion
        
        #region Methods

        public ObservableCollection<Rol> GetRols() {

            ObservableCollection<ROL> res = new ObservableCollection<ROL>();
            ObservableCollection<Rol> res2 = new ObservableCollection<Rol>();


            AppRolDataMapper rm = new AppRolDataMapper();
            res = (ObservableCollection<ROL>)rm.getElementsSecurity();

            foreach (ROL rr in res) {

                Rol r = new Rol(rr);
                res2.Add(r);
            }

            return res2;
        }

        public AddSeguridadViewModel CreateAddSeguridadViewModel() {

            AddSeguridadViewModel addSeguridadViewModel = new AddSeguridadViewModel(this);
            return addSeguridadViewModel;
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
