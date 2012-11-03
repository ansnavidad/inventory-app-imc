using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel
{
    public class CatalogItemStatusViewModel
    {
        private CatalogItemStatusModel _catalogItemStatusModel;

        public CatalogItemStatusViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new EstatusDataMapper();
                this._catalogItemStatusModel = new CatalogItemStatusModel(dataMapper);   
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
            
        }
        public CatalogItemStatusModel CatalogItemStatusModel
        {
            get
            {
                return _catalogItemStatusModel;
            }
            set
            {
                _catalogItemStatusModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogItemStatusModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddItemStatusViewModel CreateAddItemStatusViewModel()
        {
            return new AddItemStatusViewModel(this);
        }
    }
}
