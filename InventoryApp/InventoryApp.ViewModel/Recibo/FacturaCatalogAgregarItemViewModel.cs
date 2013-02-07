using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model.Recibo;
using System.Windows.Input;
using InventoryApp.DAL.Recibo;
using InventoryApp.ViewModel.CatalogItem;

namespace InventoryApp.ViewModel.Recibo
{
    public class FacturaCatalogAgregarItemViewModel : ViewModelBase, IPageViewModel
    {
        private CatalogItemStatusModel _catalogItemStatusModel;
        private AgregarItemViewModel _AgregarItemViewModel;
                
        #region RelayCommands
        
        public bool ContB
        {
            get
            {
                return _ContB;
            }
            set
            {
                if (_ContB != value)
                {
                    _ContB = value;
                    OnPropertyChanged(ContBPropertyName);
                }
            }
        }
        private bool _ContB;
        public const string ContBPropertyName = "ContB";

        public ICommand SelectFacturaCmd
        {
            get
            {
                if (_SelectFacturaCmd == null)
                {
                    _SelectFacturaCmd = new RelayCommand(p => this.AttemptSelectFactura(), p => this.CanAttemptSelectFactura());
                }
                return _SelectFacturaCmd;
            }
        }
        private RelayCommand _SelectFacturaCmd;

        private void AttemptSelectFactura()
        {
            this._AgregarItemViewModel.FillWithIFactura = false;
            this._AgregarItemViewModel.FillWithDestinos = true;
            this._AgregarItemViewModel.FillWithDestinos2 = true;
            this._AgregarItemViewModel.Factura = SelectedFactura;
            this._AgregarItemViewModel.FacturaCollection.Add(SelectedFactura);
        }

        private bool CanAttemptSelectFactura()
        {
            return true;
        }

        #endregion

        protected CatalogReciboViewModel _CatalogReciboViewModel;

        public DateTime FechaCreacion
        {
            get {
                if (_FechaCreacion == DateTime.MinValue)
                {
                    _FechaCreacion = DateTime.Now;
                }
                return _FechaCreacion; 
            }
            set
            {
                if (_FechaCreacion != value)
                {
                    _FechaCreacion = value;
                    OnPropertyChanged(FechaCreacionPropertyName);
                }
            }
        }
        private DateTime _FechaCreacion;
        public const string FechaCreacionPropertyName = "FechaCreacion";

        public long UnidRecibo
        {
            get 
            {
                if (_UnidRecibo == 0)
                {
                    _UnidRecibo = DAL.UNID.getNewUNID();
                }
                return _UnidRecibo; 
            }
            set
            {
                if (_UnidRecibo != value)
                {
                    _UnidRecibo = value;
                    OnPropertyChanged(UnidReciboPropertyName);
                }
            }
        }
        protected long _UnidRecibo;
        public const string UnidReciboPropertyName = "UnidRecibo";

        public ObservableCollection<SolicitanteModel> Solicitantes
        {
            get { return _Solicitantes; }
            set
            {
                if (_Solicitantes != value)
                {
                    _Solicitantes = value;
                    OnPropertyChanged(SolicitantesPropertyName);
                }
            }
        }
        protected ObservableCollection<SolicitanteModel> _Solicitantes;
        public const string SolicitantesPropertyName = "Solicitantes";

        public SolicitanteModel SelectedSolicitante
        {
            get { return _SelectedSolicitante; }
            set
            {
                if (_SelectedSolicitante != value)
                {
                    _SelectedSolicitante = value;
                    OnPropertyChanged(SelectedSolicitantePropertyName);
                }
            }
        }
        protected SolicitanteModel _SelectedSolicitante;
        public const string SelectedSolicitantePropertyName = "SelectedSolicitante";

        public ObservableCollection<ClienteModel> Clientes
        {
            get { return _Clientes; }
            set
            {
                if (_Clientes != value)
                {
                    _Clientes = value;
                    OnPropertyChanged(ClientesPropertyName);
                }
            }
        }
        protected ObservableCollection<ClienteModel> _Clientes;
        public const string ClientesPropertyName = "Clientes";

