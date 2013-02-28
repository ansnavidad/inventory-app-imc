using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogPropiedad
{
    public class CatalogPropiedadViewModel : IPageViewModel
    {
        #region Fields
        private RelayCommand _deletePropiedadCommand;
        private CatalogPropiedadModel _catalogPropiedadModel;
        public USUARIO ActualUser;
        #endregion

        //Exponer la propiedad item status
        #region Props
        
        public ICommand DeletePropiedadCommand
        {
            get
            {
                if (_deletePropiedadCommand == null)
                {
                    _deletePropiedadCommand = new RelayCommand(p => this.AttempDeletePropiedad(), p => this.CanAttempDeletePropiedad());
                }
                return _deletePropiedadCommand;
            }
        }

        public CatalogPropiedadModel CatalogPropiedadModel
        {
            get
            {
                return _catalogPropiedadModel;
            }
            set
            {
                _catalogPropiedadModel = value;
            }
        }
        #endregion

        #region Contructor
        public CatalogPropiedadViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new PropiedadDataMapper();
                this._catalogPropiedadModel = new CatalogPropiedadModel(dataMapper);   
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
        
        #endregion

        public void loadItems()
        {
            this._catalogPropiedadModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addPropiedad y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddPropiedadViewModel CreateAddPropiedadViewModel()
        {
            return new AddPropiedadViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyPropiedadViewModel CreateModifyPropiedadViewModel()
        {
            PropiedadModel propiedadModel = new PropiedadModel(new PropiedadDataMapper());
            if (this._catalogPropiedadModel != null && this._catalogPropiedadModel.SelectedPropiedad != null)
            {
                propiedadModel.PropiedadName = this._catalogPropiedadModel.SelectedPropiedad.PROPIEDAD1;
                propiedadModel.UnidPropiedad = this._catalogPropiedadModel.SelectedPropiedad.UNID_PROPIEDAD;
            }
            return new ModifyPropiedadViewModel(this, propiedadModel);
        }
        
        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeletePropiedad()
        {
            bool _canDeletePropiedad =false;
            foreach (DeletePropiedad d in this._catalogPropiedadModel.Propiedad)
            {
                if (d.IsChecked==true)
                {
                    _canDeletePropiedad = true;
                }
            }

            return _canDeletePropiedad;
        }
        public void AttempDeletePropiedad()
        {
            this._catalogPropiedadModel.deletePropiedad();
            
            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogPropiedadModel != null)
            {
                this._catalogPropiedadModel.loadItems();
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
