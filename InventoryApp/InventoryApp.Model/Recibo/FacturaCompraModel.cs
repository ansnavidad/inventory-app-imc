using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.DAL.Recibo;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model.Recibo
{
    public class FacturaCompraModel : ModelBase
    {
        private IDataMapper _DataMapper;

        #region Properties
        public long UnidFactura
        {
            get { return _UnidFactura; }
            set
            {
                if (_UnidFactura != value)
                {
                    _UnidFactura = value;
                    OnPropertyChanged(UnidFacturaPropertyName);
                }
            }
        }
        private long _UnidFactura;
        public const string UnidFacturaPropertyName = "UnidFactura";

        public long UnidLote
        {
            get { return _UnidLote; }
            set
            {
                if (_UnidLote != value)
                {
                    _UnidLote = value;
                    OnPropertyChanged(UnidLotePropertyName);
                }
            }
        }
        private long _UnidLote;
        public const string UnidLotePropertyName = "UnidLote";

        public string NumeroPedimento
        {
            get { return _NumeroPedimento; }
            set
            {
                if (_NumeroPedimento != value)
                {
                    _NumeroPedimento = value;
                    OnPropertyChanged(NumeroPedimentoPropertyName);
                }
            }
        }
        private string _NumeroPedimento;
        public const string NumeroPedimentoPropertyName = "NumeroPedimento";

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

        public double PorIva
        {
            get { return _PorIva; }
            set
            {
                if (_PorIva != value)
                {
                    _PorIva = value;
                    OnPropertyChanged(PorIvaPropertyName);
                }
            }
        }
        private double _PorIva;
        public const string PorIvaPropertyName = "PorIva";

        public double Iva
        {
            get
            {
                double _iva = 0d;

                _iva = this.Importe * (this._PorIva / 100);

                return _iva;
            }
        }

        public double Total
        {
            get
            {
                double _total;
                _total = this.Importe + this.Iva;
                return _total;
            }
        }

        public DateTime FechaFactura
        {
            get
            {
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

        //importe es el total de la factura
        public double Importe
        {
            get
            {
                double _importe = 0d;
                try
                {
                    _importe = (from o in _FacturaDetalle
                                select o).Sum(o => (o.Cantidad * o.PrecioUnitario) + (o.Cantidad * o.ImpuestoUnitario));
                }
                catch (Exception)
                {
                    throw;
                }
                return _importe;
            }
        }

        #endregion

        public void saveFactura()
        {
            if (_DataMapper != null)
            {
                _DataMapper.insertElement(new FACTURA()
                {
                    IS_ACTIVE = true,
                    UNID_FACTURA = this._UnidFactura,
                    UNID_LOTE = this._UnidLote,
                    FACTURA_NUMERO = this._NumeroFactura,
                    FECHA_FACTURA = this._FechaFactura,
                    UNID_PROVEEDOR = this._Proveedor.UnidProveedor,
                    UNID_MONEDA = this._Moneda.UnidMoneda
                });
            }
        }
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