        public ObservableCollection<Model.EmpresaModel> Empresas
        {
            get { return _Empresas; }
            set
            {
                if (_Empresas != value)
                {
                    _Empresas = value;
                    OnPropertyChanged(EmpresasPropertyName);
                }
            }
        }
        protected ObservableCollection<Model.EmpresaModel> _Empresas;
        public const string EmpresasPropertyName = "Empresas";

        public Model.EmpresaModel SelectedEmpresa
        {
            get { return _SelectedEmpresa; }
            set
            {
                if (_SelectedEmpresa != value)
                {
                    _SelectedEmpresa = value;
                    OnPropertyChanged(SelectedEmpresaPropertyName);
                    this.Solicitantes = this.GetSolicitantes(_SelectedEmpresa);
                }
            }
        }
        protected Model.EmpresaModel _SelectedEmpresa;
        public const string SelectedEmpresaPropertyName = "SelectedEmpresa";

        public ClienteModel SelectedCliente
        {
            get { return _SelectedCliente; }
            set
            {
                if (_SelectedCliente != value)
                {
                    _SelectedCliente = value;
                    OnPropertyChanged(SelectedClientePropertyName);
                }
            }
        }
        protected ClienteModel _SelectedCliente;
        public const string SelectedClientePropertyName = "SelectedCliente";

        public string TroubleTicket
        {
            get { return _TroubleTicket; }
            set
            {
                if (_TroubleTicket != value)
                {
                    _TroubleTicket = value;
                    OnPropertyChanged(TroubleTicketPropertyName);
                }
            }
        }
        protected string _TroubleTicket;
        public const string TroubleTicketPropertyName = "TroubleTicket";

        public string PO
        {
            get { return _PO; }
            set
            {
                if (_PO != value)
                {
                    _PO = value;
                    OnPropertyChanged(POPropertyName);
                }
            }
        }
        protected string _PO;
        public const string POPropertyName = "PO";

        public string Msj2
        {
            get { return _msj2; }
            set
            {
                if (_msj2 != value)
                {
                    _msj2 = value;
                    OnPropertyChanged(Msj2PropertyName);
                }
            }
        }
        protected string _msj2;
        public const string Msj2PropertyName = "Msj2";

        public List<long> DelFacturas
        {
            get { return _DelFacturas; }
            set
            {
                if (_DelFacturas != value)
                {
                    _DelFacturas = value;
                    OnPropertyChanged(DelFacturasPropertyName);
                }
            }
        }
        protected List<long> _DelFacturas;
        public const string DelFacturasPropertyName = "DelFacturas";

        public List<long> DelMovs
        {
            get { return _DelMovs; }
            set
            {
                if (_DelMovs != value)
                {
                    _DelMovs = value;
                    OnPropertyChanged(DelMovsPropertyName);
                }
            }
        }
        protected List<long> _DelMovs;
        public const string DelMovsPropertyName = "DelMovs";

        public ObservableCollection<PedimentoModel> Pedimentos
        {
            get { return _Pedimentos; }
            set
            {
                if (_Pedimentos != value)
                {
                    _Pedimentos = value;
                    OnPropertyChanged(PedimentosPropertyName);
                }
            }
        }
        protected ObservableCollection<PedimentoModel> _Pedimentos;
        public const string PedimentosPropertyName = "Pedimentos";

        public string PedimentoImpo
        {
            get { return _PedimentoImpo; }
            set
            {
                if (_PedimentoImpo != value)
                {
                    _PedimentoImpo = value;
                    OnPropertyChanged(PedimentoImpoPropertyName);
                }
            }
        }
        protected string _PedimentoImpo;
        public const string PedimentoImpoPropertyName = "PedimentoImpo";

        public string PedimentoExpo
        {
            get { return _PedimentoExpo; }
            set
            {
                if (_PedimentoExpo != value)
                {
                    _PedimentoExpo = value;
                    OnPropertyChanged(PedimentoExpoPropertyName);
                }
            }
        }
        protected string _PedimentoExpo;
        public const string PedimentoExpoPropertyName = "PedimentoExpo";

        public ObservableCollection<FacturaCompraModel> Facturas
        {
            get { return _Facturas; }
            set
            {
                if (_Facturas != value)
                {
                    _Facturas = value;
                    OnPropertyChanged(FacturasPropertyName);
                }
            }
        }
        protected ObservableCollection<FacturaCompraModel> _Facturas;
        public const string FacturasPropertyName = "Facturas";

