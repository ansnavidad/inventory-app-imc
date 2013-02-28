using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogTipoCotizacion
{
    public class ModifyTipoCotizacionViewModel
    {
        #region Fields
        private TipoCotizacionModel _modiTipoCotizacion;
        private RelayCommand _modifyTipoCotizacionCommand;
        private CatalogTipoCotizacionViewModel _catalogTipoCotizacionViewModel;
        #endregion

        //Exponer la propiedad de tipo de cotizacion
        #region Props
        public TipoCotizacionModel ModiTipoCotizacion
        {
            get
            {
                return _modiTipoCotizacion;
            }
            set
            {
                _modiTipoCotizacion = value;
            }
        }

        public ICommand ModifyTipoCotizacionCommand
        {
            get
            {
                if (_modifyTipoCotizacionCommand == null)
                {
                    _modifyTipoCotizacionCommand = new RelayCommand(p => this.AttempModifyTipoCotizacion(), p => this.CanAttempModifyTipoCotizacion());
                }
                return _modifyTipoCotizacionCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyTipoCotizacionViewModel(CatalogTipoCotizacionViewModel catalogTipoCotizacionViewModel, TipoCotizacionModel selectedTipoCotizacionModel)
        {
            this._modiTipoCotizacion = new TipoCotizacionModel(new TipoCotizacionDataMapper());
            this._catalogTipoCotizacionViewModel = catalogTipoCotizacionViewModel;
            this._modiTipoCotizacion.UnidTipoCotizacion = selectedTipoCotizacionModel.UnidTipoCotizacion;
            this._modiTipoCotizacion.TipoCotizacionName = selectedTipoCotizacionModel.TipoCotizacionName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyTipoCotizacion()
        {
            bool _canAddItemStatus = true;
            if (String.IsNullOrEmpty(this._modiTipoCotizacion.TipoCotizacionName))
                _canAddItemStatus = false;

            return _canAddItemStatus;
        }

        public void AttempModifyTipoCotizacion()
        {
            this._modiTipoCotizacion.updateTipoCotizacion();

            //Puede ser que para pruebas unitarias catalogTipoCotizacionViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTipoCotizacionViewModel != null)
            {
                this._catalogTipoCotizacionViewModel.loadTipoCotizacion();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.ModiTipoCotizacion);
            return historialViewModel;
        }
        #endregion
    }
}
