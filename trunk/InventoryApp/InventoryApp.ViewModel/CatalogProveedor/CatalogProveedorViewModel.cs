using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogProveedor
{
    public class CatalogProveedorViewModel
    {
        private CatalogProveedorModel _proveedorModel;

        public CatalogProveedorViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new ProveedorDataMapper();
                this._proveedorModel = new CatalogProveedorModel(dataMapper);   
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
            ProveedorModel proveedorModel = new ProveedorModel(new ProveedorDataMapper());
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
                proveedorModel.UnidCiudad = this._proveedorModel.SelectedProveedor.UNID_CIUDAD;
                proveedorModel.UnidPais = this._proveedorModel.SelectedProveedor.UNID_PAIS;
                proveedorModel.UnidProveedor = this._proveedorModel.SelectedProveedor.UNID_PROVEEDOR;
            }
            return new ModifyProveedorViewModel(this, proveedorModel);
        }
    }
}
