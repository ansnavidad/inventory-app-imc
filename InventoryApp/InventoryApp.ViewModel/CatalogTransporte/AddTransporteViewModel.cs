using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTransporte
{
    public class AddTransporteViewModel
    {
        #region Fields
        private InsertTransporteModel _addTransporte;
        private RelayCommand _addTransporteCommand;
        private CatalogTransporteViewModel _catalogTransporteViewModel;
        private CatalogTipoEmpresaModel _catalogTipoEmpresaModel;
        #endregion
        #region Props
        public InsertTransporteModel AddTransporte
        {
            get
            {
                return _addTransporte;
            }
            set
            {
                _addTransporte = value;
            }
        }
        public CatalogTipoEmpresaModel CatalogTipoEmpresaModel
        {
            get
            {
                return _catalogTipoEmpresaModel;
            }
            set
            {
                _catalogTipoEmpresaModel = value;
            }
        }
        public ICommand AddTransporteCommand
        {
            get
            {
                if (_addTransporteCommand == null)
                {
                    _addTransporteCommand = new RelayCommand(p => this.AttempTransporte(), p => this.CanAttempTransporte());
                }
                return _addTransporteCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddTransporteViewModel(CatalogTransporteViewModel catalogTransporteViewModel)
        {
            this._addTransporte = new InsertTransporteModel(new TransporteDataMapper());
            this._catalogTransporteViewModel = catalogTransporteViewModel;
            try
            {
                this._catalogTipoEmpresaModel = new CatalogTipoEmpresaModel(new TipoEmpresaDataMapper());
                
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
        public bool CanAttempTransporte()
        {
            bool _canAddTranporte = true;
            if (String.IsNullOrEmpty(this._addTransporte.TransporteName))
                _canAddTranporte = false;

            return _canAddTranporte;
        }

        public void AttempTransporte()
        {
            this._addTransporte.saveTransporte();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTransporteViewModel != null)
            {
                this._catalogTransporteViewModel.loadItems();
            }
        }
        #endregion
    }
}
