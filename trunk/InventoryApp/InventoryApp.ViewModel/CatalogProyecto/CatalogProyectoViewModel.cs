using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogProyecto
{
    public class CatalogProyectoViewModel
    {
        private CatalogProyectoModel _catalogProyectoModel;

        public CatalogProyectoViewModel()
        {
            try
            {
                IDataMapper dataMapper = new ProyectoDataMapper();
                this._catalogProyectoModel = new CatalogProyectoModel(dataMapper);
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            
        }
        public CatalogProyectoModel CatalogProyectoModel
        {
            get
            {
                return _catalogProyectoModel;
            }
            set
            {
                _catalogProyectoModel = value;
            }
        }
   
        public void loadProyecto()
        {
            this._catalogProyectoModel.loadItems();
        }
        /// <summary>
        /// Crea una nueva instancia de de AddProyectoViewModel y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddProyectoViewModel CreateAddProyectoViewModel()
        {
            return new AddProyectoViewModel(this);
        }
        /// <summary>
        /// Crea una nueva instancia de ModifyProyecto y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyProyectoViewModel CreateModifyProyectoViewModel()
        {
            ProyectoModel proyectoModel = new ProyectoModel(new ProyectoDataMapper());
            if (this._catalogProyectoModel != null && this._catalogProyectoModel.SelectedProyecto != null)
            {
                proyectoModel.UnidProyecto = this._catalogProyectoModel.SelectedProyecto.UNID_PROYECTO;
                proyectoModel.ProyectoName = this._catalogProyectoModel.SelectedProyecto.PROYECTO_NAME;
            }
            return new ModifyProyectoViewModel(this, proyectoModel);
        }
    }
}
