using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogUnidad
{
    public class AddUnidadViewModel
    {
        #region Fields
        private UnidadModel _addUnidad;
        private RelayCommand _addUnidadCommand;
        private CatalogUnidadViewModel _catalogUnidadViewModel;
        #endregion

        //Exponer la propiedad unidad
        #region Props
        public UnidadModel AddUnidad 
        {
            get
            {
                return _addUnidad;
            }
            set
            {
                _addUnidad = value;
            }
        }

        public ICommand AddUnidadCommand
        {
            get 
            {
                if (_addUnidadCommand == null)
                {
                    _addUnidadCommand = new RelayCommand(p => this.AttempAddUnidad(), p => this.CanAttempAddUnidad());
                }
                return _addUnidadCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddUnidadViewModel(CatalogUnidadViewModel catalogUnidadViewModel)
        {
            this._addUnidad = new UnidadModel(new UnidadDataMapper());
            this._catalogUnidadViewModel = catalogUnidadViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddUnidad()
        {
            bool _canAddUnidad = true;
            if (String.IsNullOrEmpty(this._addUnidad.UnidadName))
                _canAddUnidad = false;

            return _canAddUnidad;
        }
        public void AttempAddUnidad()
        {
            this._addUnidad.saveUnidad();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogUnidadViewModel != null)
            {
                this._catalogUnidadViewModel.loadItems();
            }
        }
        #endregion
    }
}
