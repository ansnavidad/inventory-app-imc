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
    public class AddFacturaViewModel:ViewModelBase
    {
        public ICommand AddFacturaCommand
        {
            get 
            {
                if (_AddFacturaCommand == null)
                {
                    _AddFacturaCommand = new RelayCommand(p=>this.AttemptAddFactura(),p=>this.CanAttemptAddFactura());
                }
                return _AddFacturaCommand; 
            }
        }
        private RelayCommand _AddFacturaCommand;

        public ICommand DeleteFacturaDetalle
        {
            get
            {
                if (_DeleteFacturaDetalle != null)
                {
                    _DeleteFacturaDetalle = new RelayCommand(p => this.AttemptDeleteFacturaDetalle(), p => this.CanAttemptDeleteFacturaDetalle());
                }
                return _DeleteFacturaDetalle;
            }
        }
        private RelayCommand _DeleteFacturaDetalle;
        public const string DeleteFacturaDetallePropertyName = "DeleteFacturaDetalle";

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
            get { return _FechaFactura; }
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

        public AddFacturaViewModel()
        {
            this.init();
        }

        #region Methods
        public void AttemptAddFactura()
        {
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

        public void AttemptDeleteFacturaDetalle()
        {
        }

        public bool CanAttemptDeleteFacturaDetalle()
        {
            bool canDeleteFacturaDetalle = false;

            return canDeleteFacturaDetalle;
        }

        public void init()
        {
            this._CanSelecteProveedor = true;
            this._Proveedores = this.GetProveedores();
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
        }

        private ObservableCollection<ProveedorModel> GetProveedores()
        {
            ObservableCollection<ProveedorModel> proveedores = new ObservableCollection<ProveedorModel>();

            try
            {
                ProveedorDataMapper provDataMapper = new ProveedorDataMapper();
                List<PROVEEDOR> listProveedor = (List<PROVEEDOR>)provDataMapper.getElements();
                listProveedor.ForEach(o=>proveedores.Add(new ProveedorModel(provDataMapper){
                    UnidProveedor=o.UNID_PROVEEDOR
                    ,ProveedorName=o.PROVEEDOR_NAME
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
                    UnidMoneda=o.UNID_MONEDA
                    ,MonedaName=o.MONEDA_NAME
                    ,MonedaAbr=o.MONEDA_ABR
                }));
            }
            catch (Exception)
            {
                ;
            }

            return monedas;
        }

        public AddFacturaArticuloViewModel CreateAddFacturaArticuloViewModel()
        {
            return new AddFacturaArticuloViewModel(this);
        }
        #endregion
    }
}
