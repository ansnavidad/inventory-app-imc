using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogPropiedad
{
    public class AddPropiedadViewModel
    {
        #region Fields
        private PropiedadModel _addPropiedad;
        private RelayCommand _addPropiedadCommand;
        private CatalogPropiedadViewModel _catalogPropiedadViewModel;
        #endregion

        //Exponer la propiedad de propiedad
        #region Props
        public PropiedadModel AddPropiedad 
        {
            get
            {
                return _addPropiedad;
            }
            set
            {
                _addPropiedad = value;
            }
        }

        public ICommand AddPropiedadCommand
        {
            get 
            {
                if (_addPropiedadCommand == null)
                {
                    _addPropiedadCommand = new RelayCommand(p => this.AttempAddPropiedad(), p => this.CanAttempAddPropiedad());
                }
                return _addPropiedadCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddPropiedadViewModel(CatalogPropiedadViewModel catalogPropiedadViewModel)
        {
            this._addPropiedad = new PropiedadModel(new PropiedadDataMapper(), catalogPropiedadViewModel.ActualUser);
            this._catalogPropiedadViewModel = catalogPropiedadViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddPropiedad()
        {
            bool _canAddPropiedad = true;
            if (String.IsNullOrEmpty(this._addPropiedad.PropiedadName))
                _canAddPropiedad = false;

            return _canAddPropiedad;
        }

        public void AttempAddPropiedad()
        {
            this._addPropiedad.savePropiedad();

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
