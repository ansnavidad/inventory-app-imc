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
            MovimientoModel movimi = new MovimientoModel(new MovimientoDataMapper());
            
            foreach (MovimientoModel mov in this.Movimiento)
            {
                mov.UnidMovimiento = movimi.UnidMovimiento;
                mov.FechaMovimiento = movimi.FechaMovimiento;
                mov.saveArticulo();
            }
            //items
            

            this.Facturas.Count();
            
        }

        private bool CanAttemptAddMovimientoCmd()
        {
            bool canAttempt = true;

           
            return canAttempt;
        }
        private CatalogReciboViewModel _CatalogReciboViewModel;

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

        public ObservableCollection<MovimientoModel> Movimiento
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
        private ObservableCollection<MovimientoModel> _Movimiento;
        public const string MovimientoPropertyName = "Movimiento";

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
            this._Movimiento = new ObservableCollection<MovimientoModel>();
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
