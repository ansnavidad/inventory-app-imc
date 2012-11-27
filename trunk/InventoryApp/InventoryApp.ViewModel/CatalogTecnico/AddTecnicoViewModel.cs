using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTecnico
{
    public class AddTecnicoViewModel
    {
        #region Fields
        private TecnicoModel _addTecnico;
        private RelayCommand _addTecnicoCommand;
        private CatalogTecnicoViewModel _catalogTecnicoViewModel;
        private CatalogCiudadModel _catalogCiudadModel;
        #endregion
        //Exponer la propiedad de tipo de cotizacion
        #region Props
        public TecnicoModel AddTecnico
        {
            get
            {
                return _addTecnico;
            }
            set
            {
                _addTecnico = value;
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

        public ICommand AddTecnicoCommand
        {
            get
            {
                if (_addTecnicoCommand == null)
                {
                    _addTecnicoCommand = new RelayCommand(p => this.AttempAddTecnico(), p => this.CanAttempAddTecnico());
                }
                return _addTecnicoCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddTecnicoViewModel(CatalogTecnicoViewModel catalogTecnicoViewModel)
        {
            this._addTecnico = new TecnicoModel(new TecnicoDataMapper());
            this._catalogTecnicoViewModel = catalogTecnicoViewModel;

            try{
                
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
        public bool CanAttempAddTecnico()
        {
            bool _canAddTecnico = true;
            if (String.IsNullOrEmpty(this._addTecnico.TecnicoName) || String.IsNullOrEmpty(this._addTecnico.Mail))
            //{
                //if (1<this._addTipoMovimiento.SignoMovimiento.Length)
                //{
                    _canAddTecnico = false;    
                //}
                
            //}

            return _canAddTecnico;
        }

        public void AttempAddTecnico()
        {
            this._addTecnico.saveTecnico();
            

            //Puede ser que para pruebas unitarias catalogTipoMovimientoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTecnicoViewModel != null)
            {
                this._catalogTecnicoViewModel.loadTecnico();
            }
        }
        #endregion
    }
}
