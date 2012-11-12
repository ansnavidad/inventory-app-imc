using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.Entradas
{
    public class EntradaPorValidacionViewModel
    {
        private MovimientoModel _movimientoModel;
        private CatalogSolicitante1Model _catalogSolicitanteModel;

        public EntradaPorValidacionViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new Solicitante1DataMapper();
                this._catalogSolicitanteModel = new CatalogSolicitante1Model(dataMapper);
                this._movimientoModel = new MovimientoModel(new MovimientoDataMapper());
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
            CatalogItemViewModel p;
            try
            {
                p = new CatalogItemViewModel(this);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            return p;
        }

    }
}
