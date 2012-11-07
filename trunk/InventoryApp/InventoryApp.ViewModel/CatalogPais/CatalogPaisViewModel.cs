using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogPais
{
    public class CatalogPaisViewModel
    {
        private CatalogPaisModel _catalogPaisModel;

        public CatalogPaisViewModel()
        {            
            try
            {
                IDataMapper dataMapper = new CiudadDataMapper();
                this._catalogPaisModel = new CatalogPaisModel(dataMapper);   
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
        public CatalogPaisModel CatalogPaisModel
        {
            get
            {
                return _catalogPaisModel;
            }
            set
            {
                _catalogPaisModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogPaisModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddPaisViewModel CreateAddPaisViewModel()
        {
            return new AddPaisViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyPaisViewModel CreateModifyPaisViewModel()
        {
            PaisModel paisModel = new PaisModel(new PaisDataMapper());
            if (this._catalogPaisModel != null && this._catalogPaisModel.SelectedPais != null)
            {
                paisModel.UnidPais = this._catalogPaisModel.SelectedPais.UNID_PAIS;
                paisModel.Pais = this._catalogPaisModel.SelectedPais.PAIS;
                paisModel.Iso = this._catalogPaisModel.SelectedPais.ISO;
            }
            return new ModifyPaisViewModel(this, paisModel);
        }
    }
}
