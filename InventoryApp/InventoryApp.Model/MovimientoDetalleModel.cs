using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;


namespace InventoryApp.Model
{
    public class MovimientoDetalleModel : INotifyPropertyChanged
    {
        private long _unidMovimientoDetalle;
        private long _unidMovimiento;
        private long _unidItem;
        private bool _isActive;
        private int _cantidad;
        private MovimientoDetalleDataMapper _dataMapper;

        public long UnidMovimientoDetalle
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
                        this.PropertyChanged(this, new PropertyChangedEventArgs("MovimientoDetalle"));
                    }
                }
            }
        }
        public long UnidMovimiento
        {
            get
            {
                return _unidMovimiento;
            }
            set
            {
                if (_unidMovimiento != value)
                {
                    _unidMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidMovimiento"));
                    }
                }
            }
        }
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

        public void saveArticulo()
        {
            if (_dataMapper != null)
            {
                //_dataMapper.insertElement(new MOVIMENTO() { UNID_MOVIMIENTO = this._unidMovimiento, FECHA_MOVIMIENTO = this._fechaMovimiento, UNID_TIPO_MOVIMIENTO = this._tipoMovimiento.UNID_TIPO_MOVIMIENTO, UNID_ALMACEN_DESTINO = this._almacenDestino.UNID_ALMACEN, UNID_PROVEEDOR_DESTINO = this._proveedorDestino.UNID_PROVEEDOR, UNID_CLIENTE_DESTINO = this._clienteDestino.UNID_CLIENTE, UNID_ALMACEN_PROCEDENCIA = this._almacenProcedencia.UNID_ALMACEN, UNID_CLIENTE_PROCEDENCIA = this._clienteProcedencia.UNID_CLIENTE, UNID_PROVEEDOR_PROCEDENCIA = this._proveedorProcedencia.UNID_PROVEEDOR, UNID_SERVICIO = this._servicio.UNID_SERVICIO, TT = this._tt, CONTACTO = this._contacto, UNID_TRANSPORTE = this._transporte.UNID_TRANSPORTE, IS_ACTIVE = this._isActive, DIRECCION_ENVIO = this._direccionEnvio, SITIO_ENLACE = this._sitioEnlace, NOMBRE_SITIO = this._nombreSitio, RECIBE = this._recibe, GUIA = this._guia, UNID_CLIENTE = this._cliente.UNID_CLIENTE, UNID_PROVEEDOR = this._proveedor.UNID_PROVEEDOR, UNID_FACTURA_VENTA = this._facturaVenta.UNID_FACTURA_VENTA });
                _dataMapper.insertElement(new MOVIMIENTO_DETALLE() {UNID_MOVIMIENTO_DETALLE = this._unidMovimientoDetalle, UNID_MOVIMIENTO = this.UnidMovimiento, UNID_ITEM = this._unidItem, IS_ACTIVE = this._isActive, CANTIDAD = this._cantidad });

            }
        }
        public MovimientoDetalleModel(IDataMapper dataMapper, long movimiento, long item)
        {
            this._unidItem = item;
            this._unidMovimiento = movimiento;
            this._unidMovimientoDetalle = UNID.getNewUNID();
            this._isActive = true;
            if ((dataMapper as MovimientoDetalleDataMapper) != null)
            {
                this._dataMapper = dataMapper as MovimientoDetalleDataMapper;
            }
        }

        public MovimientoDetalleModel(IDataMapper dataMapper, long movimiento, long item, int cantidad)
        {
            this._unidItem = item;
            this._unidMovimiento = movimiento;
            this._cantidad = cantidad;
            this._unidMovimientoDetalle = UNID.getNewUNID();
            this._isActive = true;
            if ((dataMapper as MovimientoDetalleDataMapper) != null)
            {
                this._dataMapper = dataMapper as MovimientoDetalleDataMapper;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
