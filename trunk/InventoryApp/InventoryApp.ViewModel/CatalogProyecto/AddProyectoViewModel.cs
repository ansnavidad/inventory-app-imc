using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogProyecto
{
    public class AddProyectoViewModel
    {
        #region Fields
        private ProyectoModel _addproyecto;
        private RelayCommand _addProyectoCommand;
        private CatalogProyectoViewModel _catalogProyectoViewModel;
        #endregion

        //Exponer la propiedad proyecto
        #region Props
        public ProyectoModel AddProyecto
        {
            get
            {
                return _addproyecto;
            }
            set
            {
                _addproyecto = value;
            }
        }

        public ICommand AddProyectoCommand
        {
            get
            {
                if (_addProyectoCommand == null)
                {
                    _addProyectoCommand = new RelayCommand(p => this.AttempAddProyecto(), p => this.CanAttempAddProyecto());
                }
                return _addProyectoCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddProyectoViewModel(CatalogProyectoViewModel catalogProyectoViewModel)
        {
            this._addproyecto = new ProyectoModel(new ProyectoDataMapper());
            this._catalogProyectoViewModel = catalogProyectoViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddProyecto()
        {
            bool _canAddProyecto = true;
            if (String.IsNullOrEmpty(this._addproyecto.ProyectoName))
                _canAddProyecto = false;

            return _canAddProyecto;
        }

        public void AttempAddProyecto()
        {
            this._addproyecto.saveProyecto();

            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogProyectoViewModel != null)
            {
                this._catalogProyectoViewModel.loadProyecto();
            }
        }
        #endregion
    }
}
