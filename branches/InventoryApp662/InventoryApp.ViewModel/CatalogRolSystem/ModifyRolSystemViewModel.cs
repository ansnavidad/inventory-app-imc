using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogRolSystem
{
    public class ModifyRolSystemViewModel: ViewModelBase
    {
        #region Relay Commands

        private RelayCommand _updateRolCommand;
        public ICommand UpdateRolCommand
        {
            get
            {
                if (_updateRolCommand == null)
                {
                    _updateRolCommand = new RelayCommand(p => this.AttempUpdateRol(), p => this.CanAttempUpdateRol());
                }
                return _updateRolCommand;
            }
        }
        public bool CanAttempUpdateRol()
        {
            return true;
        }
        public void AttempUpdateRol()
        {
            AppRolDataMapper ar = new AppRolDataMapper();
            ar.udpateElement(SelectedRol);
        }

        #endregion

        #region Properties

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

        public ModifyRolSystemViewModel(ROL r) {

            SelectedRol = r;
        }

        #endregion
    }
}
