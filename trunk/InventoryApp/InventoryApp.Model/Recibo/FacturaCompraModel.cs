using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.DAL.Recibo;
using System.Collections.ObjectModel;

namespace InventoryApp.Model.Recibo
{
    public class FacturaCompraModel : ModelBase
    {
        private IDataMapper _DataMapper;

        #region Properties
        public string NumeroFactura
        {
            get { return _NumeroFactura; }
            set
            {
                if (_NumeroFactura != value)
                {
                    _NumeroFactura = value;
                    OnPropertyChanged(NumeroFacturaPropertyName);
                }
            }
        }
        private string _NumeroFactura;
        public const string NumeroFacturaPropertyName = "NumeroFactura";

        public DateTime FechaFactura
        {
            get { 
                return _FechaFactura; 
            }
            set
            {
                if (_FechaFactura != value)
                {
                    _FechaFactura = value;
                    OnPropertyChanged(FechaFacturaPropertyName);
                }
            }
        }
        private DateTime _FechaFactura;
        public const string FechaFacturaPropertyName = "FechaFactura";

        public ProveedorModel Proveedor
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
        private ProveedorModel _Proveedor;
        public const string ProveedorPropertyName = "Proveedor";

        public MonedaModel Moneda
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
        private MonedaModel _Moneda;
        public const string MonedaPropertyName = "Moneda";

        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged(IsActivePropertyName);
                }
            }
        }
        private bool _IsActive;
        public const string IsActivePropertyName = "IsActive";

        public ObservableCollection<FacturaCompraDetalleModel> FacturaDetalle
        {
            get { return _FacturaDetalle; }
            set
            {
                if (_FacturaDetalle != value)
                {
                    _FacturaDetalle = value;
                    OnPropertyChanged(FacturaDetallePropertyName);
                }
            }
        }
        private ObservableCollection<FacturaCompraDetalleModel> _FacturaDetalle;
        public const string FacturaDetallePropertyName = "FacturaDetalle";
        #endregion

        #region Constructors
        public FacturaCompraModel()
            : this(null)
        {
        }

        public FacturaCompraModel(IDataMapper dataMapper)
        {
            this._DataMapper = dataMapper as FacturaCompraDataMapper;
            this.FacturaDetalle = new ObservableCollection<FacturaCompraDetalleModel>();
        } 
        #endregion


    }
}
