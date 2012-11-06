using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogMedioEnvio
{
    public class AddMedioEnvioViewModel
    {
        #region Fields
        private MedioEnvioModel _medioEnvio;
        private RelayCommand _addMedioEnvioCommand;
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

        public ICommand AddMedioEnvioCommand
        {
            get 
            {
                if (_addMedioEnvioCommand == null)
                {
                    _addMedioEnvioCommand = new RelayCommand(p => this.AttempAddMedioEnvio(), p => this.CanAttempAddMedioEnvio());
                }
                return _addMedioEnvioCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddMedioEnvioViewModel(CatalogMedioEnvioViewModel catalogMedioEnvioViewModel)
        {
            this._medioEnvio = new MedioEnvioModel(new MedioEnvioDataMapper());
            this._catalogMedioEnvioViewModel = catalogMedioEnvioViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddMedioEnvio()
        {
            bool _canAddMedioEnvio = true;
            if (String.IsNullOrEmpty(this._medioEnvio.MedioEnvioName))
                _canAddMedioEnvio = false;

            return _canAddMedioEnvio;
        }

        public void AttempAddMedioEnvio()
        {
            this._medioEnvio.saveMedioEnvio();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogMedioEnvioViewModel != null)
            {
                this._catalogMedioEnvioViewModel.loadItems();
            }
        }
        #endregion
    }
}
