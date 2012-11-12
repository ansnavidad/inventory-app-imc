using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;


namespace InventoryApp.Model
{
    public class MovimientoModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidMovimiento;
        private DateTime _fechaMovimiento;
        private TIPO_MOVIMIENTO _tipoMovimiento;
        private ALMACEN _almacenDestino;
        private long? _unidAlmacenDestino;
        private PROVEEDOR _proveedorDestino;
        private long? _unidProvedorDestino;
        private CLIENTE _clienteDestino;
        private long? _unidClienteDestino;
        private PROVEEDOR _proveedorProcedencia;
        private long? _unidProveedorProcedencia;
        private CLIENTE _clienteProcedencia;
        private long? _unidClienteProcedencia;
        private ALMACEN _almacenProcedencia;
        private long? _unidAlmacenProcedencia;
        private SERVICIO _servicio;
        private long? _unidServicio;
        private string _tt;
        private string _contacto;
        private TRANSPORTE _transporte;
        private long? _unidTransporte;
        private bool _isActive;
        private string _direccionEnvio;
        private string _sitioEnlace;
        private string _nombreSitio;
        private string _recibe;
        private string _guia;
        private CLIENTE _cliente;
        private long? _unidCliente;
        private PROVEEDOR _proveedor;
        private long? _unidProveedor;
        private FACTURA_VENTA _facturaVenta;
        private long? _unidFacturaVenta;
        private SOLICITANTE _solicitante;
        private long? _unidSolicitante;
        private MovimientoDataMapper _dataMapper;

        #endregion

        #region Props

        public SOLICITANTE Solicitante
        {
            get
            {
                return _solicitante;
;
            }
            set
            {
                if (_solicitante != value)
                {
                    _solicitante = value;
                    this._unidSolicitante = _solicitante.UNID_SOLICITANTE;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Solicitante"));
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

        public DateTime FechaMovimiento
        {
            get
            {
                return _fechaMovimiento;
            }
            set
            {
                if (_fechaMovimiento != value)
                {
                    _fechaMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("FechaMovimiento"));
                    }
                }
            }
        }

        public TIPO_MOVIMIENTO TipoMovimiento
        {
            get
            {
                return _tipoMovimiento;
            }
            set
            {
                if (_tipoMovimiento != value)
                {
                    _tipoMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoMovimiento"));
                    }
                }
            }
        }

