using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogMoneda
{
    public class CatalogMonedaViewModel
    {
        private CatalogMonedaModel _catalogMonedaModel;

        public CatalogMonedaViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new MonedaDataMapper();
                this._catalogMonedaModel = new CatalogMonedaModel(dataMapper);   
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
        public CatalogMonedaModel CatalogMonedaModel
        {
            get
            {
                return _catalogMonedaModel;
            }
            set
            {
                _catalogMonedaModel = value;
            }
        }

        public void loadMonedas()
        {
            this._catalogMonedaModel.loadMonedas();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddMonedaViewModel CreateAddMonedaViewModel()
        {
            return new AddMonedaViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyMonedaViewModel CreateModifyMonedaViewModel()
        {
            MonedaModel monedaModel = new MonedaModel(new MonedaDataMapper());
            if (this._catalogMonedaModel != null && this._catalogMonedaModel.SelectedMoneda != null)
            {
                monedaModel.UnidMoneda = this._catalogMonedaModel.SelectedMoneda.UNID_MONEDA;
                monedaModel.MonedaName = this._catalogMonedaModel.SelectedMoneda.MONEDA_NAME;
                monedaModel.MonedaAbr = this._catalogMonedaModel.SelectedMoneda.MONEDA_ABR;

            }
            return new ModifyMonedaViewModel(this,monedaModel);
        }
    }
}
