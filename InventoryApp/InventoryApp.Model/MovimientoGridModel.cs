using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class MovimientoGridModel : INotifyPropertyChanged
    {
        #region Fields        
        
        private ALMACEN ALMACEN;
        private ALMACEN ALMACEN1;
        private CLIENTE CLIENTE;
        private CLIENTE CLIENTE1;
        private CLIENTE CLIENTE2;
        private FACTURA_VENTA FACTURA_VENTA;
        private DateTime FECHA_MOVIMIENTO;
        private MOVIMIENTO_DETALLE MOVIMIENTO_DETALLE;
        private PROVEEDOR PROVEEDOR;
        private PROVEEDOR PROVEEDOR1;
        private PROVEEDOR PROVEEDOR2;
        private SERVICIO SERVICIO;
        private SOLICITANTE SOLICITANTE;
        private TIPO_MOVIMIENTO TIPO_MOVIMIENTO;
        private TRANSPORTE TRANSPORTE;
        private int _totalItems;
        private bool IS_ACTIVE;
        private string CONTACTO;
        private string DIRECCION_ENVIO;
        private string GUIA;
        private string NOMBRE_SITIO;
        private string PEDIMIENTO_EXPO;
        private string PEDIMIENTO_IMPO;
        private string RECIBE;
        private string TT;
        private string SITIO_ENLACE;
        private long? UNID_ALMACEN_DESTINO;
        private long? UNID_ALMACEN_PROCEDENCIA;
        private long? UNID_CLIENTE;
        private long? UNID_CLIENTE_DESTINO;
        private long? UNID_CLIENTE_PROCEDENCIA;
        private long? UNID_FACTURA_VENTA;
        private long? UNID_MOVIMIENTO;
        private long? UNID_PROVEEDOR;
        private long? UNID_PROVEEDOR_DESTINO;
        private long? UNID_PROVEEDOR_PROCEDENCIA;
        private long? UNID_SERVICIO;
        private long? UNID_SOLICITANTE;
        private long? UNID_TIPO_MOVIMIENTO;
        private long? UNID_TRANSPORTE;
        private MovimientoDataMapper _dataMapper;

        #endregion

        #region Props
        

        public ALMACEN AlmacenDestino
        {
            get
            {
                return ALMACEN;
            }
            set
            {
                if (ALMACEN != value)
                {
                    ALMACEN = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("AlmacenDestino"));
                    }
                }
            }
        }
        public ALMACEN AlmacenProcedencia
        {
            get
            {
                return ALMACEN1;
            }
            set
            {
                if (ALMACEN1 != value)
                {
                    ALMACEN1 = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("AlmacenProcedencia"));
                    }
                }
            }
        }
        public CLIENTE ClienteDestino
        {
            get
            {
                return CLIENTE;
            }
            set
            {
                if (CLIENTE != value)
                {
                    CLIENTE = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteDestino"));
                    }
                }
            }
        }        
        public CLIENTE ClienteProcedencia
        {
            get
            {
                return CLIENTE1;
            }
            set
            {
                if (CLIENTE1 != value)
                {
                    CLIENTE1 = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteProcedencia"));
                    }
                }
            }
        }
        public CLIENTE Cliente
        {
            get
            {
                return CLIENTE2;
            }
            set
            {
                if (CLIENTE2 != value)
                {
                    CLIENTE2 = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Cliente"));
                    }
                }
            }
        }
        public FACTURA_VENTA FacturaVenta
        {
            get
            {
                return FACTURA_VENTA;
            }
            set
            {
                if (FACTURA_VENTA != value)
                {
                    FACTURA_VENTA = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("FacturaVenta"));
                    }
                }
            }
        }
        public DateTime FechaMovimiento
        {
            get
            {
                return FECHA_MOVIMIENTO;
            }
            set
            {
                if (FECHA_MOVIMIENTO != value)
                {
                    FECHA_MOVIMIENTO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("FacturaVenta"));
                    }
                }
            }
        }
        public MOVIMIENTO_DETALLE MovimientoDetalle
        {
            get
            {
                return MOVIMIENTO_DETALLE;
            }
            set
            {
                if (MOVIMIENTO_DETALLE != value)
                {
                    MOVIMIENTO_DETALLE = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("MovimientoDetalle"));
                    }
                }
            }
        }
        public PROVEEDOR ProveedorDestino
        {
            get
            {
                return PROVEEDOR;
            }
            set
            {
                if (PROVEEDOR != value)
                {
                    PROVEEDOR = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProveedorDestino"));
                    }
                }
            }
        }
        public PROVEEDOR ProveedorProcedencia
        {
            get
            {
                return PROVEEDOR1;
            }
            set
            {
                if (PROVEEDOR1 != value)
                {
                    PROVEEDOR1 = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProveedorProcedencia"));
                    }
                }
            }
        }
        public PROVEEDOR Proveedor
        {
            get
            {
                return PROVEEDOR;
            }
            set
            {
                if (PROVEEDOR != value)
                {
                    PROVEEDOR = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Proveedor"));
                    }
                }
            }
        }
        public SERVICIO Servicio
        {
            get
            {
                return SERVICIO;
            }
            set
            {
                if (SERVICIO != value)
                {
                    SERVICIO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Servicio"));
                    }
                }
            }
        }
        public SOLICITANTE Solicitante
        {
            get
            {
                return SOLICITANTE;
            }
            set
            {
                if (SOLICITANTE != value)
                {
                    SOLICITANTE = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Solicitante"));
                    }
                }
            }
        }
        public TIPO_MOVIMIENTO TipoMovimiento
        {
            get
            {
                return TIPO_MOVIMIENTO;
            }
            set
            {
                if (TIPO_MOVIMIENTO != value)
                {
                    TIPO_MOVIMIENTO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoMovimiento"));
                    }
                }
            }
        }
        public TRANSPORTE Transporte
        {
            get
            {
                return TRANSPORTE;
            }
            set
            {
                if (TRANSPORTE != value)
                {
                    TRANSPORTE = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Transporte"));
                    }
                }
            }
        }
        public int TotalItems
        {
            get
            {
                return _totalItems;
            }
            set
            {
                if (_totalItems != value)
                {
                    _totalItems = value;
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
                return IS_ACTIVE;
            }
            set
            {
                if (IS_ACTIVE != value)
                {
                    IS_ACTIVE = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsActive"));
                    }
                }
            }
        }
        
        public string Contacto
        {
            get
            {
                return CONTACTO;
            }
            set
            {
                if (CONTACTO != value)
                {
                    CONTACTO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Contacto"));
                    }
                }
            }
        }
        public string DireccionEnvio
        {
            get
            {
                return DIRECCION_ENVIO;
            }
            set
            {
                if (DIRECCION_ENVIO != value)
                {
                    DIRECCION_ENVIO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("DireccionEnvio"));
                    }
                }
            }
        }
        public string Guia
        {
            get
            {
                return GUIA;
            }
            set
            {
                if (GUIA != value)
                {
                    GUIA = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Guia"));
                    }
                }
            }
        }
        public string NombreSitio
        {
            get
            {
                return NOMBRE_SITIO;
            }
            set
            {
                if (NOMBRE_SITIO != value)
                {
                    NOMBRE_SITIO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("NombreSitio"));
                    }
                }
            }
        }
        public string PedimentoExpo
        {
            get
            {
                return PEDIMIENTO_EXPO;
            }
            set
            {
                if (PEDIMIENTO_EXPO != value)
                {
                    PEDIMIENTO_EXPO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PedimentoExpo"));
                    }
                }
            }
        }
        public string PedimentoImpo
        {
            get
            {
                return PEDIMIENTO_IMPO;
            }
            set
            {
                if (PEDIMIENTO_IMPO != value)
                {
                    PEDIMIENTO_IMPO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PedimentoImpo"));
                    }
                }
            }
        }
        public string Recibe
        {
            get
            {
                return RECIBE;
            }
            set
            {
                if (RECIBE != value)
                {
                    RECIBE = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Recibe"));
                    }
                }
            }
        }
        public string Tt
        {
            get
            {
                return TT;
            }
            set
            {
                if (TT != value)
                {
                    TT = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Tt"));
                    }
                }
            }
        }
        public string SitioEnlace
        {
            get
            {
                return SITIO_ENLACE;
            }
            set
            {
                if (SITIO_ENLACE != value)
                {
                    SITIO_ENLACE = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("SitioEnlace"));
                    }
                }
            }
        }
        public long? UnidAlmacenDestino
        {
            get
            {
                return UNID_ALMACEN_DESTINO;
            }
            set
            {
                if (UNID_ALMACEN_DESTINO != value)
                {
                    UNID_ALMACEN_DESTINO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidAlmacenDestino"));
                    }
                }
            }
        }
        public long? UnidAlmacenProcedencia
        {
            get
            {
                return UNID_ALMACEN_PROCEDENCIA;
            }
            set
            {
                if (UNID_ALMACEN_PROCEDENCIA != value)
                {
                    UNID_ALMACEN_PROCEDENCIA = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidAlmacenProcedencia"));
                    }
                }
            }
        }
        public long? UnidCliente
        {
            get
            {
                return UNID_CLIENTE;
            }
            set
            {
                if (UNID_CLIENTE != value)
                {
                    UNID_CLIENTE = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidCliente"));
                    }
                }
            }
        }
        public long? UnidClienteDestino
        {
            get
            {
                return UNID_CLIENTE_DESTINO;
            }
            set
            {
                if (UNID_CLIENTE_DESTINO != value)
                {
                    UNID_CLIENTE_DESTINO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidClienteDestino"));
                    }
                }
            }
        }
        public long? UnidClienteProcedencia
        {
            get
            {
                return UNID_CLIENTE_PROCEDENCIA;
            }
            set
            {
                if (UNID_CLIENTE_PROCEDENCIA != value)
                {
                    UNID_CLIENTE_PROCEDENCIA = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidClienteProcedencia"));
                    }
                }
            }
        }
        public long? UnidFacturaVenta
        {
            get
            {
                return UNID_FACTURA_VENTA;
            }
            set
            {
                if (UNID_FACTURA_VENTA != value)
                {
                    UNID_FACTURA_VENTA = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidFacturaVenta"));
                    }
                }
            }
        }
        public long? UnidMovimiento
        {
            get
            {
                return UNID_MOVIMIENTO;
            }
            set
            {
                if (UNID_MOVIMIENTO != value)
                {
                    UNID_MOVIMIENTO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidMovimiento"));
                    }
                }
            }
        }
        public long? UnidProveedor
        {
            get
            {
                return UNID_PROVEEDOR;
            }
            set
            {
                if (UNID_PROVEEDOR != value)
                {
                    UNID_PROVEEDOR = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidProveedor"));
                    }
                }
            }
        }
        public long? UnidProveedorDestino
        {
            get
            {
                return UNID_PROVEEDOR_DESTINO;
            }
            set
            {
                if (UNID_PROVEEDOR_DESTINO != value)
                {
                    UNID_PROVEEDOR_DESTINO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidProveedorDestino"));
                    }
                }
            }
        }
        public long? UnidProveedorProcedencia
        {
            get
            {
                return UNID_PROVEEDOR_PROCEDENCIA;
            }
            set
            {
                if (UNID_PROVEEDOR_PROCEDENCIA != value)
                {
                    UNID_PROVEEDOR_PROCEDENCIA = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidProveedorProcedencia"));
                    }
                }
            }
        }
        public long? UnidServicio
        {
            get
            {
                return UNID_SERVICIO;
            }
            set
            {
                if (UNID_SERVICIO != value)
                {
                    UNID_SERVICIO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidServicio"));
                    }
                }
            }
        }
        public long? UnidSolicitante
        {
            get
            {
                return UNID_SOLICITANTE;
            }
            set
            {
                if (UNID_SOLICITANTE != value)
                {
                    UNID_SOLICITANTE = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidSolicitante"));
                    }
                }
            }
        }
        public long? UnidTipoMovimiento
        {
            get
            {
                return UNID_TIPO_MOVIMIENTO;
            }
            set
            {
                if (UNID_TIPO_MOVIMIENTO != value)
                {
                    UNID_TIPO_MOVIMIENTO = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTipoMovimiento"));
                    }
                }
            }
        }
        public long? UnidTransporte
        {
            get
            {
                return UNID_TRANSPORTE;
            }
            set
            {
                if (UNID_TRANSPORTE != value)
                {
                    UNID_TRANSPORTE = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTransporte"));
                    }
                }
            }
        }

        #endregion       

        #region Constructors
        public MovimientoGridModel(IDataMapper dataMapper)
        {
            if ((dataMapper as MovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as MovimientoDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
