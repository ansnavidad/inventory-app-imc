using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogProveedor
{
    public class AddProveedorViewModel
    {
        #region Fields
        private ProveedorModel _proveedorEnvio;
        private RelayCommand _addProveedorCommand;
        private CatalogProveedorViewModel _catalogProveedorViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public ProveedorModel ProveedorEnvio 
        {
            get
            {
                return _proveedorEnvio;
            }
            set
            {
                _proveedorEnvio = value;
            }
        }

        public ICommand AddProveedorCommand
        {
            get 
            {
                if (_addProveedorCommand == null)
                {
                    _addProveedorCommand = new RelayCommand(p => this.AttempAddProveedor(), p => this.CanAttempAddProveedor());
                }
                return _addProveedorCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddProveedorViewModel(CatalogProveedorViewModel catalogProveedorViewModel)
        {
            this._proveedorEnvio = new ProveedorModel(new ProveedorDataMapper());
            this._catalogProveedorViewModel = catalogProveedorViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddProveedor()
        {
            bool _canAddProveedor = true;
            if (String.IsNullOrEmpty(this._proveedorEnvio.ProveedorName))
                _canAddProveedor = false;

            return _canAddProveedor;
        }

        public void AttempAddProveedor()
        {
            this._proveedorEnvio.saveProveedor();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogProveedorViewModel != null)
            {
                this._catalogProveedorViewModel.loadItems();
            }
        }
        #endregion
    }
}
