using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace InventoryApp.Model.Recibo
{
    public class Movimiento:ModelBase
    {
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
    }
}
