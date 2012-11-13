using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogServicio
{
    public class CatalogServicioViewModel
    {
        #region Fields
        private RelayCommand _deleteServicioCommand;
        private CatalogServicioModel _catalogServicioModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        
        public ICommand DeleteServicioCommand
        {
            get
            {
                if (_deleteServicioCommand == null)
                {
                    _deleteServicioCommand = new RelayCommand(p => this.AttempDeleteServicio(), p => this.CanAttempDeleteServicio());
                }
                return _deleteServicioCommand;
            }
        }

        public CatalogServicioModel CatalogServicioModel
        {
            get
            {
                return _catalogServicioModel;
            }
            set
            {
                _catalogServicioModel = value;
            }
        }
        #endregion

        #region Contructor
        public CatalogServicioViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new ServicioDataMapper();
                this._catalogServicioModel = new CatalogServicioModel(dataMapper);   
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
            this._catalogServicioModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addServicio y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddServicioViewModel CreateAddServicioViewModel()
        {
            return new AddServicioViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyServicio y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyServicioViewModel CreateModifyServicioViewModel()
        {
            ServicioModel servicioModel = new ServicioModel(new ServicioDataMapper());
            if (this._catalogServicioModel != null && this._catalogServicioModel.SelectedServicio != null)
            {
                servicioModel.ServicioName = this._catalogServicioModel.SelectedServicio.SERVICIO_NAME;
                servicioModel.UnidServicio = this._catalogServicioModel.SelectedServicio.UNID_SERVICIO;
            }
            return new ModifyServicioViewModel(this, servicioModel);
        }
        
        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteServicio()
        {
            bool _canDeleteServicio =false;
            foreach (DeleteServicio d in this._catalogServicioModel.Servicio)
            {
                if (d.IsChecked==true)
                {
                    _canDeleteServicio = true;
                }
            }

            return _canDeleteServicio;
        }
        public void AttempDeleteServicio()
        {
            this._catalogServicioModel.deleteServicio();
            
            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogServicioModel != null)
            {
                this._catalogServicioModel.loadItems();
            }
        }
        #endregion
    }
}
