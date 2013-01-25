using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;

namespace InventoryApp.ViewModel.GridMovimientos
{
    public class MovimientoGridSalidaDemoViewModel: IPageViewModel
    {
        private MovimientoGridModel _movimientoGridModel;
        private CatalogMovimientoModel _catalogMovimientoModel;
        private CatalogSolicitanteModel _catalogSolicitanteModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        private CatalogAlmacenModel _catalogAlmacenProcedenciaModel;
        private CatalogProveedorModel _catalogProveedorProcedenciaModel;
        private CatalogClienteModel _catalogClienteProcedenciaModel;
        private CatalogTipoPedimentoModel _catalogTipoPedimentoModel;
        private CatalogItemModel _itemModel;

        public MovimientoGridSalidaDemoViewModel()
        {
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper dataMapper5 = new TipoPedimentoDataMapper();

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._catalogMovimientoModel = new CatalogMovimientoModel(new MovimientoDataMapper(), "Salidas", "Salidas");
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                //mov.UNID_TIPO_MOVIMIENTO = 1;
                //this._movimientoGridModel.TipoMovimiento = mov;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorProcedenciaModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteProcedenciaModel = new CatalogClienteModel(dataMapper4);
                this._catalogTipoPedimentoModel = new CatalogTipoPedimentoModel(dataMapper5);

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

        public CatalogMovimientoModel CatalogMovimientoModel
        {
            get
            {
                return _catalogMovimientoModel;

            }
            set
            {
                _catalogMovimientoModel = value;
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
        public MovimientoGridModel MovimientoModel
        {
            get
            {
                return _movimientoGridModel;

            }
            set
            {
                _movimientoGridModel = value;
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
            this._catalogTipoPedimentoModel.loadItems();
        }

        public void updateItems()
        {
            this.CatalogMovimientoModel.loadItemsSalidaDemo();
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
