using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogArticulo
{
    public class AddArticuloViewModel
    {
        #region Fields
        private ArticuloModel _articulo;
        private RelayCommand _addItemCommand;
        private CatalogArticuloViewModel _catalogArticuloViewModel;
        private CatalogCategoriaModel _catalogCategoriaModel;
        private CatalogMarcaModel _catalogMarcaModel;
        private CatalogModeloModel _catalogModeloModel;
        private CatalogEquipoModel _catalogEquipoModel;

        #endregion

        //Exponer la propiedad item status
        #region Props
        public ArticuloModel Articulo
        {
            get
            {
                return _articulo;
            }
            set
            {
                _articulo = value;
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

        public CatalogModeloModel CatalogModeloModel
        {
            get
            {
                return _catalogModeloModel;
            }
            set
            {
                _catalogModeloModel = value;
            }
        }

        public CatalogMarcaModel CatalogMarcaModel
        {
            get
            {
                return _catalogMarcaModel;
            }
            set
            {
                _catalogMarcaModel = value;
            }
        }

        public CatalogEquipoModel CatalogEquipoModel
        {
            get
            {
                return _catalogEquipoModel;
            }
            set
            {
                _catalogEquipoModel = value;
            }
        }


        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempArticulo(), p => this.CanAttempArticulo());
                }
                return _addItemCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddArticuloViewModel(CatalogArticuloViewModel catalogArticuloViewModel)
        {
            this._articulo = new ArticuloModel(new ArticuloDataMapper(), catalogArticuloViewModel.ActualUser);
            this._catalogArticuloViewModel = catalogArticuloViewModel;
            try
            {

                this._catalogCategoriaModel = new CatalogCategoriaModel(new CategoriaDataMapper());
                this._catalogEquipoModel = new CatalogEquipoModel(new EquipoDataMapper());
                this._catalogMarcaModel = new CatalogMarcaModel(new MarcaDataMapper());
                this._catalogModeloModel = new CatalogModeloModel(new ModeloDataMapper());
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
        public bool CanAttempArticulo()
        {
            bool _canInsertArticulo = true;
            if (String.IsNullOrEmpty(this._articulo.ArticuloName) ||
                this._articulo.Categoria == null ||
                this._articulo.Equipo == null ||
                this._articulo.Marca == null ||
                this._articulo.Modelo == null)
                _canInsertArticulo = false;

            return _canInsertArticulo;
        }

        public void AttempArticulo()
        {
            this._articulo.saveArticulo();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogArticuloViewModel != null)
            {
                this._catalogArticuloViewModel.loadItems();
            }
        }
        #endregion
    }
    
    #region Código de catálogo con relaciones
    
    //public class AddArticuloViewModel
    //{
    //    #region Fields
    //    private ArticuloModel _articulo;
    //    private RelayCommand _addItemCommand;
    //    private CatalogArticuloViewModel _catalogArticuloViewModel;
    //    private CatalogCategoriaModel _catalogCategoriaModel;
    //    private CatalogMarcaModel _catalogMarcaModel;
    //    private CatalogModeloModel _catalogModeloModel;
    //    private CatalogEquipoModel _catalogEquipoModel;

    //    #endregion

    //    //Exponer la propiedad item status
    //    #region Props
    //    public ArticuloModel Articulo
    //    {
    //        get
    //        {
    //            return _articulo;
    //        }
    //        set
    //        {
    //            _articulo = value;
    //        }
    //    }
    //    public CatalogCategoriaModel CatalogCategoriaModel
    //    {
    //        get
    //        {
    //            return _catalogCategoriaModel;
    //        }
    //        set
    //        {
    //            _catalogCategoriaModel = value;
    //        }
    //    }

    //    public CatalogModeloModel CatalogModeloModel
    //    {
    //        get
    //        {
    //            return _catalogModeloModel;
    //        }
    //        set
    //        {
    //            _catalogModeloModel = value;
    //        }
    //    }

    //    public CatalogMarcaModel CatalogMarcaModel
    //    {
    //        get
    //        {
    //            return _catalogMarcaModel;
    //        }
    //        set
    //        {
    //            _catalogMarcaModel = value;
    //        }
    //    }

    //    public CatalogEquipoModel CatalogEquipoModel
    //    {
    //        get
    //        {
    //            return _catalogEquipoModel;
    //        }
    //        set
    //        {
    //            _catalogEquipoModel = value;
    //        }
    //    }


    //    public ICommand AddItemCommand
    //    {
    //        get
    //        {
    //            if (_addItemCommand == null)
    //            {
    //                _addItemCommand = new RelayCommand(p => this.AttempArticulo(), p => this.CanAttempArticulo());
    //            }
    //            return _addItemCommand;
    //        }
    //    }
    //    #endregion

    //    #region Constructors
    //    /// <summary>
    //    /// Ejecuta la acción del command
    //    /// </summary>
    //    /// <param name="catalogItemStatusViewModel"></param>
    //    public AddArticuloViewModel(CatalogArticuloViewModel catalogArticuloViewModel)
    //    {
    //        this._articulo = new ArticuloModel(new ArticuloDataMapper());
    //        this._catalogArticuloViewModel = catalogArticuloViewModel;
    //        this._articulo.Categoria = new DAL.POCOS.CATEGORIA { IS_ACTIVE = true, IS_MODIFIED = true, UNID_CATEGORIA = UNID.getNewUNID() };
    //        this._articulo.Equipo = new DAL.POCOS.EQUIPO { IS_ACTIVE = true, IS_MODIFIED = true, UNID_EQUIPO = UNID.getNewUNID() + 1 };
    //        this._articulo.Marca = new DAL.POCOS.MARCA { IS_ACTIVE = true, IS_MODIFIED = true, UNID_MARCA = UNID.getNewUNID() + 1 };
    //        this._articulo.Modelo = new DAL.POCOS.MODELO { IS_ACTIVE = true, IS_MODIFIED = true, UNID_MODELO = UNID.getNewUNID() + 1 };
    //        this._articulo.UnidArticulo = UNID.getNewUNID() + 1;

    //        try
    //        {
    //            this._catalogCategoriaModel = new CatalogCategoriaModel(new CategoriaDataMapper());
    //            this._catalogEquipoModel = new CatalogEquipoModel(new EquipoDataMapper());
    //            this._catalogMarcaModel = new CatalogMarcaModel(new MarcaDataMapper());
    //            this._catalogModeloModel = new CatalogModeloModel(new ModeloDataMapper());
    //        }
    //        catch (ArgumentException ae)
    //        {
    //            ;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }

    //    }
    //    #endregion

    //    #region Methods
    //    /// <summary>
    //    /// Hace las validaciones necesarias para habilitar el command
    //    /// Si esta función retorna false, el command es deshabilitado
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool CanAttempArticulo()
    //    {
    //        bool _canInsertArticulo = true;
    //        if (String.IsNullOrEmpty(this._articulo.ArticuloName) ||
    //            String.IsNullOrEmpty(this._articulo.Modelo.MODELO_NAME) ||
    //            String.IsNullOrEmpty(this._articulo.Marca.MARCA_NAME) ||
    //            String.IsNullOrEmpty(this._articulo.Equipo.EQUIPO_NAME) ||
    //            String.IsNullOrEmpty(this._articulo.Categoria.CATEGORIA_NAME))
    //            _canInsertArticulo = false;

    //        return _canInsertArticulo;
    //    }

    //    public void AttempArticulo()
    //    {
    //        this._articulo.saveArticulo();

    //        //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
    //        //es una dependencia inyectada
    //        if (this._catalogArticuloViewModel != null)
    //        {
    //            this._catalogArticuloViewModel.loadItems();
    //        }
    //    }
    //    #endregion
    //}
        
    #endregion
}
