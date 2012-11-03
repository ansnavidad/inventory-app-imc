using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogItemStatus
{
    public class ModifyItemStatusViewModel
    {
        #region Fields
        private ItemStatusModel _itemStatus;
        private RelayCommand _modifyItemCommand;
        private CatalogItemStatusViewModel _catalogItemStatusViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public ItemStatusModel ItemStatus 
        {
            get
            {
                return _itemStatus;
            }
            set
            {
                _itemStatus = value;
            }
        }

        public ICommand ModifyItemCommand
        {
            get 
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyItemStatus(), p => this.CanAttempModifyItemStatus());
                }
                return _modifyItemCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyItemStatusViewModel(CatalogItemStatusViewModel catalogItemStatusViewModel,ItemStatusModel selectedItemStatusModel)
        {
            this._itemStatus = new ItemStatusModel(new ItemStatusDataMapper());
            this._catalogItemStatusViewModel = catalogItemStatusViewModel;
            this._itemStatus.UnidItemStatus = selectedItemStatusModel.UnidItemStatus;
            this._itemStatus.ItemStatusName = selectedItemStatusModel.ItemStatusName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyItemStatus()
        {
            bool _canAddItemStatus = true;
            if (String.IsNullOrEmpty(this._itemStatus.ItemStatusName))
                _canAddItemStatus = false;

            return _canAddItemStatus;
        }

        public void AttempModifyItemStatus()
        {
            this._itemStatus.updateItemStatus();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogItemStatusViewModel != null)
            {
                this._catalogItemStatusViewModel.loadItems();
            }
        }
        #endregion
    }
}
