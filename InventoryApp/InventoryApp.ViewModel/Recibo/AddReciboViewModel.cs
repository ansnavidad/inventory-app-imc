using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model.Recibo;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Recibo
{
    public class AddReciboViewModel : ViewModelBase
    {
        private CatalogItemStatusModel _catalogItemStatusModel;
        private ReciboModel _addReciboModel;
        private const int MovimientoRecibo = 16;
        #region RelayCommands
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

        public ICommand AddMovimientoCmd2
        {
            get
            {
                if (_AddMovimientoCmd2 == null)
                {
                    _AddMovimientoCmd2 = new RelayCommand(p => this.AttemptAddMovimientoCmd2(), p => this.CanAttemptAddMovimientoCmd2());
                }
                return _AddMovimientoCmd2;
            }
        }
        private RelayCommand _AddMovimientoCmd2;

        private void AttemptAddMovimientoCmd()
        {
            LoteModel lot = new LoteModel(new LoteDataMapper());
            lot.UnidLote = UNID.getNewUNID();
            lot.UnidPOM = UNID.getNewUNID();
            lot.saveLote();
            //factura
            foreach (FacturaCompraModel item in this.Facturas)
            {
                item.UnidLote = lot.UnidLote;
                item.UnidFactura = UNID.getNewUNID();
                item.saveFactura();
                //factura detalle
                foreach (FacturaCompraDetalleModel fac in item.FacturaDetalle)
                {
                    fac.Factura = item;
                    fac.saveFacturaDetalle();
                }
            }

            //Agregar recibo
            DAL.POCOS.RECIBO recibo = new DAL.POCOS.RECIBO()
            {
                
                UNID_RECIBO = this.UnidRecibo,
                FECHA_CREACION = this.FechaCreacion,
                TT = this.TroubleTicket,
                PO = this.PO,
                UNID_SOLICITANTE = (this.SelectedSolicitante.UnidSolicitante == 0) ? (long?)null : this.SelectedSolicitante.UnidSolicitante
            };
            ReciboDataMapper reciboDataMapper = new ReciboDataMapper();
            reciboDataMapper.insertElement(recibo);


            foreach (InventoryApp.Model.Recibo.MovimientoModel mov in this.Movimientos)
            {
                //Agregar movimiento
                DAL.POCOS.MOVIMENTO movimiento = new MOVIMENTO()
                {
                    UNID_MOVIMIENTO = mov.UnidMovimiento
                    ,
                    FECHA_MOVIMIENTO = mov.FechaCaptura
                    ,
                    UNID_ALMACEN_DESTINO = (mov.DestinoAlmacen != null) ? mov.DestinoAlmacen.UnidAlmacen : (long?)null
                    ,
                    UNID_ALMACEN_PROCEDENCIA = (mov.OrigenAlmacen != null) ? mov.OrigenAlmacen.UnidAlmacen : (long?)null
                    ,
                    UNID_CLIENTE_PROCEDENCIA = (mov.OrigenCliente != null) ? mov.OrigenCliente.UnidCliente : (long?)null
                    ,
                    UNID_PROVEEDOR_PROCEDENCIA = (mov.OrigenProveedor != null) ? mov.OrigenProveedor.UnidProveedor : (long?)null
                    ,
                    TT = this.TroubleTicket
                    ,
                    UNID_TIPO_MOVIMIENTO = MovimientoRecibo
                };
                MovimientoDataMapper movDataMapper = new MovimientoDataMapper();
                movDataMapper.insertElement(movimiento);

                foreach (InventoryApp.Model.Recibo.ReciboItemModel item in mov.Items)
                {
                    long? aux = null;
                    if (item.ItemStatus != null)
                        aux = item.ItemStatus.UnidItemStatus;

                    //Agregar el item
                    DAL.POCOS.ITEM pItem = new DAL.POCOS.ITEM()
                    {
                        UNID_ITEM = item.UnidItem
                        ,
                        SKU = item.Sku
                        ,
                        NUMERO_SERIE = item.NumeroSerie
                        ,
                        UNID_ITEM_STATUS = aux
                        ,
                        COSTO_UNITARIO = item.CostoUnitario
                        ,
                        UNID_FACTURA_DETALE = item.FacturaDetalle.UnidFacturaCompraDetalle
                        ,
                        UNID_ARTICULO = item.Articulo.UnidArticulo
                        , 
                        PEDIMENTO_EXPO = item.PedimentoExpo
                        , 
                        PEDIMENTO_IMPO = item.PedimentoImpo
                        ,
                        CANTIDAD = item.Cantidad
                        ,
                        IS_ACTIVE = true
                    };
                    ItemDataMapper itemDataMapper = new ItemDataMapper();
                    itemDataMapper.insertElement(pItem);

                    //Agregar detalle del movimiento
                    DAL.POCOS.MOVIMIENTO_DETALLE movDetalle = new DAL.POCOS.MOVIMIENTO_DETALLE()
                    {
                        UNID_MOVIMIENTO = mov.UnidMovimiento
                        ,
                        UNID_ITEM = item.UnidItem
                        ,
                        UNID_MOVIMIENTO_DETALLE = item.UnidMovimientoDetalle
                        ,
                        CANTIDAD = item.Cantidad
                        , 
                        UNID_ITEM_STATUS = item.ItemStatus.UnidItemStatus
                        ,
                        IS_ACTIVE = true
                    };
                    MovimientoDetalleDataMapper mdDataMapper = new MovimientoDetalleDataMapper();
                    mdDataMapper.insertElement(movDetalle);

                    //Actualizar el último movimiento
                    DAL.POCOS.ULTIMO_MOVIMIENTO ulitmoMovto = new DAL.POCOS.ULTIMO_MOVIMIENTO()
                    {
                        UNID_ITEM=item.UnidItem,
                        UNID_ALMACEN=mov.DestinoAlmacen.UnidAlmacen,
                        UNID_MOVIMIENTO_DETALLE=item.UnidMovimientoDetalle,
                        CANTIDAD = item.Cantidad,
                        UNID_ITEM_STATUS = item.ItemStatus.UnidItemStatus,                        
                        IS_ACTIVE=true
                    };
                    UltimoMovimientoDataMapper umDataMapper = new UltimoMovimientoDataMapper();
                    umDataMapper.udpateElement(ulitmoMovto);
                }

                //Agregar recibodetalle
                DAL.POCOS.RECIBO_MOVIMIENTO rm = new DAL.POCOS.RECIBO_MOVIMIENTO()
                {
                    UNID_RECIBO = this._UnidRecibo,
                    UNID_RECIBO_MOVIMIENTO = mov.UnidMovimiento,
                    UNID_MOVIMIENTO = mov.UnidMovimiento,
                    UNID_FACTURA = mov.Items.First().FacturaDetalle.Factura.UnidFactura
                };
                ReciboMovimientoDataMapper rmDataMaper = new ReciboMovimientoDataMapper();
                rmDataMaper.insertElement(rm);
            }

            if (this._CatalogReciboViewModel != null)
            {
                this._CatalogReciboViewModel.updateCollection();
            }
        }

        private bool CanAttemptAddMovimientoCmd()
        {
            bool canAttempt = false;

            if ((this.Facturas != null && this.Facturas.Count > 0) && (this.Movimientos != null && this.Movimientos.Count > 0) && this.SelectedSolicitante != null && !string.IsNullOrEmpty(this.PO) && !string.IsNullOrEmpty(this.TroubleTicket))
            {
                canAttempt = true;
            }

            if (this.Facturas.Count > this.Movimientos.Count)
            {
                Msj2 = "Favor de relacionar cada factura a un Movimiento de Recibo.";
                return false;
            }
            else {
                
                Msj2 = "";
            }

            return canAttempt;
        }

        private void AttemptAddMovimientoCmd2()
        {
            //if (System.Windows.MessageBox.Show("¿Está seguro que desea finalizar el recibo actual? No se podrá editar posteriormente.", "Captura de Recibo", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
            //{
                LoteModel lot = new LoteModel(new LoteDataMapper());
                lot.UnidLote = UNID.getNewUNID();
                lot.UnidPOM = UNID.getNewUNID();
                lot.saveLote();
                //factura
                foreach (FacturaCompraModel item in this.Facturas)
                {
                    item.UnidLote = lot.UnidLote;
                    item.UnidFactura = UNID.getNewUNID();                    
                    item.saveFactura();
                    //factura detalle
                    foreach (FacturaCompraDetalleModel fac in item.FacturaDetalle)
                    {
                        fac.Factura = item;
                        fac.saveFacturaDetalle();
                    }
                }

                //Agregar recibo
                DAL.POCOS.RECIBO recibo = new DAL.POCOS.RECIBO()
                {

                    UNID_RECIBO = this.UnidRecibo,
                    FECHA_CREACION = this.FechaCreacion,
                    TT = this.TroubleTicket,
                    PO = this.PO,
                    UNID_SOLICITANTE = (this.SelectedSolicitante.UnidSolicitante == 0) ? (long?)null : this.SelectedSolicitante.UnidSolicitante
                };
                ReciboDataMapper reciboDataMapper = new ReciboDataMapper();
                reciboDataMapper.insertElement(recibo);


                foreach (InventoryApp.Model.Recibo.MovimientoModel mov in this.Movimientos)
                {
                    //Agregar movimiento
                    DAL.POCOS.MOVIMENTO movimiento = new MOVIMENTO()
                    {
                        UNID_MOVIMIENTO = mov.UnidMovimiento
                        ,
                        FECHA_MOVIMIENTO = mov.FechaCaptura
                        ,
                        UNID_ALMACEN_DESTINO = (mov.DestinoAlmacen != null) ? mov.DestinoAlmacen.UnidAlmacen : (long?)null
                        ,
                        UNID_ALMACEN_PROCEDENCIA = (mov.OrigenAlmacen != null) ? mov.OrigenAlmacen.UnidAlmacen : (long?)null
                        ,
                        UNID_CLIENTE_PROCEDENCIA = (mov.OrigenCliente != null) ? mov.OrigenCliente.UnidCliente : (long?)null
                        ,
                        UNID_PROVEEDOR_PROCEDENCIA = (mov.OrigenProveedor != null) ? mov.OrigenProveedor.UnidProveedor : (long?)null
                        ,
                        TT = this.TroubleTicket
                        ,
                        UNID_TIPO_MOVIMIENTO = MovimientoRecibo
                        , 
                        FINISHED = true
                    };
                    MovimientoDataMapper movDataMapper = new MovimientoDataMapper();
                    movDataMapper.insertElement(movimiento);

                    foreach (InventoryApp.Model.Recibo.ReciboItemModel item in mov.Items)
                    {
                        long? aux = null;
                        if (item.ItemStatus != null)
                            aux = item.ItemStatus.UnidItemStatus;

                        //Agregar el item
                        DAL.POCOS.ITEM pItem = new DAL.POCOS.ITEM()
                        {
                            UNID_ITEM = item.UnidItem
                            ,
                            SKU = item.Sku
                            ,
                            NUMERO_SERIE = item.NumeroSerie
                            ,
                            UNID_ITEM_STATUS = aux
                            ,
                            COSTO_UNITARIO = item.CostoUnitario
                            ,
                            UNID_FACTURA_DETALE = item.FacturaDetalle.UnidFacturaCompraDetalle
                            ,
                            UNID_ARTICULO = item.Articulo.UnidArticulo
                            ,
                            PEDIMENTO_EXPO = item.PedimentoExpo
                            ,
                            PEDIMENTO_IMPO = item.PedimentoImpo
                            ,
                            CANTIDAD = item.Cantidad
                            ,
                            IS_ACTIVE = true
                        };
                        ItemDataMapper itemDataMapper = new ItemDataMapper();
                        itemDataMapper.insertElement(pItem);

                        //Agregar detalle del movimiento
                        DAL.POCOS.MOVIMIENTO_DETALLE movDetalle = new DAL.POCOS.MOVIMIENTO_DETALLE()
                        {
                            UNID_MOVIMIENTO = mov.UnidMovimiento
                            ,
                            UNID_ITEM = item.UnidItem
                            ,
                            UNID_MOVIMIENTO_DETALLE = item.UnidMovimientoDetalle
                            ,
                            CANTIDAD = item.Cantidad
                            ,
                            UNID_ITEM_STATUS = item.ItemStatus.UnidItemStatus
                            ,
                            IS_ACTIVE = true
                        };
                        MovimientoDetalleDataMapper mdDataMapper = new MovimientoDetalleDataMapper();
                        mdDataMapper.insertElement(movDetalle);

                        //Actualizar el último movimiento
                        DAL.POCOS.ULTIMO_MOVIMIENTO ulitmoMovto = new DAL.POCOS.ULTIMO_MOVIMIENTO()
                        {
                            UNID_ITEM = item.UnidItem,
                            UNID_ALMACEN = mov.DestinoAlmacen.UnidAlmacen,
                            UNID_MOVIMIENTO_DETALLE = item.UnidMovimientoDetalle,
                            CANTIDAD = item.Cantidad,
                            UNID_ITEM_STATUS = item.ItemStatus.UnidItemStatus,
                            IS_ACTIVE = true
                        };
                        UltimoMovimientoDataMapper umDataMapper = new UltimoMovimientoDataMapper();
                        umDataMapper.udpateElement(ulitmoMovto);
                    }

                    //Agregar recibodetalle
                    DAL.POCOS.RECIBO_MOVIMIENTO rm = new DAL.POCOS.RECIBO_MOVIMIENTO()
                    {
                        UNID_RECIBO = this._UnidRecibo,
                        UNID_RECIBO_MOVIMIENTO = mov.UnidMovimiento,
                        UNID_MOVIMIENTO = mov.UnidMovimiento,
                        UNID_FACTURA = mov.Items.First().FacturaDetalle.Factura.UnidFactura
                    };
                    ReciboMovimientoDataMapper rmDataMaper = new ReciboMovimientoDataMapper();
                    rmDataMaper.insertElement(rm);
                }

                if (this._CatalogReciboViewModel != null)
                {
                    this._CatalogReciboViewModel.updateCollection();
                }
            //}
        }

        private bool CanAttemptAddMovimientoCmd2()
        {
            bool canAttempt = false;

            if ((this.Facturas != null && this.Facturas.Count > 0) && (this.Movimientos != null && this.Movimientos.Count > 0) && this.SelectedSolicitante != null && !string.IsNullOrEmpty(this.PO) && !string.IsNullOrEmpty(this.TroubleTicket))
            {
                canAttempt = true;
            }

            if (this.Facturas.Count > this.Movimientos.Count)
            {
                Msj2 = "Favor de relacionar cada factura a un Movimiento de Recibo.";
                return false;
            }
            else
            {

                Msj2 = "";
            }

            if (canAttempt) {

                foreach(Model.Recibo.MovimientoModel mm in this.Movimientos) {

                    foreach (ReciboItemModel rr in mm.Items) {

                        if (string.IsNullOrEmpty(rr.Sku) || string.IsNullOrEmpty(rr.NumeroSerie))
                            return false;
                    }
                }
            }

            return canAttempt;
        }

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

        //AgregarMovimiento
        public ICommand AddMvtoCmd
        {
            get
            {
                if (_AddMvtoCmd == null)
                {
                    _AddMvtoCmd = new RelayCommand(p => this.AttemptAddMvtoCmd(), p => this.CanAttemptAddMvtoCmd());
                }
                return _AddMvtoCmd;
            }
        }
        private RelayCommand _AddMvtoCmd;

        private void AttemptAddMvtoCmd()
        {
        }

        private bool CanAttemptAddMvtoCmd()
        {
            bool canAttempt = false;

            if (this.Facturas.Count > 0)
            {

                var res = (from o in this.Facturas
                           where !this.Movimientos.Any(f => f.Factura.UnidFactura == o.UnidFactura)
                           select o).ToList();
                if (res != null && res.Count > 0)
                {
                    canAttempt = true;
                }
            }

            return canAttempt;
        }

        //Borrar factura
        public ICommand DeleteFacturaCmd
        {
            get
            {
                if (_DeleteFacturaCmd == null)
                {
                    _DeleteFacturaCmd = new RelayCommand(p => this.AttemptDeleteFacturaCmd(), p => this.CanAttemptDeleteFacturaCmd());
                }
                return _DeleteFacturaCmd;
            }
        }
        private RelayCommand _DeleteFacturaCmd;

        private void AttemptDeleteFacturaCmd()
        {
            //Eliminar de movimientos
            if (this.Movimientos.Count > 0)
            {
                try
                {
                    (from o in this.Movimientos
                     join f in this._Facturas
                     on o.Factura.UnidFactura equals f.UnidFactura
                     where f.IsChecked == true
                     select o).ToList().ForEach(o =>
                     {                         
                         this.Movimientos.Remove(o);
                         this._DelMovs.Add(o.UnidMovimiento);
                     });
                }
                catch (Exception)
                {
                    ;
                }   
            }

            //eliminar facturas
            try
            {
                (from o in this.Facturas
                 where o.IsChecked == true
                 select o).ToList().ForEach(o => 
                 { 
                     this.Facturas.Remove(o);
                     this._DelFacturas.Add(o.UnidFactura);
                 });
            }
            catch (Exception)
            {
                ;
            }
        }

        private bool CanAttemptDeleteFacturaCmd()
        {
            bool canAttempt = false;

            if (!ContB)
                return false;

            //Validar que esté seleccionado un elemento del grid
            try
            {
                var res = (from o in this.Facturas
                           where o.IsChecked == true
                           select o).ToList().Count();
                if (res > 0)
                {
                    canAttempt = true;
                }
            }
            catch (Exception ex)
            {
                ;
            }

            return canAttempt;
        }

        //Borrar movimiento
        public ICommand DeleteMvtoCmd
        {
            get
            {
                if (_DeleteMvtoCmd == null)
                {
                    _DeleteMvtoCmd = new RelayCommand(p => this.AttemptDeleteMvtoCmd(), p => this.CanAttemptDeleteMvtoCmd());
                }
                return _DeleteMvtoCmd;
            }
        }
        private RelayCommand _DeleteMvtoCmd;

        private void AttemptDeleteMvtoCmd()
        {
            try
            {
                for (int i = 0; i < this.Movimientos.Count; ) {
                    
                    if (this.Movimientos[i].IsChecked == true)
                    {
                        Model.Recibo.MovimientoModel aux = new Model.Recibo.MovimientoModel();
                        aux = this.Movimientos[i];

                        (from o in this.Facturas
                         where o.UnidFactura == this.Movimientos[i].Factura.UnidFactura
                         select o).ToList().ForEach(o => o.HasNotRecibo = true);

                        this.Movimientos.Remove(aux);
                        this._DelMovs.Add(aux.UnidMovimiento);
                    } else {
                
                        i++;
                    }
                } 
            }
            catch (Exception)
            {
                ;
            }
        }

        private bool CanAttemptDeleteMvtoCmd()
        {
            bool canAttempt = false;

            if (!ContB)
                return false;

            //Validar que esté seleccionado un elemento del grid
            try
            {
                var res = (from o in this.Movimientos
                           where o.IsChecked == true
                           select o).ToList().Count();
                if (res > 0)
                {
                    canAttempt = true;
                }
            }
            catch (Exception ex)
            {
                ;
            }

            return canAttempt;
        } 
        #endregion

        protected CatalogReciboViewModel _CatalogReciboViewModel;

        public DateTime FechaCreacion
        {
            get {
                if (_FechaCreacion == DateTime.MinValue)
                {
                    _FechaCreacion = DateTime.Now;
                }
                return _FechaCreacion; 
            }
            set
            {
                if (_FechaCreacion != value)
                {
                    _FechaCreacion = value;
                    OnPropertyChanged(FechaCreacionPropertyName);
                }
            }
        }
        private DateTime _FechaCreacion;
        public const string FechaCreacionPropertyName = "FechaCreacion";

        public long UnidRecibo
        {
            get 
            {
                if (_UnidRecibo == 0)
                {
                    _UnidRecibo = DAL.UNID.getNewUNID();
                }
                return _UnidRecibo; 
            }
            set
            {
                if (_UnidRecibo != value)
                {
                    _UnidRecibo = value;
                    OnPropertyChanged(UnidReciboPropertyName);
                }
            }
        }
        protected long _UnidRecibo;
        public const string UnidReciboPropertyName = "UnidRecibo";

        public ObservableCollection<SolicitanteModel> Solicitantes
        {
            get { return _Solicitantes; }
            set
            {
                if (_Solicitantes != value)
                {
                    _Solicitantes = value;
                    OnPropertyChanged(SolicitantesPropertyName);
                }
            }
        }
        protected ObservableCollection<SolicitanteModel> _Solicitantes;
        public const string SolicitantesPropertyName = "Solicitantes";

        public SolicitanteModel SelectedSolicitante
        {
            get { return _SelectedSolicitante; }
            set
            {
                if (_SelectedSolicitante != value)
                {
                    _SelectedSolicitante = value;
                    OnPropertyChanged(SelectedSolicitantePropertyName);
                }
            }
        }
        protected SolicitanteModel _SelectedSolicitante;
        public const string SelectedSolicitantePropertyName = "SelectedSolicitante";

        public ObservableCollection<ClienteModel> Clientes
        {
            get { return _Clientes; }
            set
            {
                if (_Clientes != value)
                {
                    _Clientes = value;
                    OnPropertyChanged(ClientesPropertyName);
                }
            }
        }
        protected ObservableCollection<ClienteModel> _Clientes;
        public const string ClientesPropertyName = "Clientes";

        public ObservableCollection<Model.EmpresaModel> Empresas
        {
            get { return _Empresas; }
            set
            {
                if (_Empresas != value)
                {
                    _Empresas = value;
                    OnPropertyChanged(EmpresasPropertyName);
                }
            }
        }
        protected ObservableCollection<Model.EmpresaModel> _Empresas;
        public const string EmpresasPropertyName = "Empresas";

        public Model.EmpresaModel SelectedEmpresa
        {
            get { return _SelectedEmpresa; }
            set
            {
                if (_SelectedEmpresa != value)
                {
                    _SelectedEmpresa = value;
                    OnPropertyChanged(SelectedEmpresaPropertyName);
                    this.Solicitantes = this.GetSolicitantes(_SelectedEmpresa);
                }
            }
        }
        protected Model.EmpresaModel _SelectedEmpresa;
        public const string SelectedEmpresaPropertyName = "SelectedEmpresa";

        public ClienteModel SelectedCliente
        {
            get { return _SelectedCliente; }
            set
            {
                if (_SelectedCliente != value)
                {
                    _SelectedCliente = value;
                    OnPropertyChanged(SelectedClientePropertyName);
                }
            }
        }
        protected ClienteModel _SelectedCliente;
        public const string SelectedClientePropertyName = "SelectedCliente";

        public string TroubleTicket
        {
            get { return _TroubleTicket; }
            set
            {
                if (_TroubleTicket != value)
                {
                    _TroubleTicket = value;
                    OnPropertyChanged(TroubleTicketPropertyName);
                }
            }
        }
        protected string _TroubleTicket;
        public const string TroubleTicketPropertyName = "TroubleTicket";

        public string PO
        {
            get { return _PO; }
            set
            {
                if (_PO != value)
                {
                    _PO = value;
                    OnPropertyChanged(POPropertyName);
                }
            }
        }
        protected string _PO;
        public const string POPropertyName = "PO";

        public string Msj2
        {
            get { return _msj2; }
            set
            {
                if (_msj2 != value)
                {
                    _msj2 = value;
                    OnPropertyChanged(Msj2PropertyName);
                }
            }
        }
        protected string _msj2;
        public const string Msj2PropertyName = "Msj2";

        public List<long> DelFacturas
        {
            get { return _DelFacturas; }
            set
            {
                if (_DelFacturas != value)
                {
                    _DelFacturas = value;
                    OnPropertyChanged(DelFacturasPropertyName);
                }
            }
        }
        protected List<long> _DelFacturas;
        public const string DelFacturasPropertyName = "DelFacturas";

        public List<long> DelMovs
        {
            get { return _DelMovs; }
            set
            {
                if (_DelMovs != value)
                {
                    _DelMovs = value;
                    OnPropertyChanged(DelMovsPropertyName);
                }
            }
        }
        protected List<long> _DelMovs;
        public const string DelMovsPropertyName = "DelMovs";

        public ObservableCollection<PedimentoModel> Pedimentos
        {
            get { return _Pedimentos; }
            set
            {
                if (_Pedimentos != value)
                {
                    _Pedimentos = value;
                    OnPropertyChanged(PedimentosPropertyName);
                }
            }
        }
        protected ObservableCollection<PedimentoModel> _Pedimentos;
        public const string PedimentosPropertyName = "Pedimentos";

        public string PedimentoImpo
        {
            get { return _PedimentoImpo; }
            set
            {
                if (_PedimentoImpo != value)
                {
                    _PedimentoImpo = value;
                    OnPropertyChanged(PedimentoImpoPropertyName);
                }
            }
        }
        protected string _PedimentoImpo;
        public const string PedimentoImpoPropertyName = "PedimentoImpo";

        public string PedimentoExpo
        {
            get { return _PedimentoExpo; }
            set
            {
                if (_PedimentoExpo != value)
                {
                    _PedimentoExpo = value;
                    OnPropertyChanged(PedimentoExpoPropertyName);
                }
            }
        }
        protected string _PedimentoExpo;
        public const string PedimentoExpoPropertyName = "PedimentoExpo";

        public ObservableCollection<FacturaCompraModel> Facturas
        {
            get { return _Facturas; }
            set
            {
                if (_Facturas != value)
                {
                    _Facturas = value;
                    OnPropertyChanged(FacturasPropertyName);
                }
            }
        }
        protected ObservableCollection<FacturaCompraModel> _Facturas;
        public const string FacturasPropertyName = "Facturas";

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

        public ObservableCollection<InventoryApp.Model.Recibo.MovimientoModel> Movimientos
        {
            get { return _Movimiento; }
            set
            {
                if (_Movimiento != value)
                {
                    _Movimiento = value;
                    OnPropertyChanged(MovimientoPropertyName);
                }
            }
        }
        protected ObservableCollection<InventoryApp.Model.Recibo.MovimientoModel> _Movimiento;
        public const string MovimientoPropertyName = "MovimientoModel";

        public AddReciboViewModel()
        {
            this.ContB = true;
            this.init();
        }

        public AddReciboViewModel(CatalogReciboViewModel catalogReciboViewModel)
        {
            this.ContB = true;
             this._CatalogReciboViewModel = catalogReciboViewModel;
            try
            {

                this._catalogItemStatusModel = new CatalogItemStatusModel(new ItemStatusDataMapper());
            }
            catch (ArgumentException ae)
            {
                ;
            }
            this.init();
        }

        public void init()
        {
            this._DelFacturas = new List<long>();
            this._DelMovs = new List<long>();
            this._Solicitantes = new ObservableCollection<SolicitanteModel>();//this.GetSolicitantes();
            this._Clientes = this.GetClientes();
            this._Facturas = new ObservableCollection<FacturaCompraModel>();
            this._Movimiento = new ObservableCollection<InventoryApp.Model.Recibo.MovimientoModel>();
            this._Empresas = this.GetEmpresas();
            
            if(this._Empresas != null && this._Empresas.Count > 0)
                this.SelectedEmpresa = this._Empresas[0];
            if (this._Solicitantes != null && this._Solicitantes.Count > 0)
                this.SelectedSolicitante = this._Solicitantes[0];
        }

        private ObservableCollection<Model.EmpresaModel> GetEmpresas()
        {
            ObservableCollection<Model.EmpresaModel> empresas = new ObservableCollection<EmpresaModel>();
            EmpresaDataMapper emDataMapper = new EmpresaDataMapper();

            try
            {
                List<EMPRESA> listEmpresa = (List<EMPRESA>)emDataMapper.getElements();
                if (listEmpresa != null)
                {
                    listEmpresa.ForEach(o =>
                    {
                        empresas.Add(new EmpresaModel(emDataMapper)
                        {
                            UnidEmpresa = o.UNID_EMPRESA,
                            EmpresaName = o.EMPRESA_NAME
                        });
                    });
                }
            }
            catch (Exception)
            {
                ;
            }

            return empresas;
        }

        public ObservableCollection<SolicitanteModel> GetSolicitantes()
        {
            ObservableCollection<SolicitanteModel> solicitantes = new ObservableCollection<SolicitanteModel>();

            try
            {
                SolicitanteDataMapper solDataMapper = new SolicitanteDataMapper();
                List<SOLICITANTE> listSolicitante = (List<SOLICITANTE>)solDataMapper.getElements();
                listSolicitante.ForEach(o => solicitantes.Add(new SolicitanteModel(solDataMapper)
                {
                    UnidSolicitante = o.UNID_SOLICITANTE
                    ,
                    SolicitanteName = o.SOLICITANTE_NAME
                    ,
                    Empresa = new EMPRESA()
                    {
                        UNID_EMPRESA = o.EMPRESA.UNID_EMPRESA
                        ,
                        EMPRESA_NAME = o.EMPRESA.EMPRESA_NAME
                    }
                    ,
                    Departamento = new DEPARTAMENTO()
                    {
                        UNID_DEPARTAMENTO = o.DEPARTAMENTO.UNID_DEPARTAMENTO
                        ,
                        DEPARTAMENTO_NAME = o.DEPARTAMENTO.DEPARTAMENTO_NAME
                    }
                }));
            }
            catch (Exception)
            {
                ;
            }

            return solicitantes;
        }

        public ObservableCollection<SolicitanteModel> GetSolicitantes(Model.EmpresaModel empresa)
        {
            ObservableCollection<SolicitanteModel> solicitantes = new ObservableCollection<SolicitanteModel>();

            try
            {
                SolicitanteDataMapper solDataMapper = new SolicitanteDataMapper();
                List<SOLICITANTE> listSolicitante = (List<SOLICITANTE>)solDataMapper.GetSolicitanteList(new EMPRESA() 
                {
                    UNID_EMPRESA=empresa.UnidEmpresa,
                    EMPRESA_NAME=empresa.EmpresaName
                });

                listSolicitante.ForEach(o => solicitantes.Add(new SolicitanteModel(solDataMapper)
                {
                    UnidSolicitante = o.UNID_SOLICITANTE
                    ,
                    SolicitanteName = o.SOLICITANTE_NAME
                    ,
                    Empresa = new EMPRESA()
                    {
                        UNID_EMPRESA = o.EMPRESA.UNID_EMPRESA
                        ,
                        EMPRESA_NAME = o.EMPRESA.EMPRESA_NAME
                    }
                    ,
                    Departamento = new DEPARTAMENTO()
                    {
                        UNID_DEPARTAMENTO = o.DEPARTAMENTO.UNID_DEPARTAMENTO
                        ,
                        DEPARTAMENTO_NAME = o.DEPARTAMENTO.DEPARTAMENTO_NAME
                    }
                }));
            }
            catch (Exception)
            {
                ;
            }

            return solicitantes;
        }

        public ObservableCollection<ClienteModel> GetClientes()
        {
            ObservableCollection<ClienteModel> clientes = new ObservableCollection<ClienteModel>();

            try
            {
                ClienteDataMapper solDataMapper = new ClienteDataMapper();
                List<CLIENTE> listCliente = solDataMapper.getClienteList();
                listCliente.ForEach(o => clientes.Add(new ClienteModel(solDataMapper)
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

        public AddFacturaViewModel CreateAddFacturaViewModel()
        {
            AddFacturaViewModel addFacturaViewModel = new AddFacturaViewModel(this);
            return addFacturaViewModel;
        }

        public AddMovimientoViewModel CreateAddMovimientoViewModel()
        {
            AddMovimientoViewModel addFacturaViewModel = new AddMovimientoViewModel(this);
            return addFacturaViewModel;
        }

        public ModifyFacturaViewModel CraeteModifyFacturaViewModel()
        {
            if (this.SelectedFactura != null)
                return new ModifyFacturaViewModel(this.SelectedFactura, this.ContB);
            else
                return null;
        }

        public ModifyMovimientoViewModel CreateModifyMovimientoViewModel()
        {
            if (this.SelectedMovimiento != null)
                return new ModifyMovimientoViewModel(this.SelectedMovimiento, this.ContB);
            else
                return null;
        }

        //recibo
        public CatalogItemStatusModel CatalogItemStatusModel
        {
            get
            {
                return _catalogItemStatusModel;
            }
            set
            {
                _catalogItemStatusModel = value;
            }
        }
        public ReciboModel AddReciboModel
        {
            get
            {
                return _addReciboModel;
            }
            set
            {
                _addReciboModel = value;
            }
        }
    }
}
