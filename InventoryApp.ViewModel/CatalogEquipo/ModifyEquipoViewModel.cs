using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogEquipo
{
    public class ModifyEquipoViewModel
    {
        #region Fields
        private EquipoModel _equipo;
        private RelayCommand _modifyItemCommand;
        private CatalogEquipoViewModel _catalogEquipoViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public EquipoModel Equipo 
        {
            get
            {
                return _equipo;
            }
            set
            {
                _equipo = value;
            }
        }

        public ICommand ModifyItemCommand
        {
            get 
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyEquipo(), p => this.CanAttempModifyEquipo());
                }
                return _modifyItemCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyEquipoViewModel(CatalogEquipoViewModel catalogEquipoViewModel, EquipoModel selectedEquipoModel)
        {
            this._equipo = new EquipoModel(new EquipoDataMapper(), catalogEquipoViewModel.ActualUser);
            this._catalogEquipoViewModel = catalogEquipoViewModel;
            this._equipo.UnidEquipo = selectedEquipoModel.UnidEquipo;
            this._equipo.EquipoName = selectedEquipoModel.EquipoName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyEquipo()
        {
            bool _canAddEquipo = true;
            if (String.IsNullOrEmpty(this._equipo.EquipoName))
                _canAddEquipo = false;

            return _canAddEquipo;
        }

        public void AttempModifyEquipo()
        {
            this._equipo.updateEquipo();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogEquipoViewModel != null)
            {
                this._catalogEquipoViewModel.loadItems();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.Equipo);
            return historialViewModel;
        }
        #endregion
    }
}
