using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class DeleteMovimiento: MOVIMENTO, INotifyPropertyChanged
    {
        private int _totalItems;
        private string _fecha;
        private string _destino;
        private string _procedencia;
        private string _tipoMovimiento;

        private MovimientoDetalleDataMapper _dataMapperArticulos;
        object articulosLectura;
        private FixupCollection<ItemModel> _articulosLectura;
        private long _unidMovimiento;
        private TIPO_MOVIMIENTO _tipoMovimientos;
        private ALMACEN _almacenDestino;
        private PROVEEDOR _proveedorDestino;
        private CLIENTE _clienteDestino;
        private ALMACEN _almacenProcedencia;
        private CLIENTE _clienteProcedencia;
        private PROVEEDOR _proveedorProcedenia;
        private SERVICIO _servicio;
        private string _tt;
        private string _contacto;
        private TRANSPORTE _transporte;
        private string _sitioEnlace;
        private string _nombreSitio;
        private string _recibe;
        private string _guia;
        private CLIENTE _unidCliente;
        private PROVEEDOR _unidProveedor;
        private FACTURA_VENTA _unidFacturaVenta;
        private string _pedimentoExpo;
        private string _pedimentoImpo;
        private SOLICITANTE _unidSolicitante;
        private TECNICO _unidTecnico;
        private INFRAESTRUCTURA _unidInfraestructura;
        private TECNICO _unidTecnicoTrans;
        private DateTime _timeFecha;
        public FixupCollection<ItemModel> ArticulosLectura
        {
            get
            {
                return _articulosLectura;
            }
            set
            {
                if (_articulosLectura != value)
                {
                    _articulosLectura = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ArticulosLectura"));
                    }
                }
            }
        }

        public long UnidMovimiento
        {
            get { return this._unidMovimiento; }
            set
            {
                if (value != this._unidMovimiento)
                {
                    this._unidMovimiento = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidMovimiento"));
                }
            }
        }
        public DateTime TimeFecha
        {
            get { return this._timeFecha; }
            set
            {
                if (value != this._timeFecha)
                {
                    this._timeFecha = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TimeFecha"));
                }
            }
        }
        public TIPO_MOVIMIENTO TipoMovientos
        {
            get { return this._tipoMovimientos; }
            set
            {
                if (value != this._tipoMovimientos)
                {
                    this._tipoMovimientos = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoMovientos"));
                }
            }
        }
        public ALMACEN AlmacenDestino
        {
            get { return this._almacenDestino; }
            set
            {
                if (value != this._almacenDestino)
                {
                    this._almacenDestino = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("AlmacenDestino"));
                }
            }
        }
        public PROVEEDOR ProveedorDestino
        {
            get { return this._proveedorDestino; }
            set
            {
                if (value != this._proveedorDestino)
                {
                    this._proveedorDestino = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProveedorDestino"));
                }
            }
        }
        public CLIENTE ClienteDestino
        {
            get { return this._clienteDestino; }
            set
            {
                if (value != this._clienteDestino)
                {
                    this._clienteDestino = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteDestino"));
                }
            }
        }
        public ALMACEN AlmacenProcedencia
        {
            get { return this._almacenProcedencia; }
            set
            {
                if (value != this._almacenProcedencia)
                {
                    this._almacenProcedencia = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("AlmacenProcedencia"));
                }
            }
        }
        public CLIENTE ClienteProcedencia
        {
            get { return this._clienteProcedencia; }
            set
            {
                if (value != this._clienteProcedencia)
                {
                    this._clienteProcedencia = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteProcedencia"));
                }
            }
        }
        public PROVEEDOR ProveedorProcedenia
        {
            get { return this._proveedorProcedenia; }
            set
            {
                if (value != this._proveedorProcedenia)
                {
                    this._proveedorProcedenia = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProveedorProcedenia"));
                }
            }
        }
        public SERVICIO Servicio
        {
            get { return this._servicio; }
            set
            {
                if (value != this._servicio)
                {
                    this._servicio = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Servicio"));
                }
            }
        }
        public string Tt
        {
            get { return this._tt; }
            set
            {
                if (value != this._tt)
                {
                    this._tt = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Tt"));
                }
            }
        }
        public string Contacto
        {
            get { return this._contacto; }
            set
            {
                if (value != this._contacto)
                {
                    this._contacto = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Contacto"));
                }
            }
        }
        public TRANSPORTE Transporte
        {
            get { return this._transporte; }
            set
            {
                if (value != this._transporte)
                {
                    this._transporte = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Transporte"));
                }
            }
        }
        public string SitioEnlace
        {
            get { return this._sitioEnlace; }
            set
            {
                if (value != this._sitioEnlace)
                {
                    this._sitioEnlace = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("SitioEnlace"));
                }
            }
        }
        public string NombreSitio
        {
            get { return this._nombreSitio; }
            set
            {
                if (value != this._nombreSitio)
                {
                    this._nombreSitio = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("NombreSitio"));
                }
            }
        }
        public string Recibe
        {
            get { return this._recibe; }
            set
            {
                if (value != this._recibe)
                {
                    this._recibe = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Recibe"));
                }
            }
        }
        public string Guia
        {
            get { return this._guia; }
            set
            {
                if (value != this._guia)
                {
                    this._guia = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Guia"));
                }
            }
        }
        public CLIENTE UnidCliente
        {
            get { return this._unidCliente; }
            set
            {
                if (value != this._unidCliente)
                {
                    this._unidCliente = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidCliente"));
                }
            }
        }
        public PROVEEDOR UnidProveedor
        {
            get { return this._unidProveedor; }
            set
            {
                if (value != this._unidProveedor)
                {
                    this._unidProveedor = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidProveedor"));
                }
            }
        }
        public FACTURA_VENTA UnidFacturaVenta
        {
            get { return this._unidFacturaVenta; }
            set
            {
                if (value != this._unidFacturaVenta)
                {
                    this._unidFacturaVenta = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidFacturaVenta"));
                }
            }
        }
        public string PedimentoExpo
        {
            get { return this._pedimentoExpo; }
            set
            {
                if (value != this._pedimentoExpo)
                {
                    this._pedimentoExpo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PedimentoExpo"));
                }
            }
        }
        public string PedimentoImpo
        {
            get { return this._pedimentoImpo; }
            set
            {
                if (value != this._pedimentoImpo)
                {
                    this._pedimentoImpo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PedimentoImpo"));
                }
            }
        }
        public SOLICITANTE UnidSolicitante
        {
            get { return this._unidSolicitante; }
            set
            {
                if (value != this._unidSolicitante)
                {
                    this._unidSolicitante = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidSolicitante"));
                }
            }
        }      
        public TECNICO UnidTecnico
        {
            get { return this._unidTecnico; }
            set
            {
                if (value != this._unidTecnico)
                {
                    this._unidTecnico = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTecnico"));
                }
            }
        }
        public INFRAESTRUCTURA UnidInfraestructura
        {
            get { return this._unidInfraestructura; }
            set
            {
                if (value != this._unidInfraestructura)
                {
                    this._unidInfraestructura = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidInfraestructura"));
                }
            }
        }
        public TECNICO UnidTecnicoTrans
        {
            get { return this._unidTecnicoTrans; }
            set
            {
                if (value != this._unidTecnicoTrans)
                {
                    this._unidTecnicoTrans = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTecnicoTrans"));
                }
            }
        }

        public int TotalItems
        {
            get { return this._totalItems; }
            set
            {
                if (value != this._totalItems)
                {
                    this._totalItems = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TotalItems"));
                }
            }
        }
        public string Destino
        {
            get { return this._destino; }
            set
            {
                if (value != this._destino)
                {
                    this._destino = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Destino"));
                }
            }
        }
        public string Procedencia
        {
            get { return this._procedencia; }
            set
            {
                if (value != this._procedencia)
                {
                    this._procedencia = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Procedencia"));
                }
            }
        }
        public string TipoMovimiento
        {
            get { return this._tipoMovimiento; }
            set
            {
                if (value != this._tipoMovimiento)
                {
                    this._tipoMovimiento = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoMovimiento"));
                }
            }
        }
        public string Fecha
        {
            get { return this._fecha; }
            set
            {
                if (value != this._fecha)
                {
                    this._fecha = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Fecha"));
                }
            }
        }

        public DeleteMovimiento(MOVIMENTO m)
        {
            this._dataMapperArticulos = new MovimientoDetalleDataMapper();
            this._almacenDestino = new ALMACEN();
            this._almacenProcedencia = new ALMACEN();
            this._unidCliente = new CLIENTE();
            this._clienteDestino = new CLIENTE();
            this._clienteProcedencia = new CLIENTE();
            this._unidFacturaVenta = new FACTURA_VENTA();
            this._unidInfraestructura = new INFRAESTRUCTURA();
            this._unidProveedor = new PROVEEDOR();
            this._proveedorDestino = new PROVEEDOR();
            this._proveedorProcedenia = new PROVEEDOR();
            this._servicio = new SERVICIO();
            this._unidSolicitante = new SOLICITANTE();
            this._unidTecnico = new TECNICO();
            this._unidTecnicoTrans = new TECNICO();
            this._tipoMovimientos = new TIPO_MOVIMIENTO();
            this._transporte = new TRANSPORTE();
            this._unidTecnicoTrans = new TECNICO();

            
            if (m.UNID_ALMACEN_DESTINO!=null)
                this._almacenDestino.UNID_ALMACEN = (long)m.UNID_ALMACEN_DESTINO;

            if (m.UNID_ALMACEN_PROCEDENCIA != null)
                this._almacenProcedencia.UNID_ALMACEN = (long)m.UNID_ALMACEN_PROCEDENCIA;

            if (m.UNID_CLIENTE != null)
                this._unidCliente.UNID_CLIENTE = (long)m.UNID_CLIENTE;

            if (m.UNID_CLIENTE_DESTINO != null)
                this._clienteDestino.UNID_CLIENTE = (long)m.UNID_CLIENTE_DESTINO;

            if (m.UNID_CLIENTE_PROCEDENCIA != null)
                this._clienteProcedencia.UNID_CLIENTE = (long)m.UNID_CLIENTE_PROCEDENCIA;

            if (m.UNID_FACTURA_VENTA != null)
                this._unidFacturaVenta.UNID_FACTURA_VENTA = (long)m.UNID_FACTURA_VENTA;

            if (m.UNID_INFRAESTRUCTURA != null)
                this._unidInfraestructura.UNID_INFRAESTRUCTURA = (long)m.UNID_INFRAESTRUCTURA;

            if (m.UNID_MOVIMIENTO != null)
                this._unidMovimiento = m.UNID_MOVIMIENTO;

            if (m.UNID_PROVEEDOR != null)
                this._unidProveedor.UNID_PROVEEDOR = (long)m.UNID_PROVEEDOR;

            if (m.UNID_PROVEEDOR_DESTINO != null)
                this._proveedorDestino.UNID_PROVEEDOR = (long)m.UNID_PROVEEDOR_DESTINO;

            if (m.UNID_PROVEEDOR_PROCEDENCIA != null)
                this._proveedorProcedenia.UNID_PROVEEDOR = (long)m.UNID_PROVEEDOR_PROCEDENCIA;

            if (m.UNID_SERVICIO != null)
                this._servicio.UNID_SERVICIO = (long)m.UNID_SERVICIO;

            if (m.UNID_SOLICITANTE != null)
                this._unidSolicitante.UNID_SOLICITANTE = (long)m.UNID_SOLICITANTE;

            if (m.UNID_TECNICO != null)
                this._unidTecnico.UNID_TECNICO = (long)m.UNID_TECNICO;

            if (m.UNID_TECNICO_TRAS != null)
                this._unidTecnicoTrans.UNID_TECNICO = (long)m.UNID_TECNICO_TRAS;

            if (m.UNID_TIPO_MOVIMIENTO != null)
                this._tipoMovimientos.UNID_TIPO_MOVIMIENTO = m.UNID_TIPO_MOVIMIENTO;

            if (m.UNID_TRANSPORTE != null)
                this._transporte.UNID_TRANSPORTE = (long)m.UNID_TRANSPORTE;

            this._contacto = m.CONTACTO;
            this._guia = m.GUIA;
            this._sitioEnlace = m.SITIO_ENLACE;
            this._nombreSitio = m.NOMBRE_SITIO;
            this._tt = m.TT;
            this._timeFecha = m.FECHA_MOVIMIENTO;

            this.TotalItems = 0;
            FixupCollection<ItemModel> ic = new FixupCollection<ItemModel>();

            foreach (MOVIMIENTO_DETALLE detalle in m.MOVIMIENTO_DETALLE)
            {

                articulosLectura = this._dataMapperArticulos.getElementLectura(detalle);
                if (articulosLectura != null)
                {
                    foreach (MOVIMIENTO_DETALLE item in (List<MOVIMIENTO_DETALLE>)articulosLectura)
                    {
                        
                        ItemModel aux = new ItemModel(item.ITEM, detalle.CANTIDAD,item.ITEM_STATUS);

                        ic.Add(aux);

                    }
                    this.ArticulosLectura = ic;
                }
                
                this.TotalItems += detalle.CANTIDAD;
            }

            this._fecha = m.FECHA_MOVIMIENTO.Year + "/" + m.FECHA_MOVIMIENTO.Month + "/" + m.FECHA_MOVIMIENTO.Day;
            //this.Fecha = m.FECHA_MOVIMIENTO.ToString();

            if (m.ALMACEN != null)
                this._destino = "Almacén: " + m.ALMACEN.ALMACEN_NAME;
            else if (m.PROVEEDOR2 != null)
                this._destino = "Proveedor: " + m.PROVEEDOR2.PROVEEDOR_NAME;
            else if (m.CLIENTE2 != null)
                this._destino = "Cliente: " + m.CLIENTE2.CLIENTE1;
            else if (m.INFRAESTRUCTURA != null)
                this._destino = "Infraestructura: " + m.INFRAESTRUCTURA.INFRAESTRUCTURA_NAME;
            else
                this._destino = "";

            if (m.ALMACEN1 != null)
                this._procedencia = "Almacén: " + m.ALMACEN1.ALMACEN_NAME;
            else if (m.PROVEEDOR != null)
                this._procedencia = "Proveedor: " + m.PROVEEDOR.PROVEEDOR_NAME;
            else if (m.CLIENTE != null)
                this._procedencia = "Cliente: " + m.CLIENTE.CLIENTE1;
            else if (m.INFRAESTRUCTURA != null)
                this._procedencia = "Infraestructura: " + m.INFRAESTRUCTURA.INFRAESTRUCTURA_NAME;
            else
                this._procedencia = "";

            this._tipoMovimiento = m.TIPO_MOVIMIENTO.TIPO_MOVIMIENTO_NAME;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
