using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel
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
    }
}
