using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using InventoryApp.ViewModel.Recibo;

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
            if (FillWithDestinos && !_numeroSerie.Equals("") && !_sku.Equals(""))
                return true;
            return false;
        }
        public void AttempGuardar()
        {

        }

        #region Juan Pablo

        //public ICommand ModifyItemCommand
        //{
        //    get
        //    {
        //        if (_modifyItemCommand == null)
        //        {
        //            _modifyItemCommand = new RelayCommand(p => this.AttempModifyMarca(), p => this.CanAttempModifyItem());
        //        }
        //        return _modifyItemCommand;
        //    }
        //}
        //private RelayCommand _modifyItemCommand;

        //public ICommand AddFactura
        //{
        //    get
        //    {
        //        if (_addFactura == null)
        //        {
        //            _addFactura = new RelayCommand(p => this.AttempAddFactura(), p => this.CanAttempAddFactura());
        //        }
        //        return _addFactura;
        //    }
        //}
        //private RelayCommand _addFactura;
        //public bool CanAttempAddFactura()
        //{
        //    bool _canAddItem = true;
        //    if (this._itemModel.Articulo == null)
        //        _canAddItem = false;

        //    return _canAddItem;
        //}
        //public void AttempAddFactura()
        //{

        //}

        //public ICommand UpdateItemCommand
        //{
        //    get
        //    {
        //        if (_updateItemCommand == null)
        //        {
        //            _updateItemCommand = new RelayCommand(p => this.AttempUpdateMarca(), p => this.CanAttempUpdateItem());
        //        }
        //        return _updateItemCommand;
        //    }
        //}
        //private RelayCommand _updateItemCommand;

        //public ICommand DeleteMovimiento
        //{
        //    get
        //    {
        //        if (_deleteMovimiento == null)
        //        {
        //            _deleteMovimiento = new RelayCommand(p => this.AttempDeleteMovimiento(), p => this.CanAttempDeleteMovimiento());
        //        }
        //        return _deleteMovimiento;
        //    }
        //}
        //private RelayCommand _deleteMovimiento;
        //public bool CanAttempDeleteMovimiento()
        //{
        //    bool _canAddItem = false;
        //    foreach (UltimoMovimientoModel um in this.UltimoMovimiento)
        //    {
        //        if (um.IsChecked)
        //            _canAddItem = true;
        //    }

        //    return _canAddItem;
        //}
        //public void AttempDeleteMovimiento()
        //{
        //    for (int i = this.UltimoMovimiento.Count() - 1; i >= 0; i--)
        //    {
        //        if (this.UltimoMovimiento[i].IsChecked)
        //            this.UltimoMovimiento.RemoveAt(i);
        //    }
        //}

        //public bool CanAttempModifyItem()
        //{
        //    bool _canAddItem = true;


        //    if (String.IsNullOrEmpty(this._itemModel.Sku) && String.IsNullOrEmpty(this._itemModel.NumeroSerie))
        //        _canAddItem = false;

        //    return _canAddItem;
        //}
        //public void AttempModifyMarca()
        //{
        //    this._itemModel.getElement();
        //}
        //public bool CanAttempUpdateItem()
        //{
        //    bool _canAddItem = true;
        //    int total = 0;
        //    foreach (UltimoMovimientoModel um in this.UltimoMovimiento)
        //    {
        //        total += um.Cantidad;
        //    }
        //    if (String.IsNullOrEmpty(this._itemModel.Sku) || String.IsNullOrEmpty(this._itemModel.NumeroSerie) || this.ItemModel.FacturaDetalle == null
        //        || this._itemModel.Factura == null || this._itemModel.Articulo == null || this._itemModel.CantidadItem < 1 || total != this.ItemModel.CantidadItem)
        //        _canAddItem = false;

        //    return _canAddItem;
        //}
        //public void AttempUpdateMarca()
        //{
        //    this._itemModel.insertItem();


        //    foreach (UltimoMovimientoModel um in this.UltimoMovimiento)
        //    {

        //        um.UnidItem = this._itemModel.UnidItem;
        //        um.saveArticulo();
        //    }

        //    this.ItemModel.clear();
        //}
        #endregion
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

        public CatalogUnidadModel CatalogUnidadModel
        {
            get
            {
                return _catalogUnidadModel;
            }
            set
            {
                _catalogUnidadModel = value;
            }
        }
        private CatalogUnidadModel _catalogUnidadModel;

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
        
        public CatalogMonedaModel CatalogMonedaModel
        {
            get
            {
                return _catalogMonedaModel;
            }
            set
            {
                _catalogMonedaModel = value;
            }
        }
        private CatalogMonedaModel _catalogMonedaModel;

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
        
        public DeleteArticulo Articulo
        {
            get
            {
                return _articulo;
            }
            set
            {
                _articulo = value;
            }
        }
        private DeleteArticulo _articulo;
        
        public CatalogArticuloModel ArticuloModel
        {
            get
            {
                return _articuloModel;
            }
            set
            {
                _articuloModel = value;
            }
        }
        private CatalogArticuloModel _articuloModel;   

        public CatalogCategoriaModel CategoriaModel
        {
            get
            {
                return _categoriaModel;
            }
            set
            {
                _categoriaModel = value;
            }
        }
        private CatalogCategoriaModel _categoriaModel;

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
                
                this._articuloModel = new CatalogArticuloModel(new ArticuloDataMapper());
                this._categoriaModel = new CatalogCategoriaModel(new CategoriaDataMapper());
                this._catalogStatus = new CatalogItemStatusModel(new ItemStatusDataMapper());                
                this._ultimoMovimiento = new ObservableCollection<UltimoMovimientoModel>();
                this._catalogPropiedad = new CatalogPropiedadModel(new PropiedadDataMapper());
                this._catalogMonedaModel = new CatalogMonedaModel(new MonedaDataMapper());
                this._catalogUnidadModel = new CatalogUnidadModel(new UnidadDataMapper());
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
