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

namespace InventoryApp.ViewModel.Recibo
{
    public class AddMovimientoViewModel:ViewModelBase
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

        private AddReciboViewModel _AddReciboViewModel;

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

        public TipoMovimientoModel SelectedTipoPedimento
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
        private TipoMovimientoModel _SelectedTipoMovimiento;
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
                        case "Cliente":
                            {

                                this.Clientes.ToList().ForEach(o => origenes.Add(o));
                                return origenes;   
                            }
                        case "Proveedor":
                            {

                                this.Proveedores.ToList().ForEach(o => origenes.Add(o));
                                return origenes;
                            }
                    }
                }
                return origenes;
            }
        }
        private ObservableCollection<IOrigenModel> _Origenes;
        public const string OrigenesPropertyName = "Origenes";

        public AddMovimientoViewModel()
        {

        }

        public AddMovimientoViewModel(AddReciboViewModel addReciboViewModel)
        {
            this._AddReciboViewModel = addReciboViewModel;
            this._Items = new ObservableCollection<ReciboItemModel>();
        }

        public AddMovimientoDetalleViewModel CreateAddMovimientoDetalleViewModel()
        {
            return new AddMovimientoDetalleViewModel(this,this._AddReciboViewModel);
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
                new TipoOrigenModel("Cliente")
                ,new TipoOrigenModel("Proveedor")
                ,new TipoOrigenModel("Almacen")
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
                MovimientoModel movimiento = new MovimientoModel()
                {
                    UnidAlmacenDestino = this._SelectedAlmacenDestino.UnidAlmacen,
                    //este codigo esta harcodeado para el tipo de movimiento
                    TipoMovimiento = new TIPO_MOVIMIENTO() { UNID_TIPO_MOVIMIENTO = 5, TIPO_MOVIMIENTO_NAME = "Salida Renta", SIGNO_MOVIMIENTO = "-", IS_ACTIVE = true },
                    UnidClienteProcedencia= this._AddReciboViewModel.SelectedCliente.UnidCliente // no estoy seguro que sea el cliente indicado duda

                };
                this._AddReciboViewModel.Movimiento.Add(movimiento);
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
