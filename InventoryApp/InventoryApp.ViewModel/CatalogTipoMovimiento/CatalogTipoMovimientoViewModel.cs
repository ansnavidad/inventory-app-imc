using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTipoMovimiento
{
    public class CatalogTipoMovimientoViewModel
    {
        private CatalogTipoMovimientoModel _catalogTipoMovimientoModel;

        public CatalogTipoMovimientoViewModel()
        {
            try
            {
                IDataMapper dataMapper = new TipoMovimientoDataMapper();
                this._catalogTipoMovimientoModel = new CatalogTipoMovimientoModel(dataMapper);
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            
        }

        public CatalogTipoMovimientoModel CatalogTipoMovimientoModel
        {
            get
            {
                return _catalogTipoMovimientoModel;
            }
            set
            {
                _catalogTipoMovimientoModel = value;
            }
        }

        public void loadTipoCotizacion()
        {
            this._catalogTipoMovimientoModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de AddTipoMovimiento y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddTipoMovimientoViewModel CreateAddTipoMovimientoViewModel()
        {
            return new AddTipoMovimientoViewModel(this);
        }
        /// <summary>
        /// Crea una nueva instancia de ModifyTipoMovimiento y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyTipoMovimientoViewModel CreateModifyTipoMovimientoViewModel()
        {
            TipoMovimientoModel tipoMovimientoModel = new TipoMovimientoModel(new TipoMovimientoDataMapper());
            if (this._catalogTipoMovimientoModel != null && this._catalogTipoMovimientoModel.SelectedTipoMovimiento != null)
            {
                tipoMovimientoModel.TipoMovimientoName = this._catalogTipoMovimientoModel.SelectedTipoMovimiento.TIPO_MOVIMIENTO_NAME;
                tipoMovimientoModel.UnidTipoMovimiento = this._catalogTipoMovimientoModel.SelectedTipoMovimiento.UNID_TIPO_MOVIMIENTO;
                tipoMovimientoModel.SignoMovimiento = this._catalogTipoMovimientoModel.SelectedTipoMovimiento.SIGNO_MOVIMIENTO;
            }
            return new ModifyTipoMovimientoViewModel(this, tipoMovimientoModel);
        }
    }
}
