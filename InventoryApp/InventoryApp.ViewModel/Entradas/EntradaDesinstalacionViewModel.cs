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
    public class EntradaDesinstalacionViewModel
    {
        private MovimientoModel _movimientoModel;
        private MovimientoDetalleModel _movimientoDetalleModel;
        private UltimoMovimientoModel _ultimoMovimientoModel;
        private CatalogSolicitanteModel _catalogSolicitanteModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        private CatalogAlmacenModel _catalogAlmacenProcedenciaModel;
        private CatalogProveedorModel _catalogProveedorProcedenciaModel;
        private CatalogClienteModel _catalogClienteProcedenciaModel;
        private CatalogItemModel _itemModel;
        private CatalogTransporteModel _catalogTransporteModel;
        private RelayCommand _addItemCommand;
        private RelayCommand _deleteItemCommand;

        public EntradaDesinstalacionViewModel()
        {            
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper dataMapper5 = new TransporteDataMapper();

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 4;
                this._movimientoModel.TipoMovimiento = mov;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorProcedenciaModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteProcedenciaModel = new CatalogClienteModel(dataMapper4);
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper5);
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

        public ICommand DeleteItemCommand
        {
            get
            {
                if (_deleteItemCommand == null)
                {
                    _deleteItemCommand = new RelayCommand(p => this.AttempDeleteArticulo(), p => this.CanAttempDeleteArticulo());
                }
                return _deleteItemCommand;
            }
        }

        public CatalogProveedorModel CatalogProveedorProcedenciaModel
        {
            get
            {
                return _catalogProveedorProcedenciaModel;

            }
            set
            {
                _catalogProveedorProcedenciaModel = value;
            }
        }

        public CatalogAlmacenModel CatalogAlmacenModel
        {
            get
            {
                return _catalogAlmacenModel;

            }
            set
            {
                _catalogAlmacenModel = value;
            }
        }

        public CatalogTransporteModel CatalogTransporteModel
        {
            get
            {
                return _catalogTransporteModel;

            }
            set
            {
                _catalogTransporteModel = value;
            }
        }

        public CatalogClienteModel CatalogClienteProcedenciaModel
        {
            get
            {
                return _catalogClienteProcedenciaModel;

            }
            set
            {
                _catalogClienteProcedenciaModel = value;
            }
        }
        public CatalogAlmacenModel CatalogAlmacenProcedenciaModel
        {
            get
            {
                return _catalogAlmacenProcedenciaModel;

            }
            set
            {
                _catalogAlmacenProcedenciaModel = value;
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
            this._catalogAlmacenModel.loadItems();
            this._catalogAlmacenProcedenciaModel.loadItems();
            this._catalogProveedorProcedenciaModel.loadItems();
            this._catalogClienteProcedenciaModel.loadCliente();
        }

        public CatalogItemViewModel CreateCatalogItemViewModel()
        {
            return new CatalogItemViewModel(this); 
        }

        public bool CanAttempArticulo()
        {
            bool _canInsertArticulo = false;

            int seleccion = 0;
            if (this.MovimientoModel.AlmacenProcedencia != null)
                seleccion++;
            if (this.MovimientoModel.ClienteProcedencia != null)
                seleccion++;
            if (this.MovimientoModel.ProveedorProcedencia != null)
                seleccion++;

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.SitioEnlace) && !String.IsNullOrEmpty(this.MovimientoModel.NombreSitio) && !String.IsNullOrEmpty(this.MovimientoModel.Contacto) && !String.IsNullOrEmpty(this.MovimientoModel.Guia) && !String.IsNullOrEmpty(this.MovimientoModel.Tt) && !String.IsNullOrEmpty(this.MovimientoModel.Recibe) && seleccion == 1)
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
                this._ultimoMovimientoModel = new UltimoMovimientoModel(new UltimoMovimientoDataMapper(), item.UnidItem, this._movimientoModel.UnidAlmacenDestino, null, null, this._movimientoDetalleModel.UnidMovimientoDetalle);
                this._ultimoMovimientoModel.saveArticulo();
            }            
        }

        public bool CanAttempDeleteArticulo()
        {
            bool _canDeleteArticulo = true;
            return _canDeleteArticulo;
        }

        public void AttempDeleteArticulo()
        {
            for (int i = 0; i < this._itemModel.ItemModel.Count; )
            {

                if (this._itemModel.ItemModel[i].IsChecked)
                    this._itemModel.ItemModel.RemoveAt(i);
                else
                    i++;
            }

            this.MovimientoModel.CantidadItems = this.ItemModel.ItemModel.Count();
            
        }
    }
}
