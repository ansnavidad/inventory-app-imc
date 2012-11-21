using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogAlmacenEmail
{
    public class AddAlmacenEmailViewModel
    {
        #region Fields
        private AlmacenEmailModel _addAlmacenEmail;
        private RelayCommand _addAlmacenEmailCommand;
        private CatalogAlmacenEmailViewModel _catalogAlmacenEmailViewModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        #endregion

        //Exponer la propiedad almacen cuidad
        #region Props
        public AlmacenEmailModel AddAlmacenEmail
        {
            get
            {
                return _addAlmacenEmail;
            }
            set
            {
                _addAlmacenEmail = value;
            }
        }
        public CatalogAlmacenModel CatalogAlmacenModel
        {
            get
            {
                return _catalogAlmacenModel;
            }
            set
            {
                _catalogAlmacenModel = value;
            }
        }
        
        public ICommand AddAlmacenEmailCommand
        {
            get
            {
                if (_addAlmacenEmailCommand == null)
                {
                    _addAlmacenEmailCommand = new RelayCommand(p => this.AttempAddAlmacenEmail(), p => this.CanAttempAddAlmacenEmail());
                }
                return _addAlmacenEmailCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddAlmacenEmailViewModel(CatalogAlmacenEmailViewModel catalogAlmacenEmailViewModel)
        {
            this._addAlmacenEmail = new AlmacenEmailModel(new AlmacenEmailDataMapper());
            this._catalogAlmacenEmailViewModel = catalogAlmacenEmailViewModel;
            try
            {

                this._catalogAlmacenModel = new CatalogAlmacenModel(new AlmacenDataMapper());
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
        public bool CanAttempAddAlmacenEmail()
        {
            bool _canAddAlmacenEmail = true;
            if (String.IsNullOrEmpty(this._addAlmacenEmail.Email) || this._addAlmacenEmail.Almacen == null)
                _canAddAlmacenEmail = false;
            return _canAddAlmacenEmail;
        }

        public void AttempAddAlmacenEmail()
        {
            this._addAlmacenEmail.saveAlmacenEmail();

            if (this._catalogAlmacenEmailViewModel != null)
            {
                this._catalogAlmacenEmailViewModel.loadAlmacenEmail();
            }
        }
        #endregion
    }
}
