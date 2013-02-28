using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogUnidad
{
    public class CatalogUnidadViewModel : IPageViewModel
    {
        #region Fields
        private RelayCommand _deleteUnidadCommand;
        private CatalogUnidadModel _catalogUnidadModel;
        public USUARIO ActualUser;
        #endregion

        //Exponer la propiedad item status
        #region Props
        
        public ICommand DeleteUnidadCommand
        {
            get
            {
                if (_deleteUnidadCommand == null)
                {
                    _deleteUnidadCommand = new RelayCommand(p => this.AttempDeleteUnidad(), p => this.CanAttempDeleteUnidad());
                }
                return _deleteUnidadCommand;
            }
        }

        public CatalogUnidadModel CatalogUnidadModel
        {
            get
            {
                return _catalogUnidadModel;
            }
            set
            {
                _catalogUnidadModel = value;
            }
        }
        #endregion

        #region Contructor
        public CatalogUnidadViewModel()
        {
            try
            {
                IDataMapper dataMapper = new UnidadDataMapper();
                this._catalogUnidadModel = new CatalogUnidadModel(dataMapper);
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

        public CatalogUnidadViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new UnidadDataMapper();
                this._catalogUnidadModel = new CatalogUnidadModel(dataMapper);
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
        
        #endregion

        public void loadItems()
        {
            this._catalogUnidadModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addUnidad y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddUnidadViewModel CreateAddUnidadViewModel()
        {
            return new AddUnidadViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyUnidad y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyUnidadViewModel CreateModifyUnidadViewModel()
        {
            UnidadModel unidadModel = new UnidadModel(new UnidadDataMapper());
            if (this._catalogUnidadModel != null && this._catalogUnidadModel.SelectedUnidad != null)
            {
                unidadModel.UnidadName = this._catalogUnidadModel.SelectedUnidad.UNIDAD1;
                unidadModel.UnidUnidad = this._catalogUnidadModel.SelectedUnidad.UNID_UNIDAD;
            }
            return new ModifyUnidadViewModel(this, unidadModel);
        }
        
        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteUnidad()
        {
            bool _canDeleteUnidad =false;
            foreach (DeleteUnidad d in this._catalogUnidadModel.Unidad)
            {
                if (d.IsChecked==true)
                {
                    _canDeleteUnidad = true;
                }
            }

            return _canDeleteUnidad;
        }
        public void AttempDeleteUnidad()
        {
            this._catalogUnidadModel.deleteUnidad();
            
            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogUnidadModel != null)
            {
                this._catalogUnidadModel.loadItems();
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
