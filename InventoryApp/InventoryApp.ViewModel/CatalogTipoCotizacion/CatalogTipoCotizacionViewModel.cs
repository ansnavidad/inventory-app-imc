using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTipoCotizacion
{
    public class CatalogTipoCotizacionViewModel
    {
        private CatalogTipoCotizacionModel _catalogTipoCotizacionModel;

        public CatalogTipoCotizacionViewModel()
        {
            try
            {
                IDataMapper dataMapper = new TipoCotizacionDataMapper();
                this._catalogTipoCotizacionModel = new CatalogTipoCotizacionModel(dataMapper);
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
        public CatalogTipoCotizacionModel CatalogTipoCotizacionModel
        {
            get
            {
                return _catalogTipoCotizacionModel;
            }
            set
            {
                _catalogTipoCotizacionModel = value;
            }
        }

        public void loadTipoCotizacion()
        {
            this._catalogTipoCotizacionModel.loadItems();
        }
        /// <summary>
        /// Crea una nueva instancia de addTipoCotizacion y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddTipoCotizacionViewModel CreateAddTipoCotizacionViewModel()
        {
            return new AddTipoCotizacionViewModel(this);
        }
        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyTipoCotizacionViewModel CreateModifyTipoCotizacionViewModel()
        {
            TipoCotizacionModel tipoCotizacionModel = new TipoCotizacionModel(new TipoCotizacionDataMapper());
            if (this._catalogTipoCotizacionModel != null && this._catalogTipoCotizacionModel.SelectedTipoCotizacion != null)
            {
                tipoCotizacionModel.TipoCotizacionName = this._catalogTipoCotizacionModel.SelectedTipoCotizacion.TIPO_COTIZACION_NAME;
                tipoCotizacionModel.UnidTipoCotizacion = this._catalogTipoCotizacionModel.SelectedTipoCotizacion.UNID_TIPO_COTIZACION;
            }
            return new ModifyTipoCotizacionViewModel(this, tipoCotizacionModel);
        }
    }
}
