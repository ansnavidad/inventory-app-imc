using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogEquipo
{
    public class CatalogEquipoViewModel
    {
        private CatalogEquipoModel _catalogEquipoModel;

        public CatalogEquipoViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new EquipoDataMapper();
                this._catalogEquipoModel = new CatalogEquipoModel(dataMapper);   
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
        public CatalogEquipoModel CatalogEquipoModel
        {
            get
            {
                return _catalogEquipoModel;
            }
            set
            {
                _catalogEquipoModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogEquipoModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddEquipoViewModel CreateAddEquipoViewModel()
        {
            return new AddEquipoViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyEquipoViewModel CreateModifyEquipoViewModel()
        {
            EquipoModel equipoModel=new EquipoModel(new EquipoDataMapper());
            if (this._catalogEquipoModel != null && this._catalogEquipoModel.SelectedEquipo != null)
            {
                equipoModel.EquipoName = this._catalogEquipoModel.SelectedEquipo.EQUIPO_NAME;
                equipoModel.UnidEquipo = this._catalogEquipoModel.SelectedEquipo.UNID_EQUIPO;
            }
            return new ModifyEquipoViewModel(this, equipoModel);
        }
    }
}
