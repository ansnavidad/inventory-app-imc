using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogArticulo
{
    public class ModifyArticuloViewModel
    {
        #region Fields
         private ArticuloModel _articulo;
        private RelayCommand _modifyItemCommand;
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


        public ICommand ModifyItemCommand
        {
            get
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyArticulo(), p => this.CanAttempModifyArticulo());
                }
                return _modifyItemCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyArticuloViewModel(CatalogArticuloViewModel catalogArticuloViewModel, ArticuloModel selectedArticuloModel)
        {
            this._articulo = new ArticuloModel(new ArticuloDataMapper(), catalogArticuloViewModel.ActualUser);
            this._catalogArticuloViewModel = catalogArticuloViewModel;
            this._articulo.UnidArticulo = selectedArticuloModel.UnidArticulo;
            this._articulo.ArticuloName = selectedArticuloModel.ArticuloName;
            this._articulo.Categoria = selectedArticuloModel.Categoria;
            this._articulo.Equipo = selectedArticuloModel.Equipo;
            this._articulo.Marca = selectedArticuloModel.Marca;
            this._articulo.Modelo = selectedArticuloModel.Modelo;
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
        public bool CanAttempModifyArticulo()
        {
            bool _canAddArtiuclo = true;
            if (String.IsNullOrEmpty(this._articulo.ArticuloName))
                _canAddArtiuclo = false;

            return _canAddArtiuclo;
        }

        public void AttempModifyArticulo()
        {
            this._articulo.updateArticulo();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogArticuloViewModel != null)
            {
                this._catalogArticuloViewModel.loadItems();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.Articulo);
            return historialViewModel;
        }
        #endregion

    }
}
