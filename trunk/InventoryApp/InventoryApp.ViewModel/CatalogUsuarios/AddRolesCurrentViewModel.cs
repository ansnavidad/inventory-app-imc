using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogUsuarios
{
    public class AddRolesCurrentViewModel
    {
        #region Fields
        private CatalogRolModel _addrol;
        private RelayCommand _addRolCommand;
        private ModifyUsuarioViewModel _modifyUsuarioViewModel;
        #endregion

        #region Props
        public CatalogRolModel Addrol
        {
            get
            {
                return _addrol;
            }
            set
            {
                _addrol = value;
            }
        }

        public ICommand AddRolCommand
        {
            get
            {
                if (_addRolCommand == null)
                {
                    _addRolCommand = new RelayCommand(p => this.AttempAddRol(), p => this.CanAttempAddRol());
                }
                return _addRolCommand;
            }
        }
        #endregion

        #region Constructors

        public AddRolesCurrentViewModel(ModifyUsuarioViewModel modifyUsuarioViewModel)
        {
            this._addrol = new CatalogRolModel(new AppRolDataMapper());
            this._modifyUsuarioViewModel = modifyUsuarioViewModel;
        }

        #endregion

        public bool CanAttempAddRol()
        {
           
            return true;
        }

        public void AttempAddRol()
        {
            AddRolUser();
        }

        public void AddRolUser()
        {
            List<long> auxUnidsRol = new List<long>();
            foreach (var r in this._modifyUsuarioViewModel.ModiUsuarioModel.Rol)
            {
                auxUnidsRol.Add(r.UNID_ROL);
            }
            foreach (DeleteRol item in this.Addrol.Rol)
            {
                if (item.IsChecked)
                {
                    if (!auxUnidsRol.Contains(item.UNID_ROL))
                    {
                        this._modifyUsuarioViewModel.ModiUsuarioModel.Rol.Add(item);
                    }     
                }
            }
        }

    }
}