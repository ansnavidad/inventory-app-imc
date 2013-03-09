using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace InventoryApp.Model
{
    public class UltimoMovimientoModel : INotifyPropertyChanged
    {
        private long _unidItem;
        private long? _unidAlmacen;
        private long? _unidCliente;
        private long? _unidProveedor;
        private long? _unidInfraestructura;
        private long? _unidMovimientoDetalle;
        private long? _unidItemStatus;
        private int _cantidad;
        private bool _isChecked;
        private string _lugar;
        private DeleteItemStatus _ItemStatus2;

        private UltimoMovimientoDataMapper _dataMapper;

        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                    }
                }
            }
        }
        public long? UnidItemStatus
        {
            get
            {
                return _unidItemStatus;
            }
            set
            {
                if (_unidItemStatus != value)
                {
                    _unidItemStatus = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidItemStatus"));
                    }
                }
            }
        }
        public string Lugar
        {
            get
            {
                return _lugar;
            }
            set
            {
                if (_lugar != value)
                {
                    _lugar = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Lugar"));
                    }
                }
            }
        }

        public int Cantidad
        {
            get
            {
                return _cantidad;
            }
            set
            {
                if (_cantidad != value)
                {
                    _cantidad = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Cantidad"));
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

        public long? UnidInfraestructura
        {
            get
            {
                return _unidInfraestructura;
            }
            set
            {
                if (_unidInfraestructura != value)
                {
                    _unidInfraestructura = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidInfraestructura"));
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

        public DeleteItemStatus ItemStatus2
        {
            get
            {
                return _ItemStatus2;
            }
            set
            {
                if (_ItemStatus2 != value)
                {
                    _ItemStatus2 = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ItemStatus2"));
                    }
                }
            }
        } 
        
        public void updateArticulo(CLIENTE cliente)
        {
            if (cliente != null)
            {
                if (_dataMapper != null)
                {
                    ITEM item = new ITEM();
                    item.UNID_ITEM = this.UnidItem;
                    ULTIMO_MOVIMIENTO aux = this._dataMapper.getCantidadItems(item, cliente);
                    aux.CANTIDAD -= this._cantidad;
                    this._dataMapper.udpateElement2(aux);
                }
            }
        }

        public void updateArticulo(PROVEEDOR proveedor)
        {
            if (proveedor != null)
            {
                if (_dataMapper != null)
                {
                    ITEM item = new ITEM();
                    item.UNID_ITEM = this.UnidItem;
                    ULTIMO_MOVIMIENTO aux = this._dataMapper.getCantidadItems(item, proveedor);
                    aux.CANTIDAD -= this._cantidad;
                    this._dataMapper.udpateElement2(aux);
                }
            }
        }

        public void updateArticulo(INFRAESTRUCTURA infraestructura)
        {
            if (infraestructura != null)
            {
                if (_dataMapper != null)
                {
                    ITEM item = new ITEM();
                    item.UNID_ITEM = this.UnidItem;
                    ULTIMO_MOVIMIENTO aux = this._dataMapper.getCantidadItems(item, infraestructura);
                    aux.CANTIDAD -= this._cantidad;
                    
                    this._dataMapper.udpateElement2(aux);
                }
            }
        }

        public void updateArticulo(ALMACEN almacen)
        {
            if (almacen != null)
            {
                if (_dataMapper != null)
                {
                    ITEM item = new ITEM();
                    item.UNID_ITEM = this.UnidItem;
                    ULTIMO_MOVIMIENTO aux = this._dataMapper.getCantidadItems(item, almacen);
                    aux.CANTIDAD -= this._cantidad;
                    this._dataMapper.udpateElement2(aux);
                }
            }
        }

        public void updateArticuloBaja(ALMACEN almacen, string soloBaja)
        {
            if (almacen != null)
            {
                if (_dataMapper != null)
                {
                    ITEM item = new ITEM();
                    item.UNID_ITEM = this.UnidItem;
                    ULTIMO_MOVIMIENTO aux = this._dataMapper.getCantidadItemsBaja(item, almacen,"salida baja");
                    aux.CANTIDAD -= this._cantidad;
                    this._dataMapper.udpateElement2Baja(aux);
                }
            }
        }
         
        public void saveArticulo()
        {
            if (_dataMapper != null)
            {
                ITEM item = new ITEM();
                item.UNID_ITEM = this.UnidItem;
                ULTIMO_MOVIMIENTO tmp = new ULTIMO_MOVIMIENTO();
             
                if (this._unidAlmacen != null)
                {
                    ALMACEN almacen = new ALMACEN();
                    almacen.UNID_ALMACEN = (long)this._unidAlmacen;
                    tmp = this._dataMapper.getCantidadItems(item, almacen);
                }
                else if(this._unidProveedor != null)
                {
                    PROVEEDOR proveedor = new PROVEEDOR();
                    proveedor.UNID_PROVEEDOR = (long)this._unidProveedor;
                    tmp = this._dataMapper.getCantidadItems(item, proveedor);
                }
                else if (this._unidCliente != null)
                {
                    CLIENTE cliente = new CLIENTE();
                    cliente.UNID_CLIENTE = (long)this._unidCliente;
                    tmp = this._dataMapper.getCantidadItems(item, cliente);
                }
                else if (this._unidInfraestructura != null)
                {
                    INFRAESTRUCTURA infraestructura = new INFRAESTRUCTURA();
                    infraestructura.UNID_INFRAESTRUCTURA = (long)this._unidInfraestructura;
                    tmp = this._dataMapper.getCantidadItems(item, infraestructura);
                }

                this._cantidad += tmp.CANTIDAD;
                
                if (tmp.UNID_ULTIMO_MOVIMIENTO == 0)
                {
                    _dataMapper.insertElement(new ULTIMO_MOVIMIENTO { UNID_ITEM = this._unidItem, UNID_ALMACEN = this._unidAlmacen, UNID_CLIENTE = this.UnidCliente, UNID_MOVIMIENTO_DETALLE = this._unidMovimientoDetalle, UNID_PROVEEDOR = this._unidProveedor, UNID_INFRAESTRUCTURA = this._unidInfraestructura, IS_ACTIVE = true, CANTIDAD = this._cantidad, UNID_ITEM_STATUS = this._unidItemStatus });
                }
                else
                {
                    _dataMapper.udpateElement2(new ULTIMO_MOVIMIENTO { UNID_ULTIMO_MOVIMIENTO = tmp.UNID_ULTIMO_MOVIMIENTO, UNID_ITEM = this._unidItem, UNID_ALMACEN = this._unidAlmacen, UNID_CLIENTE = this.UnidCliente, UNID_MOVIMIENTO_DETALLE = this._unidMovimientoDetalle, UNID_PROVEEDOR = this._unidProveedor, UNID_INFRAESTRUCTURA = this._unidInfraestructura, IS_ACTIVE = true, CANTIDAD = this._cantidad, UNID_ITEM_STATUS = this._unidItemStatus });
                }
                
                //_dataMapper.insertElement(new MOVIMENTO() { UNID_MOVIMIENTO = this._unidMovimiento, FECHA_MOVIMIENTO = this._fechaMovimiento, UNID_TIPO_MOVIMIENTO = this._tipoMovimiento.UNID_TIPO_MOVIMIENTO, UNID_ALMACEN_DESTINO = this._almacenDestino.UNID_ALMACEN, UNID_PROVEEDOR_DESTINO = this._proveedorDestino.UNID_PROVEEDOR, UNID_CLIENTE_DESTINO = this._clienteDestino.UNID_CLIENTE, UNID_ALMACEN_PROCEDENCIA = this._almacenProcedencia.UNID_ALMACEN, UNID_CLIENTE_PROCEDENCIA = this._clienteProcedencia.UNID_CLIENTE, UNID_PROVEEDOR_PROCEDENCIA = this._proveedorProcedencia.UNID_PROVEEDOR, UNID_SERVICIO = this._servicio.UNID_SERVICIO, TT = this._tt, CONTACTO = this._contacto, UNID_TRANSPORTE = this._transporte.UNID_TRANSPORTE, IS_ACTIVE = this._isActive, DIRECCION_ENVIO = this._direccionEnvio, SITIO_ENLACE = this._sitioEnlace, NOMBRE_SITIO = this._nombreSitio, RECIBE = this._recibe, GUIA = this._guia, UNID_CLIENTE = this._cliente.UNID_CLIENTE, UNID_PROVEEDOR = this._proveedor.UNID_PROVEEDOR, UNID_FACTURA_VENTA = this._facturaVenta.UNID_FACTURA_VENTA });
                //_dataMapper.udpateElement(new ULTIMO_MOVIMIENTO { UNID_ITEM = this._unidItem, UNID_ALMACEN = this._unidAlmacen, UNID_CLIENTE = this.UnidCliente, UNID_MOVIMIENTO_DETALLE = this._unidMovimientoDetalle, UNID_PROVEEDOR = this._unidProveedor, UNID_INFRAESTRUCTURA = this._unidInfraestructura, IS_ACTIVE = true, CANTIDAD = this._cantidad });
                //_dataMapper.udpateElement2(new ULTIMO_MOVIMIENTO { UNID_ITEM = this._unidItem, UNID_ALMACEN = this._unidAlmacen, UNID_CLIENTE = this.UnidCliente, UNID_MOVIMIENTO_DETALLE = this._unidMovimientoDetalle, UNID_PROVEEDOR = this._unidProveedor, UNID_INFRAESTRUCTURA = this._unidInfraestructura, IS_ACTIVE = true, CANTIDAD = this._cantidad });
            }
        }

        public void saveArticuloBaja()
        {
            if (_dataMapper != null)
            {
                ITEM item = new ITEM();
                item.UNID_ITEM = this.UnidItem;
                ULTIMO_MOVIMIENTO tmp = new ULTIMO_MOVIMIENTO();

                if (this._unidAlmacen != null)
                {
                    ALMACEN almacen = new ALMACEN();
                    almacen.UNID_ALMACEN = (long)this._unidAlmacen;
                    tmp = this._dataMapper.getCantidadItemsBaja(item, almacen,"salida baja");
                }
                
                this._cantidad += tmp.CANTIDAD;

                if (tmp.UNID_ULTIMO_MOVIMIENTO == 0)
                {
                    _dataMapper.insertElementBaja(new ULTIMO_MOVIMIENTO { UNID_ITEM = this._unidItem, UNID_ALMACEN = this._unidAlmacen, UNID_CLIENTE = this.UnidCliente, UNID_MOVIMIENTO_DETALLE = this._unidMovimientoDetalle, UNID_PROVEEDOR = this._unidProveedor, UNID_INFRAESTRUCTURA = this._unidInfraestructura, IS_ACTIVE = false, CANTIDAD = this._cantidad, UNID_ITEM_STATUS = this._unidItemStatus });
                }
                else
                {
                    _dataMapper.udpateElement2Baja(new ULTIMO_MOVIMIENTO { UNID_ULTIMO_MOVIMIENTO = tmp.UNID_ULTIMO_MOVIMIENTO, UNID_ITEM = this._unidItem, UNID_ALMACEN = this._unidAlmacen, UNID_CLIENTE = this.UnidCliente, UNID_MOVIMIENTO_DETALLE = this._unidMovimientoDetalle, UNID_PROVEEDOR = this._unidProveedor, UNID_INFRAESTRUCTURA = this._unidInfraestructura, IS_ACTIVE = false, CANTIDAD = this._cantidad, UNID_ITEM_STATUS = this._unidItemStatus });
                }

                
            }
        }

        public UltimoMovimientoModel(IDataMapper dataMapper, long item, long? almacen, long? cliente, long? proveedor, long? movDet)
        {
            this._unidItem = item;
            this._unidAlmacen = almacen;
            this._unidCliente = cliente;
            this._unidProveedor = proveedor;
            this._unidInfraestructura = null;
            this._unidMovimientoDetalle = movDet;

            if ((dataMapper as UltimoMovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as UltimoMovimientoDataMapper;
            }
        }

        public UltimoMovimientoModel(ALMACEN almacen, CLIENTE cliente, PROVEEDOR proveedor)
        {
            this._dataMapper = new UltimoMovimientoDataMapper();
            this._isChecked = false;
            if (almacen != null)
            {
                this._unidAlmacen = almacen.UNID_ALMACEN;
                this.Lugar = "Almacen: " + almacen.ALMACEN_NAME;
            }

            if (proveedor != null)
            {
                this._unidProveedor = proveedor.UNID_PROVEEDOR;
                this.Lugar = "Proveedor: " + proveedor.PROVEEDOR_NAME;
            }

            if (cliente != null)
            {
                this._unidCliente = cliente.UNID_CLIENTE;
                this.Lugar = "Cliente: " + cliente.CLIENTE1;
            }
        }

        //salida baja
        public UltimoMovimientoModel(IDataMapper dataMapper, long item, long? almacen, long? cliente, long? proveedor, long? movDet, int cantidad, long? unidItemStatus, string salidaBaja)
        {
            this._unidItem = item;
            this._unidAlmacen = almacen;
            this._unidCliente = cliente;
            this._unidProveedor = proveedor;
            this._unidInfraestructura = null;
            this._unidMovimientoDetalle = movDet;
            this._cantidad = cantidad;
            this._unidItemStatus = unidItemStatus;
            if ((dataMapper as UltimoMovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as UltimoMovimientoDataMapper;
            }
        }

        public UltimoMovimientoModel(IDataMapper dataMapper, long item, long? almacen, long? cliente, long? proveedor, long? movDet, int cantidad, long? unidItemStatus)
        {
            this._unidItem = item;
            this._unidAlmacen = almacen;
            this._unidCliente = cliente;
            this._unidProveedor = proveedor;
            this._unidInfraestructura = null;
            this._unidMovimientoDetalle = movDet;
            this._cantidad = cantidad;
            this._unidItemStatus = unidItemStatus;
            if ((dataMapper as UltimoMovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as UltimoMovimientoDataMapper;
            }
        }

        public UltimoMovimientoModel(IDataMapper dataMapper, long item, long? movDet, int cantidad, long? infra, long? itemStatus)
        {
            this._unidItem = item;
            this._unidAlmacen = null;
            this._unidCliente = null;
            this._unidProveedor = null;
            this._unidInfraestructura = infra;
            this._unidMovimientoDetalle = movDet;
            this._cantidad = cantidad;
            this._unidItemStatus = itemStatus;
            if ((dataMapper as UltimoMovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as UltimoMovimientoDataMapper;
            }
        }

        public UltimoMovimientoModel(IDataMapper dataMapper, long item, long? infraestructura, long? movDet)
        {
            this._unidItem = item;
            this._unidAlmacen = null;
            this._unidCliente = null;
            this._unidProveedor = null;
            this._unidInfraestructura = infraestructura;
            this._unidMovimientoDetalle = movDet;
            if ((dataMapper as UltimoMovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as UltimoMovimientoDataMapper;
            }
        }

        public UltimoMovimientoModel()
        {
            this._dataMapper = new UltimoMovimientoDataMapper();
        }

        public ObservableCollection<UltimoMovimientoModel> RegresaListaLugares(long? unidItem)
        {
            ITEM item = new ITEM();
            item.UNID_ITEM = (long)unidItem;
            ObservableCollection<UltimoMovimientoModel> movimientos = new ObservableCollection<UltimoMovimientoModel>();
            List<ULTIMO_MOVIMIENTO> movimientoaux = new List<ULTIMO_MOVIMIENTO>();
            this._dataMapper = new UltimoMovimientoDataMapper();
            movimientoaux = this._dataMapper.getCantidadItems(item);

            foreach (ULTIMO_MOVIMIENTO um in movimientoaux)
            {
                UltimoMovimientoModel temp = new UltimoMovimientoModel(um.ALMACEN, um.CLIENTE, um.PROVEEDOR);
                temp._cantidad = um.CANTIDAD;
                temp.ItemStatus2 = new DeleteItemStatus(um.ITEM_STATUS);
                movimientos.Add(temp);
            }

            return movimientos;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
