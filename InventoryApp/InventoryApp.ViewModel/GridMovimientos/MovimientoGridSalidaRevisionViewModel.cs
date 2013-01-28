using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using InventoryApp.ViewModel.Salidas;

namespace InventoryApp.ViewModel.GridMovimientos
{
    public class MovimientoGridSalidaRevisionViewModel : IPageViewModel
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

        public MovimientoGridSalidaRevisionViewModel()
        {
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper dataMapper5 = new TipoPedimentoDataMapper();

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._catalogMovimientoModel = new CatalogMovimientoModel(new MovimientoDataMapper(), "Salida Revision");
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

        public MovimientoGridSalidaRevisionViewModel(string readOnly)
        {
            IDataMapper dataMapper = new MovimientoDataMapper();
            this._catalogMovimientoModel = new CatalogMovimientoModel(dataMapper); 
                
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

        /// <summary>
        /// Crea una nueva instancia de SoloLecturaEntradaDesistalacionViewModel y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ReadOnlySalidaRevisionViewModel CreateReadOnlySalidaRevisionViewModel()
        {
            MovimientoModel movimientoModel = new MovimientoModel(new MovimientoDataMapper(), "solo lectura");
            if (this._catalogMovimientoModel != null && this._catalogMovimientoModel.SelectedMovimientoGrid != null)
            {
                movimientoModel.UnidMovimiento = this._catalogMovimientoModel.SelectedMovimientoGrid.UnidMovimiento;

                movimientoModel.UnidAlmacenDestino = this._catalogMovimientoModel.SelectedMovimientoGrid.AlmacenDestino.UNID_ALMACEN;
                movimientoModel.AlmacenDestino = this._catalogMovimientoModel.SelectedMovimientoGrid.AlmacenDestino;

                movimientoModel.UnidAlmacenProcedencia = this._catalogMovimientoModel.SelectedMovimientoGrid.AlmacenProcedencia.UNID_ALMACEN;
                movimientoModel.AlmacenProcedenciaLectura = this._catalogMovimientoModel.SelectedMovimientoGrid.AlmacenProcedencia;

                movimientoModel.UnidClienteProcedencia = this._catalogMovimientoModel.SelectedMovimientoGrid.ClienteProcedencia.UNID_CLIENTE;
                movimientoModel.ClienteProcedenciaLectura = this._catalogMovimientoModel.SelectedMovimientoGrid.ClienteProcedencia;

                movimientoModel.UnidInfraestructura = this._catalogMovimientoModel.SelectedMovimientoGrid.UnidInfraestructura.UNID_INFRAESTRUCTURA;
                movimientoModel.Infraestructura = this._catalogMovimientoModel.SelectedMovimientoGrid.UnidInfraestructura;

                movimientoModel.UnidProveedorProcedencia = this._catalogMovimientoModel.SelectedMovimientoGrid.ProveedorProcedenia.UNID_PROVEEDOR;
                movimientoModel.ProveedorProcedencia = this._catalogMovimientoModel.SelectedMovimientoGrid.ProveedorProcedenia;

                movimientoModel.UnidTecnico = this._catalogMovimientoModel.SelectedMovimientoGrid.UnidTecnico.UNID_TECNICO;
                movimientoModel.Tecnico = this._catalogMovimientoModel.SelectedMovimientoGrid.UnidTecnico;

                movimientoModel.SolicitanteLectura = this._catalogMovimientoModel.SelectedMovimientoGrid.UnidSolicitante;

                movimientoModel.Transporte = this._catalogMovimientoModel.SelectedMovimientoGrid.Transporte;

                movimientoModel.Tt = this._catalogMovimientoModel.SelectedMovimientoGrid.Tt;
                movimientoModel.SitioEnlace = this._catalogMovimientoModel.SelectedMovimientoGrid.SitioEnlace;
                movimientoModel.NombreSitio = this._catalogMovimientoModel.SelectedMovimientoGrid.NombreSitio;
                movimientoModel.Guia = this._catalogMovimientoModel.SelectedMovimientoGrid.Guia;
                movimientoModel.Contacto = this._catalogMovimientoModel.SelectedMovimientoGrid.Contacto;
                movimientoModel.FechaMovimiento = this._catalogMovimientoModel.SelectedMovimientoGrid.TimeFecha;
                //carga el grid
                this._itemModel.ItemModel = this._catalogMovimientoModel.SelectedMovimientoGrid.ArticulosLectura;


            }
            return new ReadOnlySalidaRevisionViewModel(this, movimientoModel);
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
            this.CatalogMovimientoModel.loadItemsSalidaRevision();
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
