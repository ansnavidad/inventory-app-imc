using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogMarca
{
    public class AddMarcaViewModel
    {
         #region Fields
        private MarcaModel _marca;
        private RelayCommand _addItemCommand;
        private CatalogMarcaViewModel _catalogMarcaViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public MarcaModel Marca 
        {
            get
            {
                return _marca;
            }
            set
            {
                _marca = value;
            }
        }

        public ICommand AddItemCommand
        {
            get 
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempAddMarca(), p => this.CanAttempAddMarca());
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
        public AddMarcaViewModel(CatalogMarcaViewModel catalogMarcaViewModel)
        {
            this._marca = new MarcaModel(new MarcaDataMapper());
            this._catalogMarcaViewModel = catalogMarcaViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddMarca()
        {
            bool _canAddMarca = true;
            if (String.IsNullOrEmpty(this._marca.MarcaName))
                _canAddMarca = false;

            return _canAddMarca;
        }

        public void AttempAddMarca()
        {
            this._marca.saveMarca();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogMarcaViewModel != null)
            {
                this._catalogMarcaViewModel.loadItems();
            }
        }
        #endregion
    }
}