        public bool CheckCerrar
        {
            get { return _CheckCerrar; }
            set
            {
                if (_CheckCerrar != value)
                {
                    _CheckCerrar = value;
                    OnPropertyChanged(CheckCerrarPropertyName);
                }
            }
        }
        protected bool _CheckCerrar;
        public const string CheckCerrarPropertyName = "CheckCerrar";

        public FacturaCompraModel SelectedFactura
        {
            get { return _SelectedFactura; }
            set
            {
                if (_SelectedFactura != value)
                {
                    _SelectedFactura = value;
                    this._AgregarItemViewModel.FillWithItemDetalles = false;
                    this._AgregarItemViewModel.FillWithDestinos2 = true;
                    this._AgregarItemViewModel.FillWithIFactura = true;                    
                    this._AgregarItemViewModel.Factura = SelectedFactura;
                    this._AgregarItemViewModel.FacturaCollection = new ObservableCollection<FacturaCompraModel>();
                    this._AgregarItemViewModel.FacturaCollection.Add(SelectedFactura);
                    this.CheckCerrar = true;
                    OnPropertyChanged(SelectedFacturaPropertyName);
                }
            }
        }
        private FacturaCompraModel _SelectedFactura;
        public const string SelectedFacturaPropertyName = "SelectedFactura";

        public Model.Recibo.MovimientoModel SelectedMovimiento
        {
            get { return _SelectedMovimiento; }
            set
            {
                if (_SelectedMovimiento != value)
                {
                    _SelectedMovimiento = value;
                    OnPropertyChanged(SelectedReciboPropertyName);
                }
            }
        }
        private Model.Recibo.MovimientoModel _SelectedMovimiento;
        public const string SelectedReciboPropertyName = "SelectedMovimiento";

        public ObservableCollection<InventoryApp.Model.Recibo.MovimientoModel> Movimientos
        {
            get { return _Movimiento; }
            set
            {
                if (_Movimiento != value)
                {
                    _Movimiento = value;
                    OnPropertyChanged(MovimientoPropertyName);
                }
            }
        }
        protected ObservableCollection<InventoryApp.Model.Recibo.MovimientoModel> _Movimiento;
        public const string MovimientoPropertyName = "MovimientoModel";

        public FacturaCatalogAgregarItemViewModel()
        {
            this.ContB = true;
            this.init();
        }

        public FacturaCatalogAgregarItemViewModel(AgregarItemViewModel agregarItemViewModel)
        {
            this._AgregarItemViewModel = agregarItemViewModel;
            this.ContB = true;
            this.init();
        }

        public void init()
        {
            this._DelFacturas = new List<long>();
            this._DelMovs = new List<long>();
            this._Solicitantes = new ObservableCollection<SolicitanteModel>();//this.GetSolicitantes();
            this._Clientes = this.GetClientes();
            this._Facturas = new ObservableCollection<FacturaCompraModel>();
            this._Movimiento = new ObservableCollection<InventoryApp.Model.Recibo.MovimientoModel>();
            this._Empresas = this.GetEmpresas();
            
            if(this._Empresas != null && this._Empresas.Count > 0)
                this.SelectedEmpresa = this._Empresas[0];
            if (this._Solicitantes != null && this._Solicitantes.Count > 0)
                this.SelectedSolicitante = this._Solicitantes[0];
            
            this.Facturas = this.GetFacturas();
        }

