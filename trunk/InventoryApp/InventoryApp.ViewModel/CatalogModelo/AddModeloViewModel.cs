using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogModelo
{
    public class AddModeloViewModel
    {
         #region Fields
        private ModeloModel _modelo;
        private RelayCommand _addItemCommand;
        private CatalogModeloViewModel _catalogModeloViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public ModeloModel Modelo 
        {
            get
            {
                return _modelo;
            }
            set
            {
                _modelo = value;
            }
        }

        public ICommand AddItemCommand
        {
            get 
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempAddModelo(), p => this.CanAttempAddModelo());
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
        public AddModeloViewModel(CatalogModeloViewModel catalogModeloViewModel)
        {
            this._modelo = new ModeloModel(new ModeloDataMapper());
            this._catalogModeloViewModel = catalogModeloViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddModelo()
        {
            bool _canAddModelo = true;
            if (String.IsNullOrEmpty(this._modelo.ModeloName))
                _canAddModelo = false;

            return _canAddModelo;
        }

        public void AttempAddModelo()
        {
            this._modelo.saveModelo();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogModeloViewModel != null)
            {
                this._catalogModeloViewModel.loadItems();
            }
        }
        #endregion
    }
}
