using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Salidas
{
    public class SalidaPruebasViewModel : IPageViewModel
    {
         private MovimientoSalidasModel _movimientoModel;
        private MovimientoDetalleModel _movimientoDetalleModel;
        private UltimoMovimientoModel _ultimoMovimientoModel;
        private CatalogSolicitanteModel _catalogSolicitanteModel;
        private CatalogAlmacenModel _catalogAlmacenDestinoModel;
        private CatalogAlmacenModel _catalogAlmacenProcedenciaModel;
        private CatalogProveedorModel _catalogProveedorDestinoModel;
        private CatalogClienteModel _catalogClienteModel;
        private CatalogClienteModel _catalogClienteDestinoModel;
        private CatalogTransporteModel _catalogTransporteModel;
        private CatalogItemModel _itemModel;
        private CatalogServicioModel _catalogServicioModel;
        private CatalogTipoPedimentoModel _catalogTipoPedimentoModel;
        private CatalogTecnicoModel _catalogTecnicoModel;
        private RelayCommand _addItemCommand;
        private RelayCommand _deleteItemCommand;

        public SalidaPruebasViewModel()
        {            
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper dataMapper5 = new ServicioDataMapper();
                IDataMapper dataMapper6 = new TipoPedimentoDataMapper();
                IDataMapper dataMapper7 = new TransporteDataMapper();
                IDataMapper dataMapper8 = new TecnicoDataMapper();

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoSalidasModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 11;
                this._movimientoModel.TipoMovimiento = mov;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenDestinoModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorDestinoModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteDestinoModel = new CatalogClienteModel(dataMapper4);
                this._catalogServicioModel = new CatalogServicioModel(dataMapper5);
                this._catalogTipoPedimentoModel = new CatalogTipoPedimentoModel(dataMapper6);
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper7);
                this._catalogClienteModel = new CatalogClienteModel(dataMapper4);
                this._catalogTecnicoModel = new CatalogTecnicoModel(dataMapper8);
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
        public CatalogTecnicoModel CatalogTecnicoModel
        {
            get
            {
                return _catalogTecnicoModel;

            }
            set
            {
                if (_catalogTecnicoModel != value)
                {
                    _catalogTecnicoModel = value;
                }
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
        public CatalogTipoPedimentoModel CatalogTipoPedimentoModel
        {
            get
            {
                return _catalogTipoPedimentoModel;

            }
            set
            {
                _catalogTipoPedimentoModel = value;
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
        public CatalogClienteModel CatalogClienteModel
        {
            get
            {
                return _catalogClienteModel;

            }
            set
            {
                _catalogClienteModel = value;
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
            this._catalogAlmacenDestinoModel.loadItems();
            this._catalogAlmacenProcedenciaModel.loadItems();
            this._catalogProveedorDestinoModel.loadItems();
            this._catalogClienteDestinoModel.loadCliente();
            this._catalogServicioModel.loadItems();
            this._catalogTipoPedimentoModel.loadItems();
            this._catalogTransporteModel.loadItems();
            this._catalogClienteModel.loadCliente();
            this._catalogTecnicoModel.loadItems();
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

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.NombreSitio) && !String.IsNullOrEmpty(this.MovimientoModel.PedimentoExpo) && !String.IsNullOrEmpty(this.MovimientoModel.DireccionEnvio) && !String.IsNullOrEmpty(this.MovimientoModel.Contacto) && !String.IsNullOrEmpty(this.MovimientoModel.Tt) && !String.IsNullOrEmpty(this.MovimientoModel.Guia) && seleccion == 1)
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
