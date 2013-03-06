using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using InventoryApp.Model;
using InventoryApp.Model.Recibo;
using System.Collections.Specialized;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL.Recibo;
using InventoryApp.ViewModel.CatalogBanco;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.Recibo
{
    public class ModifyFacturaViewModel : ViewModelBase, IFacturaViewModel, IViewModelChangeTrack
    {
        bool Catalog;
        USUARIO ActualUser;
        public FacturaCompraModel SelectedFactura
        {
            get
            {                
                return _SelectedFactura;
            }
            set
            {
                if (_SelectedFactura != value)
                {
                    _SelectedFactura = value;
                    OnPropertyChanged(SelectedFacturaPropertyName);
                }
            }
        }
        private FacturaCompraModel _SelectedFactura;
        public const string SelectedFacturaPropertyName = "SelectedFactura";

        public ICommand ModifyFacturaCommand
        {
            get
            {
                if (_ModifyFacturaCommand == null)
                {
                    _ModifyFacturaCommand = new RelayCommand(p => this.AttemptModifyFactura(), p => this.CanAttemptModifyFactura());
                }
                return _ModifyFacturaCommand;
            }
        }
        private RelayCommand _ModifyFacturaCommand;

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

        public string Msj1
        {
            get { return _Msj1; }
            set
            {
                if (_Msj1 != value)
                {
                    _Msj1 = value;
                    OnPropertyChanged(Msj1PropertyName);
                }
            }
        }
        private string _Msj1;
        public const string Msj1PropertyName = "Msj1";

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

        public TipoPedimentoModel SelectedTipoPedimento
        {
            get { return _SelectedTipoPedimento; }
            set
            {
                if (_SelectedTipoPedimento != value)
                {
                    _SelectedTipoPedimento = value;
                    OnPropertyChanged(TipoPedimentoPropertyName);
                }
            }
        }
        private TipoPedimentoModel _SelectedTipoPedimento;
        public const string TipoPedimentoPropertyName = "SelectedTipoPedimento";

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
        public ModifyFacturaViewModel()
        {
            this.ContB = true;
            this.init();
        }

        public ModifyFacturaViewModel(FacturaCompraModel SelectedFactura, bool finished, USUARIO u)
        {
            this._SelectedFactura = SelectedFactura;
            this.ContB = finished;
            this.ActualUser = u;
            this.init();
        }

        public ModifyFacturaViewModel(FacturaCompraModel SelectedFactura, bool finished)
        {
            this._SelectedFactura = SelectedFactura;
            this.ContB = finished;
            
            this.init();
        }

        public ModifyFacturaViewModel(FacturaCompraModel SelectedFactura, bool finished, bool catalog, USUARIO u)
        {
            this._SelectedFactura = SelectedFactura;
            this.ContB = finished;
            this.Catalog = catalog;
            this.ActualUser = u;
            this.init();
        }

        public ModifyFacturaViewModel(FacturaCompraModel SelectedFactura, bool finished, bool catalog)
        {
            this._SelectedFactura = SelectedFactura;
            this.ContB = finished;
            this.Catalog = catalog;
            
            this.init();
        }

        #endregion

        #region Methods
        public void init()
        {
            this.Msj1 = "";
            this.IsModified = _SelectedFactura.IsModified;            
            this.IsNew = _SelectedFactura.IsNew;
            this.PropertyChanged += delegate(object sender, PropertyChangedEventArgs eventArgs)
            {
                this.IsModified = true;
            };

            //Cargar colecciones
            this._Proveedores = this.GetProveedores();
            this._TipoPedimentos = this.GetPedimentos();
            this._Monedas = this.GetMonedas();
            this._FacturaDetalles = new ObservableCollection<FacturaCompraDetalleModel>();
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

            if (this._SelectedFactura != null)
            {
                this.FechaFactura = this._SelectedFactura.FechaFactura;
                this.NumeroFactura = this._SelectedFactura.NumeroFactura;
                this.NumeroPedimento = this._SelectedFactura.NumeroPedimento;
                this.TC = this._SelectedFactura.TC;

                if (this._SelectedFactura.Proveedor != null)
                {
                    this.SelectedProveedor =
                        this.Proveedores.FirstOrDefault(prv => prv.UnidProveedor == this._SelectedFactura.Proveedor.UnidProveedor);
                }

                if (this._SelectedFactura.TipoPedimento != null)
                {
                    this.SelectedTipoPedimento =
                        this.TipoPedimentos.FirstOrDefault(tpe => tpe.UnidTipoPedimento == this._SelectedFactura.TipoPedimento.UnidTipoPedimento);
                }

                if (this._SelectedFactura.Moneda != null)
                {
                    this.SelectedMoneda =
                        this.Monedas.FirstOrDefault(mnd => mnd.UnidMoneda == this._SelectedFactura.Moneda.UnidMoneda);
                }

                this.NumeroPedimento = this._SelectedFactura.NumeroPedimento;

                this.PorIva = this._SelectedFactura.PorIva;

                if (this._SelectedFactura.FacturaDetalle != null && this._SelectedFactura.FacturaDetalle.Count > 0)
                    this._CanSelecteProveedor = false;
                else
                    this._CanSelecteProveedor = true;

                if (this._SelectedFactura.FacturaDetalle != null)
                {
                    this._SelectedFactura.FacturaDetalle.ToList().ForEach(fd => this.FacturaDetalles.Add(fd));
                }
            }
        }
        
        public void AttemptModifyFactura()
        {
            if (Catalog)
            {
                FacturaCompraDataMapper fcdmp = new FacturaCompraDataMapper();
                FacturaCompraDetalleDataMapper fcdDm = new FacturaCompraDetalleDataMapper();

                //Actualizar Factura
                fcdmp.udpateElement(new FACTURA()
                {
                    UNID_FACTURA = this.SelectedFactura.UnidFactura,
                    FACTURA_NUMERO = this.NumeroFactura,
                    FECHA_FACTURA = this.FechaFactura,
                    IS_ACTIVE = true,
                    IVA_POR = this.PorIva,
                    UNID_MONEDA = this.SelectedMoneda.UnidMoneda,
                    UNID_PROVEEDOR = this.SelectedProveedor.UnidProveedor,
                    NUMERO_PEDIMENTO = this.NumeroPedimento,
                    UNID_TIPO_PEDIMENTO = this.SelectedTipoPedimento.UnidTipoPedimento,
                    TC = this.TC
                }, this.ActualUser);

                //Generar Array para insertar/actualizar/eliminar las factura detalle
                List<FACTURA_DETALLE> fds = new List<FACTURA_DETALLE>();
                foreach (FacturaCompraDetalleModel det in this.FacturaDetalles)
                {
                    fds.Add(det.ConvertToPoco(det));
                    fds[fds.Count - 1].UNID_FACTURA = this.SelectedFactura.UnidFactura;
                }

                if (fds.Count > 0)
                    fcdDm.udpateElements(fds);
            }
                
            if (this._SelectedFactura != null)
            {
                this._SelectedFactura.FechaFactura = this.FechaFactura;
                this._SelectedFactura.Moneda = this.SelectedMoneda;
                this._SelectedFactura.NumeroFactura = this.NumeroFactura;
                this._SelectedFactura.NumeroPedimento = this.NumeroPedimento;
                this._SelectedFactura.PorIva = this.PorIva;
                this._SelectedFactura.Proveedor = this.SelectedProveedor;
                this._SelectedFactura.IsNew = this.IsNew;
                this._SelectedFactura.IsModified = this.IsModified;
                this._SelectedFactura.FacturaDetalle = this.FacturaDetalles;
                this._SelectedFactura.TC = this.TC;
                this._SelectedFactura.TipoPedimento = this.SelectedTipoPedimento;
            }            
        }

        public bool CanAttemptModifyFactura()
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

            if (!this._SelectedFactura.HasNotRecibo)
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

            if (!this._SelectedFactura.HasNotRecibo)
            {
                this.Msj1 = "La factura no se puede modificar ya que está ligado a un recibo existente.";
                return false;                
            }
            else {

                this.Msj1 = "";
            }

            if (this.SelectedProveedor != null)
            {
                canDeleteFacturaDetalle = true;
            }

            return canDeleteFacturaDetalle;
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

        #endregion

        public bool IsModified
        {
            get
            {
                return _IsModified;
            }
            set
            {
                if (_IsModified != value)
                {
                    _IsModified = value;
                }
            }
        }
        private bool _IsModified;

        public bool IsNew
        {
            get
            {
                return _IsNew;
            }
            set
            {
                if (_IsNew != value)
                {
                    _IsNew = value;
                }
            }
        }
        private bool _IsNew;

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.SelectedFactura);
            return historialViewModel;
        }

        public IFacturaArticuloViewModel CreateFacturaArticuloViewModel()
        {
            return this.CreateAddFacturaArticuloViewModel();
        }
    }
}
