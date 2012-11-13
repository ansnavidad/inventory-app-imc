using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogPropiedad
{
    public class ModifyPropiedadViewModel
    {
        #region Fields
        private PropiedadModel _modiPropiedad;
        private RelayCommand _modifyPropiedadCommand;
        private CatalogPropiedadViewModel _catalogPropiedadViewModel;
        #endregion

        //Exponer la propiedad  de propiedad
        #region Props
        public PropiedadModel ModiPropiedad 
        {
            get
            {
                return _modiPropiedad;
            }
            set
            {
                _modiPropiedad = value;
            }
        }

        public ICommand ModifyPropiedadCommand
        {
            get 
            {
                if (_modifyPropiedadCommand == null)
                {
                    _modifyPropiedadCommand = new RelayCommand(p => this.AttempModifyPropiedad(), p => this.CanAttempModifyPropiedad());
                }
                return _modifyPropiedadCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyPropiedadViewModel(CatalogPropiedadViewModel catalogPropiedadViewModel, PropiedadModel selectedPropiedadModel)
        {
            this._modiPropiedad = new PropiedadModel(new PropiedadDataMapper());
            this._catalogPropiedadViewModel = catalogPropiedadViewModel;
            this._modiPropiedad.UnidPropiedad = selectedPropiedadModel.UnidPropiedad;
            this._modiPropiedad.PropiedadName = selectedPropiedadModel.PropiedadName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyPropiedad()
        {
            bool _canAddPropiedad = true;
            if (String.IsNullOrEmpty(this._modiPropiedad.PropiedadName))
                _canAddPropiedad = false;

            return _canAddPropiedad;
        }

        public void AttempModifyPropiedad()
        {
            this._modiPropiedad.updatePropiedad();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogPropiedadViewModel != null)
            {
                this._catalogPropiedadViewModel.loadItems();
            }
        }
        #endregion
    }
}
