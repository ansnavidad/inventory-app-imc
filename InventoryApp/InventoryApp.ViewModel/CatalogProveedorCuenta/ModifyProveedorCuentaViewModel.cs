using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogProveedorCuenta
{
    public class ModifyProveedorCuentaViewModel
    {
        #region Fields
        private ProveedorCuentaModel _proveedorCuentaModel;
        private RelayCommand _modifyProveedorCuentaCommand;
        private CatalogProveedorCuentaViewModel _catalogProveedorCuentaViewModel;
        private CatalogBancoModel _catalogBancoModel;
        private CatalogProveedorModel _catalogProveedorModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
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

        public ICommand ModifyProveedorCuentaCommand
        {
            get 
            {
                if (_modifyProveedorCuentaCommand == null)
                {
                    _modifyProveedorCuentaCommand = new RelayCommand(p => this.AttempModifyProveedorCuenta(), p => this.CanAttempModifyProveedorCuenta());
                }
                return _modifyProveedorCuentaCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyProveedorCuentaViewModel(CatalogProveedorCuentaViewModel catalogProveedorCuentaViewModel, ProveedorCuentaModel selectedProveedorCuentaModel)
        {
            this._proveedorCuentaModel = new ProveedorCuentaModel(new ProveedorCuentaDataMapper());
            this._catalogProveedorCuentaViewModel = catalogProveedorCuentaViewModel;
            this._proveedorCuentaModel.UnidProveedorCuenta = selectedProveedorCuentaModel.UnidProveedorCuenta;
            this._proveedorCuentaModel.Proveedor = selectedProveedorCuentaModel.Proveedor;
            this._proveedorCuentaModel.NumeroCuenta = selectedProveedorCuentaModel.NumeroCuenta;
            this._proveedorCuentaModel.Clabe = selectedProveedorCuentaModel.Clabe;
            this._proveedorCuentaModel.Beneficiario = selectedProveedorCuentaModel.Beneficiario;
            this._proveedorCuentaModel.Banco = selectedProveedorCuentaModel.Banco;

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
        public bool CanAttempModifyProveedorCuenta()
        {
            bool _canAddProveedorCuenta = true;
            if (String.IsNullOrEmpty(this._proveedorCuentaModel.Beneficiario) || String.IsNullOrEmpty(this._proveedorCuentaModel.Clabe) || String.IsNullOrEmpty(this._proveedorCuentaModel.NumeroCuenta))
                _canAddProveedorCuenta = false;

            return _canAddProveedorCuenta;
        }

        public void AttempModifyProveedorCuenta()
        {
            this._proveedorCuentaModel.updateProveedorCuenta();

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
