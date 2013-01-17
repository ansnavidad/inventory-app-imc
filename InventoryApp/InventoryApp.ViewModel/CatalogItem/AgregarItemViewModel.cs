using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace InventoryApp.ViewModel.CatalogItem
{
    public class AgregarItemViewModel : IPageViewModel
    {
        public AgregarItemModel _itemModel;
        public string _sku;
        public string _numeroSerie;
        private DeleteArticulo _articulo;
        private RelayCommand _modifyItemCommand;
        private RelayCommand _addFactura;
        private RelayCommand _updateItemCommand;
        private RelayCommand _deleteMovimiento;
        private CatalogArticuloModel _articuloModel;
        private CatalogCategoriaModel _categoriaModel;
        private CatalogItemStatusModel _catalogStatus;
        private CatalogProveedorModel _catalogProveedor;
        private CatalogPropiedadModel _catalogPropiedad;
        private CatalogMonedaModel _catalogMonedaModel;
        private CatalogUnidadModel _catalogUnidadModel;
        private string _error;
        private ObservableCollection<UltimoMovimientoModel> _ultimoMovimiento;

  
        public AgregarItemViewModel()
        {
            try
            {
                this._itemModel = new  AgregarItemModel();
                this._articuloModel = new CatalogArticuloModel(new ArticuloDataMapper());
                this._categoriaModel = new CatalogCategoriaModel(new CategoriaDataMapper());
                this._catalogStatus = new CatalogItemStatusModel(new ItemStatusDataMapper());
                this._catalogProveedor = new CatalogProveedorModel(new ProveedorDataMapper());
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
        #region Props

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

        public ICommand ModifyItemCommand
        {
            get
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyMarca(), p => this.CanAttempModifyItem());
                }
                return _modifyItemCommand;
            }
        }


        public ICommand AddFactura
        {
            get
            {
                if (_addFactura == null)
                {
                    _addFactura = new RelayCommand(p => this.AttempAddFactura(), p => this.CanAttempAddFactura());
                }
                return _addFactura;
            }
        }

        public ICommand UpdateItemCommand
        {
            get
            {
                if (_updateItemCommand == null)
                {
                    _updateItemCommand = new RelayCommand(p => this.AttempUpdateMarca(), p => this.CanAttempUpdateItem());
                }
                return _updateItemCommand;
            }
        }

        public ICommand DeleteMovimiento
        {
            get
            {
                if (_deleteMovimiento == null)
                {
                    _deleteMovimiento = new RelayCommand(p => this.AttempDeleteMovimiento(), p => this.CanAttempDeleteMovimiento());
                }
                return _deleteMovimiento;
            }
        }

        #endregion

        #region methods
        public bool CanAttempDeleteMovimiento()
        {
            bool _canAddItem = false;
            foreach (UltimoMovimientoModel um in this.UltimoMovimiento)
            {
                if (um.IsChecked)
                    _canAddItem = true;
            }

            return _canAddItem;
        }

        public void AttempDeleteMovimiento()
        {
            for (int i = this.UltimoMovimiento.Count() -1 ; i >= 0; i--)
            {
                if (this.UltimoMovimiento[i].IsChecked)
                    this.UltimoMovimiento.RemoveAt(i);
            }
        }

        public bool CanAttempModifyItem()
        {
            bool _canAddItem = true;
         
            
            if (String.IsNullOrEmpty(this._itemModel.Sku) && String.IsNullOrEmpty(this._itemModel.NumeroSerie))
                _canAddItem = false;

            return _canAddItem;
        }

        public void AttempModifyMarca()
        {
            this._itemModel.getElement();
        }

        public void Update()
        {
            this.ItemModel.updateFacturas();
        }

        public bool CanAttempUpdateItem()
        {
            bool _canAddItem = true;
            int total = 0;
            foreach (UltimoMovimientoModel um in this.UltimoMovimiento)
            {
                total += um.Cantidad;
            }
            if (String.IsNullOrEmpty(this._itemModel.Sku) || String.IsNullOrEmpty(this._itemModel.NumeroSerie) || this.ItemModel.FacturaDetalle == null
                || this._itemModel.Factura == null || this._itemModel.Articulo == null ||  this._itemModel.CantidadItem < 1 || total != this.ItemModel.CantidadItem)
                _canAddItem = false;

            return _canAddItem;
        }

        public void AttempUpdateMarca()
        {
            this._itemModel.insertItem();


            foreach (UltimoMovimientoModel um in this.UltimoMovimiento)
            {
                
                um.UnidItem = this._itemModel.UnidItem;
                um.saveArticulo();
            }

            this.ItemModel.clear();
        }

        public bool CanAttempAddFactura()
        {
            bool _canAddItem = true;
            if (this._itemModel.Articulo == null)
                _canAddItem = false;

            return _canAddItem;
        }

        public void AttempAddFactura()
        {
            
        }

        public AgregarFacturaViewModel CreateAgregarFacturaViewModel()
        {
            return new AgregarFacturaViewModel(this);
        }

        public AgregarItemDestinoViewModel CreateAgregarDestinoViewModel()
        {
            return new AgregarItemDestinoViewModel(this);
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
