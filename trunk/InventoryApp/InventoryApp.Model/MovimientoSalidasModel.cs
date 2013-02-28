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
    public class MovimientoSalidasModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidMovimiento;
        private int _cantidaditems;
        private DateTime _fechaMovimiento;
        private TIPO_MOVIMIENTO _tipoMovimiento;
        private ALMACEN _almacenProcedencia;
        private long? _unidAlmacenProcedencia;
        
        private ALMACEN _almacenDestino;
        private long? _unidAlmacenDestino;        
        private PROVEEDOR _proveedorDestino;
        private long? _unidProveedorDestino;
        private CLIENTE _clienteDestino;
        private long? _unidClienteDestino;        

        private SERVICIO _servicio;
        private long? _unidServicio;


        private TECNICO _tecnico;
        private long? _unidTecnico;

        private TECNICO _tecnico2;
        private long? _unidTecnico2;

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
        private string _pedimentoExpo;
        private string _pedimentoImpo;
        private TIPO_PEDIMENTO _tipoPedimento;
        private long? _unidTipoPedimento;
        private CLIENTE _cliente;
        private long? _unidCliente;
        private PROVEEDOR _proveedor;
        private long? _unidProveedor;
        private FACTURA_VENTA _facturaVenta;
        private long? _unidFacturaVenta;
        private DeleteSolicitante _solicitante;
        private long? _unidSolicitante;
        private MovimientoDataMapper _dataMapper;
        private EMPRESA _empresa;
        public ObservableCollection<DeleteSolicitante> _solicitantes;
        private ObservableCollection<TECNICO> _tecnicos;
        private ObservableCollection<TECNICO> _tecnicos2;
        private INFRAESTRUCTURA _infraestructura;
        private long? _unidInfraestructura;

        #endregion

        #region Props
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
        public ObservableCollection<TECNICO> Tecnicos2
        {
            get
            {
                return _tecnicos2;
            }
            set
            {
                if (_tecnicos2 != value)
                {
                    _tecnicos2 = value;

                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Tecnicos2"));
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

        public DeleteSolicitante Solicitante
        {
            get
            {
                return _solicitante;
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
        public TIPO_PEDIMENTO TipoPedimento
        {
            get
            {
                return _tipoPedimento;
            }
            set
            {
                if (_tipoPedimento != value)
                {
                    _tipoPedimento = value;
                    this._unidTipoPedimento = _tipoPedimento.UNID_TIPO_PEDIMENTO;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoPedimento"));
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
                    {
                        this._unidTecnico = _tecnico.UNID_TECNICO;
                    }
                    else
                    {
                        this._unidTecnico = null;
                    }
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

        public TECNICO Tecnico2
        {
            get
            {
                return _tecnico2;
            }
            set
            {
                if (_tecnico2 != value)
                {
                    _tecnico2 = value;
                    if (value != null)
                    {
                        this._unidTecnico2 = _tecnico2.UNID_TECNICO;
                    }
                    else
                    {
                        this._unidTecnico2 = null;
                    }
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Tecnico2"));
                    }
                }
            }
        }
        public long? UnidTecnico2
        {
            get
            {
                return _unidTecnico2;
            }
            set
            {
                if (_unidTecnico2 != value)
                {
                    _unidTecnico2 = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTecnico2"));
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
        public long? UnidTipoPedimento
        {
            get
            {
                return _unidTipoPedimento;
            }
            set
            {
                if (_unidTipoPedimento != value)
                {
                    _unidTipoPedimento = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTipoPedimento"));
                    }
                }
            }
        }
        public long? UnidClienteDestino
        {
            get
            {
                return _unidClienteDestino;
            }
            set
            {
                if (_unidClienteDestino != value)
                {
                    _unidClienteDestino = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidClienteProcedencia"));
                    }
                }
            }
        }
        public long? UnidProveedorDestino
        {
            get
            {
                return _unidProveedorDestino;
            }
            set
            {
                if (_unidProveedorDestino != value)
                {
                    _unidProveedorDestino = value;
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

        public long? UnidFacturaVenta
        {
            get
            {
                return _unidFacturaVenta;
            }
            set
            {
                if (_unidFacturaVenta != value)
                {
                    _unidFacturaVenta = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidFacturaVenta"));
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

                    this.Tecnicos2 = GetTecnicosbyAlmacen(value);

                    if (this.Tecnicos2.Count != 0)
                    {
                        this.Tecnico2 = Tecnicos2[0];    
                    }
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

                    this._clienteDestino = null;
                    this._unidClienteDestino = null;
                    this._almacenDestino = null;
                    this._unidAlmacenDestino = null;

                    this._unidProveedorDestino = ((_proveedorDestino != null) ? _proveedorDestino.UNID_PROVEEDOR : (long?)null);
                  

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

                    this._proveedorDestino = null;
                    this._unidProveedorDestino = null;
                    this._almacenDestino = null;
                    this._unidAlmacenDestino = null;

                    this._unidClienteDestino = ((_clienteDestino != null) ? _clienteDestino.UNID_CLIENTE : (long?)null);
         
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteDestino"));
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

                    this._unidAlmacenProcedencia = _almacenProcedencia.UNID_ALMACEN;

                    this.Tecnicos = GetTecnicosbyAlmacen(value);
                    if (this.Tecnicos.Count!=0)
                    {
                        this.Tecnico = Tecnicos[0];    
                    }
                    
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
        public string PedimentoExpo
        {
            get
            {
                return _pedimentoExpo;
            }
            set
            {
                if (_pedimentoExpo != value)
                {
                    _pedimentoExpo = value;
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
                return _pedimentoImpo;
            }
            set
            {
                if (_pedimentoImpo != value)
                {
                    _pedimentoImpo = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PedimentoImpo"));
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
                    this._unidCliente = _cliente.UNID_CLIENTE;
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

        #endregion
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


        public void saveArticulo()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new MOVIMENTO() { 
                    
                    UNID_MOVIMIENTO = this._unidMovimiento, 
                    FECHA_MOVIMIENTO = this._fechaMovimiento, 
                    UNID_TIPO_MOVIMIENTO = this._tipoMovimiento.UNID_TIPO_MOVIMIENTO, 
                    UNID_ALMACEN_DESTINO = this._unidAlmacenDestino, 
                    UNID_PROVEEDOR_DESTINO = this._unidProveedorDestino, 
                    UNID_CLIENTE_DESTINO = this._unidClienteDestino, 
                    UNID_ALMACEN_PROCEDENCIA = this._unidAlmacenProcedencia, 
                    UNID_CLIENTE_PROCEDENCIA = null, 
                    UNID_PROVEEDOR_PROCEDENCIA = null, 
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
                    PEDIMIENTO_EXPO = this._pedimentoExpo, 
                    PEDIMIENTO_IMPO = this._pedimentoImpo,
                    UNID_TECNICO = this._unidTecnico,
                    UNID_INFRAESTRUCTURA = this._unidInfraestructura,
                    UNID_TECNICO_TRAS = this.UnidTecnico2
                    });
                //_dataMapper.insertElement(new MOVIMENTO() {UNID_MOVIMIENTO = this._unidMovimiento, FECHA_MOVIMIENTO = this._fechaMovimiento, UNID_TIPO_MOVIMIENTO = this._tipoMovimiento.UNID_TIPO_MOVIMIENTO,  TT = this._tt,IS_ACTIVE = this._isActive, RECIBE = this._recibe, UNID_ALMACEN_DESTINO = this._unidSolicitante});          
            }
        }

        public void saveArticuloBaja()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElementBaja(new MOVIMENTO()
                {

                    UNID_MOVIMIENTO = this._unidMovimiento,
                    FECHA_MOVIMIENTO = this._fechaMovimiento,
                    UNID_TIPO_MOVIMIENTO = this._tipoMovimiento.UNID_TIPO_MOVIMIENTO,
                    UNID_ALMACEN_PROCEDENCIA = this._unidAlmacenProcedencia,
                    IS_ACTIVE = this._isActive,
                    UNID_SOLICITANTE = this._unidSolicitante,
                    UNID_TECNICO = this._unidTecnico,
                });
                
            }
        }

        #region Constructors
        public MovimientoSalidasModel(IDataMapper dataMapper)
        {
            this._unidMovimiento = UNID.getNewUNID();
            this._fechaMovimiento = DateTime.Now;
            this._isActive = true;
            if ((dataMapper as MovimientoDataMapper) != null)
            {
                this._dataMapper = new MovimientoDataMapper() ;
            }            
            this._almacenDestino = new ALMACEN();
            this._proveedorDestino = new PROVEEDOR();
            this._clienteDestino = null;
            this._almacenProcedencia = new ALMACEN();
            this._servicio = new SERVICIO();
            this._transporte = new TRANSPORTE();
            this._cliente = new CLIENTE();
            this._proveedor = new PROVEEDOR();
            this._facturaVenta = new FACTURA_VENTA();
            this._solicitante = null;
            this._tipoPedimento = new TIPO_PEDIMENTO();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
