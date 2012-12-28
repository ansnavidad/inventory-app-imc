﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogItem
{
    public class ModifyItemViewModel
    {
        public ItemModel _itemModel;
        public string _sku;
        public string _numeroSerie;
        private DeleteArticulo _articulo;
        private RelayCommand _modifyItemCommand;
        private RelayCommand _updateItemCommand;
        private CatalogArticuloModel _articuloModel;
        private CatalogCategoriaModel _categoriaModel;
        private CatalogItemStatusModel _catalogStatus;

        public ModifyItemViewModel()
        {
            try
            {
                this._itemModel = new ItemModel();
                this._articuloModel = new CatalogArticuloModel(new ArticuloDataMapper());
                this._categoriaModel = new CatalogCategoriaModel(new CategoriaDataMapper());
                this._catalogStatus = new CatalogItemStatusModel(new ItemStatusDataMapper());
            }
            catch (ArgumentException ae)
            {
                
                throw;
            }

        }
        #region Props
        public ItemModel ItemModel
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
        #endregion

        #region methods
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
            if (String.IsNullOrEmpty(this._itemModel.Sku) || String.IsNullOrEmpty(this._itemModel.NumeroSerie))
                _canAddItem = false;

            return _canAddItem;
        }

        public void AttempUpdateMarca()
        {
            this._itemModel.updateItem();
        }

        public AgregarFacturaViewModel CreateAgregarFacturaViewModel()
        {
            return new AgregarFacturaViewModel(this);
        }
        #endregion 
    }
}
