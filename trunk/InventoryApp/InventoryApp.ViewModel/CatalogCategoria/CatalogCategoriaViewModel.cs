using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogCategoria
{
    public class CatalogCategoriaViewModel : IPageViewModel
    {
        private RelayCommand _deleteCategoriaCommand;

        private CatalogCategoriaModel _catalogCategoriaModel;

        public ICommand DeleteCategoriaCommand
        {
            get
            {
                if (_deleteCategoriaCommand == null)
                {
                    _deleteCategoriaCommand = new RelayCommand(p => this.AttempDeleteCategoria(), p => this.CanAttempDeleteCategoria());
                }
                return _deleteCategoriaCommand;
            }
        }

        public CatalogCategoriaViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new CategoriaDataMapper();
                this._catalogCategoriaModel = new CatalogCategoriaModel(dataMapper);   
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

        public void loadItems()
        {
            this._catalogCategoriaModel.loadItems();
        }
        /// <summary>
        /// Crea una nueva instancia de AddCategoria y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddCategoriaViewModel CreateAddCategoriaViewModel()
        {
            return new AddCategoriaViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyCategoria y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyCategoriaViewModel CreateModifyCategoriaViewModel()
        {
            CategoriaModel categoriaModel = new CategoriaModel(new CategoriaDataMapper());
            if (this._catalogCategoriaModel != null && this._catalogCategoriaModel.SelectedCategoria != null)
            {
                categoriaModel.CategoriaName = this._catalogCategoriaModel.SelectedCategoria.CATEGORIA_NAME;
                categoriaModel.UnidCategoria = this._catalogCategoriaModel.SelectedCategoria.UNID_CATEGORIA;
            }
            return new ModifyCategoriaViewModel(this, categoriaModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteCategoria()
        {
            bool _canDeleteCategoria = false;
            foreach (DeleteCategoria d in this._catalogCategoriaModel.Categoria)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteCategoria = true;
                }
            }

            return _canDeleteCategoria;
        }

        public void AttempDeleteCategoria()
        {
            this._catalogCategoriaModel.deleteCategoria();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogCategoriaModel != null)
            {
                this._catalogCategoriaModel.loadItems();
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
