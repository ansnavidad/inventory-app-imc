using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;


namespace InventoryApp.ViewModel.CatalogModelo
{
    public class CatalogModeloViewModel
    {
         private CatalogModeloModel _catalogModeloModel;

        public CatalogModeloViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new ModeloDataMapper();
                this._catalogModeloModel = new CatalogModeloModel(dataMapper);   
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
        public CatalogModeloModel CatalogModeloModel
        {
            get
            {
                return _catalogModeloModel;
            }
            set
            {
                _catalogModeloModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogModeloModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddModeloViewModel CreateAddModeloViewModel()
        {
            return new AddModeloViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyModeloViewModel CreateModifyModeloViewModel()
        {
            ModeloModel modeloModel=new ModeloModel(new ModeloDataMapper());
            if (this._catalogModeloModel != null && this._catalogModeloModel.SelectedModelo != null)
            {
                modeloModel.ModeloName = this._catalogModeloModel.SelectedModelo.MODELO_NAME;
                modeloModel.UnidModelo = this._catalogModeloModel.SelectedModelo.UNID_MODELO;
            }
            return new ModifyModeloViewModel(this, modeloModel);
        }
    
    }
}
