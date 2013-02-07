using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.Model;
using InventoryApp.Model.Recibo;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Collections.Specialized;
using InventoryApp.DAL.Recibo;
using InventoryApp.ViewModel.CatalogItem;

namespace InventoryApp.ViewModel.Recibo
{
    public class AddFacturaViewModel : ViewModelBase,IFacturaViewModel
    {
        private AgregarItemViewModel _AgregarItemViewModel;
        private AddReciboViewModel _AddReciboViewModel;
        private FacturaCatalogViewModel _FacturaCatalogViewModel;
        private bool CatalogFactura;

        public ICommand AddFacturaCommand
        {
            get
            {
                if (_AddFacturaCommand == null)
                {
                    _AddFacturaCommand = new RelayCommand(p => this.AttemptAddFactura(), p => this.CanAttemptAddFactura());
                }
                return _AddFacturaCommand;
            }
        }
        private RelayCommand _AddFacturaCommand;

        public ICommand AddFacturaArticuloCommand
        {
            get
            {
                if (_AddFacturaArticuloCommand == null)
                {
                    _AddFacturaArticuloCommand = new RelayCommand(p => this.AttemptAddFacturaArticuloCommand(), p => this.CanAddFacturaArticuloCommand());
                }
                return _AddFacturaArticuloCommand;
            }
        }
        private RelayCommand _AddFacturaArticuloCommand;

        public ICommand DeleteFacturaArticuloCommand
        {
            get
            {
                if (_DeleteFacturaArticuloCommand == null)
                {
                    _DeleteFacturaArticuloCommand = new RelayCommand(p => this.AttemptDeleteFacturaArticuloCommand(), p => this.CanAttemptDeleteFacturaArticuloCommand());
                }
                return _DeleteFacturaArticuloCommand;
            }
        }
        private RelayCommand _DeleteFacturaArticuloCommand;
        public const string DeleteFacturaDetallePropertyName = "DeleteFacturaArticuloCommand";

        public long UnidFactura
        {
            get
            {
                if (_UnidFactura == 0)
                {
                    _UnidFactura = DAL.UNID.getNewUNID();
                }
                return _UnidFactura;
            }
            set
            {
                if (_UnidFactura != value)
                {
                    _UnidFactura = value;
                    OnPropertyChanged(UnidFacturaPropertyName);
                }
            }
        }
        private long _UnidFactura;
        public const string UnidFacturaPropertyName = "UnidFactura";

        //porcentaje de iva
        public double PorIva
        {
            get { return _PorIva; }
            set
            {
                if (_PorIva != value)
                {
                    _PorIva = value;
                    OnPropertyChanged(PorIvaPropertyName);
                }
            }
        }
        private double _PorIva;
        public const string PorIvaPropertyName = "PorIva";

        public double TC
        {
            get { return _tc; }
            set
            {
                if (_tc != value)
                {
                    _tc = value;
                    OnPropertyChanged(TCPropertyName);
                }
            }
        }
        private double _tc;
        public const string TCPropertyName = "TC";

        public String NumeroFactura
        {
            get { return _NumeroFactura; }
            set
            {
                if (_NumeroFactura != value)
                {
                    _NumeroFactura = value;
                    OnPropertyChanged(NumeroFacturaPropertyName);
                }
            }
        }
        private String _NumeroFactura;
        public const string NumeroFacturaPropertyName = "NumeroFactura";

        public DateTime FechaFactura
        {
            get
            {
                if (_FechaFactura == null)
                {
                    _FechaFactura = DateTime.Now;
                    
                }
                return _FechaFactura;
            }
            set
            {
                if (_FechaFactura != value)
                {
                    _FechaFactura = value;
                    OnPropertyChanged(FechaFacturaPropertyName);
                }
            }
        }
        private DateTime _FechaFactura;
        public const string FechaFacturaPropertyName = "FechaFactura";

