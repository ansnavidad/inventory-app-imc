using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace InventoryApp.Model.Recibo
{
    public class MovimientoModel:ModelBase
    {
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    OnPropertyChanged(IsCheckedPropertyName);
                }
            }
        }
        private bool _IsChecked;
        public const string IsCheckedPropertyName = "IsChecked";

        public long UnidMovimiento
        {
            get { return _UnidMovimiento; }
            set
            {
                if (_UnidMovimiento != value)
                {
                    _UnidMovimiento = value;
                    OnPropertyChanged(UnidMovimientoPropertyName);
                }
            }
        }
        private long _UnidMovimiento;
        public const string UnidMovimientoPropertyName = "UnidMovimiento";

        public DateTime FechaCaptura
        {
            get { return _FechaCaptura; }
            set
            {
                if (_FechaCaptura != value)
                {
                    _FechaCaptura = value;
                    OnPropertyChanged(FechaCapturaPropertyName);
                }
            }
        }
        private DateTime _FechaCaptura;
        public const string FechaCapturaPropertyName = "FechaCaptura";

        public IOrigenModel Origen
        {
            get { return _Origen; }
            set
            {
                if (_Origen != value)
                {
                    _Origen = value;
                    OnPropertyChanged(SelectedOrigenPropertyName);
                }
            }
        }
        private IOrigenModel _Origen;
        public const string SelectedOrigenPropertyName = "Origen";

        public TipoPedimentoModel TipoPedimento
        {
            get { return _TipoPedimento; }
            set
            {
                if (_TipoPedimento != value)
                {
                    _TipoPedimento = value;
                    OnPropertyChanged(TipoPedimentoPropertyName);
                }
            }
        }
        private TipoPedimentoModel _TipoPedimento;
        public const string TipoPedimentoPropertyName = "TipoPedimento";

        public OrigenClienteModel OrigenCliente
        {
            get { return _OrigenCliente; }
            set
            {
                if (_OrigenCliente != value)
                {
                    _OrigenCliente = value;
                    OnPropertyChanged(OrigenClientePropertyName);
                }
            }
        }
        private OrigenClienteModel _OrigenCliente;
        public const string OrigenClientePropertyName = "OrigenCliente";

        public OrigenProveedorModel OrigenProveedor
        {
            get { return _OrigenProveedor; }
            set
            {
                if (_OrigenProveedor != value)
                {
                    _OrigenProveedor = value;
                    OnPropertyChanged(OrigenProveedorPropertyName);
                }
            }
        }
        private OrigenProveedorModel _OrigenProveedor;
        public const string OrigenProveedorPropertyName = "OrigenProveedor";

        public AlmacenModel OrigenAlmacen
        {
            get { return _OrigenAlmacen; }
            set
            {
                if (_OrigenAlmacen != value)
                {
                    _OrigenAlmacen = value;
                    OnPropertyChanged(OrigenAlmacenPropertyName);
                }
            }
        }
        private AlmacenModel _OrigenAlmacen;
        public const string OrigenAlmacenPropertyName = "OrigenAlmacen";

        public AlmacenModel DestinoAlmacen
        {
            get { return _DestinoAlmacen; }
            set
            {
                if (_DestinoAlmacen != value)
                {
                    _DestinoAlmacen = value;
                    OnPropertyChanged(DestinoAlmacenPropertyName);
                }
            }
        }
        private AlmacenModel _DestinoAlmacen;
        public const string DestinoAlmacenPropertyName = "DestinoAlmacen";

        public FacturaCompraModel Factura
        {
            get { return _Factura; }
            set
            {
                if (_Factura != value)
                {
                    _Factura = value;
                    OnPropertyChanged(FacturaPropertyName);
                }
            }
        }
        private FacturaCompraModel _Factura;
        public const string FacturaPropertyName = "Factura";

        //FacturaCompraModel

        public int CantidadItems
        {
            get {

                int auxx = 0;
                foreach (ReciboItemModel rr in this._Items)
                    auxx += rr.Cantidad;

                return auxx;             
            }
        }
        

        public ObservableCollection<ReciboItemModel> Items
        {
            get { return _Items; }
            set
            {
                if (_Items != value)
                {
                    _Items = value;
                    OnPropertyChanged(ItemsPropertyName);
                }
            }
        }
        private ObservableCollection<ReciboItemModel> _Items;
        public const string ItemsPropertyName = "Items";

        //Indica cuantos items tienen sku diferente de vaio
        public int ContItemSku 
        {
            get
            {
                int _ContItemSku = 0;
                var res = (from o in this._Items
                           where !String.IsNullOrEmpty(o.Sku)
                           select o).ToList();
                if (res != null)
                {
                    _ContItemSku = res.Count;
                }
                return _ContItemSku;
            }
        }

        //Indica cuantos items tienen numero de serie diferente de vaio
        public int ContItemSerie
        {
            get
            {
                int _ContItemSerie = 0;
                var res = (from o in this._Items
                           where !String.IsNullOrEmpty(o.NumeroSerie)
                           select o).ToList();
                if (res != null)
                {
                    _ContItemSerie = res.Count;
                }
                return _ContItemSerie;
            }
        }
    }
}
