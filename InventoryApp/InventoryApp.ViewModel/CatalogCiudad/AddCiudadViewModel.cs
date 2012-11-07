using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogCiudad
{
    public class AddCiudadViewModel
    {
         #region Fields
        private CiudadModel _ciudadModel;
        private RelayCommand _addCiudadCommand;
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

        public ICommand AddCiudadCommand
        {
            get 
            {
                if (_addCiudadCommand == null)
                {
                    _addCiudadCommand = new RelayCommand(p => this.AttempAddCiudad(), p => this.CanAttempAddCiudad());
                }
                return _addCiudadCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddCiudadViewModel(CatalogCiudadViewModel catalogCiudadViewModel)
        {
            this._ciudadModel = new CiudadModel(new CiudadDataMapper());
            this._catalogCiudadViewModel = catalogCiudadViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddCiudad()
        {
            bool _canAddCiudad = true;
            if (String.IsNullOrEmpty(this._ciudadModel.Ciudad) || String.IsNullOrEmpty(this._ciudadModel.Iso))
                _canAddCiudad = false;

            return _canAddCiudad;
        }

        public void AttempAddCiudad()
        {
            this._ciudadModel.saveCiudad();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogCiudadViewModel != null)
            {
                this._catalogCiudadViewModel.loadItems();
            }
        }
        #endregion
    }
}
