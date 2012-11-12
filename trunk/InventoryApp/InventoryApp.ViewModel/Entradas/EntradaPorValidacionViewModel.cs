﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Entradas
{
    public class EntradaPorValidacionViewModel
    {
        private MovimientoModel _movimientoModel;
        private MovimientoDetalleModel _movimientoDetalleModel;
        private CatalogSolicitanteModel _catalogSolicitanteModel;
        private CatalogItemModel _itemModel;
        private RelayCommand _addItemCommand;

        public EntradaPorValidacionViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 1;
                this._movimientoModel.TipoMovimiento = mov;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
            
        }

        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempArticulo(), p => this.CanAttempArticulo());
                }
                return _addItemCommand;
            }
        }
        public MovimientoModel MovimientoModel
        {
            get
            {
                return _movimientoModel;
                
            }
            set
            {
                _movimientoModel = value;
            }
        }
        public CatalogItemModel ItemModel
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

        public CatalogSolicitanteModel CatalogSolicitanteModel
        {
            get
            {
                return _catalogSolicitanteModel;
            }
            set
            {
                _catalogSolicitanteModel = value;
            }
        }


        public void loadItems()
        {
            this._catalogSolicitanteModel.loadSolicitante();
        }

        public CatalogItemViewModel CreateCatalogItemViewModel()
        {
            return new CatalogItemViewModel(this); 
        }

        public bool CanAttempArticulo()
        {
            bool _canInsertArticulo = false;
            if (this.ItemModel.ItemModel.Count() != 0)
                _canInsertArticulo = true;

            return _canInsertArticulo;
        }

        public void AttempArticulo()
        {
            this._movimientoModel.saveArticulo();

            foreach (ItemModel item in this._itemModel.ItemModel)
            {
                this._movimientoDetalleModel = new MovimientoDetalleModel(new MovimientoDetalleDataMapper(), this._movimientoModel.UnidMovimiento, item.UnidItem);
                this._movimientoDetalleModel.saveArticulo();
            }
            
        }
    }
}