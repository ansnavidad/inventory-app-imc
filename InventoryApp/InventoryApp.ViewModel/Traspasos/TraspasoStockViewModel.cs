using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using InventoryApp.ViewModel.GridMovimientos;

namespace InventoryApp.ViewModel.Traspasos
{
    public class TraspasoStockViewModel : IPageViewModel
    {
        private MovimientoSalidasModel _movimientoModel;
        private MovimientoDetalleModel _movimientoDetalleModel;
        private UltimoMovimientoModel _ultimoMovimientoModel;
        private CatalogSolicitanteModel _catalogSolicitanteModel;
        private CatalogAlmacenModel _catalogAlmacenProcedenciaModel;
        private CatalogAlmacenModel _catalogAlmacenDestinoModel;
        private CatalogProveedorModel _catalogProveedorDestinoModel;
        private CatalogClienteModel _catalogClienteDestinoModel;
        private CatalogItemModel _itemModel;
        private CatalogTransporteModel _catalogTransporteModel;
        private CatalogServicioModel _catalogServicioModel;
        private MovimientoGridTraspasoStockViewModel _movimientoTraspaso;
        private RelayCommand _addItemCommand;
        private RelayCommand _deleteItemCommand;

        public TraspasoStockViewModel()
        {            
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper dataMapper5 = new TransporteDataMapper();
                IDataMapper dataMapper6 = new ServicioDataMapper();

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoSalidasModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 17;
                this._movimientoModel.TipoMovimiento = mov;                
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenDestinoModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorDestinoModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteDestinoModel = new CatalogClienteModel(dataMapper4);
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper5);
                this._catalogServicioModel = new CatalogServicioModel(dataMapper6);
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

        public TraspasoStockViewModel(MovimientoGridTraspasoStockViewModel traspaso)
        {
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper dataMapper5 = new TransporteDataMapper();
                IDataMapper dataMapper6 = new ServicioDataMapper();

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoSalidasModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 17;
                this._movimientoModel.TipoMovimiento = mov;
                this._movimientoTraspaso = traspaso;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenDestinoModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorDestinoModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteDestinoModel = new CatalogClienteModel(dataMapper4);
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper5);
                this._catalogServicioModel = new CatalogServicioModel(dataMapper6);
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
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

        public CatalogProveedorModel CatalogProveedorDestinoModel
        {
            get
            {
                return _catalogProveedorDestinoModel;

            }
            set
            {
                _catalogProveedorDestinoModel = value;
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

        public CatalogServicioModel CatalogServicioModel
        {
            get
            {
                return _catalogServicioModel;

            }
            set
            {
                _catalogServicioModel = value;
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

        public CatalogClienteModel CatalogClienteDestinoModel
        {
            get
            {
                return _catalogClienteDestinoModel;

            }
            set
            {
                _catalogClienteDestinoModel = value;
            }
        }
        public CatalogAlmacenModel CatalogAlmacenDestinoModel
        {
            get
            {
                return _catalogAlmacenDestinoModel;

            }
            set
            {
                _catalogAlmacenDestinoModel = value;
            }
        }
        public MovimientoSalidasModel MovimientoModel
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
            this._catalogAlmacenProcedenciaModel.loadItems();
            this._catalogAlmacenDestinoModel.loadItems();
            this._catalogProveedorDestinoModel.loadItems();
            this._catalogClienteDestinoModel.loadCliente();
        }

        public CatalogItemViewModel CreateCatalogItemViewModel()
        {
            return new CatalogItemViewModel(this); 
        }

        public bool CanAttempArticulo()
        {
            bool _canInsertArticulo = false;

            int seleccion = 0;
            if (this.MovimientoModel.AlmacenDestino != null)
                seleccion++;
            if (this.MovimientoModel.ClienteDestino != null)
                seleccion++;
            if (this.MovimientoModel.ProveedorDestino != null)
                seleccion++;

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.Tt) && !String.IsNullOrEmpty(this.MovimientoModel.Recibe) && !String.IsNullOrEmpty(this.MovimientoModel.DireccionEnvio) && !String.IsNullOrEmpty(this.MovimientoModel.Contacto) && !String.IsNullOrEmpty(this.MovimientoModel.Guia) && !String.IsNullOrEmpty(this.MovimientoModel.SitioEnlace) && seleccion == 1)
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
                this._ultimoMovimientoModel = new UltimoMovimientoModel(new UltimoMovimientoDataMapper(), item.UnidItem, this._movimientoModel.UnidAlmacenDestino, this._movimientoModel.UnidClienteDestino, this._movimientoModel.UnidProveedorDestino, this._movimientoDetalleModel.UnidMovimientoDetalle);
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
