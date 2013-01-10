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
    public class ModifyProveedorViewModel
    {
        #region Fields
        private ProveedorModel _proveedorModel;
        private RelayCommand _modifyProveedorCommand;
        private RelayCommand _deleteProveedorCuentaCommand; 
        private CatalogProveedorViewModel _catalogProveedorViewModel;
        private CatalogCiudadModel _catalogCiudadModel;
        private CatalogPaisModel _catalogPaisModel;
        private CatalogCategoriaModel _catalogCategoriaModel;
        private CatalogProveedorCuentaModel _catalogProveedorCuentaModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public CatalogProveedorCuentaModel CatalogProveedorCuentaModel
        {
            get
            {
                return _catalogProveedorCuentaModel;
            }
            set
            {
                _catalogProveedorCuentaModel = value;
            }
        }

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
        public ICommand DeleteProveedorCuentaCommand
        {
            get
            {
                if (_deleteProveedorCuentaCommand == null)
                {
                    _deleteProveedorCuentaCommand = new RelayCommand(p => this.AttempDeleteCuenta(), p => this.CanAttempDeleteCuenta());
                }
                return _deleteProveedorCuentaCommand;
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
                object ret = this._proveedorModel.GetProveedorCategoria(selectedProveedorModel.UnidProveedor);
                this._catalogCategoriaModel = new CatalogCategoriaModel(new CategoriaDataMapper());
                //muestra los valores de las categorias que estan relacionadas
                foreach (var item in this._catalogCategoriaModel.Categoria)
                {
                    foreach (var ite in ((List<CATEGORIA>)ret))
                    {
                        if (item.UNID_CATEGORIA== ite.UNID_CATEGORIA)
                        {
                            item.IsChecked = true;
                            this._proveedorModel._auxUnidsCategorias.Add(ite.UNID_CATEGORIA);
                        }
                    }
                }
                
                object ret2 = this._proveedorModel.GetProveedorCuenta(selectedProveedorModel.UnidProveedor);
                this._catalogProveedorCuentaModel = new CatalogProveedorCuentaModel(new ProveedorCuentaDataMapper());
                //muestra los valores de las categorias que estan relacionadas
                
                foreach (var ite in ((List<PROVEEDOR_CUENTA>)ret2))
                {
                    //DeleteProveedorCuenta dpc = new DeleteProveedorCuenta(new PROVEEDOR_CUENTA { UNID_PROVEEDOR_CUENTA = ite.UNID_PROVEEDOR_CUENTA, UNID_PROVEEDOR = ite.UNID_PROVEEDOR, UNID_BANCO = ite.UNID_BANCO, NUMERO_CUENTA = ite.NUMERO_CUENTA, LAST_MODIFIED_DATE = ite.LAST_MODIFIED_DATE, IS_MODIFIED = ite.IS_MODIFIED, IS_ACTIVE = ite.IS_ACTIVE, BENEFICIARIO = ite.BENEFICIARIO, CLABE = ite.CLABE });
                    DeleteProveedorCuenta dpc = new DeleteProveedorCuenta(ite);
                    dpc.IsChecked = false;
                    this._proveedorModel._auxUnidsCuenta.Add(ite.UNID_PROVEEDOR_CUENTA);
                    this._proveedorModel._unidsCuenta.Add(ite.UNID_PROVEEDOR_CUENTA);
                    this.CatalogProveedorCuentaModel.ProveedorCuenta.Add(dpc);                        
                }
                
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
            //modificar para actualizar las relaciones proveedor categoria
            foreach (DeleteCategoria pc in this._catalogCategoriaModel.Categoria)
            {
                if (pc.IsChecked == true)
                {
                    this._proveedorModel._unidsCategorias.Add(pc.UNID_CATEGORIA);
                }   
            }

            foreach (DeleteProveedorCuenta pc in this.CatalogProveedorCuentaModel.ProveedorCuenta)
            {
                if (pc.IsChecked == true)
                {
                    this._proveedorModel._unidsCuenta.Add(pc.UNID_PROVEEDOR_CUENTA);
                }
            }

            this._proveedorModel.updateProveedor(this.CatalogProveedorCuentaModel.ProveedorCuenta);

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogProveedorViewModel != null)
            {
                this._catalogProveedorViewModel.loadItems();
            }
        }

        public bool CanAttempDeleteCuenta()
        {
            return true;
        }
        public void AttempDeleteCuenta()
        {
            for (int i = 0; i < this.CatalogProveedorCuentaModel.ProveedorCuenta.Count; )
            {

                if (this.CatalogProveedorCuentaModel.ProveedorCuenta[i].IsChecked){
                    this.ProveedorModel._unidsCuenta.Remove(this.CatalogProveedorCuentaModel.ProveedorCuenta[i].UNID_PROVEEDOR_CUENTA);
                    this.CatalogProveedorCuentaModel.ProveedorCuenta.RemoveAt(i);
                }
                else
                    i++;
            }
        }
        #endregion
    }
}