        public ObservableCollection<ProveedorModel> Proveedores
        {
            get { return _Proveedores; }
            set
            {
                if (_Proveedores != value)
                {
                    _Proveedores = value;
                    OnPropertyChanged(ProveedoresPropertyName);
                }
            }
        }
        private ObservableCollection<ProveedorModel> _Proveedores;
        public const string ProveedoresPropertyName = "Proveedores";

        public string NumeroPedimento
        {
            get { return _NumeroPedimento; }
            set
            {
                if (_NumeroPedimento != value)
                {
                    _NumeroPedimento = value;
                    OnPropertyChanged(NumeroPedimentoPropertyName);
                }
            }
        }
        private string _NumeroPedimento;
        public const string NumeroPedimentoPropertyName = "NumeroPedimento";

        public string Msj
        {
            get { return _msj; }
            set
            {
                if (_msj != value)
                {
                    _msj = value;
                    OnPropertyChanged(MsjPropertyName);
                }
            }
        }
        private string _msj;
        public const string MsjPropertyName = "Msj";

        public bool HasNotRecibo
        {
            get { return _HasNotRecibo; }
            set
            {
                if (_HasNotRecibo != value)
                {
                    _HasNotRecibo = value;
                    OnPropertyChanged(HasNotReciboPropertyName);
                }
            }
        }
        private bool _HasNotRecibo;
        public const string HasNotReciboPropertyName = "HasNotRecibo";

        public TipoPedimentoModel SelectedTipoPedimento
        {
            get { return _SelectedTipoPedimento; }
            set
            {
                if (_SelectedTipoPedimento != value)
                {
                    _SelectedTipoPedimento = value;
                    OnPropertyChanged(SelectedTipoPedimentoPropertyName);
                }
            }
        }
        private TipoPedimentoModel _SelectedTipoPedimento;
        public const string SelectedTipoPedimentoPropertyName = "SelectedTipoPedimento";

        public ObservableCollection<TipoPedimentoModel> TipoPedimentos
        {
            get { return _TipoPedimentos; }
            set
            {
                if (_TipoPedimentos != value)
                {
                    _TipoPedimentos = value;
                    OnPropertyChanged(TipoPedimentosPropertyName);
                }
            }
        }
        private ObservableCollection<TipoPedimentoModel> _TipoPedimentos;
        public const string TipoPedimentosPropertyName = "TipoPedimentos";

        public ProveedorModel SelectedProveedor
        {
            get { return _SelectedProveedor; }
            set
            {
                if (_SelectedProveedor != value)
                {
                    if (this._FacturaDetalles.Count == 0)
                    {
                        _SelectedProveedor = value;
                        OnPropertyChanged(SelectedProveedorPropertyName);
                    }
                    else
                    {

                    }
                }
            }
        }
        private ProveedorModel _SelectedProveedor;
        public const string SelectedProveedorPropertyName = "SelectedProveedor";

        public bool CanSelecteProveedor
        {
            get { return _CanSelecteProveedor; }
            set
            {
                if (_CanSelecteProveedor != value)
                {
                    _CanSelecteProveedor = value;
                    OnPropertyChanged(CanSelecteProveedorPropertyName);
                }
            }
        }
        private bool _CanSelecteProveedor;
        public const string CanSelecteProveedorPropertyName = "CanSelecteProveedor";

        public ObservableCollection<MonedaModel> Monedas
        {
            get { return _Monedas; }
            set
            {
                if (_Monedas != value)
                {
                    _Monedas = value;
                    OnPropertyChanged(MonedasPropertyName);
                }
            }
        }
        private ObservableCollection<MonedaModel> _Monedas;
        public const string MonedasPropertyName = "Monedas";

        public MonedaModel SelectedMoneda
        {
            get { return _SelectedModena; }
            set
            {
                if (_SelectedModena != value)
                {
                    _SelectedModena = value;
                    OnPropertyChanged(SelectedModenaPropertyName);
                }
            }
        }
        private MonedaModel _SelectedModena;
        public const string SelectedModenaPropertyName = "SelectedMoneda";

