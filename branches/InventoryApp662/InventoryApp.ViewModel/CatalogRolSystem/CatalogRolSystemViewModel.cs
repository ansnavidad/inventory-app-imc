using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogRolSystem
{
    public class CatalogRolSystemViewModel : ViewModelBase, IPageViewModel
    {
        #region Properties

        public ObservableCollection<ROL> RolCollection
        {
            get { return _RolCollection; }
            set
            {
                if (_RolCollection != value)
                {
                    _RolCollection = value;
                    OnPropertyChanged(RolCollectionPropertyName);
                }
            }
        }
        protected ObservableCollection<ROL> _RolCollection;
        public const string RolCollectionPropertyName = "RolCollection";

        public ROL SelectedRol
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
        protected ROL _SelectedRol;
        public const string SelectedRolPropertyName = "SelectedRol";


        #endregion

        #region Constructors

        public CatalogRolSystemViewModel() {

            RolCollection = GetRols();
        }

        #endregion

        #region Methods

        public ModifyRolSystemViewModel CraeteModifyRolSystemViewModel()
        {
            if (this.SelectedRol != null)
                return new ModifyRolSystemViewModel(this.SelectedRol);
            else
                return null;            
        }

        public ObservableCollection<ROL> GetRols() {

            ObservableCollection<ROL> res = new ObservableCollection<ROL>();

            AppRolDataMapper appRols = new AppRolDataMapper();
            res = (ObservableCollection<ROL>)appRols.getElements();

            if (res == null)
                res = new ObservableCollection<ROL>();

            return res;
        }

        #endregion

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
