﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogCategoria
{
    public class CatalogCategoriaViewModel
    {
        private CatalogCategoriaModel _catalogCategoriaModel;

        public CatalogCategoriaViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new CategoriaDataMapper();
                this._catalogCategoriaModel = new CatalogCategoriaModel(dataMapper);   
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
        public CatalogCategoriaModel CatalogCategoriaModel
        {
            get
            {
                return _catalogCategoriaModel;
            }
            set
            {
                _catalogCategoriaModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogCategoriaModel.loadItems();
        }
        /// <summary>
        /// Crea una nueva instancia de AddCategoria y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddCategoriaViewModel CreateAddCategoriaViewModel()
        {
            return new AddCategoriaViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyCategoria y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyCategoriaViewModel CreateModifyCategoriaViewModel()
        {
            CategoriaModel categoriaModel = new CategoriaModel(new CategoriaDataMapper());
            if (this._catalogCategoriaModel != null && this._catalogCategoriaModel.SelectedCategoria != null)
            {
                categoriaModel.CategoriaName = this._catalogCategoriaModel.SelectedCategoria.CATEGORIA_NAME;
                categoriaModel.UnidCategoria = this._catalogCategoriaModel.SelectedCategoria.UNID_CATEGORIA;
            }
            return new ModifyCategoriaViewModel(this, categoriaModel);
        }
    }
}
