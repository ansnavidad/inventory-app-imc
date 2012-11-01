using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel
{
    public class CatalogTransporteViewModel
    {
        private CatalogTransporteModel _catalogTransporteModel;

        public CatalogTransporteViewModel()
        {
            try
            {
                IDataMapper dataMapper = new TransporteDataMapper();
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper);
            }
            catch (ArgumentException ae)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public CatalogTransporteModel CatalogTransporteModel
        {
            get
            {
                return _catalogTransporteModel;
            }
            set
            {
                _catalogTransporteModel = value;
            }
        }
    }
}
