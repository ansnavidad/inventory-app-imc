using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogMedioEnvio
{
    public class ModifyMedioEnvioViewModel
    {
        #region Fields
        private MedioEnvioModel _medioEnvio;
        private RelayCommand _modifyMedioEnvioCommand;
        private CatalogMedioEnvioViewModel _catalogMedioEnvioViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public MedioEnvioModel MedioEnvio 
        {
            get
            {
                return _medioEnvio;
            }
            set
            {
                _medioEnvio = value;
            }
        }

        public ICommand ModifyMedioEnvioCommand
        {
            get 
            {
                if (_modifyMedioEnvioCommand == null)
                {
                    _modifyMedioEnvioCommand = new RelayCommand(p => this.AttempModifyMedioEnvio(), p => this.CanAttempModifyMedioEnvio());
                }
                return _modifyMedioEnvioCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyMedioEnvioViewModel(CatalogMedioEnvioViewModel catalogMedioEnvioViewModel, MedioEnvioModel selectedMedioEnvioModel)
        {
            this._medioEnvio = new MedioEnvioModel(new MedioEnvioDataMapper());
            this._catalogMedioEnvioViewModel = catalogMedioEnvioViewModel;
            this._medioEnvio.UnidMedioEnvio = selectedMedioEnvioModel.UnidMedioEnvio;
            this._medioEnvio.MedioEnvioName = selectedMedioEnvioModel.MedioEnvioName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyMedioEnvio()
        {
            bool _canAddMedioEnvio = true;
            if (String.IsNullOrEmpty(this._medioEnvio.MedioEnvioName))
                _canAddMedioEnvio = false;

            return _canAddMedioEnvio;
        }

        public void AttempModifyMedioEnvio()
        {
            this._medioEnvio.updateMedioEnvio();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogMedioEnvioViewModel != null)
            {
                this._catalogMedioEnvioViewModel.loadItems();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.MedioEnvio);
            return historialViewModel;
        }
        #endregion
    }
}