        private ObservableCollection<FacturaCompraModel> GetFacturas()
        {
            FacturaCompraDataMapper fcDataMapper = new FacturaCompraDataMapper();

            List<FACTURA> facturaList = fcDataMapper.GetFacturaListCatalog();

            ObservableCollection<FacturaCompraModel> facturas = new ObservableCollection<FacturaCompraModel>();

            facturaList.ForEach(f =>
            {
                FacturaCompraModel fcm = new FacturaCompraModel()
                {
                    UnidFactura = f.UNID_FACTURA,
                    FechaFactura = (DateTime)f.FECHA_FACTURA,
                    IsActive = f.IS_ACTIVE,
                    IsChecked = false,
                    IsNew = false,
                    Moneda = new MonedaModel(null)
                    {
                        UnidMoneda = f.MONEDA.UNID_MONEDA,
                        MonedaName = f.MONEDA.MONEDA_NAME,
                        MonedaAbr = f.MONEDA.MONEDA_ABR
                    },
                    TC = f.TC,
                    NumeroFactura = f.FACTURA_NUMERO,
                    FacturaDetalle = new ObservableCollection<FacturaCompraDetalleModel>(),
                    Proveedor = new ProveedorModel(null)
                    {
                        UnidProveedor = f.PROVEEDOR.UNID_PROVEEDOR,
                        ProveedorName = f.PROVEEDOR.PROVEEDOR_NAME
                    },
                    PorIva = f.IVA_POR == null ? 0d : (double)f.IVA_POR,
                    NumeroPedimento = f.NUMERO_PEDIMENTO,
                    TipoPedimento = new TipoPedimentoModel(null)
                    {
                        UnidTipoPedimento = f.TIPO_PEDIMENTO.UNID_TIPO_PEDIMENTO,
                        TipoPedimentoName = f.TIPO_PEDIMENTO.TIPO_PEDIMENTO_NAME,
                        Clave = f.TIPO_PEDIMENTO.CLAVE,
                        Nota = f.TIPO_PEDIMENTO.NOTA,
                        Regimen = f.TIPO_PEDIMENTO.REGIMEN
                    }
                };

                f.FACTURA_DETALLE.ToList().ForEach(fd =>
                {
                    if (fd.IS_ACTIVE)
                    {
                        fcm.FacturaDetalle.Add(new FacturaCompraDetalleModel()
                        {
                            UnidFacturaCompraDetalle = fd.UNID_FACTURA_DETALE,
                            Articulo = new ArticuloModel()
                            {
                                UnidArticulo = fd.ARTICULO.UNID_ARTICULO,
                                ArticuloName = fd.ARTICULO.ARTICULO1,
                                Categoria = fd.ARTICULO.CATEGORIA,
                                Equipo = fd.ARTICULO.EQUIPO,
                                EquipoModel = new EquipoModel(null)
                                {
                                    UnidEquipo = fd.ARTICULO.EQUIPO.UNID_EQUIPO,
                                    EquipoName = fd.ARTICULO.EQUIPO.EQUIPO_NAME
                                },
                                Marca = fd.ARTICULO.MARCA,
                                Modelo = fd.ARTICULO.MODELO
                            },
                            Cantidad = fd.CANTIDAD,
                            Descripcion = fd.DESCRIPCION,
                            Factura = fcm,
                            ImpuestoUnitario = fcm.PorIva,
                            IsActive = fd.IS_ACTIVE,
                            Numero = fd.NUMERO,
                            CostoUnitario = fd.PRECIO_UNITARIO,
                            Unidad = new UnidadModel(null)
                            {
                                UnidUnidad = fd.UNIDAD.UNID_UNIDAD,
                                UnidadName = fd.UNIDAD.UNIDAD1
                            }
                        });
                    }
                });

                facturas.Add(fcm);
            });//factura foreach

            return facturas;
        }

        private ObservableCollection<Model.EmpresaModel> GetEmpresas()
        {
            ObservableCollection<Model.EmpresaModel> empresas = new ObservableCollection<EmpresaModel>();
            EmpresaDataMapper emDataMapper = new EmpresaDataMapper();

            try
            {
                List<EMPRESA> listEmpresa = (List<EMPRESA>)emDataMapper.getElements();
                if (listEmpresa != null)
                {
                    listEmpresa.ForEach(o =>
                    {
                        empresas.Add(new EmpresaModel(emDataMapper)
                        {
                            UnidEmpresa = o.UNID_EMPRESA,
                            EmpresaName = o.EMPRESA_NAME
                        });
                    });
                }
            }
            catch (Exception)
            {
                ;
            }

            return empresas;
        }

