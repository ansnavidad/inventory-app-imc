using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogMoneda
{
    public class ModifyMonedaViewModel
    {
        #region Fields
        private MonedaModel _moneda;
        private RelayCommand _modifyMonedaCommand;
        private CatalogMonedaViewModel _catalogMonedaViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public MonedaModel Moneda 
        {
            get
            {
                return _moneda;
            }
            set
            {
                _moneda = value;
            }
        }

        public ICommand ModifyMonedaCommand
        {
            get 
            {
                if (_modifyMonedaCommand == null)
                {
                    _modifyMonedaCommand = new RelayCommand(p => this.AttempModifyMoneda(), p => this.CanAttempModifyMoneda());
                }
                return _modifyMonedaCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyMonedaViewModel(CatalogMonedaViewModel catalogMonedaViewModel,MonedaModel selectedMonedaModel)
        {
            this._moneda = new MonedaModel(new MonedaDataMapper());
            this._catalogMonedaViewModel = catalogMonedaViewModel;
            this._moneda.UnidMoneda = selectedMonedaModel.UnidMoneda;
            this._moneda.MonedaName = selectedMonedaModel.MonedaName;
            this._moneda.MonedaAbr = selectedMonedaModel.MonedaAbr;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyMoneda()
        {
            bool _canAddMoneda = true;
            if (String.IsNullOrEmpty(this._moneda.MonedaName) || String.IsNullOrEmpty(this._moneda.MonedaAbr))
                _canAddMoneda = false;

            return _canAddMoneda;
        }

        public void AttempModifyMoneda()
        {
            this._moneda.updateMoneda();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogMonedaViewModel != null)
            {
                this._catalogMonedaViewModel.loadMonedas();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.Moneda);
            return historialViewModel;
        }

        #endregion
    }
}
