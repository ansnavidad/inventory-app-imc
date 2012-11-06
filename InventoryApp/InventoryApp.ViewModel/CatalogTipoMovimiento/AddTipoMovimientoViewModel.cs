using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTipoMovimiento
{
    public class AddTipoMovimientoViewModel
    {
        #region Fields
        private TipoMovimientoModel _addTipoMovimiento;
        private RelayCommand _addTipoMovimientoCommand;
        private CatalogTipoMovimientoViewModel _catalogTipoMovimientoViewModel;
        #endregion
        //Exponer la propiedad de tipo de cotizacion
        #region Props
        public TipoMovimientoModel AddTipoMovimiento
        {
            get
            {
                return _addTipoMovimiento;
            }
            set
            {
                _addTipoMovimiento = value;
            }
        }

        public ICommand AddTipoMovimientoCommand
        {
            get
            {
                if (_addTipoMovimientoCommand == null)
                {
                    _addTipoMovimientoCommand = new RelayCommand(p => this.AttempAddTipoMovimiento(), p => this.CanAttempAddTipoMovimiento());
                }
                return _addTipoMovimientoCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddTipoMovimientoViewModel(CatalogTipoMovimientoViewModel catalogTipoMovimientoViewModel)
        {
            this._addTipoMovimiento = new TipoMovimientoModel(new TipoMovimientoDataMapper());
            this._catalogTipoMovimientoViewModel = catalogTipoMovimientoViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddTipoMovimiento()
        {
            bool _canAddTipoMovimiento = true;
            if (String.IsNullOrEmpty(this._addTipoMovimiento.TipoMovimientoName) || String.IsNullOrEmpty(this._addTipoMovimiento.SignoMovimiento))
            //{
                //if (1<this._addTipoMovimiento.SignoMovimiento.Length)
                //{
                    _canAddTipoMovimiento = false;    
                //}
                
            //}
               
            return _canAddTipoMovimiento;
        }

        public void AttempAddTipoMovimiento()
        {
            this._addTipoMovimiento.saveTipoMovimiento();

            //Puede ser que para pruebas unitarias catalogTipoMovimientoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTipoMovimientoViewModel != null)
            {
                this._catalogTipoMovimientoViewModel.loadTipoCotizacion();
            }
        }
        #endregion
    }
}
