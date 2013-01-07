using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;
using System.Collections.ObjectModel;
using InventoryApp.DAL.Recibo;

namespace InventoryApp.Model
{
    public class ItemModel : ITEM, INotifyPropertyChanged
    {
        //item
        private bool _isChecked;
        private string _nombre;
        private long _unidItem;
        private string _sku;
        private string _numeroSerie;
        private double _costoUnitario;
        private double _pedimentoExpo;
        private double _pedimentoImpo;
        private int _cantidadDisponible;
        private int _cantidadMovimiento;
        private int _cantidaditem;
        private MODELO _modelo;
        private MARCA _marca;
        private CATEGORIA _categoria;
        private EQUIPO _equipo;
        private ItemDataMapper _dataMapper;

        private string _error;

        private ARTICULO _articulo;
        private ITEM_STATUS _itemStatus;
        private FACTURA_DETALLE _facturaDetalle;
        
        //Factura
        private FACTURA _factura;
        private PROVEEDOR _proveedor;
        private MONEDA _moneda;
        ObservableCollection<DeleteFacturaDetalleModel> _detalles;

        

        private ObservableCollection<ARTICULO> _Articulos;
        private List<PROVEEDOR> _Proveedores;
        private ObservableCollection<FACTURA> _Facturas;
        //Articulos
        public ObservableCollection<ARTICULO> Articulos
        {
            get { return this._Articulos; }
            set
            {
                if (value != this._Articulos)
                {
                    this._Articulos = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Articulos"));
                }
            }
        }

        public ObservableCollection<DeleteFacturaDetalleModel> Detalles
        {
            get { return this._detalles; }
            set
            {
                if (value != this._detalles)
                {
                    this._detalles = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Detalles"));
                }
            }
        }

        //Proveedores
        public List<PROVEEDOR> Proveedores
        {
            get { return this._Proveedores; }
            set
            {
                if (value != this._Proveedores)
                {
                    this._Proveedores = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Proveedores"));
                }
            }
        }


        //Facturas
        public ObservableCollection<FACTURA> Facturas
        {
            get { return this._Facturas; }
            set
            {
                if (value != this._Facturas)
                {
                    this._Facturas = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Facturas"));
                }
            }
        }


        public string Error
        {
            get { return this._error; }
            set
            {
                if (value != this._error)
                {
                    this._error = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Error"));
                }
            }
        }

        public int CantidadDisponible
        {
            get { return this._cantidadDisponible; }
            set
            {
                if (value != this._cantidadDisponible)
                {
                    this._cantidadDisponible = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Cantidad Disponible"));
                }
            }
        }

        public int CantidadMovimiento
        {
            get { return this._cantidadMovimiento; }
            set
            {
                if (value != this._cantidadMovimiento)
                {
                    this._cantidadMovimiento = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Cantidad Disponible"));
                }
            }
        }


        public MONEDA Moneda
        {
            get { return this._moneda; }
            set
            {
                if (value != this._moneda)
                {
                    this._moneda = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Moneda"));
                }
            }
        }

        public PROVEEDOR Proveedor
        {
            get { return this._proveedor; }
            set
            {
                if (value != this._proveedor)
                {
                    this._proveedor = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Proveedor"));
                }
            }
        }

        public MODELO Modelo
        {
            get { return this._modelo; }
            set
            {
                if (value != this._modelo)
                {
                    this._modelo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Modelo"));
                }
            }
        }

        public MARCA Marca
        {
            get { return this._marca; }
            set
            {
                if (value != this._marca)
                {
                    this._marca = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Marca"));
                }
            }
        }

        public FACTURA Factura
        {
            get { return this._factura; }
            set
            {
                if (value != this._factura)
                {
                    this._factura = value;
                    if (this._factura != null)
                    {
                        this.Proveedor = GetProveedorbyID((long)this._factura.UNID_PROVEEDOR);
                        this.Moneda = GetMonedabyID((long)this._factura.UNID_MONEDA);
                        this.Detalles = GetDetallebyFactura(this._factura);
                    }
                    else
                    {
                        this.Proveedor = new PROVEEDOR();
                        this.Moneda = new MONEDA();
                        this.FacturaDetalle = new FACTURA_DETALLE();
                        this.Detalles = new ObservableCollection<DeleteFacturaDetalleModel>();
                    }
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Factura"));
                }
            }
        }


