﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogProveedorCenta
{
    public class CatalogProveedorCuentaViewModel
    {
        private CatalogProveedorCuentaModel _proveedorCuentaModel;

        public CatalogProveedorCuentaViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new ProveedorCuentaDataMapper();
                this._proveedorCuentaModel = new CatalogProveedorCuentaModel(dataMapper);   
            }
            catch (ArgumentException a)
            {
                ;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
            
        }
        public CatalogProveedorCuentaModel ProveedorCuentaModel
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

        public void loadItems()
        {
            this._proveedorCuentaModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddProveedorCuentaViewModel CreateAddProveedorCuentaViewModel()
        {
            return new AddProveedorCuentaViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyProveedorCuentaViewModel CreateModifyProveedorCuentaViewModel()
        {
            ProveedorCuentaModel proveedorCuentaModel = new ProveedorCuentaModel(new ProveedorCuentaDataMapper());
            if (this._proveedorCuentaModel != null && this._proveedorCuentaModel.SelectedProveedorCuenta != null)
            {
                proveedorCuentaModel.Banco = this._proveedorCuentaModel.SelectedProveedorCuenta.BANCO;
                proveedorCuentaModel.Beneficiario = this._proveedorCuentaModel.SelectedProveedorCuenta.BENEFICIARIO;
                proveedorCuentaModel.Clabe = this._proveedorCuentaModel.SelectedProveedorCuenta.CLABE;
                proveedorCuentaModel.NumeroCuenta = this._proveedorCuentaModel.SelectedProveedorCuenta.NUMERO_CUENTA;
                proveedorCuentaModel.Proveedor = this._proveedorCuentaModel.SelectedProveedorCuenta.PROVEEDOR;
                proveedorCuentaModel.UnidProveedorCuenta = this._proveedorCuentaModel.SelectedProveedorCuenta.UNID_PROVEEDOR_CUENTA;
            }
            return new ModifyProveedorCuentaViewModel(this, proveedorCuentaModel);
        }
    }
}
