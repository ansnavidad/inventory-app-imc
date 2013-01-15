using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.Model;
using InventoryApp.Model.Recibo;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace InventoryApp.ViewModel.Recibo
{
    public class AddMovimientoViewModel:ViewModelBase
    {
        private const string TipoOrigenCliente = "Cliente";
        private const string TipoOrigenAlmacen = "Almacen";
        private const string TipoOrigenProveedor = "Proveedor";

        private MovimientoSelectArticuloViewModel _MovimientoSelectArticuloViewModel;

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

        public TipoOrigenModel SelectedTipoOrigen
        {
            get { return _SelectedTipoOrigen; }
            set
            {
                if (_SelectedTipoOrigen != value)
                {
                    _SelectedTipoOrigen = value;
                    OnPropertyChanged(SelectedTipoOrigenPropertyName);
                    OnPropertyChanged(OrigenesPropertyName);
                }
            }
        }
        private TipoOrigenModel _SelectedTipoOrigen;
        public const string SelectedTipoOrigenPropertyName = "SelectedTipoOrigen";

        public ObservableCollection<TipoOrigenModel> TiposOrigen
        {
            get 
            {
                if (_TiposOrigen == null)
                {
                    _TiposOrigen = this.GetTiposOrigen();
                }

                return _TiposOrigen; 
            }
            set
            {
                if (_TiposOrigen != value)
                {
                    _TiposOrigen = value;
                    OnPropertyChanged(TiposOrigenPropertyName);
                }
            }
        }
        private ObservableCollection<TipoOrigenModel> _TiposOrigen;
        public const string TiposOrigenPropertyName = "TiposOrigen";

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

        public ObservableCollection<IOrigenModel> Origenes
        {
            get 
            {
                ObservableCollection<IOrigenModel> origenes = new ObservableCollection<IOrigenModel>();
                if (this.SelectedTipoOrigen != null)
                {

                    switch (this.SelectedTipoOrigen.TipoOrigenName)
                    {
                        case TipoOrigenCliente:
                            {

                                this.Clientes.ToList().ForEach(o => origenes.Add(o));
                                return origenes;   
                            }
                        case TipoOrigenProveedor:
                            {

                                this.Proveedores.ToList().ForEach(o => origenes.Add(o));
                                return origenes;
                            }
                        case TipoOrigenAlmacen:
                            {

                                this.Almacenes.ToList().ForEach(o => origenes.Add(o));
                                return origenes;
                            }
                    }
                }
                return origenes;
            }
        }
        private ObservableCollection<IOrigenModel> _Origenes;
        public const string OrigenesPropertyName = "Origenes";

        public FacturaCompraModel SelectedFactura
        {
            get { return _SelectedFactura; }
            set
            {
                if (_SelectedFactura != value)
                {
                    _SelectedFactura = value;
                    OnPropertyChanged(SelectedFacturaPropertyName);

                    this.SelectedOrigen = new Model.Recibo.OrigenProveedorModel() 
                    {
                        UnidProveedor = _SelectedFactura.Proveedor.UnidProveedor,
                        ProveedorName=_SelectedFactura.Proveedor.ProveedorName
                    }; 

                    this.Items = new ObservableCollection<ReciboItemModel>();
                    long unid = DAL.UNID.getNewUNID();
                    this._MovimientoSelectArticuloViewModel.FacturaDetalles.ToList().ForEach(o =>
                    {
                        if (o.IsSelected)
                        {
                            unid++;
                            this.Items.Add(new ReciboItemModel()
                            {
                                Articulo = o.Articulo,
                                FacturaDetalle = o,
                                UnidMovimiento = this.UnidMovimiento,
                                UnidItem = unid,
                                UnidMovimientoDetalle = unid,
                                Cantidad = o.Cantidad,
                                //IsCantidadEnabled = o.IsSelected ? true : false
                                IsCantidadEnabled = true                               
                            });
                        }
                        else
                        {
                            for (int i = 0; i < o.Cantidad; i++)
                            {
                                unid++;
                                this.Items.Add(new ReciboItemModel()
                                {
                                    Articulo = o.Articulo,
                                    FacturaDetalle = o,
                                    UnidMovimiento = this.UnidMovimiento,
                                    UnidItem = unid,
                                    UnidMovimientoDetalle = unid,
                                    Cantidad = 1,
                                    //IsCantidadEnabled = o.IsSelected ? true : false
                                    IsCantidadEnabled = false
                                });
                            }
                        }
                    });
                }
            }
        }
        private FacturaCompraModel _SelectedFactura;
        public const string SelectedFacturaPropertyName = "SelectedFactura";

        public ObservableCollection<FacturaCompraModel> FacturasDisponibles
        {
            get 
            {
                ObservableCollection<FacturaCompraModel> facturas = new ObservableCollection<FacturaCompraModel>();
                (from o in this.AddReciboViewModel.Facturas
                 where !this.AddReciboViewModel.Movimientos.Any(f => f.Factura.UnidFactura == o.UnidFactura)
                 select o).ToList().ForEach(o => facturas.Add(o));

                return facturas;
            }
        }
        

        public AddMovimientoViewModel()
        {

        }

        public AddMovimientoViewModel(AddReciboViewModel addReciboViewModel)
        {
            this._AddReciboViewModel = addReciboViewModel;
            this._Items = new ObservableCollection<ReciboItemModel>();
            this.SelectedTipoOrigen = new TipoOrigenModel(TipoOrigenProveedor);
        }

        public AddMovimientoDetalleViewModel CreateAddMovimientoDetalleViewModel()
        {
            return new AddMovimientoDetalleViewModel(this,this._AddReciboViewModel);
        }

        public MovimientoSelectArticuloViewModel CreateMovimientoSelectArticuloViewModel()
        {
            this._MovimientoSelectArticuloViewModel= new MovimientoSelectArticuloViewModel(this.SelectedFactura);
            return this._MovimientoSelectArticuloViewModel;
        }

        public MovimientoSelectArticuloViewModel CreateMovimientoSelectArticuloViewModel(Object SelectedFactura)
        {
            this._MovimientoSelectArticuloViewModel = new MovimientoSelectArticuloViewModel(SelectedFactura as FacturaCompraModel);
            return this._MovimientoSelectArticuloViewModel;
        }

        private ObservableCollection<TipoPedimentoModel> GetTipoPedimento()
        {
            ObservableCollection<TipoPedimentoModel> tpoPedimentos = new ObservableCollection<TipoPedimentoModel>();

            try
            {
                TipoPedimentoDataMapper dataMapper = new TipoPedimentoDataMapper();
                dataMapper.getListElements().ForEach(o => tpoPedimentos.Add(new TipoPedimentoModel(dataMapper)
                {
                    UnidTipoPedimento=o.UNID_TIPO_PEDIMENTO
                    ,TipoPedimentoName=o.TIPO_PEDIMENTO_NAME
                }));
            }
            catch (Exception ex)
            {
            }

            return tpoPedimentos;
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

        private ObservableCollection<TipoOrigenModel> GetTiposOrigen()
        {
            ObservableCollection<TipoOrigenModel> tiposOrigen = new ObservableCollection<TipoOrigenModel>()
            {
                new TipoOrigenModel(TipoOrigenCliente)
                ,new TipoOrigenModel(TipoOrigenAlmacen)
                ,new TipoOrigenModel(TipoOrigenProveedor)
            };

            return tiposOrigen;
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
                    UnidAlmacen=o.UNID_ALMACEN
                    ,AlmacenName=o.ALMACEN_NAME                 
                    
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
            if (this._AddReciboViewModel != null)
            {
                InventoryApp.Model.Recibo.MovimientoModel movimiento = new InventoryApp.Model.Recibo.MovimientoModel()
                {
                    DestinoAlmacen=this._SelectedAlmacenDestino
                    ,
                    OrigenAlmacen = this.SelectedTipoOrigen.TipoOrigenName.ToLower() == TipoOrigenAlmacen.ToLower() ? this._SelectedOrigen as InventoryApp.Model.Recibo.AlmacenModel : null
                    ,
                    OrigenCliente = this.SelectedTipoOrigen.TipoOrigenName.ToLower() == TipoOrigenCliente.ToLower() ? this._SelectedOrigen as InventoryApp.Model.Recibo.OrigenClienteModel : null
                    ,
                    OrigenProveedor = this.SelectedTipoOrigen.TipoOrigenName.ToLower() == TipoOrigenProveedor.ToLower() ? this._SelectedOrigen as InventoryApp.Model.Recibo.OrigenProveedorModel : null
                    ,
                    TipoPedimento=this.SelectedTipoPedimento
                    ,
                    Items=this.Items
                    ,
                    UnidMovimiento=this.UnidMovimiento
                    ,
                    FechaCaptura=this.FechaCaptura
                    ,
                    Factura=this.SelectedFactura
                    ,
                    Origen=this.SelectedOrigen
                };
                this._AddReciboViewModel.Movimientos.Add(movimiento);
            }
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
                excel.Cells[17, 12] = this._Items.Count;
                //IMPORTE:
                excel.Cells[17, 26] = this._SelectedFactura.Importe.ToString();
                //IVA %
                excel.Cells[19, 26] = this._SelectedFactura.PorIva.ToString();
                //IVA $
                excel.Cells[21, 26] = this._SelectedFactura.Iva.ToString();
                //TOTAL $
                excel.Cells[23, 26] = this._SelectedFactura.Total.ToString();                

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


        public void init()
        {
            //this._CanSelecteProveedor = true;
            //this._Proveedores = this.GetProveedores();
            //this._Monedas = this.GetMonedas();
            //this._FacturaDetalles = new ObservableCollection<FacturaCompraDetalleModel>();
            //this._TipoPedimentos = this.GetPedimentos();
            //this._FechaFactura = DateTime.Now;
            //this._FacturaDetalles.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e)
            //{
            //    if ((sender as ObservableCollection<FacturaCompraDetalleModel>).Count > 0)
            //    {
            //        this.CanSelecteProveedor = false;
            //    }
            //    else
            //    {
            //        this.CanSelecteProveedor = true;
            //    }
            //};
        }
    }
}
