using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
//using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogItemStatus
{
    public class AddItemStatusViewModel
    {
        #region Fields
        private ItemStatusModel _itemStatus;
        private RelayCommand _addItemCommand;
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

        public ICommand AddItemCommand
        {
            get 
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempAddItemStatus(), p => this.CanAttempAddItemStatus());
                }
                return _addItemCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddItemStatusViewModel(CatalogItemStatusViewModel catalogItemStatusViewModel)
        {
            this._itemStatus = new ItemStatusModel(new ItemStatusDataMapper());
            this._catalogItemStatusViewModel = catalogItemStatusViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddItemStatus()
        {
            bool _canAddItemStatus = true;
            if (String.IsNullOrEmpty(this._itemStatus.ItemStatusName))
                _canAddItemStatus = false;

            return _canAddItemStatus;
        }

        public void AttempAddItemStatus()
        {
            this._itemStatus.saveItemStatus();

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
