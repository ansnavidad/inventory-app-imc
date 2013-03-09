using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogUnidad
{
    public class ModifyUnidadViewModel
    {
        #region Fields
        private UnidadModel _modiUnidad;
        private RelayCommand _modifyUnidadCommand;
        private CatalogUnidadViewModel _catalogUnidadViewModel;
        #endregion

        //Exponer la propiedad unidad
        #region Props
        public UnidadModel ModiUnidad 
        {
            get
            {
                return _modiUnidad;
            }
            set
            {
                _modiUnidad = value;
            }
        }

        public ICommand ModifyUnidadCommand
        {
            get 
            {
                if (_modifyUnidadCommand == null)
                {
                    _modifyUnidadCommand = new RelayCommand(p => this.AttempModifyUnidad(), p => this.CanAttempModifyUnidad());
                }
                return _modifyUnidadCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyUnidadViewModel(CatalogUnidadViewModel catalogUnidadViewModel, UnidadModel selectedUnidadModel)
        {
            this._modiUnidad = new UnidadModel (new UnidadDataMapper(), catalogUnidadViewModel.ActualUser);
            this._catalogUnidadViewModel = catalogUnidadViewModel;
            this._modiUnidad.UnidUnidad = selectedUnidadModel.UnidUnidad;
            this._modiUnidad.UnidadName = selectedUnidadModel.UnidadName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyUnidad()
        {
            bool _canAddUnidad = true;
            if (String.IsNullOrEmpty(this._modiUnidad.UnidadName))
                _canAddUnidad = false;

            return _canAddUnidad;
        }

        public void AttempModifyUnidad()
        {
            this._modiUnidad.updateUnidad();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogUnidadViewModel != null)
            {
                this._catalogUnidadViewModel.loadItems();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.ModiUnidad);
            return historialViewModel;
        }
        #endregion
    }
}
