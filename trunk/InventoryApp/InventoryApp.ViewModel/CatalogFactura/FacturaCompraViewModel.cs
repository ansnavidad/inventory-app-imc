using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model.Recibo;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogFactura
{
    public class FacturaCompraViewModel:ViewModelBase
    {
        private RelayCommand _AddCommand;

        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(p => this.AttempAddCommand(), p => this.CanAttempAddCommand());
                }
                return _AddCommand;
            }
        }

        public FacturaCompraModel FacturaCompra
        {
            get { return _FacturaCompra; }
            set
            {
                if (_FacturaCompra != value)
                {
                    _FacturaCompra = value;
                    OnPropertyChanged(FacturaCompraPropertyName);
                }
            }
        }
        private FacturaCompraModel _FacturaCompra;
        public const string FacturaCompraPropertyName = "FacturaCompra";

        public CatalogProveedorModel Proveedor
        {
            get { return _Proveedor; }
            set
            {
                if (_Proveedor != value)
                {
                    _Proveedor = value;
                    OnPropertyChanged(ProveedorPropertyName);
                }
            }
        }
        private CatalogProveedorModel _Proveedor;
        public const string ProveedorPropertyName = "Proveedor";

        public CatalogMonedaModel Moneda
        {
            get { return _Moneda; }
            set
            {
                if (_Moneda != value)
                {
                    _Moneda = value;
                    OnPropertyChanged(MonedaPropertyName);
                }
            }
        }
        private CatalogMonedaModel _Moneda;
        public const string MonedaPropertyName = "Moneda";

        public FacturaCompraViewModel()
        {
            this.init();
        }

        private void init()
        {
            this._Moneda = new CatalogMonedaModel(new MonedaDataMapper());
            this._Proveedor = new CatalogProveedorModel(new ProveedorDataMapper());
            
        }

        public bool CanAttempAddCommand()
        {
            bool canAttemp = false;
            if (this.FacturaCompra!=null && this.FacturaCompra.FacturaDetalle!=null && this.FacturaCompra.FacturaDetalle.Count > 0 && this.Moneda.SelectedMoneda != null && this.Proveedor.SelectedProveedor != null)
            {
                canAttemp = true;
            }
            return canAttemp;
        }

        public void AttempAddCommand()
        {
            return;
        }
    }
}
