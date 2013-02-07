using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using InventoryApp.ViewModel.Recibo;
using InventoryApp.Model.Recibo;

namespace InventoryApp.ViewModel.CatalogItem
{
    public class AgregarItemViewModel : ViewModelBase, IPageViewModel
    {
        #region Relay Commands

        /// <summary>
        /// Relay Command para manejar la parte de DETALLES DE ITEM
        /// </summary>
        public ICommand AddItemDetallesCommand
        {
            get
            {
                if (_AddItemDetallesCommand == null)
                {
                    _AddItemDetallesCommand = new RelayCommand(p => this.AttempAddItemDetalles(), p => this.CanAttempAddItemDetalles());
                }
                return _AddItemDetallesCommand;
            }
        }
        private RelayCommand _AddItemDetallesCommand;
        
        public ICommand DeleteItemDetallesCommand
        {
            get
            {
                if (_DeleteItemDetallesCommand == null)
                {
                    _DeleteItemDetallesCommand = new RelayCommand(p => this.AttempDeleteItemDetalles(), p => this.CanAttempDeleteItemDetalles());
                }
                return _DeleteItemDetallesCommand;
            }
        }
        private RelayCommand _DeleteItemDetallesCommand;

        public bool CanAttempAddItemDetalles()
        {
            return true;
        }
        public void AttempAddItemDetalles()
        {

        }

        public bool CanAttempDeleteItemDetalles()
        {
            if (FillWithItemDetalles)
                return true;
            return false;
        }
        public void AttempDeleteItemDetalles()
        {
            if (this.ItemModelCollection[0].IsChecked)
            {
                this.FillWithItemDetallesAnterior = true;
                this.FillWithItemDetalles = false;
                this.ItemModelCollection = new ObservableCollection<AgregarItemModel>();
            }
        }

        public bool FillWithItemDetalles
        {
            get { return _FillWithItemDetalles; }
            set
            {
                if (_FillWithItemDetalles != value)
                {
                    _FillWithItemDetalles = value;
                    OnPropertyChanged(FillWithItemDetallesPropertyName);
                }
            }
        }
        private bool _FillWithItemDetalles;
        public const string FillWithItemDetallesPropertyName = "FillWithItemDetalles";

        public bool FillWithItemDetallesAnterior
        {
            get { return _FillWithItemDetallesAnterior; }
            set
            {
                if (_FillWithItemDetallesAnterior != value)
                {
                    _FillWithItemDetallesAnterior = value;
                    OnPropertyChanged(FillWithItemDetallesAnteriorPropertyName);
                }
            }
        }
        private bool _FillWithItemDetallesAnterior;
        public const string FillWithItemDetallesAnteriorPropertyName = "FillWithItemDetallesAnterior";

        /// <summary>
        /// Relay Command para manejar la parte de FACTURA
        /// </summary>
        public ICommand AddFacturaCommand
        {
            get
            {
                if (_AddFacturaCommand == null)
                {
                    _AddFacturaCommand = new RelayCommand(p => this.AttempAddFactura(), p => this.CanAttempAddFactura());
                }
                return _AddFacturaCommand;
            }
        }
        private RelayCommand _AddFacturaCommand;

        public ICommand DeleteFacturaCommand
        {
            get
            {
                if (_DeleteFacturaCommand == null)
                {
                    _DeleteFacturaCommand = new RelayCommand(p => this.AttempDeleteFactura(), p => this.CanAttempDeleteFactura());
                }
                return _DeleteFacturaCommand;
            }
        }
        private RelayCommand _DeleteFacturaCommand;

        public bool CanAttempAddFactura()
        {
            return true;
        }
        public void AttempAddFactura()
        {

        }

        public bool CanAttempDeleteFactura()
        {
            if (FillWithIFactura)
                return true;
            return false;
        }
        public void AttempDeleteFactura()
        {
            if (this.FacturaCollection[0].IsChecked)
            {                
                this.FillWithItemDetalles = true;
                this.FillWithIFactura = false;
                this.FillWithDestinos = false;
                this.FillWithDestinos2 = false;
                this.FacturaCollection = new ObservableCollection<FacturaCompraModel>();
            }
        }

