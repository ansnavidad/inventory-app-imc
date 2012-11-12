using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.Entradas
{
    public class EntradaPorValidacionViewModel
    {
        private MovimientoModel _movimientoModel;
        private CatalogSolicitante1Model _catalogSolicitanteModel;
        private CatalogItemModel _itemModel;

        public EntradaPorValidacionViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new Solicitante1DataMapper();
                this._catalogSolicitanteModel = new CatalogSolicitante1Model(dataMapper);
                this._movimientoModel = new MovimientoModel(new MovimientoDataMapper());
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
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
        public MovimientoModel MovimientoModel
        {
            get
            {
                return _movimientoModel;
                
            }
            set
            {
                _movimientoModel = value;
            }
        }
        public CatalogItemModel ItemModel
        {
            get
            {
                return _itemModel;

            }
            set
            {
                _itemModel = value;
            }
        }

        public CatalogSolicitante1Model CatalogSolicitanteModel
        {
            get
            {
                return _catalogSolicitanteModel;
            }
            set
            {
                _catalogSolicitanteModel = value;
            }
        }


        public void loadItems()
        {
            this._catalogSolicitanteModel.loadItems();
        }

        public CatalogItemViewModel CreateCatalogItemViewModel()
        {
            return new CatalogItemViewModel(this); 
        }

    }
}
