using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogItemStatus
{
    public class ModifyItemStatusViewModel
    {
        #region Fields
        private ItemStatusModel _modiItemStatus;
        private RelayCommand _modifyItemCommand;
        private CatalogItemStatusViewModel _catalogItemStatusViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public ItemStatusModel ModiItemStatus 
        {
            get
            {
                return _modiItemStatus;
            }
            set
            {
                _modiItemStatus = value;
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
            this._modiItemStatus = new ItemStatusModel(new ItemStatusDataMapper());
            this._catalogItemStatusViewModel = catalogItemStatusViewModel;
            this._modiItemStatus.UnidItemStatus = selectedItemStatusModel.UnidItemStatus;
            this._modiItemStatus.ItemStatusName = selectedItemStatusModel.ItemStatusName;
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
            if (String.IsNullOrEmpty(this._modiItemStatus.ItemStatusName))
                _canAddItemStatus = false;

            return _canAddItemStatus;
        }

        public void AttempModifyItemStatus()
        {
            this._modiItemStatus.updateItemStatus();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogItemStatusViewModel != null)
            {
                this._catalogItemStatusViewModel.loadItems();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.ModiItemStatus);
            return historialViewModel;
        }
        #endregion
    }
}
