using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTerminoEnvio
{
    public class CatalogTerminoEnvioViewModel
    {
        private CatalogTerminoEnvioModel _catalogTerminoEnvioModel;

        public CatalogTerminoEnvioViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new TerminoEnvioDataMapper();
                this._catalogTerminoEnvioModel = new CatalogTerminoEnvioModel(dataMapper);   
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
        public CatalogTerminoEnvioModel CatalogTerminoEnvioModel
        {
            get
            {
                return _catalogTerminoEnvioModel;
            }
            set
            {
                _catalogTerminoEnvioModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogTerminoEnvioModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddTerminoEnvioViewModel CreateAddTerminoEnvioViewModel()
        {
            return new AddTerminoEnvioViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyTerminoEnvioViewModel CreateModifyTerminoEnvioViewModel()
        {
            TerminoEnvioModel terminoEnvioModel = new TerminoEnvioModel(new TerminoEnvioDataMapper());
            if (this._catalogTerminoEnvioModel != null && this._catalogTerminoEnvioModel.SelectedTerminoEnvio != null)
            {
                terminoEnvioModel.Clave = this._catalogTerminoEnvioModel.SelectedTerminoEnvio.CLAVE;
                terminoEnvioModel.GeneraLotes = this._catalogTerminoEnvioModel.SelectedTerminoEnvio.GENERA_LOTES;
                terminoEnvioModel.Significado = this._catalogTerminoEnvioModel.SelectedTerminoEnvio.SIGNIFICADO;
                terminoEnvioModel.Termino = this._catalogTerminoEnvioModel.SelectedTerminoEnvio.TERMINO;
                terminoEnvioModel.UnidTerminoEnvio = this._catalogTerminoEnvioModel.SelectedTerminoEnvio.UNID_TERMINO_ENVIO;                
            }
            return new ModifyTerminoEnvioViewModel(this, terminoEnvioModel);
        }
    }
}
