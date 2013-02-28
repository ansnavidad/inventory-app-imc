using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogCiudad 
{
    public class CatalogCiudadViewModel : IPageViewModel
    {
        private RelayCommand _deleteCiudadCommand;

        private CatalogCiudadModel _catalogCiudadModel;
        public USUARIO ActualUser;

        public ICommand DeleteCiudadCommand
        {
            get
            {
                if (_deleteCiudadCommand == null)
                {
                    _deleteCiudadCommand = new RelayCommand(p => this.AttempDeleteCiudad(), p => this.CanAttempDeleteCiudad());
                }
                return _deleteCiudadCommand;
            }
        }

        public CatalogCiudadViewModel(USUARIO u)
        {
            try
            {
                this.ActualUser = u;
                IDataMapper dataMapper = new CiudadDataMapper();
                this._catalogCiudadModel = new CatalogCiudadModel(dataMapper);
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

        public CatalogCiudadViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new CiudadDataMapper();
                this._catalogCiudadModel = new CatalogCiudadModel(dataMapper);   
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch(Exception ex)
            {
                throw ex;
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

        public void loadItems()
        {
            this._catalogCiudadModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddCiudadViewModel CreateAddCiudadViewModel()
        {
            return new AddCiudadViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyCiudadViewModel CreateModifyCiudadViewModel()
        {
            CiudadModel ciudadModel = new CiudadModel(new CiudadDataMapper(), this.ActualUser);
            if (this._catalogCiudadModel != null && this._catalogCiudadModel.SelectedCiudad != null)
            {
                ciudadModel.Ciudad = this._catalogCiudadModel.SelectedCiudad.CIUDAD1;
                ciudadModel.Iso = this._catalogCiudadModel.SelectedCiudad.ISO;
                ciudadModel.UnidCiudad = this._catalogCiudadModel.SelectedCiudad.UNID_CIUDAD;        
            }
            return new ModifyCiudadViewModel(this, ciudadModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteCiudad()
        {
            bool _canDeleteCiudad = false;
            foreach (DeleteCiudad d in this._catalogCiudadModel.Ciudad)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteCiudad = true;
                }
            }

            return _canDeleteCiudad;
        }

        public void AttempDeleteCiudad()
        {
            this._catalogCiudadModel.deleteCiudad(this.ActualUser);

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogCiudadModel != null)
            {
                this._catalogCiudadModel.loadItems();
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
