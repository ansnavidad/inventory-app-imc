using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogServicio
{
    public class ModifyServicioViewModel
    {
        #region Fields
        private ServicioModel _modiServicio;
        private RelayCommand _modifyServicioCommand;
        private CatalogServicioViewModel _catalogServicioViewModel;
        #endregion

         //Exponer la propiedad servicio
        #region Props
        public ServicioModel ModiServicio 
        {
            get
            {
                return _modiServicio;
            }
            set
            {
                _modiServicio = value;
            }
        }

        public ICommand ModifyServicioCommand
        {
            get 
            {
                if (_modifyServicioCommand == null)
                {
                    _modifyServicioCommand = new RelayCommand(p => this.AttempModifyServicio(), p => this.CanAttempModifyServicio());
                }
                return _modifyServicioCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyServicioViewModel(CatalogServicioViewModel catalogServicioViewModel, ServicioModel selectedServicioModel)
        {
            this._modiServicio = new ServicioModel(new ServicioDataMapper());
            this._catalogServicioViewModel = catalogServicioViewModel;
            this._modiServicio.UnidServicio = selectedServicioModel.UnidServicio;
            this._modiServicio.ServicioName = selectedServicioModel.ServicioName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyServicio()
        {
            bool _canAddServicio = true;
            if (String.IsNullOrEmpty(this._modiServicio.ServicioName))
                _canAddServicio = false;

            return _canAddServicio;
        }

        public void AttempModifyServicio()
        {
            this._modiServicio.updateServicio();

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
