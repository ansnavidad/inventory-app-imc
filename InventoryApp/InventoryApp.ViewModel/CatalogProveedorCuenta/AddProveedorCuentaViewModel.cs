using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.CatalogProveedor;
using InventoryApp.DAL.POCOS;

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
        private AddProveedorViewModel _altaDM;
        private ModifyProveedorViewModel _altaMod;

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

        public AddProveedorCuentaViewModel(CatalogProveedorCuentaViewModel catalogProveedorCuentaViewModel, AddProveedorViewModel altaDM)
        {
            _altaDM = altaDM;
            this._proveedorCuentaModel = new ProveedorCuentaModel(new ProveedorCuentaDataMapper());
            this._catalogProveedorCuentaViewModel = catalogProveedorCuentaViewModel;

            try
            {
                this._catalogBancoModel = new CatalogBancoModel(new BancoDataMapper());
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

        public AddProveedorCuentaViewModel(CatalogProveedorCuentaViewModel catalogProveedorCuentaViewModel, ModifyProveedorViewModel altaDM)
        {
            _altaMod = altaDM;
            this._proveedorCuentaModel = new ProveedorCuentaModel(new ProveedorCuentaDataMapper());
            this._catalogProveedorCuentaViewModel = catalogProveedorCuentaViewModel;

            try
            {
                this._catalogBancoModel = new CatalogBancoModel(new BancoDataMapper());
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
            if (String.IsNullOrEmpty(this._proveedorCuentaModel.Beneficiario) || String.IsNullOrEmpty(this._proveedorCuentaModel.Clabe) || String.IsNullOrEmpty(this._proveedorCuentaModel.NumeroCuenta))
                _canAddProveedorCuenta = false;

            return _canAddProveedorCuenta;
        }

        public void AttempAddProveedorCuenta()
        {
            if (_altaDM != null) {

                PROVEEDOR_CUENTA provCuenta = new PROVEEDOR_CUENTA
                {
                    BANCO = this._proveedorCuentaModel.Banco
                  ,
                    BENEFICIARIO = this._proveedorCuentaModel.Beneficiario
                  ,
                    CLABE = this._proveedorCuentaModel.Clabe
                  ,
                    NUMERO_CUENTA = this._proveedorCuentaModel.NumeroCuenta
                  ,
                    UNID_BANCO = this._proveedorCuentaModel.Banco.UNID_BANCO
                  ,
                    UNID_PROVEEDOR = this._altaDM.ProveedorEnvio.UnidProveedor                  
                  ,
                    IS_ACTIVE = true
                  ,
                    IS_MODIFIED = true
                };

                DeleteProveedorCuenta provDel = new DeleteProveedorCuenta(provCuenta);
                provDel.IsChecked = false;

                this._altaDM.CatalogProveedorCuentaModel.ProveedorCuenta.Add(provDel);
            }

            if (_altaMod != null)
            {

                PROVEEDOR_CUENTA provCuenta = new PROVEEDOR_CUENTA
                {
                    BANCO = this._proveedorCuentaModel.Banco
                  ,
                    BENEFICIARIO = this._proveedorCuentaModel.Beneficiario
                  ,
                    CLABE = this._proveedorCuentaModel.Clabe
                  ,
                    NUMERO_CUENTA = this._proveedorCuentaModel.NumeroCuenta
                  ,
                    UNID_BANCO = this._proveedorCuentaModel.Banco.UNID_BANCO
                  ,
                    UNID_PROVEEDOR = this._altaMod.ProveedorModel.UnidProveedor
                  ,
                    IS_ACTIVE = true
                  ,
                    IS_MODIFIED = true
                  , 
                    UNID_PROVEEDOR_CUENTA = UNID.getNewUNID() 
                };

                DeleteProveedorCuenta provDel = new DeleteProveedorCuenta(provCuenta);
                provDel.IsChecked = false;

                this._altaMod.CatalogProveedorCuentaModel.ProveedorCuenta.Add(provDel);
                this._altaMod.ProveedorModel._unidsCuenta.Add(provDel.UNID_PROVEEDOR_CUENTA);
            }  
        }

        #endregion
    }
}
