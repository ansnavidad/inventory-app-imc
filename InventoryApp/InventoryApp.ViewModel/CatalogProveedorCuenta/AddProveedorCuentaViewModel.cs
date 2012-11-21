using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogProveedorCuenta
{
    public class AddProveedorCuentaViewModel
    {
        #region Fields
        private ProveedorCuentaModel _proveedorCuentaModel;
        private RelayCommand _addProveedorCuentaCommand;
        private CatalogProveedorCuentaViewModel _catalogProveedorCuentaViewModel;
        private CatalogProveedorModel _catalogProveedorModel;
        private CatalogBancoModel _catalogBancoModel;

        #endregion

        //Exponer la propiedad item status
        #region Props
        public CatalogProveedorModel CatalogProveedorModel
        {
            get
            {
                return _catalogProveedorModel;
            }
            set
            {
                _catalogProveedorModel = value;
            }
        }

        public CatalogBancoModel CatalogBancoModel
        {
            get
            {
                return _catalogBancoModel;
            }
            set
            {
                _catalogBancoModel = value;
            }
        }

        public ProveedorCuentaModel ProveedorCuentaModel
        {
            get
            {
                return _proveedorCuentaModel;
            }
            set
            {
                _proveedorCuentaModel = value;
            }
        }

        public ICommand AddProveedorCommand
        {
            get 
            {
                if (_addProveedorCuentaCommand == null)
                {
                    _addProveedorCuentaCommand = new RelayCommand(p => this.AttempAddProveedorCuenta(), p => this.CanAttempAddProveedorCuenta());
                }
                return _addProveedorCuentaCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddProveedorCuentaViewModel(CatalogProveedorCuentaViewModel catalogProveedorCuentaViewModel)
        {
            this._proveedorCuentaModel = new ProveedorCuentaModel(new ProveedorCuentaDataMapper());
            this._catalogProveedorCuentaViewModel = catalogProveedorCuentaViewModel;

            try
            {

                this._catalogBancoModel = new CatalogBancoModel(new BancoDataMapper());
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {

                this._catalogProveedorModel = new CatalogProveedorModel(new ProveedorDataMapper());
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddProveedorCuenta()
        {
            bool _canAddProveedorCuenta = true;
            if (String.IsNullOrEmpty(this._proveedorCuentaModel.Beneficiario) || String.IsNullOrEmpty(this._proveedorCuentaModel.Clabe) || String.IsNullOrEmpty(this._proveedorCuentaModel.NumeroCuenta) || this._proveedorCuentaModel.Proveedor == null)
                _canAddProveedorCuenta = false;

            return _canAddProveedorCuenta;
        }

        public void AttempAddProveedorCuenta()
        {
            this._proveedorCuentaModel.saveProveedorCuenta();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogProveedorCuentaViewModel != null)
            {
                this._catalogProveedorCuentaViewModel.loadItems();
            }
        }
        #endregion
    }
}