        public ObservableCollection<FacturaCompraDetalleModel> FacturaDetalles
        {
            get { return _FacturaDetalles; }
            set
            {
                if (_FacturaDetalles != value)
                {
                    _FacturaDetalles = value;
                    OnPropertyChanged(FacturaDetallesPropertyName);
                }
            }
        }
        private ObservableCollection<FacturaCompraDetalleModel> _FacturaDetalles;
        public const string FacturaDetallesPropertyName = "FacturaDetalles";

        #region Constructors
        public AddFacturaViewModel()
        {
            this.init();            
        }

        public AddFacturaViewModel(AddReciboViewModel addReciboViewModel)
        {
            this._AddReciboViewModel = addReciboViewModel;
            this.init();
        }

        public AddFacturaViewModel(FacturaCatalogViewModel facturaCatalogViewModel)
        {
            this._FacturaCatalogViewModel = facturaCatalogViewModel;
            this.CatalogFactura = true;
            this.init();
        }

        public AddFacturaViewModel(AgregarItemViewModel agregarItemViewModel)
        {
            this._AgregarItemViewModel = agregarItemViewModel;
            this.CatalogFactura = true;
            this.init();
        }
        #endregion

        #region Methods
        public void AttemptAddFactura()
        {
            FacturaCompraModel factura = new FacturaCompraModel()
            {
                UnidFactura = this.UnidFactura
                ,
                Proveedor = this.SelectedProveedor
                ,
                FechaFactura = this.FechaFactura
                ,
                NumeroFactura = this.NumeroFactura
                ,
                Moneda = this.SelectedMoneda
                ,
                FacturaDetalle = this._FacturaDetalles
                ,
                PorIva = this.PorIva
                ,
                NumeroPedimento = this.NumeroPedimento
                ,
                TC = this.TC
                ,
                TipoPedimento = this.SelectedTipoPedimento
                ,
                HasNotRecibo = this.HasNotRecibo
                ,
                IsNew = true
            };

            if (this.CatalogFactura)
            {                               
                factura.UnidFactura = UNID.getNewUNID();
                factura.saveFactura2();
                //factura detalle
                foreach (FacturaCompraDetalleModel fac in factura.FacturaDetalle)
                {
                    fac.Factura = factura;
                    fac.saveFacturaDetalle();
                }

                if (this._FacturaCatalogViewModel != null)
                    this._FacturaCatalogViewModel.Facturas = this.GetFacturas();
            }
            else
            {
                if (this._AddReciboViewModel != null)
                {                    
                    this._AddReciboViewModel.Facturas.Add(factura);
                }
            }
        }

        public bool CanAttemptAddFactura()
        {
            bool canAddFactura = false;
            
            if (this._FacturaDetalles.Count > 0
                && !String.IsNullOrEmpty(this.NumeroFactura)
                && this._SelectedProveedor != null
                && this._SelectedModena != null
            )
            {
                canAddFactura = true;
            }

            if (this._tc <= 0)
            {

                Msj = "El Tipo de Cambio debe de ser mayor a cero.";
                return false;
            }
            else {

                Msj = "";
            }

            return canAddFactura;
        }

        public void AttemptDeleteFacturaArticuloCommand()
        {

            try
            {
                (from o in this._FacturaDetalles
                 where o.IsSelected == true
                 select o).ToList().ForEach(o => this._FacturaDetalles.Remove(o));
            }
            catch (Exception)
            {
                ;
            }
        }

        public bool CanAttemptDeleteFacturaArticuloCommand()
        {
            bool canDeleteFacturaDetalle = false;

            if (!this.HasNotRecibo)
                return false;

            if (this._FacturaDetalles != null && this._FacturaDetalles.Count > 0)
            {
                int res = 0;
                try
                {
                    res = (from o in this._FacturaDetalles
                           where o.IsSelected == true
                           select o).ToList().Count;
                }
                catch (Exception)
                {
                    res = 0;
                }

                if (res > 0)
                {
                    canDeleteFacturaDetalle = true;
                }
            }

            return canDeleteFacturaDetalle;
        }

        public void AttemptAddFacturaArticuloCommand()
        {
        }

