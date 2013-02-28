using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogAlmacen
{
    public class CatalogAlmacenViewModel : IPageViewModel
    {
        private RelayCommand _deleteAlmacenCommand;
        private CatalogAlmacenModel _catalogAlmacenModel;
        public USUARIO ActualUser;

        public ICommand DeleteAlmacenCommand
        {
            get
            {
                if (_deleteAlmacenCommand == null)
                {
                    _deleteAlmacenCommand = new RelayCommand(p => this.AttempDeleteAlmacen(), p => this.CanAttempDeleteAlmacen());
                }
                return _deleteAlmacenCommand;
            }
        }
        public CatalogAlmacenViewModel()
        {
            try
            {
                IDataMapper dataMapper = new AlmacenDataMapper();
                this._catalogAlmacenModel = new CatalogAlmacenModel(dataMapper);
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

        public CatalogAlmacenViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new AlmacenDataMapper();
                this._catalogAlmacenModel = new CatalogAlmacenModel(dataMapper);
                this.ActualUser = u;
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

        public void loadAlmacen()
        {
            this._catalogAlmacenModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de de AddAlmacenViewModel y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddAlmacenViewModel CreateAddAlmacenViewModel()
        {
            return new AddAlmacenViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyAlmacenViewModel y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyAlmacenViewModel CreateModifyAlmacenViewModel()
        {
            AlmacenModel almacenModel = new AlmacenModel(new AlmacenDataMapper());
            if (this._catalogAlmacenModel != null && this._catalogAlmacenModel.SelectedAlmacen != null)
            {
                almacenModel.UnidAlmacen = this._catalogAlmacenModel.SelectedAlmacen.UNID_ALMACEN;
                almacenModel.AlmacenName = this._catalogAlmacenModel.SelectedAlmacen.ALMACEN_NAME;                
                almacenModel.Contacto = this._catalogAlmacenModel.SelectedAlmacen.CONTACTO;
                almacenModel.Direccion = this._catalogAlmacenModel.SelectedAlmacen.DIRECCION;
                almacenModel.Mail = this._catalogAlmacenModel.SelectedAlmacen.MAIL;
                almacenModel.MailDefault = this._catalogAlmacenModel.SelectedAlmacen.MAIL_DEFAULT;
            }
            return new ModifyAlmacenViewModel(this, almacenModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteAlmacen()
        {
            bool _canDeleteAlmacen = false;
            foreach (DeleteAlmacen d in this._catalogAlmacenModel.Almacen)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteAlmacen = true;
                }
            }

            return _canDeleteAlmacen;
        }

        public void AttempDeleteAlmacen()
        {
            this._catalogAlmacenModel.deleteAlamacen();

            if (this._catalogAlmacenModel != null)
            {
                this._catalogAlmacenModel.loadItems();
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
