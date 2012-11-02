using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel
{
    public class CatalogProyectoViewModel
    {
        private CatalogProyectoModel _catalogProyectoModel;

        public CatalogProyectoViewModel()
        {
            try
            {
                IDataMapper dataMapper = new ProyectoDataMapper();
                this._catalogProyectoModel = new CatalogProyectoModel(dataMapper);
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
        public CatalogProyectoModel CatalogProyectoModel
        {
            get
            {
                return _catalogProyectoModel;
            }
            set
            {
                _catalogProyectoModel = value;
            }
        }
    }
}
