using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogTipoMovimiento
{
    public class CatalogTipoMovimientoViewModel : IPageViewModel
    {
        private RelayCommand _deleteTipoMovimientoCommand;
        private CatalogTipoMovimientoModel _catalogTipoMovimientoModel;
        public USUARIO ActualUser;

        public CatalogTipoMovimientoViewModel()
        {
            try
            {
                IDataMapper dataMapper = new TipoMovimientoDataMapper();
                this._catalogTipoMovimientoModel = new CatalogTipoMovimientoModel(dataMapper);
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

        public ICommand DeleteTipoMovimientoCommand
        {
            get
            {
                if (_deleteTipoMovimientoCommand == null)
                {
                    _deleteTipoMovimientoCommand = new RelayCommand(p => this.AttempDeleteTipoMovimiento(), p => this.CanAttempDeleteTipoMovimiento());
                }
                return _deleteTipoMovimientoCommand;
            }
        }

        public CatalogTipoMovimientoModel CatalogTipoMovimientoModel
        {
            get
            {
                return _catalogTipoMovimientoModel;
            }
            set
            {
                _catalogTipoMovimientoModel = value;
            }
        }

        public void loadTipoCotizacion()
        {
            this._catalogTipoMovimientoModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de AddTipoMovimiento y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddTipoMovimientoViewModel CreateAddTipoMovimientoViewModel()
        {
            return new AddTipoMovimientoViewModel(this);
        }
        /// <summary>
        /// Crea una nueva instancia de ModifyTipoMovimiento y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyTipoMovimientoViewModel CreateModifyTipoMovimientoViewModel()
        {
            TipoMovimientoModel tipoMovimientoModel = new TipoMovimientoModel(new TipoMovimientoDataMapper());
            if (this._catalogTipoMovimientoModel != null && this._catalogTipoMovimientoModel.SelectedTipoMovimiento != null)
            {
                tipoMovimientoModel.TipoMovimientoName = this._catalogTipoMovimientoModel.SelectedTipoMovimiento.TIPO_MOVIMIENTO_NAME;
                tipoMovimientoModel.UnidTipoMovimiento = this._catalogTipoMovimientoModel.SelectedTipoMovimiento.UNID_TIPO_MOVIMIENTO;
                tipoMovimientoModel.SignoMovimiento = this._catalogTipoMovimientoModel.SelectedTipoMovimiento.SIGNO_MOVIMIENTO;
            }
            return new ModifyTipoMovimientoViewModel(this, tipoMovimientoModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteTipoMovimiento()
        {
            bool _canDeleteTipoMovimiento = false;
            foreach (DeleteTipoMovimiento d in this._catalogTipoMovimientoModel.TipoMovimiento)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteTipoMovimiento = true;
                }
            }

            return _canDeleteTipoMovimiento;
        }

        public void AttempDeleteTipoMovimiento()
        {
            this._catalogTipoMovimientoModel.deleteTipoMovimiento();

            if (this._catalogTipoMovimientoModel != null)
            {
                this._catalogTipoMovimientoModel.loadItems();
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
