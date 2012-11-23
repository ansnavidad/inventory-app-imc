using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogProveedor
{
    public class AddProveedorViewModel
    {
        #region Fields
        private ProveedorModel _proveedorEnvio;
        private RelayCommand _addProveedorCommand;
        private CatalogProveedorViewModel _catalogProveedorViewModel;
        private CatalogCiudadModel _catalogCiudadModel;
        private CatalogPaisModel _catalogPaisModel;
        private CatalogCategoriaModel _catalogCategoriaModel;
        #endregion

        //Exponer la propiedad proveedor
        #region Props
        public CatalogCategoriaModel CatalogCategoriaModel
        {
            get
            {
                return _catalogCategoriaModel;
            }
            set
            {
                _catalogCategoriaModel = value;
            }
        }
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

            try
            {

                this._catalogCategoriaModel = new CatalogCategoriaModel(new CategoriaDataMapper());
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
        public bool CanAttempAddProveedor()
        {
            bool _canAddProveedor = true;
            if (String.IsNullOrEmpty(this._proveedorEnvio.ProveedorName) || String.IsNullOrEmpty(this._proveedorEnvio.Calle) || String.IsNullOrEmpty(this._proveedorEnvio.CodigoPostal) || String.IsNullOrEmpty(this._proveedorEnvio.Contacto) || String.IsNullOrEmpty(this._proveedorEnvio.Mail) || String.IsNullOrEmpty(this._proveedorEnvio.RFC) || String.IsNullOrEmpty(this._proveedorEnvio.Tel1) || String.IsNullOrEmpty(this._proveedorEnvio.Tel2) || this._proveedorEnvio.Ciudad == null || this._proveedorEnvio.Pais == null)
                _canAddProveedor = false;

            return _canAddProveedor;
        }

        public void AttempAddProveedor()
        {
            foreach (DeleteCategoria item in this._catalogCategoriaModel.Categoria)
            {
                if (item.IsChecked == true)
                {
                    this._proveedorEnvio._unidsCategorias.Add(item.UNID_CATEGORIA);
                }
            }
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
