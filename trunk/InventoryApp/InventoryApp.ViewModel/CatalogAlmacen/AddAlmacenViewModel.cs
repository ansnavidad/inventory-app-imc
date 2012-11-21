using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogAlmacen
{
    public class AddAlmacenViewModel
    {
        #region Fields
        private AlmacenModel _addAlmacen;
        private RelayCommand _addAlmacenCommand;
        private CatalogAlmacenViewModel _catalogAlmacenViewModel;
        private CatalogCiudadModel _catalogCiudadModel;
        #endregion

        //Exponer la propiedad almacen cuidad
        #region Props
        public AlmacenModel AddAlmacen
        {
            get
            {
                return _addAlmacen;
            }
            set
            {
                _addAlmacen = value;
            }
        }
        public CatalogCiudadModel CatalogCiudadModel
        {
            get
            {
                return _catalogCiudadModel;
            }
            set
            {
                _catalogCiudadModel = value;
            }
        }
        
        public ICommand AddAlmacenCommand
        {
            get
            {
                if (_addAlmacenCommand == null)
                {
                    _addAlmacenCommand = new RelayCommand(p => this.AttempAddAlmacen(), p => this.CanAttempAddAlmacen());
                }
                return _addAlmacenCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddAlmacenViewModel(CatalogAlmacenViewModel catalogAlmacenViewModel)
        {
            this._addAlmacen = new AlmacenModel(new AlmacenDataMapper());
            this._catalogAlmacenViewModel = catalogAlmacenViewModel;
            try
            {

                this._catalogCiudadModel = new CatalogCiudadModel(new CiudadDataMapper());
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddAlmacen()
        {
            bool _canAddAlmacen = true;
            if (String.IsNullOrEmpty(this._addAlmacen.AlmacenName) ||
                String.IsNullOrEmpty(this._addAlmacen.Contacto) ||
                String.IsNullOrEmpty(this._addAlmacen.Direccion)||
                String.IsNullOrEmpty(this._addAlmacen.Tecnico) || this._addAlmacen.Ciudad == null)
                _canAddAlmacen = false;
            return _canAddAlmacen;
        }

        public void AttempAddAlmacen()
        {
            this._addAlmacen.saveAlmacen();

            if (this._catalogAlmacenViewModel != null)
            {
                this._catalogAlmacenViewModel.loadAlmacen();
            }
        }
        #endregion
    }
}