        public double CostoUnitario
        {
            get { return this._costoUnitario; }
            set
            {
                if (value != this._costoUnitario)
                {
                    this._costoUnitario = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("CostoUnitario"));
                }
            }
        }

        public double PedimentoExpo
        {
            get { return this._pedimentoExpo; }
            set
            {
                if (value != this._pedimentoExpo)
                {
                    this._pedimentoExpo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PedimentoExpo"));
                }
            }
        }

        public double PedimentoImpo
        {
            get { return this._pedimentoImpo; }
            set
            {
                if (value != this._pedimentoImpo)
                {
                    this._pedimentoImpo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PedimentoImpo"));
                }
            }
        }

        public CATEGORIA Categoria
        {
            get { return this._categoria; }
            set
            {
                if (value != this._categoria)
                {
                    this._categoria = value;
                    this.Articulos = GetArticulosByCategoria(this._categoria);
                    this.Proveedores = GetProveedorbyCategoria(this._categoria);
                    this.Facturas = GetFacturasbyProveedores(this._Proveedores);

                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Categoria"));
                }
            }
        }

        public EQUIPO Equipo
        {
            get { return this._equipo; }
            set
            {
                if (value != this._equipo)
                {
                    this._equipo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Equipo"));
                }
            }
        }

        public bool IsChecked
        {
            get { return this._isChecked; }
            set
            {
                if (value != this._isChecked)
                {
                    this._isChecked = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }

        public int CantidadItem
        {
            get { return this._cantidaditem; }
            set
            {
                if (value != this._cantidaditem)
                {
                    this._cantidaditem = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("CantidadItem"));
                }
            }
        }

        public long UnidItem
        {
            get { return this._unidItem; }
            set
            {
                if (value != this._unidItem)
                {
                    this._unidItem = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidItem"));
                }
            }
        }

        public string Nombre
        {
            get { return this._nombre; }
            set
            {
                if (value != this._nombre)
                {
                    this._nombre = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Nombre"));
                }
            }
        }

        public string Sku
        {
            get { return this._sku; }
            set
            {
                if (value != this._sku)
                {
                    this._sku = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("SKU"));
                }
            }
        }

