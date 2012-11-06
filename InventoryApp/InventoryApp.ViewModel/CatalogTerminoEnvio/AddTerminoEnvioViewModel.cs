using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTerminoEnvio
{
    public class AddTerminoEnvioViewModel
    {
        #region Fields
        private TerminoEnvioModel _terminoEnvio;
        private RelayCommand _addTerminoEnvioCommand;
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

        public ICommand AddTerminoEnvioCommand
        {
            get 
            {
                if (_addTerminoEnvioCommand == null)
                {
                    _addTerminoEnvioCommand = new RelayCommand(p => this.AttempAddTerminoEnvio(), p => this.CanAttempAddTerminoEnvio());
                }
                return _addTerminoEnvioCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddTerminoEnvioViewModel(CatalogTerminoEnvioViewModel catalogTerminoEnvioViewModel)
        {
            this._terminoEnvio = new TerminoEnvioModel(new TerminoEnvioDataMapper());
            this._catalogTerminoEnvioViewModel = catalogTerminoEnvioViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddTerminoEnvio()
        {
            bool _canAddTerminoEnvio = true;
            if (String.IsNullOrEmpty(this._terminoEnvio.Termino))
                _canAddTerminoEnvio = false;

            return _canAddTerminoEnvio;
        }

        public void AttempAddTerminoEnvio()
        {
            this._terminoEnvio.saveTerminoEnvio();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTerminoEnvioViewModel != null)
            {
                this._catalogTerminoEnvioViewModel.loadItems();
            }
        }
        #endregion
    }
}
