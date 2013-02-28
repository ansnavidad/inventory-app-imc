using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogProyecto
{
    public class ModifyProyectoViewModel
    {
        #region Fields
        private ProyectoModel _modiProyecto;
        private RelayCommand _modifyItemCommand;
        private CatalogProyectoViewModel _catalogProyectoViewModel;
        #endregion
        //Exponer la propiedad Proyecto
        #region Props
        public ProyectoModel ModiProyecto
        {
            get
            {
                return _modiProyecto;
            }
            set
            {
                _modiProyecto = value;
            }
        }

        public ICommand ModifyProyectoCommand
        {
            get
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyProyecto(), p => this.CanAttempModifyProyecto());
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
        public ModifyProyectoViewModel(CatalogProyectoViewModel catalogProyectoViewModel, ProyectoModel selectedItemProyectoModel)
        {
            this._modiProyecto = new ProyectoModel(new ProyectoDataMapper());
            this._catalogProyectoViewModel = catalogProyectoViewModel;
            this._modiProyecto.UnidProyecto = selectedItemProyectoModel.UnidProyecto;
            this._modiProyecto.ProyectoName = selectedItemProyectoModel.ProyectoName;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyProyecto()
        {
            bool _canAddProyecto = true;
            if (String.IsNullOrEmpty(this._modiProyecto.ProyectoName))
                _canAddProyecto = false;

            return _canAddProyecto;
        }

        public void AttempModifyProyecto()
        {
            this._modiProyecto.updateProyecto();

            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogProyectoViewModel != null)
            {
                this._catalogProyectoViewModel.loadProyecto();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.ModiProyecto);
            return historialViewModel;
        }

        #endregion
    }
}
