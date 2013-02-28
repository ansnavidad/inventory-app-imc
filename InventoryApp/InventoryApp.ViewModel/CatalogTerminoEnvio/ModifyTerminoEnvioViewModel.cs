using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogTerminoEnvio
{
    public class ModifyTerminoEnvioViewModel
    {
        #region Fields
        private TerminoEnvioModel _terminoEnvio;
        private RelayCommand _modifyTerminoEnvioCommand;
        private CatalogTerminoEnvioViewModel _catalogTerminoEnvioViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public TerminoEnvioModel TerminoEnvio 
        {
            get
            {
                return _terminoEnvio;
            }
            set
            {
                _terminoEnvio = value;
            }
        }

        public ICommand ModifyTerminoEnvioCommand
        {
            get 
            {
                if (_modifyTerminoEnvioCommand == null)
                {
                    _modifyTerminoEnvioCommand = new RelayCommand(p => this.AttempModifyTerminoEnvio(), p => this.CanAttempModifyTerminoEnvio());
                }
                return _modifyTerminoEnvioCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyTerminoEnvioViewModel(CatalogTerminoEnvioViewModel catalogTerminoEnvioViewModel, TerminoEnvioModel selectedTerminoEnvioModel)
        {
            this._terminoEnvio = new TerminoEnvioModel(new TerminoEnvioDataMapper());
            this._catalogTerminoEnvioViewModel = catalogTerminoEnvioViewModel;

            this._terminoEnvio.UnidTerminoEnvio = selectedTerminoEnvioModel.UnidTerminoEnvio;
            this._terminoEnvio.Termino = selectedTerminoEnvioModel.Termino;
            this._terminoEnvio.Significado = selectedTerminoEnvioModel.Significado;
            this._terminoEnvio.GeneraLotes = selectedTerminoEnvioModel.GeneraLotes;
            this._terminoEnvio.Clave = selectedTerminoEnvioModel.Clave;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyTerminoEnvio()
        {
            bool _canAddTerminoEnvio = true;
            if (String.IsNullOrEmpty(this._terminoEnvio.Termino) || String.IsNullOrEmpty(this._terminoEnvio.Clave) || String.IsNullOrEmpty(this._terminoEnvio.Significado))
                _canAddTerminoEnvio = false;

            return _canAddTerminoEnvio;
        }

        public void AttempModifyTerminoEnvio()
        {
            this._terminoEnvio.updateTerminoEnvio();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTerminoEnvioViewModel != null)
            {
                this._catalogTerminoEnvioViewModel.loadItems();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.TerminoEnvio);
            return historialViewModel;
        }

        #endregion
    }
}