        public bool FillWithIFactura
        {
            get { return _FillWithIFactura; }
            set
            {
                if (_FillWithIFactura != value)
                {
                    _FillWithIFactura = value;
                    OnPropertyChanged(FillWithIFacturaPropertyName);
                }
            }
        }
        private bool _FillWithIFactura;
        public const string FillWithIFacturaPropertyName = "FillWithIFactura";

        /// <summary>
        /// Relay Command para manejar la parte de DESTINOS
        /// </summary>-
        public ICommand AddDestinosCommand
        {
            get
            {
                if (_AddDestinosCommand == null)
                {
                    _AddDestinosCommand = new RelayCommand(p => this.AttempAddDestinos(), p => this.CanAttempAddDestinos());
                }
                return _AddDestinosCommand;
            }
        }
        private RelayCommand _AddDestinosCommand;

        public ICommand DeleteDestinosCommand
        {
            get
            {
                if (_DeleteDestinosCommand == null)
                {
                    _DeleteDestinosCommand = new RelayCommand(p => this.AttempDeleteDestinos(), p => this.CanAttempDeleteDestinos());
                }
                return _DeleteDestinosCommand;
            }
        }
        private RelayCommand _DeleteDestinosCommand;

        public bool CanAttempAddDestinos()
        {
            return true;
        }
        public void AttempAddDestinos()
        {

        }

        public bool CanAttempDeleteDestinos()
        {
            if (FillWithDestinos)
                return true;
            return false;
        }
        public void AttempDeleteDestinos()
        {
            bool aux = false;

            List<long> borrarUM = new List<long>();
            for (int i = 0; i < this.UltimoMovimiento.Count;) {

                if (this.UltimoMovimiento[i].IsChecked)
                {
                    aux = true;
                    borrarUM.Add(i);
                    this.UltimoMovimiento.RemoveAt(i);
                }
                else {

                    i++;
                }
            }

            if (aux)
            {                
                this.FillWithDestinos2 = true;
            }

            if (this.UltimoMovimiento.Count == 0)
                FillWithIFactura = true;
        }

        public bool FillWithDestinos
        {
            get { return _FillWithDestinos; }
            set
            {
                if (_FillWithDestinos != value)
                {
                    _FillWithDestinos = value;
                    OnPropertyChanged(FillWithDestinosPropertyName);
                }
            }
        }
        private bool _FillWithDestinos;
        public const string FillWithDestinosPropertyName = "FillWithDestinos";
        
        public bool FillWithDestinos2
        {
            get { return _FillWithDestinos2; }
            set
            {
                if (_FillWithDestinos2 != value)
                {
                    _FillWithDestinos2 = value;
                    OnPropertyChanged(FillWithDestinos2PropertyName);
                }
            }
        }
        private bool _FillWithDestinos2;
        public const string FillWithDestinos2PropertyName = "FillWithDestinos2";

        /// <summary>
        /// Relay Command para GUARDAR
        /// </summary>-
        public ICommand GuardarCommand
        {
            get
            {
                if (_GuardarCommand == null)
                {
                    _GuardarCommand = new RelayCommand(p => this.AttempGuardar(), p => this.CanAttempGuardar());
                }
                return _GuardarCommand;
            }
        }
        private RelayCommand _GuardarCommand;

        public bool CanAttempGuardar()
        {
            if (FillWithDestinos && !FillWithDestinos2 && !String.IsNullOrEmpty(ItemModel.NumeroSerie) && !String.IsNullOrEmpty(ItemModel.Sku))
                return true;
            return false;
        }
        public void AttempGuardar()
        {

        }

        #endregion
        
        #region Props

        public string Sku
        {
            get
            {
                return _sku;
            }
            set
            {
                _sku = value;
            }
        }
        public string _sku;