        public ObservableCollection<SolicitanteModel> GetSolicitantes()
        {
            ObservableCollection<SolicitanteModel> solicitantes = new ObservableCollection<SolicitanteModel>();

            try
            {
                SolicitanteDataMapper solDataMapper = new SolicitanteDataMapper();
                List<SOLICITANTE> listSolicitante = (List<SOLICITANTE>)solDataMapper.getElements();
                listSolicitante.ForEach(o => solicitantes.Add(new SolicitanteModel(solDataMapper)
                {
                    UnidSolicitante = o.UNID_SOLICITANTE
                    ,
                    SolicitanteName = o.SOLICITANTE_NAME
                    ,
                    Empresa = new EMPRESA()
                    {
                        UNID_EMPRESA = o.EMPRESA.UNID_EMPRESA
                        ,
                        EMPRESA_NAME = o.EMPRESA.EMPRESA_NAME
                    }
                    ,
                    Departamento = new DEPARTAMENTO()
                    {
                        UNID_DEPARTAMENTO = o.DEPARTAMENTO.UNID_DEPARTAMENTO
                        ,
                        DEPARTAMENTO_NAME = o.DEPARTAMENTO.DEPARTAMENTO_NAME
                    }
                }));
            }
            catch (Exception)
            {
                ;
            }

            return solicitantes;
        }

        public ObservableCollection<SolicitanteModel> GetSolicitantes(Model.EmpresaModel empresa)
        {
            ObservableCollection<SolicitanteModel> solicitantes = new ObservableCollection<SolicitanteModel>();

            try
            {
                SolicitanteDataMapper solDataMapper = new SolicitanteDataMapper();
                List<SOLICITANTE> listSolicitante = (List<SOLICITANTE>)solDataMapper.GetSolicitanteList(new EMPRESA() 
                {
                    UNID_EMPRESA=empresa.UnidEmpresa,
                    EMPRESA_NAME=empresa.EmpresaName
                });

                listSolicitante.ForEach(o => solicitantes.Add(new SolicitanteModel(solDataMapper)
                {
                    UnidSolicitante = o.UNID_SOLICITANTE
                    ,
                    SolicitanteName = o.SOLICITANTE_NAME
                    ,
                    Empresa = new EMPRESA()
                    {
                        UNID_EMPRESA = o.EMPRESA.UNID_EMPRESA
                        ,
                        EMPRESA_NAME = o.EMPRESA.EMPRESA_NAME
                    }
                    ,
                    Departamento = new DEPARTAMENTO()
                    {
                        UNID_DEPARTAMENTO = o.DEPARTAMENTO.UNID_DEPARTAMENTO
                        ,
                        DEPARTAMENTO_NAME = o.DEPARTAMENTO.DEPARTAMENTO_NAME
                    }
                }));
            }
            catch (Exception)
            {
                ;
            }

            return solicitantes;
        }

        public ObservableCollection<ClienteModel> GetClientes()
        {
            ObservableCollection<ClienteModel> clientes = new ObservableCollection<ClienteModel>();

            try
            {
                ClienteDataMapper solDataMapper = new ClienteDataMapper();
                List<CLIENTE> listCliente = solDataMapper.getClienteList();
                listCliente.ForEach(o => clientes.Add(new ClienteModel(solDataMapper)
                {
                    UnidCliente = o.UNID_CLIENTE
                    ,
                    ClienteName = o.CLIENTE1
                }));
            }
            catch (Exception)
            {
                ;
            }

            return clientes;
        }
        
        public ModifyFacturaViewModel CraeteModifyFacturaViewModel()
        {
            if (this.SelectedFactura != null)
                return new ModifyFacturaViewModel(this.SelectedFactura, this.ContB);
            else
                return null;
        }

        public ModifyFacturaViewModel CraeteModifyFacturaViewModel2()
        {
            if (this.SelectedFactura != null)
                return new ModifyFacturaViewModel(this.SelectedFactura, this.ContB, true);
            else
                return null;
        }

        public ModifyMovimientoViewModel CreateModifyMovimientoViewModel()
        {
            if (this.SelectedMovimiento != null)
                return new ModifyMovimientoViewModel(this.SelectedMovimiento, this.ContB);
            else
                return null;
        }

        //recibo
        public CatalogItemStatusModel CatalogItemStatusModel
        {
            get
            {
                return _catalogItemStatusModel;
            }
            set
            {
                _catalogItemStatusModel = value;
            }
        }
        
        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
