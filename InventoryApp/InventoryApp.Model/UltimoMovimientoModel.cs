using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class UltimoMovimientoModel : INotifyPropertyChanged
    {
        private long _unidItem;
        private long? _unidAlmacen;
        private long? _unidCliente;
        private long? _unidProveedor;
        private long? _unidMovimientoDetalle;
        private UltimoMovimientoDataMapper _dataMapper;

        public long UnidItem
        {
            get
            {
                return _unidItem;
            }
            set
            {
                if (_unidItem != value)
                {
                    _unidItem = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidItem"));
                    }
                }
            }
        }

        public long? UnidAlmacen
        {
            get
            {
                return _unidAlmacen;
            }
            set
            {
                if (_unidAlmacen != value)
                {
                    _unidAlmacen = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidAlmacen"));
                    }
                }
            }
        }

        public long? UnidCliente
        {
            get
            {
                return _unidCliente;
            }
            set
            {
                if (_unidCliente != value)
                {
                    _unidCliente = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidCliente"));
                    }
                }
            }
        }

        public long? UnidProveedor
        {
            get
            {
                return _unidProveedor;
            }
            set
            {
                if (_unidProveedor != value)
                {
                    _unidProveedor = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidProveedor"));
                    }
                }
            }
        }

        public long? UnidMovimientoDetalle
        {
            get
            {
                return _unidMovimientoDetalle;
            }
            set
            {
                if (_unidMovimientoDetalle != value)
                {
                    _unidMovimientoDetalle = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidMovimiento"));
                    }
                }
            }
        }

        public void saveArticulo()
        {
            if (_dataMapper != null)
            {
                //_dataMapper.insertElement(new MOVIMENTO() { UNID_MOVIMIENTO = this._unidMovimiento, FECHA_MOVIMIENTO = this._fechaMovimiento, UNID_TIPO_MOVIMIENTO = this._tipoMovimiento.UNID_TIPO_MOVIMIENTO, UNID_ALMACEN_DESTINO = this._almacenDestino.UNID_ALMACEN, UNID_PROVEEDOR_DESTINO = this._proveedorDestino.UNID_PROVEEDOR, UNID_CLIENTE_DESTINO = this._clienteDestino.UNID_CLIENTE, UNID_ALMACEN_PROCEDENCIA = this._almacenProcedencia.UNID_ALMACEN, UNID_CLIENTE_PROCEDENCIA = this._clienteProcedencia.UNID_CLIENTE, UNID_PROVEEDOR_PROCEDENCIA = this._proveedorProcedencia.UNID_PROVEEDOR, UNID_SERVICIO = this._servicio.UNID_SERVICIO, TT = this._tt, CONTACTO = this._contacto, UNID_TRANSPORTE = this._transporte.UNID_TRANSPORTE, IS_ACTIVE = this._isActive, DIRECCION_ENVIO = this._direccionEnvio, SITIO_ENLACE = this._sitioEnlace, NOMBRE_SITIO = this._nombreSitio, RECIBE = this._recibe, GUIA = this._guia, UNID_CLIENTE = this._cliente.UNID_CLIENTE, UNID_PROVEEDOR = this._proveedor.UNID_PROVEEDOR, UNID_FACTURA_VENTA = this._facturaVenta.UNID_FACTURA_VENTA });
                _dataMapper.udpateElement(new ULTIMO_MOVIMIENTO { UNID_ITEM = this._unidItem, UNID_ALMACEN = this._unidAlmacen, UNID_CLIENTE = this.UnidCliente, UNID_MOVIMIENTO_DETALLE = this._unidMovimientoDetalle, UNID_PROVEEDOR = this._unidProveedor });

            }
        }
        public UltimoMovimientoModel(IDataMapper dataMapper, long item, long? almacen, long? cliente, long? proveedor, long? movDet)
        {
            this._unidItem = item;
            this._unidAlmacen = almacen;
            this._unidCliente = cliente;
            this._unidProveedor = proveedor;
            this._unidMovimientoDetalle = movDet;
            if ((dataMapper as UltimoMovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as UltimoMovimientoDataMapper;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