        public string NumeroSerie
        {
            get { return this._numeroSerie; }
            set
            {
                if (value != this._numeroSerie)
                {
                    this._numeroSerie = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("NumeroSerie"));
                }
            }
        }

        public ARTICULO Articulo
        {
            get { return this._articulo; }
            set
            {
                if (value != this._articulo)
                {
                    this._articulo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Articulo"));
                }
            }
        }

        public FACTURA_DETALLE FacturaDetalle
        {
            get { return this._facturaDetalle; }
            set
            {
                if (value != this._facturaDetalle)
                {
                    this._facturaDetalle = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("FacturaDetalle"));
                }
            }
        }

        public ITEM_STATUS ItemStatus
        {
            get { return this._itemStatus; }
            set
            {
                if (value != this._itemStatus)
                {
                    this._itemStatus = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ItemStatus"));
                }
            }
        }

        public ItemModel(ITEM item) 
        {
            this._articulo = item.ARTICULO;
            this._nombre = item.ARTICULO.ARTICULO1;
            this._unidItem = item.UNID_ITEM;
            this.SKU = item.SKU;
            this.NUMERO_SERIE = item.NUMERO_SERIE;
            this._itemStatus = item.ITEM_STATUS;
            this.COSTO_UNITARIO = item.COSTO_UNITARIO;
            this._pedimentoExpo = item.PEDIMENTO_EXPO;
            this._pedimentoImpo = item.PEDIMENTO_IMPO;
            //this._facturaDetalle = item.FACTURA_DETALLE;
            this.IS_ACTIVE = item.IS_ACTIVE;
            this.IsChecked = false;
            this._equipo = item.ARTICULO.EQUIPO;
            this._categoria = item.ARTICULO.CATEGORIA;
            this._marca = item.ARTICULO.MARCA;
            this._modelo = item.ARTICULO.MODELO;
            this.Detalles = new ObservableCollection<DeleteFacturaDetalleModel>();
        }

        public ItemModel()
        {
            this._dataMapper = new ItemDataMapper();
            this.Detalles = new ObservableCollection<DeleteFacturaDetalleModel>();
        }


        public void updateItem()
        {
            this._dataMapper.udpateElement(new ITEM() {UNID_ITEM = this._unidItem, UNID_ARTICULO = this._articulo.UNID_ARTICULO, SKU = this._sku, NUMERO_SERIE = this._numeroSerie, ITEM_STATUS = this._itemStatus, COSTO_UNITARIO = this._costoUnitario, UNID_FACTURA_DETALE = this._facturaDetalle.UNID_FACTURA_DETALE, PEDIMENTO_IMPO = this._pedimentoImpo, PEDIMENTO_EXPO = this._pedimentoExpo, CANTIDAD = this._cantidaditem});
        }

        public void updateFacturas()
        {
            this.Facturas = GetFacturasbyProveedores(this._Proveedores);
        }

        public void init()
        {
            this.Categoria = new CATEGORIA();
            this.Articulo = new ARTICULO();
            this.Proveedor = new PROVEEDOR();
            this._unidItem = new long();
            this.ItemStatus = new ITEM_STATUS();
            this.FacturaDetalle = new FACTURA_DETALLE();
            this.CostoUnitario = new long();
            this.PedimentoExpo = new long();
            this.PedimentoImpo = new long();
            this.CantidadItem = new int();
            this.Detalles = new ObservableCollection<DeleteFacturaDetalleModel>();
        }

        public void clear()
        {
            this.NumeroSerie = "";
            this.Sku = "";
            this.Categoria = new CATEGORIA();
            this.Articulo = new ARTICULO();
            this.Proveedor = new PROVEEDOR();
            this._unidItem = new long();
            this.ItemStatus = new ITEM_STATUS();
            this.FacturaDetalle = new FACTURA_DETALLE();
            this.CostoUnitario = new long();
            this.PedimentoExpo = new long();
            this.PedimentoImpo = new long();
            this.CantidadItem = new int();
            this.Detalles = new ObservableCollection<DeleteFacturaDetalleModel>();
        }

        public void getElement()
        {
            ITEM aux = new ITEM();
            aux.SKU = this.Sku;
            aux.NUMERO_SERIE = this.NumeroSerie;
            this.Error = "";
           
            ITEM res = (this._dataMapper.getElement(aux)) as ITEM;
            this.init();
            if (res != null)
            {
                this._unidItem = res.UNID_ITEM;
                this.NumeroSerie = res.NUMERO_SERIE;
                this.Sku = res.SKU;
                this.ItemStatus = res.ITEM_STATUS;
                this.FacturaDetalle = res.FACTURA_DETALLE;
                this.CostoUnitario = res.COSTO_UNITARIO;
                this.PedimentoExpo = res.PEDIMENTO_EXPO;
                this.PedimentoImpo = res.PEDIMENTO_IMPO;
                this.CantidadItem = res.CANTIDAD;

                CategoriaDataMapper datacat = new CategoriaDataMapper();
                this.Categoria = datacat.getElementByArticulo(res.ARTICULO);

                foreach (ARTICULO i in this.Articulos)
                {
                    if (i.UNID_ARTICULO == res.ARTICULO.UNID_ARTICULO)
                        this.Articulo = i;
                }

                this.FacturaDetalle = res.FACTURA_DETALLE;
                FACTURA temp = new FACTURA();
                temp = GetFacturabyDetalle(this.FacturaDetalle);

                foreach (FACTURA f in this.Facturas)
                {
                    if (temp.UNID_FACTURA == f.UNID_FACTURA)
                        this.Factura = f;
                }

                foreach (DeleteFacturaDetalleModel d in this.Detalles)
                {
                    if (this.FacturaDetalle != null)
                    {
                        if (d.UNID_FACTURA_DETALE == this.FacturaDetalle.UNID_FACTURA_DETALE)
                            this.FacturaDetalle = d;
                    }
                    else
                        this.FacturaDetalle = new FACTURA_DETALLE();
                }
            }
            else
            {
                this.Error = "El número de serie o SKU no existe";
            }
        }

        private ObservableCollection<ARTICULO> GetArticulosByCategoria(CATEGORIA categoria)
        {
            ObservableCollection<ARTICULO> articuloModels = new ObservableCollection<ARTICULO>();
            try
            {
                ArticuloDataMapper artDataMapper = new ArticuloDataMapper();
                List<ARTICULO> articulos = (List<ARTICULO>)artDataMapper.getElementsByCategoria(new CATEGORIA() { UNID_CATEGORIA = categoria.UNID_CATEGORIA });

                articulos.ForEach(o => articuloModels.Add(new ARTICULO()
                {
                    UNID_ARTICULO = o.UNID_ARTICULO
                    ,
                    ARTICULO1 = o.ARTICULO1
                    
                }));
            }
            catch (Exception)
            {
                ;
            }

            return articuloModels;
        }

        private List<PROVEEDOR> GetProveedorbyCategoria(CATEGORIA categoria)
        {
            List<PROVEEDOR> articuloModels = new List<PROVEEDOR>();
            try
            {
                ProveedorDataMapper artDataMapper = new ProveedorDataMapper();
                List<PROVEEDOR> articulos = artDataMapper.getElementsByCategoria(new CATEGORIA() { UNID_CATEGORIA = categoria.UNID_CATEGORIA });

                articulos.ForEach(o => articuloModels.Add(new PROVEEDOR()
                {
                    UNID_PROVEEDOR = o.UNID_PROVEEDOR
                    ,
                    PROVEEDOR_NAME = o.PROVEEDOR_NAME

                }));
            }
            catch (Exception)
            {
                ;
            }

            return articuloModels;
        }

        private FACTURA GetFacturabyDetalle(FACTURA_DETALLE detalle)
        {

            FACTURA fac = new FACTURA();
            try
            {
                FacturaCompraDataMapper artDataMapper = new FacturaCompraDataMapper();
                fac = artDataMapper.getFacturabyDetalle(detalle);

                
            }
            catch (Exception)
            {
                ;
            }

            return fac;
        }

        public PROVEEDOR GetProveedorbyID(long unidproveedor)
        {
            PROVEEDOR prov = new PROVEEDOR();
            if (unidproveedor!= null)
            {
                
                PROVEEDOR aux = new PROVEEDOR();
                aux.UNID_PROVEEDOR = unidproveedor;
                ProveedorDataMapper pdm = new ProveedorDataMapper();
                prov = (PROVEEDOR)pdm.getElement(aux);
                 
            }
            return prov;
        }

        public MONEDA GetMonedabyID(long unidMoneda)
        {
            MONEDA mon = new MONEDA();
            if (unidMoneda != null)
            {

                MONEDA aux = new MONEDA();
                aux.UNID_MONEDA = unidMoneda;
                MonedaDataMapper pdm = new MonedaDataMapper();
                mon = (MONEDA)pdm.getElement(aux);

            }
            return mon;
        }


        private ObservableCollection<FACTURA> GetFacturasbyProveedores(List<PROVEEDOR> Proveedores)
        {
            ObservableCollection<FACTURA> facturasmodel = new ObservableCollection<FACTURA>();
            try
            {
                FacturaCompraDataMapper facDataMapper = new FacturaCompraDataMapper();
                List<FACTURA> facturas = facDataMapper.getFacturabyProveedores(Proveedores);

                facturas.ForEach(o => facturasmodel.Add(new FACTURA()
                {
                    UNID_FACTURA = o.UNID_FACTURA
                    ,
                    FACTURA_NUMERO = o.FACTURA_NUMERO
                    ,
                    UNID_PROVEEDOR = o.UNID_PROVEEDOR
                    ,
                    UNID_MONEDA = o.UNID_MONEDA
                    ,
                    IVA_POR = o.IVA_POR

                }));
            }
            catch (Exception)
            {
                ;
            }

            return facturasmodel;
        }

        private ObservableCollection<DeleteFacturaDetalleModel> GetDetallebyFactura(FACTURA factura)
        {
            ObservableCollection<DeleteFacturaDetalleModel> facturasmodel = new ObservableCollection<DeleteFacturaDetalleModel>();
             try
            {
                FacturaCompraDetalleDataMapper facDataMapper = new FacturaCompraDetalleDataMapper();
                List<FACTURA_DETALLE> facturas = facDataMapper.GetDetallebyFactura(factura);

                foreach (FACTURA_DETALLE o in facturas)
                {
                    facturasmodel.Add(new DeleteFacturaDetalleModel(o));
                }


                
            }
            catch (Exception)
            {
                ;
            }

            return facturasmodel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
