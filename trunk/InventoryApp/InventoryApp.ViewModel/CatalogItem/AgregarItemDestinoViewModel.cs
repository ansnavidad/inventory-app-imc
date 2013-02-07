using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogItem
{
    public class AgregarItemDestinoViewModel
    {
        private AgregarItemDestinoModel _agregarItemDestinoModel;
        private AgregarItemViewModel _agregarItemViewModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        private CatalogProveedorModel _catalogProveedorModel;
        private CatalogClienteModel _catalogClienteModel;

        private RelayCommand _agregarMovimiento;

        #region Props

        public ICommand AgregarMovimiento
        {
            get
            {
                if (_agregarMovimiento == null)
                {
                    _agregarMovimiento = new RelayCommand(p => this.AttempAgregarMovimiento(), p => this.CanAttempAgregarMovimiento());
                }
                return _agregarMovimiento;
            }
        }

        public AgregarItemDestinoModel AgregarItemDestinoModel
        {
            get
            {
                return _agregarItemDestinoModel;
            }
            set
            {
                _agregarItemDestinoModel = value;
            }
        }

        public AgregarItemViewModel AgregarItemViewModel
        {
            get
            {
                return _agregarItemViewModel;
            }
            set
            {
                _agregarItemViewModel = value;
            }
        }

        public CatalogAlmacenModel CatalogAlmacenModel
        {
            get
            {
                return _catalogAlmacenModel;
            }
            set
            {
                _catalogAlmacenModel = value;
            }
        }

        public CatalogProveedorModel CatalogProveedorModel
        {
            get
            {
                return _catalogProveedorModel;
            }
            set
            {
                _catalogProveedorModel = value;
            }
        }

        public CatalogClienteModel CatalogClienteModel
        {
            get
            {
                return _catalogClienteModel;
            }
            set
            {
                _catalogClienteModel = value;
            }
        }

        #endregion

        #region RelayCommand
        public bool CanAttempAgregarMovimiento()
        {
            bool _canAddItem = false;
            int total = 0;
            foreach (UltimoMovimientoModel um in this._agregarItemViewModel.UltimoMovimiento)
            {
                total += um.Cantidad;
            }
            total += this.AgregarItemDestinoModel.Cantidad;
            if (this.AgregarItemViewModel.ItemModel.CantidadItem >= total )
                _canAddItem = true;

            if (this.AgregarItemDestinoModel.Cantidad == 0)
                _canAddItem = false;

            return _canAddItem;
        }

        public void AttempAgregarMovimiento()
        {
            UltimoMovimientoModel movimiento = new UltimoMovimientoModel(this._agregarItemDestinoModel.Almacen, this._agregarItemDestinoModel.Cliente, this._agregarItemDestinoModel.Proveedor);
            if (this.AgregarItemViewModel.CatalogStatus.ItemStatus[0] != null && this.AgregarItemViewModel.CatalogStatus.ItemStatus.Count > 0)
                movimiento.ItemStatus2 = this.AgregarItemViewModel.CatalogStatus.ItemStatus[0];
            movimiento.Cantidad = this._agregarItemDestinoModel.Cantidad;
            this.AgregarItemViewModel.UltimoMovimiento.Add(movimiento);
            this.AgregarItemViewModel.FillWithIFactura = false;
            this.AgregarItemViewModel.FillWithDestinos = true;
            this.AgregarItemViewModel.FillWithDestinos2 = true;

            int total = 0;
            foreach (UltimoMovimientoModel um in this._agregarItemViewModel.UltimoMovimiento)
            {
                total += um.Cantidad;
            }
            
            if (this.AgregarItemViewModel.ItemModel.CantidadItem == total)
                this.AgregarItemViewModel.FillWithDestinos2 = false;
            else
                this.AgregarItemViewModel.FillWithDestinos2 = true;                
        }
        #endregion

        #region Constructor

        public AgregarItemDestinoViewModel(AgregarItemViewModel agregarItemViewModel)
        {
            this._agregarItemDestinoModel = new AgregarItemDestinoModel();
            this._agregarItemViewModel = agregarItemViewModel;
            this._catalogAlmacenModel = new CatalogAlmacenModel(new AlmacenDataMapper());
            this._catalogProveedorModel = new CatalogProveedorModel(new ProveedorDataMapper());
            this._catalogClienteModel = new CatalogClienteModel(new ClienteDataMapper());
        }

        #endregion
    }
}
