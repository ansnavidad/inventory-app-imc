using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogCategoria
{
    public class ModifyCategoriaViewModel
    {
        #region Fields
        private CategoriaModel _modiCategoria;
        private RelayCommand _modifyCategoriaCommand;
        private CatalogCategoriaViewModel _catalogCategoriaViewModel;
        #endregion

        //Exponer la propiedad categoria
        #region Props
        public CategoriaModel ModiCategoria
        {
            get
            {
                return _modiCategoria;
            }
            set
            {
                _modiCategoria = value;
            }
        }

        public ICommand ModifyCategoriaCommand
        {
            get
            {
                if (_modifyCategoriaCommand == null)
                {
                    _modifyCategoriaCommand = new RelayCommand(p => this.AttempModifyCategoria(), p => this.CanAttempModifyCategoria());
                }
                return _modifyCategoriaCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyCategoriaViewModel(CatalogCategoriaViewModel catalogCategoriaViewModel, CategoriaModel selectedCategoriaModel)
        {
            this._modiCategoria = new CategoriaModel(new CategoriaDataMapper());
            this._catalogCategoriaViewModel = catalogCategoriaViewModel;
            this._modiCategoria.UnidCategoria = selectedCategoriaModel.UnidCategoria;
            this._modiCategoria.CategoriaName = selectedCategoriaModel.CategoriaName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyCategoria()
        {
            bool _canAddCategoria = true;
            if (String.IsNullOrEmpty(this._modiCategoria.CategoriaName))
                _canAddCategoria = false;

            return _canAddCategoria;
        }

        public void AttempModifyCategoria()
        {
            this._modiCategoria.updateCategoria();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogCategoriaViewModel != null)
            {
                this._catalogCategoriaViewModel.loadItems();
            }
        }
        #endregion
    }
}
