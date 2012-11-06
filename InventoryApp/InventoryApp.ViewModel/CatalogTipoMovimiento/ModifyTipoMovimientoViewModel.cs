using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTipoMovimiento
{
    public class ModifyTipoMovimientoViewModel
    {
        #region Fields
        private TipoMovimientoModel _modiTipoMovimiento;
        private RelayCommand _modifyTipoMovimientoCommand;
        private CatalogTipoMovimientoViewModel _catalogTipoMovimientoViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public TipoMovimientoModel ModiTipoMovimiento 
        {
            get
            {
                return _modiTipoMovimiento;
            }
            set
            {
                _modiTipoMovimiento = value;
            }
        }

        public ICommand ModifyTipoMovimientoCommand
        {
            get 
            {
                if (_modifyTipoMovimientoCommand == null)
                {
                    _modifyTipoMovimientoCommand = new RelayCommand(p => this.AttempModifyTipoMovimiento(), p => this.CanAttempModifyTipoMovimiento());
                }
                return _modifyTipoMovimientoCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyTipoMovimientoViewModel(CatalogTipoMovimientoViewModel catalogTipoMovimientoViewModel, TipoMovimientoModel selectedTipoMovimientoModel)
        {
            this._modiTipoMovimiento = new TipoMovimientoModel(new TipoMovimientoDataMapper());
            this._catalogTipoMovimientoViewModel = catalogTipoMovimientoViewModel;
            this._modiTipoMovimiento.UnidTipoMovimiento = selectedTipoMovimientoModel.UnidTipoMovimiento;
            this._modiTipoMovimiento.TipoMovimientoName = selectedTipoMovimientoModel.TipoMovimientoName;
            this._modiTipoMovimiento.SignoMovimiento = selectedTipoMovimientoModel.SignoMovimiento;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyTipoMovimiento()
        {
            bool _canAddTipoMovimiento = true;
            if (String.IsNullOrEmpty(this._modiTipoMovimiento.TipoMovimientoName) || String.IsNullOrEmpty(this._modiTipoMovimiento.SignoMovimiento))
                _canAddTipoMovimiento = false;

            return _canAddTipoMovimiento;
        }

        public void AttempModifyTipoMovimiento()
        {
            this._modiTipoMovimiento.updateTipoMovimiento();

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
