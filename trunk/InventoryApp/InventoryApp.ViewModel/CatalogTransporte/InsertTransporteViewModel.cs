using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;

namespace InventoryApp.ViewModel
{
    public class InsertTransporteViewModel
    {
        #region Fields
        private InsertTransporteModel _transporte;
        private RelayCommand _addItemCommand;
        private CatalogTransporteViewModel _catalogTransporteViewModel;
        private CatalogTipoEmpresaModel _catalogTipoEmpresaModel;

        #endregion

        //Exponer la propiedad item status
        #region Props
        public InsertTransporteModel Transporte
        {
            get
            {
                return _transporte;
            }
            set
            {
                _transporte = value;
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

        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempInsertTransporte(), p => this.CanAttempInsertTransporte());
                }
                return _addItemCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public InsertTransporteViewModel(CatalogTransporteViewModel catalogTransporteViewModel)
        {
            this._transporte = new InsertTransporteModel(new TransporteDataMapper());
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
        public bool CanAttempInsertTransporte()
        {
            bool _canInsertTransporte = true;
            if (String.IsNullOrEmpty(this._transporte.TransporteName))
                _canInsertTransporte = false;

            return _canInsertTransporte;
        }

        public void AttempInsertTransporte()
        {
            this._transporte.saveTransporte();

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
