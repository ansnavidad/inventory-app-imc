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
            //movimientos
            //MovimientoModel movimi = new MovimientoModel(new MovimientoDataMapper());

            //foreach (MovimientoModel mov in this.Movimientos)
            //{
            //    mov.UnidMovimiento = movimi.UnidMovimiento;
            //    mov.FechaMovimiento = movimi.FechaMovimiento;
            //    mov.saveArticulo();
            //}
            //items

            //Agregar recibo
            DAL.POCOS.RECIBO recibo = new DAL.POCOS.RECIBO()
            {
                UNID_RECIBO = this.UnidRecibo,
                FECHA_CREACION = this.FechaCreacion,
                TT = this.TroubleTicket,
                PO=this.PO
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
                    UNID_TIPO_MOVIMIENTO = 1
                };
                MovimientoDataMapper movDataMapper = new MovimientoDataMapper();
                movDataMapper.insertElement(movimiento);

                foreach (InventoryApp.Model.Recibo.ReciboItemModel item in mov.Items)
                {
                    //Agregar el item
                    DAL.POCOS.ITEM pItem = new DAL.POCOS.ITEM()
                    {
                        UNID_ITEM = item.UnidItem
                        ,
                        SKU = item.Sku
                        ,
                        NUMERO_SERIE = item.NumeroSerie
                        ,
                        UNID_ITEM_STATUS = 1
                        ,
                        COSTO_UNITARIO = item.CostoUnitario
                        ,
                        UNID_FACTURA_DETALE = item.FacturaDetalle.UnidFacturaCompraDetalle
                        ,
                        UNID_ARTICULO = item.Articulo.UnidArticulo
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
                        IS_ACTIVE = true
                    };
                    MovimientoDetalleDataMapper mdDataMapper = new MovimientoDetalleDataMapper();
                    mdDataMapper.insertElement(movDetalle);
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

            if ((this.Facturas != null && this.Facturas.Count > 0)&&(this.Movimientos != null && this.Movimientos.Count > 0))
            {
                canAttempt = true;
            }

            return canAttempt;
        }

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
                 select o).ToList().ForEach(o => this.Facturas.Remove(o));
            }
            catch (Exception)
            {
                ;
            }
        }

        private bool CanAttemptDeleteFacturaCmd()
        {
            bool canAttempt = false;

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
            //eliminar facturas
            try
            {
                (from o in this.Movimientos
                 where o.IsChecked == true
                 select o).ToList().ForEach(o => this.Movimientos.Remove(o));
            }
            catch (Exception)
            {
                ;
            }
        }

        private bool CanAttemptDeleteMvtoCmd()
        {
            bool canAttempt = false;

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

        private CatalogReciboViewModel _CatalogReciboViewModel;

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
        private long _UnidRecibo;
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
        private ObservableCollection<SolicitanteModel> _Solicitantes;
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
        private SolicitanteModel _SelectedSolicitante;
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
        private ObservableCollection<ClienteModel> _Clientes;
        public const string ClientesPropertyName = "Clientes";

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
        private ClienteModel _SelectedCliente;
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
        private string _TroubleTicket;
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
        private string _PO;
        public const string POPropertyName = "PO";

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
        private ObservableCollection<PedimentoModel> _Pedimentos;
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
        private string _PedimentoImpo;
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
        private string _PedimentoExpo;
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
        private ObservableCollection<FacturaCompraModel> _Facturas;
        public const string FacturasPropertyName = "Facturas";

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
        private ObservableCollection<InventoryApp.Model.Recibo.MovimientoModel> _Movimiento;
        public const string MovimientoPropertyName = "MovimientoModel";

        public AddReciboViewModel()
        {
            this.init();
        }

        public AddReciboViewModel(CatalogReciboViewModel catalogReciboViewModel)
        {
            this._CatalogReciboViewModel = catalogReciboViewModel;
            this.init();
        }

        public void init()
        {
            this._Solicitantes = this.GetSolicitantes();
            this._Clientes = this.GetClientes();
            this._Facturas = new ObservableCollection<FacturaCompraModel>();
            this._Movimiento = new ObservableCollection<InventoryApp.Model.Recibo.MovimientoModel>();
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


    }
}
