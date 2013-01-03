using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model.Recibo;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogItem
{
    public class AgregarFacturaViewModel
    {
        private ModifyItemViewModel _modifyItemViewModel;
        private FacturaCompraModel _facturaCompraModel;
        private FacturaCompraDetalleModel _facturaCompraDetalleModel;
        private ObservableCollection<ProveedorModel> _Proveedores;
        private ObservableCollection<MonedaModel> _Monedas;
        private ObservableCollection<UnidadModel> _Unidades;
        private RelayCommand _AddDetalle;
        #region RelayCommand 
        public ICommand AddDetalle
        {
            get
            {
                if (_AddDetalle == null)
                {
                    _AddDetalle = new RelayCommand(p => this.AttemptAddDetalle(), p => this.CanAttemptAddDetalle());
                }
                return _AddDetalle;
            }
        }

        public void AttemptAddDetalle()
        {
            LoteModel lot = new LoteModel(new LoteDataMapper());
            lot.UnidLote = UNID.getNewUNID();
            lot.UnidPOM = UNID.getNewUNID();
            lot.saveLote();
            this._facturaCompraModel.UnidLote = lot.UnidLote;
            this._facturaCompraModel.UnidFactura = UNID.getNewUNID();
            this.FacturaCompraModel.saveFactura();
            this.FacturaCompraDetalleModel.UnidFacturaCompraDetalle = UNID.getNewUNID();
            FacturaCompraModel aux = new FacturaCompraModel(new FacturaDataMapper());
            aux.UnidFactura = this._facturaCompraModel.UnidFactura;
            this._facturaCompraDetalleModel.Factura = aux;
            this.FacturaCompraDetalleModel.saveFacturaDetalle();
            this.ModifyItemViewModel.Update();
            
        }

        public bool CanAttemptAddDetalle()
        {
            bool canAddDetalle = false;

            if (this._facturaCompraModel.NumeroFactura != null && this._facturaCompraModel.FechaFactura != null
                && this._facturaCompraModel.PorIva > 0 && this._facturaCompraModel.Proveedor != null &&
                this._facturaCompraModel.Moneda != null && this._facturaCompraDetalleModel.Unidad != null
                && this.FacturaCompraDetalleModel.Cantidad > 0 && this.FacturaCompraDetalleModel.CostoUnitario > 0)
            {
                canAddDetalle = true;
            }

            return canAddDetalle;
        }
        #endregion

        public ObservableCollection<UnidadModel> Unidades
        {
            get
            {
                return _Unidades;
            }
            set
            {
                _Unidades = value;
            }
        }

        public ObservableCollection<ProveedorModel> Proveedores
        {
            get
            {
                return _Proveedores;
            }
            set
            {
                _Proveedores = value;
            }
        }

        public ObservableCollection<MonedaModel> Monedas
        {
            get
            {
                return _Monedas;
            }
            set
            {
                _Monedas = value;
            }
        }

        public ModifyItemViewModel ModifyItemViewModel
        {
            get
            {
                return _modifyItemViewModel;
            }
            set
            {
                _modifyItemViewModel = value;
            }
        }

        public FacturaCompraModel FacturaCompraModel
        {
            get
            {
                return _facturaCompraModel;
            }
            set
            {
                _facturaCompraModel = value;
            }
        }

        public FacturaCompraDetalleModel FacturaCompraDetalleModel
        {
            get
            {
                return _facturaCompraDetalleModel;
            }
            set
            {
                _facturaCompraDetalleModel = value;
            }
        }



        public AgregarFacturaViewModel(ModifyItemViewModel modifyItemViewModel)
        {
            this._modifyItemViewModel = modifyItemViewModel;
            this._facturaCompraModel = new FacturaCompraModel();
            this.Proveedores = new ObservableCollection<ProveedorModel>();
            this.Monedas = new ObservableCollection<MonedaModel>();
            this.Monedas = this.GetMonedas();
            this.FacturaCompraModel.FechaFactura = DateTime.Today;
            this._facturaCompraDetalleModel = new FacturaCompraDetalleModel();
            this.Unidades = GetUnidades();

            this._modifyItemViewModel.ItemModel.Proveedores.ForEach(o => this.Proveedores.Add(new ProveedorModel(new ProveedorDataMapper())
            {
                UnidProveedor = o.UNID_PROVEEDOR
                ,
                ProveedorName = o.PROVEEDOR_NAME
            }));
            this._facturaCompraDetalleModel.Articulo.UnidArticulo = this._modifyItemViewModel.ItemModel.Articulo.UNID_ARTICULO;
        }

        private ObservableCollection<UnidadModel> GetUnidades()
        {
            ObservableCollection<UnidadModel> unidades = new ObservableCollection<UnidadModel>();

            try
            {
                UnidadDataMapper uniDataMapper = new UnidadDataMapper();
                uniDataMapper.getListElements().ForEach(o => unidades.Add(new UnidadModel(uniDataMapper)
                {
                    UnidUnidad = o.UNID_UNIDAD
                    ,
                    UnidadName = o.UNIDAD1
                }));
            }
            catch (Exception)
            {
                ;
            }

            return unidades;
        }

        private ObservableCollection<MonedaModel> GetMonedas()
        {
            ObservableCollection<MonedaModel> monedas = new ObservableCollection<MonedaModel>();

            try
            {
                MonedaDataMapper monDataMapper = new MonedaDataMapper();
                List<MONEDA> listProveedor = (List<MONEDA>)monDataMapper.getElements();
                listProveedor.ForEach(o => monedas.Add(new MonedaModel(monDataMapper)
                {
                    UnidMoneda = o.UNID_MONEDA
                    ,
                    MonedaName = o.MONEDA_NAME
                    ,
                    MonedaAbr = o.MONEDA_ABR
                }));
            }
            catch (Exception)
            {
                ;
            }

            return monedas;
        }

    }
}
