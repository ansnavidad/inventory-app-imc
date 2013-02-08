using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using InventoryApp.Model.Recibo;
using InventoryApp.ViewModel.Recibo;

namespace InventoryApp.ViewModel.CatalogItem
{
    public class ModifyItemViewModel : ViewModelBase, IPageViewModel
    {
        #region Relay Commands        
                
        public ICommand ModifyItemCommand
        {
            get
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyItem(), p => this.CanAttempModifyItem());
                }
                return _modifyItemCommand;
            }
        }
        private RelayCommand _modifyItemCommand;
        public bool CanAttempModifyItem()
        {
            bool _canAddItem = true;
            if (String.IsNullOrEmpty(this.Sku) && String.IsNullOrEmpty(this.NumeroSerie))
                _canAddItem = false;

            return _canAddItem;
        }
        public void AttempModifyItem()
        {
            this._itemModel.Sku = this.Sku;
            this._itemModel.NumeroSerie = this.NumeroSerie;
            this._itemModel.getElement2();
            
            ProveedorModel = this._itemModel.Proveedor.PROVEEDOR_NAME;

            FacturaCompraModel fAux = new FacturaCompraModel();
            fAux.FechaFactura = (DateTime)this._itemModel.FacturaDetalle.FACTURA.FECHA_FACTURA;
            fAux.NumeroFactura = this._itemModel.FacturaDetalle.FACTURA.FACTURA_NUMERO;
            fAux.NumeroPedimento = this._itemModel.FacturaDetalle.FACTURA.NUMERO_PEDIMENTO;
            fAux.PorIva = (double)this._itemModel.FacturaDetalle.FACTURA.IVA_POR;

            foreach (DAL.POCOS.FACTURA_DETALLE ffdd in this._itemModel.FacturaDetalle.FACTURA.FACTURA_DETALLE)
            {
                FacturaCompraDetalleModel auxx = new FacturaCompraDetalleModel();
                auxx.CostoUnitario = ffdd.PRECIO_UNITARIO;
                auxx.Cantidad = ffdd.CANTIDAD;
                fAux.FacturaDetalle.Add(auxx);
            }
            
            this.Factura = fAux;
            this.FacturaCollection.Add(fAux);

            UltimoMovimientoModel aux = new UltimoMovimientoModel();
            ObservableCollection<UltimoMovimientoModel> temp = aux.RegresaListaLugares(this._itemModel.UnidItem);

            this.UltimoMovimiento.Clear();
            this.ItemModelCollection.Clear();

            this.ItemModelCollection.Add(this._itemModel);

            foreach (UltimoMovimientoModel um in temp)
            {
                this.UltimoMovimiento.Add(um);
            }
        }
        
        public ICommand UpdateItemCommand
        {
            get
            {
                if (_updateItemCommand == null)
                {
                    _updateItemCommand = new RelayCommand(p => this.AttempUpdateItem(), p => this.CanAttempUpdateItem());
                }
                return _updateItemCommand;
            }
        }
        private RelayCommand _updateItemCommand;                
        public bool CanAttempUpdateItem()
        {
            bool _canAddItem = true;
            if ((String.IsNullOrEmpty(this._itemModel.Sku) && String.IsNullOrEmpty(this._itemModel.NumeroSerie)) || this.ItemModel.FacturaDetalle == null || this._itemModel.Factura == null || this._itemModel.Articulo == null || this._itemModel.CantidadItem < 1)
                _canAddItem = false;

            return _canAddItem;
        }
        public void AttempUpdateItem()
        {
            this._itemModel.updateItem();
            this.ItemModel.clear();
            this.UltimoMovimiento.Clear();
        }        

        //Factura
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
            return true;
        }
        public void AttempDeleteFactura()
        {
            if (this.FacturaCollection[0].IsChecked)
            {
                this.FacturaCollection = new ObservableCollection<FacturaCompraModel>();
            }
        }

        #endregion

        #region Properties
        
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

        public string ProveedorModel
        {
            get
            {
                return _ProveedorModel;
            }
            set
            {
                if (_ProveedorModel != value)
                {
                    _ProveedorModel = value;
                    OnPropertyChanged("ProveedorModel");
                }
            }
        }
        private string _ProveedorModel;
        
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
        
        #endregion

        #region Constructors
        
        public ModifyItemViewModel()
        {
            try
            {
                this._itemModel = new AgregarItemModel();
                this._ItemModelCollection = new ObservableCollection<AgregarItemModel>();
                this._Factura = new FacturaCompraModel();
                this._FacturaCollection = new ObservableCollection<FacturaCompraModel>();
                this._articuloModel = new CatalogArticuloModel(new ArticuloDataMapper());
                this._categoriaModel = new CatalogCategoriaModel(new CategoriaDataMapper());
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

        #region Methods

        public void Update()
        {
            this.ItemModel.updateFacturas();
        }
        
        public AgregarFacturaViewModel CreateAgregarFacturaViewModel()
        {
            return new AgregarFacturaViewModel(this);
        }

        public FacturaCatalogAgregarItemViewModel CreateModifyFacturaViewModel()
        {
            FacturaCatalogAgregarItemViewModel addFacturaViewModel = new FacturaCatalogAgregarItemViewModel(this);
            return addFacturaViewModel;
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
