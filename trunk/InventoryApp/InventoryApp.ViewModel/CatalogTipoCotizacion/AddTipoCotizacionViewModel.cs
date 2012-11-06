using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTipoCotizacion
{
    public class AddTipoCotizacionViewModel
    {
        #region Fields
        private TipoCotizacionModel _addTipoCotizacion;
        private RelayCommand _addTipoCotizacionCommand;
        private CatalogTipoCotizacionViewModel _catalogTipoCotizacionViewModel;
        #endregion
        //Exponer la propiedad tipo de cotizacion
        #region Props
        public TipoCotizacionModel AddTipoCotizacion
        {
            get
            {
                return _addTipoCotizacion;
            }
            set
            {
                _addTipoCotizacion = value;
            }
        }

        public ICommand AddTipoCotizacionCommand
        {
            get
            {
                if (_addTipoCotizacionCommand == null)
                {
                    _addTipoCotizacionCommand = new RelayCommand(p => this.AttempAddTipoCotizacion(), p => this.CanAttempAddTipoCotizacion());
                }
                return _addTipoCotizacionCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddTipoCotizacionViewModel(CatalogTipoCotizacionViewModel catalogTipoCotizacionViewModel)
        {
            this._addTipoCotizacion = new TipoCotizacionModel(new TipoCotizacionDataMapper());
            this._catalogTipoCotizacionViewModel = catalogTipoCotizacionViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddTipoCotizacion()
        {
            bool _canAddTipoCotizacion = true;
            if (String.IsNullOrEmpty(this._addTipoCotizacion.TipoCotizacionName))
                _canAddTipoCotizacion = false;

            return _canAddTipoCotizacion;
        }

        public void AttempAddTipoCotizacion()
        {
            this._addTipoCotizacion.saveTipoCotizacion();

            //Puede ser que para pruebas unitarias catalogTipoCotizacionViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTipoCotizacionViewModel != null)
            {
                this._catalogTipoCotizacionViewModel.loadTipoCotizacion();
            }
        }
        #endregion
    }
}
