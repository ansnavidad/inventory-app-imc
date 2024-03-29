﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogProveedor
{
    public class CatalogProveedorViewModel : IPageViewModel
    {
        private RelayCommand _deleteProveedorCommand;

        private CatalogProveedorModel _proveedorModel;
        public USUARIO ActualUser;
        public ICommand DeleteProveedorCommand
        {
            get
            {
                if (_deleteProveedorCommand == null)
                {
                    _deleteProveedorCommand = new RelayCommand(p => this.AttempDeleteProveedor(), p => this.CanAttempDeleteProveedor());
                }
                return _deleteProveedorCommand;
            }
        }

        public CatalogProveedorViewModel(USUARIO u)
        {
            
            try
            {
                IDataMapper dataMapper = new ProveedorDataMapper();
                this._proveedorModel = new CatalogProveedorModel(dataMapper);
                this.ActualUser = u;
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

        public CatalogProveedorModel ProveedorModel
        {
            get
            {
                return _proveedorModel;
            }
            set
            {
                _proveedorModel = value;
            }
        }

        public void loadItems()
        {
            this._proveedorModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddProveedorViewModel CreateAddProveedorViewModel()
        {
            return new AddProveedorViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyProveedorViewModel CreateModifyProveedorViewModel()
        {
            ProveedorModel proveedorModel = new ProveedorModel(new ProveedorDataMapper(), this.ActualUser);
            if (this._proveedorModel != null && this._proveedorModel.SelectedProveedor != null)
            {
                
                proveedorModel.Calle = this._proveedorModel.SelectedProveedor.CALLE;
                proveedorModel.CodigoPostal = this._proveedorModel.SelectedProveedor.CODIGO_POSTAL;
                proveedorModel.Contacto = this._proveedorModel.SelectedProveedor.CONTACTO;
                proveedorModel.Mail = this._proveedorModel.SelectedProveedor.MAIL;
                proveedorModel.ProveedorName = this._proveedorModel.SelectedProveedor.PROVEEDOR_NAME;
                proveedorModel.RFC = this._proveedorModel.SelectedProveedor.RFC;
                proveedorModel.Tel1 = this._proveedorModel.SelectedProveedor.TEL1;
                proveedorModel.Tel2 = this._proveedorModel.SelectedProveedor.TEL2;
                proveedorModel.Ciudad = this._proveedorModel.SelectedProveedor.Ciudad;
                proveedorModel.Pais = this._proveedorModel.SelectedProveedor.Pais;                
                proveedorModel.UnidProveedor = this._proveedorModel.SelectedProveedor.UNID_PROVEEDOR;
                
            }
            try
            {
                return new ModifyProveedorViewModel(this, proveedorModel);
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteProveedor()
        {
            bool _canDeleteProveedor = false;
            foreach (DeleteProveedor d in this._proveedorModel.Proveedor)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteProveedor = true;
                }
            }

            return _canDeleteProveedor;
        }

        public void AttempDeleteProveedor()
        {
            this._proveedorModel.deleteProveedor(this.ActualUser);

            if (this._proveedorModel != null)
            {
                this._proveedorModel.loadItems();
            }
        }
        #endregion

        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
