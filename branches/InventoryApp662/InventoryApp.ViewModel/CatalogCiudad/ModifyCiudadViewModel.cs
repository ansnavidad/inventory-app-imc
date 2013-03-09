using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogCiudad
{
    public class ModifyCiudadViewModel
    {
        #region Fields
        private CiudadModel _ciudadModel;
        private RelayCommand _modifyCiudadCommand;
        private CatalogCiudadViewModel _catalogCiudadViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public CiudadModel CiudadModel 
        {
            get
            {
                return _ciudadModel;
            }
            set
            {
                _ciudadModel = value;
            }
        }

        public ICommand ModifyCiudadCommand
        {
            get 
            {
                if (_modifyCiudadCommand == null)
                {
                    _modifyCiudadCommand = new RelayCommand(p => this.AttempModifyCiudad(), p => this.CanAttempModifyCiudad());
                }
                return _modifyCiudadCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyCiudadViewModel(CatalogCiudadViewModel catalogCiudadViewModel,CiudadModel selectedCiudadModel)
        {
            this._ciudadModel = new CiudadModel(new CiudadDataMapper(), catalogCiudadViewModel.ActualUser);
            this._catalogCiudadViewModel = catalogCiudadViewModel;
            this._ciudadModel.Ciudad = selectedCiudadModel.Ciudad;
            this._ciudadModel.Iso = selectedCiudadModel.Iso;
            this._ciudadModel.UnidCiudad = selectedCiudadModel.UnidCiudad;
            this._ciudadModel.UnidPais = selectedCiudadModel.UnidPais;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyCiudad()
        {
            bool _canAddCiudad = true;
            if (String.IsNullOrEmpty(this._ciudadModel.Ciudad) || String.IsNullOrEmpty(this._ciudadModel.Iso))
                _canAddCiudad = false;

            return _canAddCiudad;
        }

        public void AttempModifyCiudad()
        {
            this._ciudadModel.updateCiudad();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogCiudadViewModel != null)
            {
                this._catalogCiudadViewModel.loadItems();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.CiudadModel);
            return historialViewModel;
        }
        #endregion
    }
}
