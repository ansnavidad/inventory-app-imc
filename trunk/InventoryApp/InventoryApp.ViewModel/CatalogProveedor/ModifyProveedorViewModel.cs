using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogProveedor
{
    public class ModifyProveedorViewModel
    {
        #region Fields
        private ProveedorModel _proveedorModel;
        private RelayCommand _modifyProveedorCommand;
        private CatalogProveedorViewModel _catalogProveedorViewModel;
        private CatalogCiudadModel _catalogCiudadModel;
        private CatalogPaisModel _catalogPaisModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public CatalogCiudadModel CatalogCiudadModel 
        {
            get
            {
                return _catalogCiudadModel;
            }
            set
            {
                _catalogCiudadModel = value;
            }
        }

        public CatalogPaisModel CatalogPaisModel
        {
            get
            {
                return _catalogPaisModel;
            }
            set
            {
                _catalogPaisModel = value;
            }
        }

        public ProveedorModel ProveedorModel
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

        public ICommand ModifyProveedorCommand
        {
            get 
            {
                if (_modifyProveedorCommand == null)
                {
                    _modifyProveedorCommand = new RelayCommand(p => this.AttempModifyProveedor(), p => this.CanAttempModifyProveedor());
                }
                return _modifyProveedorCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyProveedorViewModel(CatalogProveedorViewModel catalogProveedorViewModel, ProveedorModel selectedProveedorModel)
        {
            this._proveedorModel = new ProveedorModel(new ProveedorDataMapper());
            this._catalogProveedorViewModel = catalogProveedorViewModel;
            this._proveedorModel.UnidProveedor = selectedProveedorModel.UnidProveedor;
            this._proveedorModel.Pais = selectedProveedorModel.Pais;
            this._proveedorModel.Ciudad = selectedProveedorModel.Ciudad;
            this._proveedorModel.Tel2 = selectedProveedorModel.Tel2;
            this._proveedorModel.Tel1 = selectedProveedorModel.Tel1;
            this._proveedorModel.RFC = selectedProveedorModel.RFC;
            this._proveedorModel.ProveedorName = selectedProveedorModel.ProveedorName;
            this._proveedorModel.Mail = selectedProveedorModel.Mail;
            this._proveedorModel.Contacto = selectedProveedorModel.Contacto;
            this._proveedorModel.CodigoPostal = selectedProveedorModel.CodigoPostal;
            this._proveedorModel.Calle = selectedProveedorModel.Calle;

            try
            {

                this._catalogCiudadModel = new CatalogCiudadModel(new CiudadDataMapper());
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

                this._catalogPaisModel = new CatalogPaisModel(new PaisDataMapper());
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
        public bool CanAttempModifyProveedor()
        {
            bool _canAddProveedor = true;
            if (String.IsNullOrEmpty(this._proveedorModel.ProveedorName) || String.IsNullOrEmpty(this._proveedorModel.Calle) || String.IsNullOrEmpty(this._proveedorModel.CodigoPostal) || String.IsNullOrEmpty(this._proveedorModel.Contacto) || String.IsNullOrEmpty(this._proveedorModel.Mail) || String.IsNullOrEmpty(this._proveedorModel.RFC) || String.IsNullOrEmpty(this._proveedorModel.Tel1) || String.IsNullOrEmpty(this._proveedorModel.Tel2))
                _canAddProveedor = false;

            return _canAddProveedor;
        }

        public void AttempModifyProveedor()
        {
            this._proveedorModel.updateProveedor();

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
