using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;

namespace InventoryApp.ViewModel
{
    public class CatalogTipoEmpresaViewModel
    {
         private CatalogTipoEmpresaModel _catalogTipoEmpresaModel;

        public CatalogTipoEmpresaViewModel()
        {
            try
            {
                IDataMapper dataMapper = new TipoEmpresaDataMapper();
                this._catalogTipoEmpresaModel = new CatalogTipoEmpresaModel(dataMapper);
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

        public CatalogTipoEmpresaModel CatalogTipoEmpresaModel
        {
            get
            {
                return _catalogTipoEmpresaModel;
            }
            set
            {
                _catalogTipoEmpresaModel = value;
            }
        }
    }
}
