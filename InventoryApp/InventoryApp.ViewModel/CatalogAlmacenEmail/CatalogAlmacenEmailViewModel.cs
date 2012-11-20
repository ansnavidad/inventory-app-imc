using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogAlmacenEmail
{
    public class CatalogAlmacenEmailViewModel : IPageViewModel
    {
        private RelayCommand _deleteAlmacenEmailCommand;
        private CatalogAlmacenEmailModel _catalogAlmacenEmailModel;

        public ICommand DeleteAlmacenEmailCommand
        {
            get
            {
                if (_deleteAlmacenEmailCommand == null)
                {
                    _deleteAlmacenEmailCommand = new RelayCommand(p => this.AttempDeleteAlmacenEmail(), p => this.CanAttempDeleteAlmacenEmail());
                }
                return _deleteAlmacenEmailCommand;
            }
        }
        public CatalogAlmacenEmailViewModel()
        {
            try
            {
                IDataMapper dataMapper = new AlmacenEmailDataMapper();
                this._catalogAlmacenEmailModel = new CatalogAlmacenEmailModel(dataMapper);
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            
        }

        public CatalogAlmacenEmailModel CatalogAlmacenEmailModel
        {
            get
            {
                return _catalogAlmacenEmailModel;
            }
            set
            {
                _catalogAlmacenEmailModel = value;
            }
        }

        public void loadAlmacenEmail()
        {
            this._catalogAlmacenEmailModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de de AddAlmacenEmailViewModel y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddAlmacenEmailViewModel CreateAddAlmacenEmailViewModel()
        {
            return new AddAlmacenEmailViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyAlmacenEmailViewModel y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyAlmacenEmailViewModel CreateModifyAlmacenEmailViewModel()
        {
            AlmacenEmailModel almacenEmailModel = new AlmacenEmailModel(new AlmacenEmailDataMapper());
            if (this._catalogAlmacenEmailModel != null && this._catalogAlmacenEmailModel.SelectedAlmacenEmail != null)
            {
                almacenEmailModel.UnidAlmacenEmail = this._catalogAlmacenEmailModel.SelectedAlmacenEmail.UNID_ALMACEN_EMAIL;
                almacenEmailModel.Email = this._catalogAlmacenEmailModel.SelectedAlmacenEmail.EMAIL;
                almacenEmailModel.IsDefault = this._catalogAlmacenEmailModel.SelectedAlmacenEmail.IS_DEFAULT;
                almacenEmailModel.Almacen = this._catalogAlmacenEmailModel.SelectedAlmacenEmail.Almacen;
            }
            return new ModifyAlmacenEmailViewModel(this, almacenEmailModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteAlmacenEmail()
        {
            bool _canDeleteAlmacenEmail = false;
            foreach (DeleteAlmacenEmail d in this._catalogAlmacenEmailModel.AlmacenEmail)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteAlmacenEmail = true;
                }
            }

            return _canDeleteAlmacenEmail;
        }

        public void AttempDeleteAlmacenEmail()
        {
            this._catalogAlmacenEmailModel.deleteAlamacenEmail();

            if (this._catalogAlmacenEmailModel != null)
            {
                this._catalogAlmacenEmailModel.loadItems();
            }
        }
        #endregion

        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
