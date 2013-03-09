using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogMoneda
{
    public class AddMonedaViewModel
    {
        #region Fields
        private MonedaModel _moneda;
        private RelayCommand _addMonedaCommand;
        private CatalogMonedaViewModel _catalogMonedaViewModel;
        #endregion

        //Exponer la propiedad moneda
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

        public ICommand AddMonedaCommand
        {
            get 
            {
                if (_addMonedaCommand == null)
                {
                    _addMonedaCommand = new RelayCommand(p => this.AttempAddMoneda(), p => this.CanAttempAddMoneda());
                }
                return _addMonedaCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddMonedaViewModel(CatalogMonedaViewModel catalogMonedaViewModel)
        {
            this._moneda = new MonedaModel(new MonedaDataMapper(), catalogMonedaViewModel.ActualUser);
            this._catalogMonedaViewModel = catalogMonedaViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddMoneda()
        {
            bool _canAddMoneda = true;
            if (String.IsNullOrEmpty(this._moneda.MonedaName) || String.IsNullOrEmpty(this._moneda.MonedaAbr))
                _canAddMoneda = false;

            return _canAddMoneda;
        }

        public void AttempAddMoneda()
        {
            this._moneda.saveMoneda();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogMonedaViewModel != null)
            {
                this._catalogMonedaViewModel.loadMonedas();
            }
        }
        #endregion
    }
}
