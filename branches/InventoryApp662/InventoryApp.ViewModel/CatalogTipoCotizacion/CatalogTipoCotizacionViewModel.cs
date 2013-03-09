using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogTipoCotizacion
{
    public class CatalogTipoCotizacionViewModel : IPageViewModel
    {
        private RelayCommand _deleteTipoCotizacionCommand;
        private CatalogTipoCotizacionModel _catalogTipoCotizacionModel;
        public USUARIO ActualUser;

        public ICommand DeleteTipoCotizacionCommand
        {
            get
            {
                if (_deleteTipoCotizacionCommand == null)
                {
                    _deleteTipoCotizacionCommand = new RelayCommand(p => this.AttempDeleteTipoCotizacion(), p => this.CanAttempDeleteTipoCotizacion());
                }
                return _deleteTipoCotizacionCommand;
            }
        }

        public CatalogTipoCotizacionViewModel()
        {
            try
            {
                IDataMapper dataMapper = new TipoCotizacionDataMapper();
                this._catalogTipoCotizacionModel = new CatalogTipoCotizacionModel(dataMapper);
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

        public CatalogTipoCotizacionViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new TipoCotizacionDataMapper();
                this._catalogTipoCotizacionModel = new CatalogTipoCotizacionModel(dataMapper);
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

        public CatalogTipoCotizacionModel CatalogTipoCotizacionModel
        {
            get
            {
                return _catalogTipoCotizacionModel;
            }
            set
            {
                _catalogTipoCotizacionModel = value;
            }
        }

        public void loadTipoCotizacion()
        {
            this._catalogTipoCotizacionModel.loadItems();
        }
        /// <summary>
        /// Crea una nueva instancia de addTipoCotizacion y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddTipoCotizacionViewModel CreateAddTipoCotizacionViewModel()
        {
            return new AddTipoCotizacionViewModel(this);
        }
        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyTipoCotizacionViewModel CreateModifyTipoCotizacionViewModel()
        {
            TipoCotizacionModel tipoCotizacionModel = new TipoCotizacionModel(new TipoCotizacionDataMapper(), this.ActualUser);
            if (this._catalogTipoCotizacionModel != null && this._catalogTipoCotizacionModel.SelectedTipoCotizacion != null)
            {
                tipoCotizacionModel.TipoCotizacionName = this._catalogTipoCotizacionModel.SelectedTipoCotizacion.TIPO_COTIZACION_NAME;
                tipoCotizacionModel.UnidTipoCotizacion = this._catalogTipoCotizacionModel.SelectedTipoCotizacion.UNID_TIPO_COTIZACION;
            }
            return new ModifyTipoCotizacionViewModel(this, tipoCotizacionModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteTipoCotizacion()
        {
            bool _canDeleteTipoCotizacion = false;
            foreach (DeleteTipoCotizacion d in this._catalogTipoCotizacionModel.TipoCotizacion)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteTipoCotizacion = true;
                }
            }

            return _canDeleteTipoCotizacion;
        }

        public void AttempDeleteTipoCotizacion()
        {
            this._catalogTipoCotizacionModel.deleteTipoCotizacion(this.ActualUser);

            if (this._catalogTipoCotizacionModel != null)
            {
                this._catalogTipoCotizacionModel.loadItems();
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
