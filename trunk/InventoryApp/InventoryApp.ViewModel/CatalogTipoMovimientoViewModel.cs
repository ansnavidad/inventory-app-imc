using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel
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
    }
}