        public bool CanAddFacturaArticuloCommand()
        {
            bool canDeleteFacturaDetalle = false;

            if (!this.HasNotRecibo)
                return false;

            if (this.SelectedProveedor != null)
            {
                canDeleteFacturaDetalle = true;
            }

            return canDeleteFacturaDetalle;
        }

        public void init()
        {
            this.HasNotRecibo = true;
            this._CanSelecteProveedor = true;
            this.TC = 1;
            this._Proveedores = this.GetProveedores();
            this._Monedas = this.GetMonedas();
            this._FacturaDetalles = new ObservableCollection<FacturaCompraDetalleModel>();
            this._TipoPedimentos = this.GetPedimentos();
            this._FechaFactura = DateTime.Now;
            this._FacturaDetalles.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e)
            {
                if ((sender as ObservableCollection<FacturaCompraDetalleModel>).Count > 0)
                {
                    this.CanSelecteProveedor = false;
                }
                else
                {
                    this.CanSelecteProveedor = true;
                }
            };

            if (this._Proveedores != null && this._Proveedores.Count > 0)
                this.SelectedProveedor = this._Proveedores[1];
            if (this._Monedas != null && this._Monedas.Count > 0)
                this.SelectedMoneda = this._Monedas[0];
            if (this._TipoPedimentos != null && this._TipoPedimentos.Count > 0)
                this.SelectedTipoPedimento = this._TipoPedimentos[0];           
        }

        private ObservableCollection<ProveedorModel> GetProveedores()
        {
            ObservableCollection<ProveedorModel> proveedores = new ObservableCollection<ProveedorModel>();

            try
            {
                ProveedorDataMapper provDataMapper = new ProveedorDataMapper();
                List<PROVEEDOR> listProveedor = (List<PROVEEDOR>)provDataMapper.getElements();
                listProveedor.ForEach(o => proveedores.Add(new ProveedorModel(provDataMapper)
                {
                    UnidProveedor = o.UNID_PROVEEDOR
                    ,
                    ProveedorName = o.PROVEEDOR_NAME
                }));
            }
            catch (Exception)
            {
                ;
            }

            return proveedores;
        }

        private ObservableCollection<MonedaModel> GetMonedas()
        {
            ObservableCollection<MonedaModel> monedas = new ObservableCollection<MonedaModel>();

            try
            {
                MonedaDataMapper monDataMapper = new MonedaDataMapper();
                List<MONEDA> listProveedor = (List<MONEDA>)monDataMapper.getElements();
                listProveedor.ForEach(o => monedas.Add(new MonedaModel(monDataMapper)
                {
                    UnidMoneda = o.UNID_MONEDA
                    ,
                    MonedaName = o.MONEDA_NAME
                    ,
                    MonedaAbr = o.MONEDA_ABR
                }));
            }
            catch (Exception)
            {
                ;
            }

            return monedas;
        }

        private ObservableCollection<TipoPedimentoModel> GetPedimentos()
        {
            ObservableCollection<TipoPedimentoModel> tipoPedimentos = new ObservableCollection<TipoPedimentoModel>();

            try
            {
                TipoPedimentoDataMapper tpDataMapper = new TipoPedimentoDataMapper();
                List<TIPO_PEDIMENTO> listTipoPedimento = new List<TIPO_PEDIMENTO>();
                listTipoPedimento = tpDataMapper.getListElements();
                listTipoPedimento.ForEach(o => tipoPedimentos.Add(new TipoPedimentoModel(tpDataMapper)
                {
                    UnidTipoPedimento = o.UNID_TIPO_PEDIMENTO
                    ,
                    TipoPedimentoName = o.TIPO_PEDIMENTO_NAME
                }));
            }
            catch (Exception ex)
            {
                ;
            }

            return tipoPedimentos;
        }

        public AddFacturaArticuloViewModel CreateAddFacturaArticuloViewModel()
        {
            return new AddFacturaArticuloViewModel(this);
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
        
        #endregion


        public IFacturaArticuloViewModel CreateFacturaArticuloViewModel()
        {
            return this.CreateAddFacturaArticuloViewModel();
        }
    }
}
