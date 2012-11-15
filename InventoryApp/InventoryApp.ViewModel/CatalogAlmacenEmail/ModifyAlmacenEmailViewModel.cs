using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogAlmacenEmail
{
    public class ModifyAlmacenEmailViewModel
    {
        #region Fields
        private AlmacenEmailModel _modiAlmacenEmail;
        private RelayCommand _modifyAlmacenEmailCommand;
        private CatalogAlmacenEmailViewModel _catalogAlmacenEmailViewModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        #endregion

        //Exponer la propiedad almacenEmail y almacen
        #region Props
        public AlmacenEmailModel ModiAlmacenEmail
        {
            get
            {
                return _modiAlmacenEmail;
            }
            set
            {
                _modiAlmacenEmail = value;
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
        
        public ICommand ModifyAlmacenEmailCommand
        {
            get
            {
                if (_modifyAlmacenEmailCommand == null)
                {
                    _modifyAlmacenEmailCommand = new RelayCommand(p => this.AttempModifyAlmacenEmail(), p => this.CanAttempModifyAlmacenEmail());
                }
                return _modifyAlmacenEmailCommand;
            }
        }
        #endregion

         #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyAlmacenEmailViewModel(CatalogAlmacenEmailViewModel catalogAlmacenEmailViewModel, AlmacenEmailModel selectedAlmacenEmailModel)
        {
            this._modiAlmacenEmail = new AlmacenEmailModel(new AlmacenEmailDataMapper());
            this._catalogAlmacenEmailViewModel = catalogAlmacenEmailViewModel;
            this._modiAlmacenEmail.UnidAlmacenEmail = selectedAlmacenEmailModel.UnidAlmacenEmail;
            this._modiAlmacenEmail.Email = selectedAlmacenEmailModel.Email;
            this._modiAlmacenEmail.IsDefault = selectedAlmacenEmailModel.IsDefault;
            this._modiAlmacenEmail.Almacen = selectedAlmacenEmailModel.Almacen;
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
        public bool CanAttempModifyAlmacenEmail()
        {
            bool _canAddAlmacenEmail = true;
            if (String.IsNullOrEmpty(this._modiAlmacenEmail.Email))
                _canAddAlmacenEmail = false;
            return _canAddAlmacenEmail;
        }

        public void AttempModifyAlmacenEmail()
        {
            this._modiAlmacenEmail.updateAlmacenEmail();

            if (this._catalogAlmacenEmailViewModel != null)
            {
                this._catalogAlmacenEmailViewModel.loadAlmacenEmail();
            }
        }
        #endregion
    }
}
