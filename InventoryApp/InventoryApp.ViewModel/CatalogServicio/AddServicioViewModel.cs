using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogServicio
{
    public class AddServicioViewModel
    {
        #region Fields
        private ServicioModel _addServicio;
        private RelayCommand _addServicioCommand;
        private CatalogServicioViewModel _catalogServicioViewModel;
        #endregion

        //Exponer la propiedad servicio
        #region Props
        public ServicioModel AddServicio 
        {
            get
            {
                return _addServicio;
            }
            set
            {
                _addServicio = value;
            }
        }

        public ICommand AddServicioCommand
        {
            get 
            {
                if (_addServicioCommand == null)
                {
                    _addServicioCommand = new RelayCommand(p => this.AttempAddServicio(), p => this.CanAttempAddServicio());
                }
                return _addServicioCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddServicioViewModel(CatalogServicioViewModel catalogServicioViewModel)
        {
            this._addServicio = new ServicioModel(new ServicioDataMapper());
            this._catalogServicioViewModel = catalogServicioViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddServicio()
        {
            bool _canAddServicio = true;
            if (String.IsNullOrEmpty(this._addServicio.ServicioName))
                _canAddServicio = false;

            return _canAddServicio;
        }

        public void AttempAddServicio()
        {
            this._addServicio.saveServicio();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogServicioViewModel != null)
            {
                this._catalogServicioViewModel.loadItems();
            }
        }
        #endregion
    }
}
