using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogCiudad
{
    public class CatalogCiudadViewModel
    {
        private CatalogCiudadModel _catalogCiudadModel;

        public CatalogCiudadViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new CiudadDataMapper();
                this._catalogCiudadModel = new CatalogCiudadModel(dataMapper);   
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
        public CatalogCiudadModel CatalogCiudadModel
        {
            get
            {
                return _catalogCiudadModel;
            }
            set
            {
                _catalogCiudadModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogCiudadModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddCiudadViewModel CreateAddCiudadViewModel()
        {
            return new AddCiudadViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyCiudadViewModel CreateModifyCiudadViewModel()
        {
            CiudadModel ciudadModel = new CiudadModel(new CiudadDataMapper());
            if (this._catalogCiudadModel != null && this._catalogCiudadModel.SelectedCiudad != null)
            {
                ciudadModel.Ciudad = this._catalogCiudadModel.SelectedCiudad.CIUDAD1;
                ciudadModel.Iso = this._catalogCiudadModel.SelectedCiudad.ISO;
                ciudadModel.UnidCiudad = this._catalogCiudadModel.SelectedCiudad.UNID_CIUDAD;        
            }
            return new ModifyCiudadViewModel(this, ciudadModel);
        }
    }
}
