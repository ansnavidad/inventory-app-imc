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

namespace InventoryApp.ViewModel.Recibo
{
    public class AddFacturaViewModel : ViewModelBase
    {
        private AddReciboViewModel _AddReciboViewModel;

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
                if (_UnidFactura == null)
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
        #endregion

        #region Methods
        public void AttemptAddFactura()
        {
            if (this._AddReciboViewModel != null)
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
                };
                this._AddReciboViewModel.Facturas.Add(factura);
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

            return canAddFactura;
        }

        public void AttemptDeleteFacturaArticuloCommand()
        {
        }

        public bool CanAttemptDeleteFacturaArticuloCommand()
        {
            bool canDeleteFacturaDetalle = false;

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

            if (this.SelectedProveedor != null)
            {
                canDeleteFacturaDetalle = true;
            }

            return canDeleteFacturaDetalle;
        }

        public void init()
        {
            this._CanSelecteProveedor = true;
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
    }
}
