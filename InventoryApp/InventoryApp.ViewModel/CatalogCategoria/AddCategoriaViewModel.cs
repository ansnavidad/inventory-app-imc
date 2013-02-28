using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.CatalogArticulo;

namespace InventoryApp.ViewModel.CatalogCategoria
{
    public class AddCategoriaViewModel
    {
        #region Fields
        private CategoriaModel _addCategoria;
        private RelayCommand _addCategoriaCommand;
        private CatalogCategoriaViewModel _catalogCategoriaViewModel;
        private AddArticuloViewModel _artVM;
        private ModifyArticuloViewModel _artMod;
        #endregion

        //Exponer la propiedad categoria
        #region Props
        public CategoriaModel AddCategoria
        {
            get
            {
                return _addCategoria;
            }
            set
            {
                _addCategoria = value;
            }
        }

        public ICommand AddCategoriaCommand
        {
            get
            {
                if (_addCategoriaCommand == null)
                {
                    _addCategoriaCommand = new RelayCommand(p => this.AttempAddCategoria(), p => this.CanAttempAddCategoria());
                }
                return _addCategoriaCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddCategoriaViewModel(CatalogCategoriaViewModel catalogCategoriaViewModel)
        {
            this._addCategoria = new CategoriaModel(new CategoriaDataMapper(), catalogCategoriaViewModel.ActualUser);
            this._catalogCategoriaViewModel = catalogCategoriaViewModel;
        }

        public AddCategoriaViewModel(CatalogCategoriaViewModel catalogCategoriaViewModel, AddArticuloViewModel artVM)
        {
            _artVM = artVM;
            this._addCategoria = new CategoriaModel(new CategoriaDataMapper(), artVM.Articulo.ActualUser);
            this._catalogCategoriaViewModel = catalogCategoriaViewModel;
        }

        public AddCategoriaViewModel(CatalogCategoriaViewModel catalogCategoriaViewModel, ModifyArticuloViewModel artVM)
        {
            _artMod = artVM;
            this._addCategoria = new CategoriaModel(new CategoriaDataMapper(), artVM.Articulo.ActualUser);
            this._catalogCategoriaViewModel = catalogCategoriaViewModel;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddCategoria()
        {
            bool _canAddCategoria = true;
            if (String.IsNullOrEmpty(this._addCategoria.CategoriaName))
                _canAddCategoria = false;

            return _canAddCategoria;
        }

        public void AttempAddCategoria()
        {
            this._addCategoria.saveCategoria();

            //Puede ser que para pruebas unitarias catalogCategoriaViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogCategoriaViewModel != null)
            {
                this._catalogCategoriaViewModel.loadItems();
            }

            if (_artVM != null) {

                _artVM.CatalogCategoriaModel.loadItems();
                _artVM.Articulo.Categoria = _artVM.CatalogCategoriaModel.Categoria[_artVM.CatalogCategoriaModel.Categoria.Count - 1];
                //_artVM.CatalogCategoriaModel.SelectedCategoria = _artVM.CatalogCategoriaModel.Categoria[0];
            }

            if (_artMod != null)
            {
                _artMod.CatalogCategoriaModel.loadItems();
                _artMod.Articulo.Categoria = _artMod.CatalogCategoriaModel.Categoria[_artMod.CatalogCategoriaModel.Categoria.Count - 1];
                //_artVM.CatalogCategoriaModel.SelectedCategoria = _artVM.CatalogCategoriaModel.Categoria[0];
            }
        }
        #endregion
    }
}