        public ALMACEN AlmacenDestino
        {
            get
            {
                return _almacenDestino;
            }
            set
            {
                if (_almacenDestino != value)
                {
                    _almacenDestino = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("AlmacenDestino"));
                    }
                }
            }
        }

        public PROVEEDOR ProveedorDestino
        {
            get
            {
                return _proveedorDestino;
            }
            set
            {
                if (_proveedorDestino != value)
                {
                    _proveedorDestino = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProveedorDestino"));
                    }
                }
            }
        }

        public CLIENTE ClienteDestino
        {
            get
            {
                return _clienteDestino;
            }
            set
            {
                if (_clienteDestino != value)
                {
                    _clienteDestino = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteDestino"));
                    }
                }
            }
        }

        public PROVEEDOR ProveedorProcedencia
        {
            get
            {
                return _proveedorProcedencia;
            }
            set
            {
                if (_proveedorProcedencia != value)
                {
                    _proveedorProcedencia = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProveedorProcedencia"));
                    }
                }
            }
        }

        public CLIENTE ClienteProcedencia
        {
            get
            {
                return _clienteProcedencia;
            }
            set
            {
                if (_clienteProcedencia != value)
                {
                    _clienteProcedencia = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteProcedencia"));
                    }
                }
            }
        }

        public ALMACEN AlmacenProcedencia
        {
            get
            {
                return _almacenProcedencia;
            }
            set
            {
                if (_almacenProcedencia != value)
                {
                    _almacenProcedencia = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("AlmacenProcenencia"));
                    }
                }
            }
        }

        public SERVICIO Servicio
        {
            get
            {
                return _servicio;
            }
            set
            {
                if (_servicio != value)
                {
                    _servicio = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Servicio"));
                    }
                }
            }
        }

        public string Tt
        {
            get
            {
                return _tt;
            }
            set
            {
                if (_tt != value)
                {
                    _tt = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Tt"));
                    }
                }
            }
        }

        public string Contacto
        {
            get
            {
                return _contacto;
            }
            set
            {
                if (_contacto != value)
                {
                    _contacto = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Contacto"));
                    }
                }
            }
        }

        public TRANSPORTE Transporte
        {
            get
            {
                return _transporte;
            }
            set
            {
                if (_transporte != value)
                {
                    _transporte = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Transporte"));
                    }
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsActive"));
                    }
                }
            }
        }

        public string DireccionEnvio
        {
            get
            {
                return _direccionEnvio;
            }
            set
            {
                if (_direccionEnvio != value)
                {
                    _direccionEnvio = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("DireccionEnvio"));
                    }
                }
            }
        }

        public string SitioEnlace
        {
            get
            {
                return _sitioEnlace;
            }
            set
            {
                if (_sitioEnlace != value)
                {
                    _sitioEnlace = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("SitioEnlace"));
                    }
                }
            }
        }

        public string NombreSitio
        {
            get
            {
                return _nombreSitio;
            }
            set
            {
                if (_nombreSitio != value)
                {
                    _nombreSitio = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("NombreSitio"));
                    }
                }
            }
        }

        public string Recibe
        {
            get
            {
                return _recibe;
            }
            set
            {
                if (_recibe != value)
                {
                    _recibe = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Recibe"));
                    }
                }
            }
        }

        public string Guia
        {
            get
            {
                return _guia;
            }
            set
            {
                if (_guia != value)
                {
                    _guia = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Guia"));
                    }
                }
            }
        }

        public CLIENTE Cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                if (_cliente != value)
                {
                    _cliente = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Cliente"));
                    }
                }
            }
        }

        public PROVEEDOR Proveedor
        {
            get
            {
                return _proveedor;
            }
            set
            {
                if (_proveedor != value)
                {
                    _proveedor = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Proveedor"));
                    }
                }
            }
        }

        public FACTURA_VENTA FacturaVenta
        {
            get
            {
                return _facturaVenta;
            }
            set
            {
                if (_facturaVenta != value)
                {
                    _facturaVenta = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("FacturaVenta"));
                    }
                }
            }
        }

        #endregion

        public void saveArticulo()
        {
            if (_dataMapper != null)
            {
                //_dataMapper.insertElement(new MOVIMENTO() { UNID_MOVIMIENTO = this._unidMovimiento, FECHA_MOVIMIENTO = this._fechaMovimiento, UNID_TIPO_MOVIMIENTO = this._tipoMovimiento.UNID_TIPO_MOVIMIENTO, UNID_ALMACEN_DESTINO = this._almacenDestino.UNID_ALMACEN, UNID_PROVEEDOR_DESTINO = this._proveedorDestino.UNID_PROVEEDOR, UNID_CLIENTE_DESTINO = this._clienteDestino.UNID_CLIENTE, UNID_ALMACEN_PROCEDENCIA = this._almacenProcedencia.UNID_ALMACEN, UNID_CLIENTE_PROCEDENCIA = this._clienteProcedencia.UNID_CLIENTE, UNID_PROVEEDOR_PROCEDENCIA = this._proveedorProcedencia.UNID_PROVEEDOR, UNID_SERVICIO = this._servicio.UNID_SERVICIO, TT = this._tt, CONTACTO = this._contacto, UNID_TRANSPORTE = this._transporte.UNID_TRANSPORTE, IS_ACTIVE = this._isActive, DIRECCION_ENVIO = this._direccionEnvio, SITIO_ENLACE = this._sitioEnlace, NOMBRE_SITIO = this._nombreSitio, RECIBE = this._recibe, GUIA = this._guia, UNID_CLIENTE = this._cliente.UNID_CLIENTE, UNID_PROVEEDOR = this._proveedor.UNID_PROVEEDOR, UNID_FACTURA_VENTA = this._facturaVenta.UNID_FACTURA_VENTA });
                _dataMapper.insertElement(new MOVIMENTO() {UNID_MOVIMIENTO = this._unidMovimiento, FECHA_MOVIMIENTO = this._fechaMovimiento, UNID_TIPO_MOVIMIENTO = this._tipoMovimiento.UNID_TIPO_MOVIMIENTO,  TT = this._tt,IS_ACTIVE = this._isActive, RECIBE = this._recibe, UNID_ALMACEN_DESTINO = this._unidSolicitante});
          
            }
        }

        #region Constructors
        public MovimientoModel(IDataMapper dataMapper)
        {
            this._unidMovimiento = UNID.getNewUNID();
            this._fechaMovimiento = DateTime.Now;
            this._isActive = true;
            if ((dataMapper as MovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as MovimientoDataMapper;
            }
            
            this._almacenDestino = new ALMACEN();
            this._proveedorDestino = new PROVEEDOR();
            this._clienteDestino = new CLIENTE();
            this._proveedorProcedencia = new PROVEEDOR();
            this._clienteProcedencia = new CLIENTE();
            this._almacenProcedencia = new ALMACEN();
            this._servicio = new SERVICIO();
            this._tt = " ";
           this._contacto =" ";
           this._transporte = new TRANSPORTE();
           
          this._direccionEnvio = " ";
          this._sitioEnlace = " ";
          this._nombreSitio = " ";
          this._recibe = " ";
          this._guia = " ";
          this._cliente = new CLIENTE();
          this._proveedor = new PROVEEDOR();
          this._facturaVenta = new FACTURA_VENTA();
          this._solicitante = new SOLICITANTE();
        
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
