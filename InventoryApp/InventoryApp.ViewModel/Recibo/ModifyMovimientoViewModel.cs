using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using InventoryApp.Model;
using InventoryApp.Model.Recibo;
using System.Collections.Specialized;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace InventoryApp.ViewModel.Recibo
{
    public class ModifyMovimientoViewModel : ViewModelBase, IViewModelChangeTrack
    {
        public ICommand AddMovimientoCmd
        {
            get
            {
                if (_AddMovimientoCmd == null)
                {
                    _AddMovimientoCmd = new RelayCommand(p => this.AttemptAddMovimientoCmd(), p => this.CanAttemptAddMovimientoCmd());
                }
                return _AddMovimientoCmd;
            }
        }
        private RelayCommand _AddMovimientoCmd;

        public ICommand AddImprimir
        {
            get
            {
                if (_AddImprimir == null)
                {
                    _AddImprimir = new RelayCommand(p => this.AttemptAddImprimir(), p => this.CanAttemptAddImprimir());
                }
                return _AddImprimir;
            }
        }
        private RelayCommand _AddImprimir;

        public ObservableCollection<FacturaCompraModel> FacturasDisponibles
        {
            get
            {
                ObservableCollection<FacturaCompraModel> facturas = new ObservableCollection<FacturaCompraModel>();

                facturas.Add(SelectedMovimiento.Factura);

                return facturas;
            }
        }
        public FacturaCompraModel SelectedFactura
        {
            get { return _SelectedFactura; }
            set
            {
                if (_SelectedFactura != value)
                {
                    _SelectedFactura = value;
                    OnPropertyChanged(SelectedFacturaPropertyName);
                }
            }
        }
        private FacturaCompraModel _SelectedFactura;
        public const string SelectedFacturaPropertyName = "SelectedFactura";

        public IOrigenModel SelectedOrigen
        {
            get { return _SelectedOrigen; }
            set
            {
                if (_SelectedOrigen != value)
                {
                    _SelectedOrigen = value;
                    OnPropertyChanged(SelectedOrigenPropertyName);
                }
            }
        }
        private IOrigenModel _SelectedOrigen;
        public const string SelectedOrigenPropertyName = "SelectedOrigen";

        public bool EnabledT
        {
            get
            {
                return _EnabledT;
            }
            set
            {
                if (_EnabledT != value)
                {
                    _EnabledT = value;
                    OnPropertyChanged(EnabledTPropertyName);
                }
            }
        }
        private bool _EnabledT;
        public const string EnabledTPropertyName = "EnabledT";
                
        private AddReciboViewModel _AddReciboViewModel;
        public AddReciboViewModel AddReciboViewModel
        {
            get { return _AddReciboViewModel; }
        }        

        public long UnidMovimiento
        {
            get
            {
                if (_UnidMovimiento == 0)
                {
                    _UnidMovimiento = DAL.UNID.getNewUNID();
                }
                return _UnidMovimiento;
            }
            set
            {
                if (_UnidMovimiento != value)
                {
                    _UnidMovimiento = value;
                    OnPropertyChanged(UnidMovimientoPropertyName);
                }
            }
        }
        private long _UnidMovimiento;
        public const string UnidMovimientoPropertyName = "UnidMovimiento";
        
        public DateTime FechaCaptura
        {
            get
            {
                if (_FechaCaptura == DateTime.MinValue)
                {
                    _FechaCaptura = DateTime.Now;
                }
                return _FechaCaptura;
            }
            set
            {
                if (_FechaCaptura != value)
                {
                    _FechaCaptura = value;
                    OnPropertyChanged(FechaCapturaPropertyName);
                }
            }
        }
        private DateTime _FechaCaptura;
        public const string FechaCapturaPropertyName = "FechaCaptura";

        public ObservableCollection<TipoPedimentoModel> TipoPedimentos
        {
            get
            {
                if (_TipoPedimentos == null)
                {
                    _TipoPedimentos = this.GetTipoPedimento();
                }

                return _TipoPedimentos;
            }
            set
            {
                if (_TipoPedimentos != value)
                {
                    _TipoPedimentos = value;
                    OnPropertyChanged(TipoPedimentosPropertyName);
                }
            }
        }
        private ObservableCollection<TipoPedimentoModel> _TipoPedimentos;
        public const string TipoPedimentosPropertyName = "TipoPedimentos";

        public TipoPedimentoModel SelectedTipoPedimento
        {
            get { return _SelectedTipoMovimiento; }
            set
            {
                if (_SelectedTipoMovimiento != value)
                {
                    _SelectedTipoMovimiento = value;
                    OnPropertyChanged(SelectedTipoMovimientoPropertyName);
                }
            }
        }
        private TipoPedimentoModel _SelectedTipoMovimiento;
        public const string SelectedTipoMovimientoPropertyName = "SelectedTipoPedimento";

        public ObservableCollection<OrigenClienteModel> Clientes
        {
            get
            {
                if (_Clientes == null)
                {
                    _Clientes = this.GetOrigenClienteModel();
                }

                return _Clientes;
            }
            set
            {
                if (_Clientes != value)
                {
                    _Clientes = value;
                    OnPropertyChanged(ClientesPropertyName);
                }
            }
        }
        private ObservableCollection<OrigenClienteModel> _Clientes;
        public const string ClientesPropertyName = "Clientes";

        public ObservableCollection<OrigenProveedorModel> Proveedores
        {
            get
            {
                if (_Proveedores == null)
                {
                    _Proveedores = this.GetProveedores();
                }

                return _Proveedores;
            }
            set
            {
                if (_Proveedores != value)
                {
                    _Proveedores = value;
                    OnPropertyChanged(ProveedoresPropertyName);
                }
            }
        }
        private ObservableCollection<OrigenProveedorModel> _Proveedores;
        public const string ProveedoresPropertyName = "Proveedores";

        public ObservableCollection<InventoryApp.Model.Recibo.AlmacenModel> Almacenes
        {
            get
            {
                if (_Almacenes == null)
                {
                    _Almacenes = this.GetAlmacenes();

                    int i = 0;
                    for (i = 0; i < _Almacenes.Count; i++) {

                        if (_Almacenes[i].UnidAlmacen == this._SelectedMovimiento.DestinoAlmacen.UnidAlmacen)
                            break;
                    }

                    this.SelectedAlmacenDestino = _Almacenes[i];
                }

                return _Almacenes;
            }
            set
            {
                if (_Almacenes != value)
                {
                    _Almacenes = value;
                    OnPropertyChanged(AlmacenesPropertyName);
                }
            }
        }
        private ObservableCollection<InventoryApp.Model.Recibo.AlmacenModel> _Almacenes;
        public const string AlmacenesPropertyName = "Almacenes";

        public InventoryApp.Model.Recibo.AlmacenModel SelectedAlmacenDestino
        {
            get { return _SelectedAlmacenDestino; }
            set
            {
                if (_SelectedAlmacenDestino != value)
                {
                    _SelectedAlmacenDestino = value;
                    OnPropertyChanged(SelectedAlmacenDestinoPropertyName);
                }
            }
        }
        private InventoryApp.Model.Recibo.AlmacenModel _SelectedAlmacenDestino;
        public const string SelectedAlmacenDestinoPropertyName = "SelectedAlmacenDestino";
        
        public ObservableCollection<ReciboItemModel> Items
        {
            get { return _Items; }
            set
            {
                if (_Items != value)
                {
                    _Items = value;
                    OnPropertyChanged(ItemsPropertyName);
                }
            }
        }
        private ObservableCollection<ReciboItemModel> _Items;
        public const string ItemsPropertyName = "Items";

        public ObservableCollection<ItemStatusModel> ItemStatus
        {
            get
            {
                if (_ItemStatus == null)
                {
                    _ItemStatus = this.GetStatuss();
                }

                return _ItemStatus;
            }
            set
            {
                if (_ItemStatus != value)
                {
                    _ItemStatus = value;
                    OnPropertyChanged(ItemStatusPropertyName);
                }
            }
        }
        private ObservableCollection<ItemStatusModel> _ItemStatus;
        public const string ItemStatusPropertyName = "ItemStatus";

        public Model.Recibo.MovimientoModel SelectedMovimiento
        {
            get { return _SelectedMovimiento; }
            set
            {
                if (_SelectedMovimiento != value)
                {
                    _SelectedMovimiento = value;
                    OnPropertyChanged(SelectedReciboPropertyName);
                }
            }
        }
        private Model.Recibo.MovimientoModel _SelectedMovimiento;
        public const string SelectedReciboPropertyName = "SelectedMovimiento";

        public bool ContB
        {
            get
            {
                return _ContB;
            }
            set
            {
                if (_ContB != value)
                {
                    _ContB = value;
                    OnPropertyChanged(ContBPropertyName);
                }
            }
        }
        private bool _ContB;
        public const string ContBPropertyName = "ContB";

        #region Constructors
        public ModifyMovimientoViewModel()
        {
            this.ContB = true;
            this.init();
        }

        public ModifyMovimientoViewModel(Model.Recibo.MovimientoModel SelectedMovimiento, bool  admirar)
        {
            this._SelectedMovimiento = SelectedMovimiento;
            this.ContB = admirar;
            this.init();
        }
        #endregion

        #region Methods
        
        public void init()
        {
            this.SelectedFactura = this.SelectedMovimiento.Factura;            
            this.SelectedOrigen = this.SelectedMovimiento.Origen;            
            
            this.Items = new ObservableCollection<ReciboItemModel>();

            foreach (ReciboItemModel rr in this.SelectedMovimiento.Items) {

                ReciboItemModel aux = new ReciboItemModel();
                aux.Articulo = rr.Articulo;
                aux.Cantidad = rr.Cantidad;
                aux.CostoUnitario = rr.CostoUnitario;
                aux.Empresa = rr.Empresa;
                aux.FacturaDetalle = rr.FacturaDetalle;
                aux.IsCantidadEnabled = rr.IsCantidadEnabled;
                aux.ItemStatus = rr.ItemStatus;
                aux.NumeroSerie = rr.NumeroSerie;
                aux.PedimentoExpo = rr.PedimentoExpo;
                aux.PedimentoImpo = rr.PedimentoImpo;
                aux.Sku = rr.Sku;
                aux.UnidItem = rr.UnidItem;
                aux.UnidMovimiento = rr.UnidMovimiento;
                aux.UnidMovimientoDetalle = rr.UnidMovimientoDetalle;

                this.Items.Add(aux);
            }
             
            this.EnabledT = false;
        }
        
        private ObservableCollection<TipoPedimentoModel> GetTipoPedimento()
        {
            ObservableCollection<TipoPedimentoModel> tpoPedimentos = new ObservableCollection<TipoPedimentoModel>();

            try
            {
                TipoPedimentoDataMapper dataMapper = new TipoPedimentoDataMapper();
                dataMapper.getListElements().ForEach(o => tpoPedimentos.Add(new TipoPedimentoModel(dataMapper)
                {
                    UnidTipoPedimento = o.UNID_TIPO_PEDIMENTO
                    ,
                    TipoPedimentoName = o.TIPO_PEDIMENTO_NAME
                }));
            }
            catch (Exception ex)
            {
            }

            return tpoPedimentos;
        }

        private ObservableCollection<ItemStatusModel> GetStatuss()
        {
            ObservableCollection<ItemStatusModel> ItemStatusAux = new ObservableCollection<ItemStatusModel>();

            try
            {
                ItemStatusDataMapper dataMapper = new ItemStatusDataMapper();

                List<ITEM_STATUS> ii = new List<ITEM_STATUS>();
                ii = (List<ITEM_STATUS>)dataMapper.getElements();

                foreach (ITEM_STATUS i in ii)
                {

                    ItemStatusModel ism = new ItemStatusModel(new ItemStatusDataMapper());
                    ism.ItemStatusName = i.ITEM_STATUS_NAME;
                    ism.UnidItemStatus = i.UNID_ITEM_STATUS;
                    ItemStatusAux.Add(ism);
                }
            }
            catch (Exception ex)
            {
            }

            return ItemStatusAux;
        }

        private ObservableCollection<OrigenClienteModel> GetOrigenClienteModel()
        {
            ObservableCollection<OrigenClienteModel> clientes = new ObservableCollection<OrigenClienteModel>();

            try
            {
                ClienteDataMapper solDataMapper = new ClienteDataMapper();
                List<CLIENTE> listCliente = solDataMapper.getClienteList();
                listCliente.ForEach(o => clientes.Add(new OrigenClienteModel()
                {
                    UnidCliente = o.UNID_CLIENTE
                    ,
                    ClienteName = o.CLIENTE1
                }));
            }
            catch (Exception)
            {
                ;
            }

            return clientes;
        }

        private ObservableCollection<OrigenProveedorModel> GetProveedores()
        {
            ObservableCollection<OrigenProveedorModel> proveedores = new ObservableCollection<OrigenProveedorModel>();

            try
            {
                ProveedorDataMapper provDataMapper = new ProveedorDataMapper();
                List<PROVEEDOR> listProveedor = (List<PROVEEDOR>)provDataMapper.getElements();
                listProveedor.ForEach(o => proveedores.Add(new OrigenProveedorModel()
                {
                    UnidProveedor = o.UNID_PROVEEDOR
                    ,
                    ProveedorName = o.PROVEEDOR_NAME
                }));
            }
            catch (Exception)
            {
                ;
            }

            return proveedores;
        }

        private ObservableCollection<InventoryApp.Model.Recibo.AlmacenModel> GetAlmacenes()
        {
            ObservableCollection<InventoryApp.Model.Recibo.AlmacenModel> almacenes = new ObservableCollection<InventoryApp.Model.Recibo.AlmacenModel>();

            try
            {
                AlmacenDataMapper dataMapper = new AlmacenDataMapper();
                List<ALMACEN> listAlmacen = (List<ALMACEN>)dataMapper.getElements();
                listAlmacen.ForEach(o => almacenes.Add(new InventoryApp.Model.Recibo.AlmacenModel()
                {
                    UnidAlmacen = o.UNID_ALMACEN
                    ,
                    AlmacenName = o.ALMACEN_NAME

                }));
            }
            catch (Exception)
            {
                ;
            }

            return almacenes;
        }

        private void AttemptAddMovimientoCmd()
        {
            this.SelectedMovimiento.Items = this.Items;
            this.SelectedMovimiento.DestinoAlmacen = this.SelectedAlmacenDestino;            
        }

        private bool CanAttemptAddMovimientoCmd()
        {
            bool canAttempt = false;

            if (this.SelectedAlmacenDestino != null && this._SelectedOrigen != null)
            {
                canAttempt = true;
            }

            return canAttempt;
        }

        private void AttemptAddImprimir()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Documentos Excel (.xlsx)|*.xlsx";
            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;

                Workbook excelPrint = excel.Workbooks.Open(@"C:\Programs\ElaraInventario\Resources\Factura.xlsx", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Worksheet excelSheetPrint = (Worksheet)excelPrint.Worksheets[1];

                //Folio del recibo
                excel.Cells[8, 9] = this._UnidMovimiento.ToString();
                //Fecha
                excel.Cells[8, 23] = this._SelectedFactura.FechaFactura;
                //NOMBRE DEL PROVEEDOR
                excel.Cells[11, 12] = this._SelectedOrigen.OrigenName;
                //ALMACÉN DE DESTINO:
                excel.Cells[13, 12] = this._SelectedAlmacenDestino.AlmacenName;
                //NÚMERO DE PEDIMENTO:
                excel.Cells[19, 12] = this._SelectedFactura.NumeroPedimento.ToString();
                //NÚMERO DE FACTURA:
                excel.Cells[21, 12] = this._SelectedFactura.NumeroFactura.ToString();
                //CANTIDAD DE ITEMS:
                int auxx = 0;
                foreach (ReciboItemModel rr in this._Items)
                {

                    auxx += rr.Cantidad;
                }
                excel.Cells[17, 12] = auxx;
                //IMPORTE:
                excel.Cells[17, 26] = this._SelectedFactura.Importe.ToString();
                //IVA %
                excel.Cells[19, 26] = this._SelectedFactura.PorIva.ToString();
                //IVA $
                double aux = this._SelectedFactura.Iva;
                aux = Math.Round(aux, 2);
                excel.Cells[21, 26] = aux.ToString();
                //TOTAL $
                aux = this._SelectedFactura.Total;
                aux = Math.Round(aux, 2);
                excel.Cells[23, 26] = aux.ToString() + " " + this._SelectedFactura.Moneda.MonedaAbr;
                //excel.Cells[23, 26] = this._SelectedFactura.Total.ToString();                

                excelSheetPrint.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
        }

        private bool CanAttemptAddImprimir()
        {
            bool canAttempt = false;

            if (this.SelectedAlmacenDestino != null && this._SelectedOrigen != null)
            {
                canAttempt = true;
            }

            return canAttempt;
        }

        #endregion

        public bool IsModified
        {
            get
            {
                return _IsModified;
            }
            set
            {
                if (_IsModified != value)
                {
                    _IsModified = value;
                }
            }
        }
        private bool _IsModified;

        public bool IsNew
        {
            get
            {
                return _IsNew;
            }
            set
            {
                if (_IsNew != value)
                {
                    _IsNew = value;
                }
            }
        }
        private bool _IsNew;
    }
}