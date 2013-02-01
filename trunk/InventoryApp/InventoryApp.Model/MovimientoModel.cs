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
    public class MovimientoModel :INotifyPropertyChanged
    {
        #region Fields
        private long _unidMovimiento;
        private int _cantidaditems;
        private DateTime _fechaMovimiento;
        private TIPO_MOVIMIENTO _tipoMovimiento;
        private ALMACEN _almacenDestino;
        private long? _unidAlmacenDestino;
        
        private PROVEEDOR _proveedorProcedencia;
        private PROVEEDOR _proveedorProcedenciaLectura;
        private PROVEEDOR _proveedorDestinoLectura;
        private long? _unidProveedorProcedencia;
        private CLIENTE _clienteProcedencia;
        private CLIENTE _clienteProcedenciaLectura;
        private CLIENTE _clienteDestinoLectura;
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
        private FACTURA_VENTA _facturaVentaLectura;
        private long? _unidFacturaVenta;
        private DeleteSolicitante _solicitante;
        private long? _unidSolicitante;
        private TECNICO _tecnico;
        private TECNICO _tecnicoTrnas;
        private long? _unidTecnico;
        private MovimientoDataMapper _dataMapper;
        private EMPRESA _empresa;
        private INFRAESTRUCTURA _infraestructura;
        private long? _unidInfraestructura;
        private ObservableCollection<DeleteSolicitante> _solicitantes;
        private SOLICITANTE _solicitanteLectura;
        private EMPRESA _empresaLectura;
        private ALMACEN _almacenProcedenciaLectura;
        private DEPARTAMENTO _departamentoLectura;
        private ObservableCollection<TECNICO> _tecnicos;
        public float _totalLectura;
        #endregion

        #region Props

        public ObservableCollection<TECNICO> Tecnicos
        {
            get
            {
                return _tecnicos;
            }
            set
            {
                if (_tecnicos != value)
                {
                    _tecnicos = value;

                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Tecnicos"));
                    }
                }
            }
        }
        public EMPRESA Empresa
        {
            get
            {
                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                    this.Solicitantes = GetSolicitantesbyEmpresa(this._empresa);
                    this.Solicitante = Solicitantes[0];
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Empresa"));
                    }
                }
            }
        }

        public INFRAESTRUCTURA Infraestructura
        {
            get
            {
                return _infraestructura;
            }
            set
            {
                if (_infraestructura != value)
                {
                    _infraestructura = value;
                    if (value != null)
                        this._unidInfraestructura = value.UNID_INFRAESTRUCTURA;
                    else
                        this._unidInfraestructura = null;

                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Infraestructura"));
                    }
                }
            }
        }

        public ObservableCollection<DeleteSolicitante> Solicitantes
        {
            get
            {
                return _solicitantes;
            }
            set
            {
                if (_solicitantes != value)
                {
                    _solicitantes = value;

                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Solicitantes"));
                    }
                }
            }
        }

        public TECNICO Tecnico
        {
            get
            {
                return _tecnico;
            }
            set
            {
                if (_tecnico != value)
                {
                    _tecnico = value;
                    if (value != null)
                        this._unidTecnico = _tecnico.UNID_TECNICO;
                    else
                        this._unidTecnico = null;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Tecnico"));
                    }
                }
            }
        }
        public long? UnidTecnico
        {
            get
            {
                return _unidTecnico;
            }
            set
            {
                if (_unidTecnico != value)
                {
                    _unidTecnico = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTecnico"));
                    }
                }
            }
        }
        public DeleteSolicitante Solicitante
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
                    if (_solicitante != null)
                        this._unidSolicitante = _solicitante.UNID_SOLICITANTE;
                    else
                        this._unidSolicitante = null;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Solicitante"));
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
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidAlmacenProcedencia"));
                    }
                }
            }
        }

        public long? UnidAlmacenProcedencia
        {
            get
            {
                return _unidAlmacenProcedencia;
            }
            set
            {
                if (_unidAlmacenProcedencia != value)
                {
                    _unidAlmacenProcedencia = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidAlmacenProcedencia"));
                    }
                }
            }
        }
        public long? UnidClienteProcedencia
        {
            get
            {
                return _unidClienteProcedencia;
            }
            set
            {
                if (_unidClienteProcedencia != value)
                {
                    _unidClienteProcedencia = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidClienteProcedencia"));
                    }
                }
            }
        }
        public long? UnidProveedorProcedencia
        {
            get
            {
                return _unidProveedorProcedencia;
            }
            set
            {
                if (_unidProveedorProcedencia != value)
                {
                    _unidProveedorProcedencia = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidProveedorProcedencia"));
                    }
                }
            }
        }
        public long? UnidAlmacenDestino
        {
            get
            {
                return _unidAlmacenDestino;
            }
            set
            {
                if (_unidAlmacenDestino != value)
                {
                    _unidAlmacenDestino = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidAlmacenDestino"));
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

        public int CantidadItems
        {
            get
            {
                return _cantidaditems;
            }
            set
            {
                if (_cantidaditems != value)
                {
                    _cantidaditems = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("CantidadItems"));
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

                    this._unidAlmacenDestino = _almacenDestino.UNID_ALMACEN;

                    this.Tecnicos = GetTecnicosbyAlmacen(value);
                    if (this.Tecnicos.Count!=0)
                    {
                        this.Tecnico = Tecnicos[0];   
                    }
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("AlmacenDestino"));
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

                    this._clienteProcedencia = null;
                    this._unidClienteProcedencia = null;
                    this._almacenProcedencia = null;
                    this._unidAlmacenProcedencia = null;

                    this._unidProveedorProcedencia = ((_proveedorProcedencia != null) ? _proveedorProcedencia.UNID_PROVEEDOR : (long?)null);
                  

                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProveedorProcedencia"));
                    }
                }
            }
        }

        public PROVEEDOR ProveedorProcedenciaLectura
        {
            get { return this._proveedorProcedenciaLectura; }
            set
            {
                if (value != this._proveedorProcedenciaLectura)
                {
                    this._proveedorProcedenciaLectura = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProveedorProcedenciaLectura"));
                }
            }
        }

        public PROVEEDOR ProveedorDestinoLectura
        {
            get { return this._proveedorDestinoLectura; }
            set
            {
                if (value != this._proveedorDestinoLectura)
                {
                    this._proveedorDestinoLectura = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProveedorDestinoLectura"));
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

                    this._proveedorProcedencia = null;
                    this._unidProveedorProcedencia = null;
                    this._almacenProcedencia = null;
                    this._unidAlmacenProcedencia = null;

                    this._unidClienteProcedencia = ((_clienteProcedencia != null) ? _clienteProcedencia.UNID_CLIENTE : (long?)null);
         
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteProcedencia"));
                    }
                }
            }
        }

        public CLIENTE ClienteProcedenciaLectura
        {
            get { return this._clienteProcedenciaLectura; }
            set
            {
                if (value != this._clienteProcedenciaLectura)
                {
                    this._clienteProcedenciaLectura = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteProcedenciaLectura"));
                }
            }
        }

        public CLIENTE ClienteDestinoLectura
        {
            get { return this._clienteDestinoLectura; }
            set
            {
                if (value != this._clienteDestinoLectura)
                {
                    this._clienteDestinoLectura = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteDestinoLectura"));
                }
            }
        }

        public SOLICITANTE SolicitanteLectura
        {
            get { return this._solicitanteLectura; }
            set
            {
                if (value != this._solicitanteLectura)
                {
                    this._solicitanteLectura = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("SolicitanteLectura"));
                }
            }
        }

        public EMPRESA EmpresaLectura
        {
            get { return this._empresaLectura; }
            set
            {
                if (value != this._empresaLectura)
                {
                    this._empresaLectura = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("EmpresaLectura"));
                }
            }
        }

        public TECNICO TecnicoTrnas
        {
            get { return this._tecnicoTrnas; }
            set
            {
                if (value != this._tecnicoTrnas)
                {
                    this._tecnicoTrnas = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TecnicoTrnas"));
                }
            }
        }

        public DEPARTAMENTO DepartamentoLectura
        {
            get { return this._departamentoLectura; }
            set
            {
                if (value != this._departamentoLectura)
                {
                    this._departamentoLectura = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("DepartamentoLectura"));
                }
            }
        }

        public ALMACEN AlmacenProcedenciaLectura
        {
            get
            {
                return _almacenProcedenciaLectura;
            }
            set
            {
                if (_almacenProcedenciaLectura != value)
                {
                    _almacenProcedenciaLectura = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("AlmacenProcedenciaLectura"));
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

                    this._proveedorProcedencia = null;
                    this._unidProveedorProcedencia = null;
                    this._clienteProcedencia = null;
                    this._unidClienteProcedencia = null;

                    this._unidAlmacenProcedencia = ((_almacenProcedencia != null) ? _almacenProcedencia.UNID_ALMACEN : (long?)null);
                    
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("AlmacenProcedencia"));
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
                    this._unidServicio = _servicio.UNID_SERVICIO;
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
                    this._unidTransporte = _transporte.UNID_TRANSPORTE;
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

                    //this._unidCliente = _cliente.UNID_CLIENTE;
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
                    this._unidProveedor = _proveedor.UNID_PROVEEDOR;
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
                    this._unidFacturaVenta = _facturaVenta.UNID_FACTURA_VENTA;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("FacturaVenta"));
                    }
                }
            }
        }

        public FACTURA_VENTA FacturaVentaLectura
        {
            get { return this._facturaVentaLectura; }
            set
            {
                if (value != this._facturaVentaLectura)
                {
                    this._facturaVentaLectura = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("FacturaVentaLectura"));
                }
            }
        }

        public float TotalLectura
        {
            get
            {
                return _totalLectura;
            }
            set
            {
                if (_totalLectura != value)
                {
                    _totalLectura = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TotalLectura"));
                    }
                }
            }
        }

        #endregion

        public void saveArticulo()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new MOVIMENTO() { 
                    UNID_MOVIMIENTO = this._unidMovimiento, 
                    FECHA_MOVIMIENTO = this._fechaMovimiento,
                    UNID_TIPO_MOVIMIENTO = this._tipoMovimiento.UNID_TIPO_MOVIMIENTO,
                    UNID_ALMACEN_DESTINO = this._unidAlmacenDestino,
                    UNID_PROVEEDOR_DESTINO = null,
                    UNID_CLIENTE_DESTINO = null,
                    UNID_ALMACEN_PROCEDENCIA = this._unidAlmacenProcedencia,
                    UNID_CLIENTE_PROCEDENCIA = this._unidClienteProcedencia,
                    UNID_PROVEEDOR_PROCEDENCIA = this._unidProveedorProcedencia,
                    UNID_SERVICIO = this._unidServicio,
                    TT = this._tt,
                    CONTACTO = this._contacto,
                    UNID_TRANSPORTE = this._unidTransporte,
                    IS_ACTIVE = this._isActive,
                    DIRECCION_ENVIO = this._direccionEnvio,
                    SITIO_ENLACE = this._sitioEnlace,
                    NOMBRE_SITIO = this._nombreSitio,
                    RECIBE = this._recibe,
                    GUIA = this._guia,
                    UNID_CLIENTE = this._unidCliente,
                    UNID_PROVEEDOR = this._unidProveedor,
                    UNID_FACTURA_VENTA = this._unidFacturaVenta,
                    UNID_SOLICITANTE = this._unidSolicitante,
                    UNID_TECNICO = this._unidTecnico,
                    UNID_INFRAESTRUCTURA = this._unidInfraestructura,
                });
                //_dataMapper.insertElement(new MOVIMENTO() {UNID_MOVIMIENTO = this._unidMovimiento, FECHA_MOVIMIENTO = this._fechaMovimiento, UNID_TIPO_MOVIMIENTO = this._tipoMovimiento.UNID_TIPO_MOVIMIENTO,  TT = this._tt,IS_ACTIVE = this._isActive, RECIBE = this._recibe, UNID_ALMACEN_DESTINO = this._unidSolicitante});
            }
        }

        #region filtros
        private ObservableCollection<DeleteSolicitante> GetSolicitantesbyEmpresa(EMPRESA empresa)
        {
            ObservableCollection<DeleteSolicitante> solicitanteModel = new ObservableCollection<DeleteSolicitante>();
            try
            {
                SolicitanteDataMapper artDataMapper = new SolicitanteDataMapper();
                List<SOLICITANTE> articulos = (List<SOLICITANTE>)artDataMapper.GetSolicitanteList(new EMPRESA() { UNID_EMPRESA = Empresa.UNID_EMPRESA });

                foreach (SOLICITANTE sol in articulos)
                {
                    solicitanteModel.Add(new DeleteSolicitante(sol));
                }
               
            }
            catch (Exception)
            {
                ;
            }

            return solicitanteModel;
        }

        private ObservableCollection<TECNICO> GetTecnicosbyAlmacen(ALMACEN almacen)
        {
            ObservableCollection<TECNICO> tecnicoModel = new ObservableCollection<TECNICO>();
            try
            {
                TecnicoDataMapper artDataMapper = new TecnicoDataMapper();
                List<TECNICO> articulos = (List<TECNICO>)artDataMapper.getTecnicosByAlmancen(new ALMACEN() { UNID_ALMACEN = almacen.UNID_ALMACEN });

                foreach (TECNICO tec in articulos)
                {
                    tecnicoModel.Add(tec);
                }

            }
            catch (Exception)
            {
                ;
            }

            return tecnicoModel;
        }

        #endregion 
        #region Constructors
        public MovimientoModel()
        {
            this._dataMapper = new MovimientoDataMapper();
        }
        public MovimientoModel(IDataMapper dataMapper)
        {
            this._unidMovimiento = UNID.getNewUNID();
            this._fechaMovimiento = DateTime.Now;
            this._isActive = true;
            if ((dataMapper as MovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as MovimientoDataMapper;
            }
            this._tipoMovimiento = new TIPO_MOVIMIENTO();
            this._almacenDestino = new ALMACEN();
            this._proveedorProcedencia = new PROVEEDOR();
            this._clienteProcedencia = null;
            this._almacenProcedencia = null;
            this._servicio = new SERVICIO();
            this._transporte = new TRANSPORTE();
            this._cliente = new CLIENTE();
            this._proveedor = new PROVEEDOR();
            this._facturaVenta = new FACTURA_VENTA();
            this._solicitante = null;
            this._tecnico = new TECNICO();
            
        }

        public MovimientoModel(IDataMapper dataMapper, int mov)
        {
            
            if ((dataMapper as MovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as MovimientoDataMapper;
            }
            this._tipoMovimiento = new TIPO_MOVIMIENTO();
            this._almacenDestino = new ALMACEN();
            this._proveedorProcedencia = new PROVEEDOR();
            this._clienteProcedencia = new CLIENTE();
            this._almacenProcedencia = new ALMACEN();
            this._servicio = new SERVICIO();
            this._transporte = new TRANSPORTE();
            this._cliente = new CLIENTE();
            this._proveedor = new PROVEEDOR();
            this._facturaVenta = new FACTURA_VENTA();
            this._solicitante = null;
            this._tecnico = new TECNICO();
            this._solicitanteLectura = new SOLICITANTE();
            this._empresaLectura = new EMPRESA();
            this._departamentoLectura = new DEPARTAMENTO();

        }

        public MovimientoModel(IDataMapper dataMapper, string readOnly)
        {
            if ((dataMapper as MovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as MovimientoDataMapper;
            }

        }
        #endregion

        public void calculaTotal()
        {

            float faux = 0f;
            faux = (float)this._facturaVentaLectura.IMPORTE_FACTURA + (float)this._facturaVentaLectura.IVA_PESOS;
            
            this.TotalLectura = faux;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
