using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogMarca
{
    public class CatalogMarcaViewModel
    {
        private CatalogMarcaModel _catalogMarcaModel;

        public CatalogMarcaViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new MarcaDataMapper();
                this._catalogMarcaModel = new CatalogMarcaModel(dataMapper);   
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
        public CatalogMarcaModel CatalogMarcaModel
        {
            get
            {
                return _catalogMarcaModel;
            }
            set
            {
                _catalogMarcaModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogMarcaModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddMarcaViewModel CreateAddMarcaViewModel()
        {
            return new AddMarcaViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyMarcaViewModel CreateModifyMarcaViewModel()
        {
            MarcaModel marcaModel=new MarcaModel(new MarcaDataMapper());
            if (this._catalogMarcaModel != null && this._catalogMarcaModel.SelectedMarca != null)
            {
                marcaModel.MarcaName = this._catalogMarcaModel.SelectedMarca.MARCA_NAME;
                marcaModel.UnidMarca = this._catalogMarcaModel.SelectedMarca.UNID_MARCA;
            }
            return new ModifyMarcaViewModel(this, marcaModel);
        }
    }
}