        public string NumeroSerie
        {
            get
            {
                return _numeroSerie;
            }
            set
            {
                _numeroSerie = value;
            }
        }
        public string _numeroSerie;

        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
            }
        }
        private string _error;

        public AgregarItemModel ItemModel
        {
            get
            {
                return _itemModel;
            }
            set
            {
                _itemModel = value;
            }
        }
        public AgregarItemModel _itemModel;
        
        public ObservableCollection<AgregarItemModel> ItemModelCollection
        {
            get
            {
                return _ItemModelCollection;
            }
            set
            {
                if (_ItemModelCollection != value)
                {
                    _ItemModelCollection = value;
                    OnPropertyChanged("ItemModelCollection");
                }
            }
        }
        private ObservableCollection<AgregarItemModel> _ItemModelCollection;

        public FacturaCompraModel Factura
        {
            get
            {
                return _Factura;
            }
            set
            {
                _Factura = value;
            }
        }
        public FacturaCompraModel _Factura;

        public ObservableCollection<FacturaCompraModel> FacturaCollection
        {
            get
            {
                return _FacturaCollection;
            }
            set
            {
                if (_FacturaCollection != value)
                {
                    _FacturaCollection = value;
                    OnPropertyChanged("FacturaCollection");
                }
            }
        }
        private ObservableCollection<FacturaCompraModel> _FacturaCollection;
        
        public CatalogPropiedadModel CatalogPropiedad
        {
            get
            {
                return _catalogPropiedad;
            }
            set
            {
                _catalogPropiedad = value;
            }
        }
        private CatalogPropiedadModel _catalogPropiedad;
        
        public CatalogProveedorModel CatalogProveedor
        {
            get
            {
                return _catalogProveedor;
            }
            set
            {
                _catalogProveedor = value;
            }
        }
        private CatalogProveedorModel _catalogProveedor;
        
        public CatalogItemStatusModel CatalogStatus
        {
            get
            {
                return _catalogStatus;
            }
            set
            {
                _catalogStatus = value;
            }
        }
        private CatalogItemStatusModel _catalogStatus;
        
        public ObservableCollection<UltimoMovimientoModel> UltimoMovimiento
        {
            get
            {
                return _ultimoMovimiento;
            }
            set
            {
                _ultimoMovimiento = value;
            }
        }
        private ObservableCollection<UltimoMovimientoModel> _ultimoMovimiento;

        #endregion

        #region Methods
        
        public void Update()
        {
            this.ItemModel.updateFacturas();
        }

        public AgregarFacturaViewModel CreateAgregarFacturaViewModel()
        {
            return new AgregarFacturaViewModel(this);
        }

        public AgregarItemDestinoViewModel CreateAgregarDestinoViewModel()
        {
            return new AgregarItemDestinoViewModel(this);
        }

        public AddFacturaArticuloViewModel CreateAddFacturaArticuloViewModel()
        {
            AddFacturaArticuloViewModel addFacturaViewModel = new AddFacturaArticuloViewModel(this, true);
            return addFacturaViewModel;
        }

        public AddFacturaViewModel CreateAddFacturaViewModel()
        {
            AddFacturaViewModel addFacturaViewModel = new AddFacturaViewModel(this);
            return addFacturaViewModel;
        }

        public FacturaCatalogAgregarItemViewModel CreateModifyFacturaViewModel()
        {
            FacturaCatalogAgregarItemViewModel addFacturaViewModel = new FacturaCatalogAgregarItemViewModel(this);
            return addFacturaViewModel;
        }
        
        #endregion 
    
        #region Constructors

        public AgregarItemViewModel()
        {
            try
            {
                this.FillWithItemDetallesAnterior = true;
                this._itemModel = new AgregarItemModel();
                this._catalogProveedor = new CatalogProveedorModel(new ProveedorDataMapper());
                if (this._catalogProveedor != null && this._catalogProveedor.Proveedor.Count > 1)
                    this._itemModel.Proveedor = this._catalogProveedor.Proveedor[1];
                
                this._catalogStatus = new CatalogItemStatusModel(new ItemStatusDataMapper());                
                this._ultimoMovimiento = new ObservableCollection<UltimoMovimientoModel>();
                this._catalogPropiedad = new CatalogPropiedadModel(new PropiedadDataMapper());                
            }
            catch (ArgumentException ae)
            {

                throw;
            }

        }

        #endregion

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
