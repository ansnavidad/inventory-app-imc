using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogTerminoEnvio
{
    public class CatalogTerminoEnvioViewModel : IPageViewModel
    {
        private RelayCommand _deleteTerminoEnvioCommand;
        private CatalogTerminoEnvioModel _catalogTerminoEnvioModel;
        public USUARIO ActualUser;

        public ICommand DeleteTerminoEnvioCommand
        {
            get
            {
                if (_deleteTerminoEnvioCommand == null)
                {
                    _deleteTerminoEnvioCommand = new RelayCommand(p => this.AttempDeleteTerminoEnvio(), p => this.CanAttempDeleteTerminoEnvio());
                }
                return _deleteTerminoEnvioCommand;
            }
        }

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

        public CatalogTerminoEnvioViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new TerminoEnvioDataMapper();
                this._catalogTerminoEnvioModel = new CatalogTerminoEnvioModel(dataMapper);
                this.ActualUser = u;
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

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteTerminoEnvio()
        {
            bool _canDeleteTerminoEnvio = false;
            foreach (DeleteTerminoEnvio d in this._catalogTerminoEnvioModel.TerminoEnvio)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteTerminoEnvio = true;
                }
            }

            return _canDeleteTerminoEnvio;
        }

        public void AttempDeleteTerminoEnvio()
        {
            this._catalogTerminoEnvioModel.deleteTerminoEnvio();

            if (this._catalogTerminoEnvioModel != null)
            {
                this._catalogTerminoEnvioModel.loadItems();
            }
        }
        #endregion

        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
