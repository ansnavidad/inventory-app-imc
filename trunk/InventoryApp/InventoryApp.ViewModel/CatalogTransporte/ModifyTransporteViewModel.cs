using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel
{
    public class ModifyTransporteViewModel
    {
        #region Fields
        private InsertTransporteModel _transporte;
        private RelayCommand _modifyItemCommand;
        private CatalogTransporteViewModel _catalogTransporteViewModel;
        private CatalogTipoEmpresaModel _catalogTipoEmpresaModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
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

        public ICommand ModifyItemCommand
        {
            get
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyTransporte(), p => this.CanAttempModifyTransporte());
                }
                return _modifyItemCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyTransporteViewModel(CatalogTransporteViewModel catalogTransporteViewModel, InsertTransporteModel selectedTransporteModel)
        {
            this._transporte = new InsertTransporteModel(new TransporteDataMapper());
            this._catalogTransporteViewModel = catalogTransporteViewModel;
            this._transporte.UnidTransporte = selectedTransporteModel.UnidTransporte;
            this._transporte.TransporteName = selectedTransporteModel.TransporteName;
            this._transporte.TipoEmpresa = selectedTransporteModel.TipoEmpresa;
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
        public bool CanAttempModifyTransporte()
        {
            bool _canAddTransporte = true;
            if (String.IsNullOrEmpty(this._transporte.TransporteName))
                _canAddTransporte = false;

            return _canAddTransporte;
        }

        public void AttempModifyTransporte()
        {
            this._transporte.updateTransporte();

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
