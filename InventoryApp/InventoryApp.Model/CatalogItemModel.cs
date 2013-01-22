using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;


namespace InventoryApp.Model
{
    public class CatalogItemModel : ModelBase,INotifyPropertyChanged
    {
        private FixupCollection<ItemModel> _itemModel;
        private string _serie;
        private string _sku;
        private string _mensaje1;
        private string _mensaje2;
        private string _mensaje3;
        private ItemDataMapper _dataMapper;
        

        public FixupCollection<ItemModel> ItemModel
        {
            get
            {
                return _itemModel;
            }
            set
            {
                if (_itemModel != value)
                {
                    _itemModel = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ItemModel"));
                    }
                }
            }
        }

        public string Mensaje1
        {
            get
            {
                return _mensaje1;
            }
            set
            {
                if (_mensaje1 != value)
                {
                    _mensaje1 = value;
                    
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Mensaje1"));
                    }
                }
            }
        }

        public string Mensaje2
        {
            get
            {
                return _mensaje2;
            }
            set
            {
                if (_mensaje2 != value)
                {
                    _mensaje2 = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Mensaje2"));
                    }
                }
            }
        }

        public string Mensaje3
        {
            get
            {
                return _mensaje3;
            }
            set
            {
                if (_mensaje3 != value)
                {
                    _mensaje3 = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Mensaje3"));
                    }
                }
            }
        }

        public string Serie
        {
            get
            {
                return _serie;
            }
            set
            {
                if (_serie != value)
                {
                    _serie = value;
                    _sku = "";
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Serie"));
                    }
                }
            }
        }

        public string SKU
        {
            get
            {
                return _sku;
            }
            set
            {
                if (_sku != value)
                {
                    _sku = value;
                    _serie = "";
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SKU"));
                    }
                }
            }
        }

        public void loadItems(ALMACEN almacenDirecto)
        {
            try
            {
                bool ban = false;
                this.Mensaje1 = "";
                this.Mensaje2 = "";
                object element = this._dataMapper.getElements_EntradasSalidasSerie(almacenDirecto, this._serie, this._sku);
                FixupCollection<ItemModel> ic = new FixupCollection<ItemModel>();

                if (this.ItemModel.Count != 0)
                {
                    foreach (ItemModel elem in this.ItemModel)
                    {
                        if (elem.IsChecked)
                        {
                            ic.Add(elem);
                        }
                    }
                }

                this.ItemModel.Clear();

                if (element != null)
                {
                    foreach (ITEM elemento in (List<ITEM>)element)
                    {
                        ban = true;
                        ItemModel aux = new ItemModel(elemento);
                        aux.IsChecked = true;
                        ic.Add(aux);
                    }

                }
                if (ic != null)
                {
                    this.ItemModel = ic;
                }
                if (!ban)
                {
                    this.Mensaje1 = "Este artículo no se encuentra en el lugar especificado";
                    object element2 = this._dataMapper.getAlmacenDisponible(this._serie, this._sku);

                    bool aux = false;
                    this.Mensaje2 = "El artículo se encuentra en: ";
                    foreach (ULTIMO_MOVIMIENTO um in (List<ULTIMO_MOVIMIENTO>)element2)
                    {

                        if (um.CLIENTE != null)
                        {
                            this.Mensaje2 += " El cliente: " + um.CLIENTE.CLIENTE1;
                            aux = true;
                        }

                        if (um.PROVEEDOR != null)
                        {
                            aux = true;
                            this.Mensaje2 += " El proveedor: " + um.PROVEEDOR.PROVEEDOR_NAME;
                        }

                        if (um.INFRAESTRUCTURA != null)
                        {
                            aux = true;
                            this.Mensaje2 += " La infraestructura: " + um.INFRAESTRUCTURA.INFRAESTRUCTURA_NAME;
                        }

                        if (um.ALMACEN != null)
                        {
                            aux = true;
                            this.Mensaje2 += " El almacén: " + um.ALMACEN.ALMACEN_NAME;
                        }
                    }

                    if (!aux)
                        this.Mensaje2 = "El artículo no se encuentra en ningún cliente o proveedor";
                }
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void loadItems(INFRAESTRUCTURA infra)
        {
            try
            {
                bool ban = false;
                this.Mensaje1 = "";
                this.Mensaje2 = "";
                object element = this._dataMapper.getElements_EntradasSalidasSerie(infra, this._serie, this._sku);
                FixupCollection<ItemModel> ic = new FixupCollection<ItemModel>();

                if (this.ItemModel.Count != 0)
                {
                    foreach (ItemModel elem in this.ItemModel)
                    {
                        if (elem.IsChecked)
                        {
                            ic.Add(elem);
                        }
                    }
                }

                this.ItemModel.Clear();

                if (element != null)
                {




                    foreach (ITEM elemento in (List<ITEM>)element)
                    {
                        ban = true;
                        ItemModel aux = new ItemModel(elemento);
                        UltimoMovimientoDataMapper ultimomovimientodm = new UltimoMovimientoDataMapper();
                        ULTIMO_MOVIMIENTO tmp = new ULTIMO_MOVIMIENTO();
                        if (infra != null)
                            tmp = ultimomovimientodm.getCantidadItems(elemento, infra);
                        aux.CantidadDisponible = tmp.CANTIDAD;
                        aux.CantidadMovimiento = 1;
                        if (aux.CantidadDisponible > 0)
                        {
                            bool auxx = true;
                            foreach (ItemModel elem in ic)
                            {
                                if (elem.UNID_ITEM == aux.UNID_ITEM && elem.NUMERO_SERIE == aux.NUMERO_SERIE && elem.SKU == aux.SKU)
                                {
                                    auxx = false;
                                }
                            }

                            if (auxx)
                            {
                                aux.IsChecked = true;
                                ic.Add(aux);
                            }
                        }
                        else
                        {

                            this.Mensaje1 = "Este artículo no se encuentra en el lugar especificado";
                        }
                    }

                }
                if (ic != null)
                {
                    this.ItemModel = ic;
                }
                if (!ban)
                {
                    this.Mensaje1 = "Este artículo no se encuentra en el lugar especificado";
                    object element2 = this._dataMapper.getAlmacenDisponible(this._serie, this._sku);

                    bool aux = false;
                    this.Mensaje2 = "El artículo se encuentra en: ";
                    foreach (ULTIMO_MOVIMIENTO um in (List<ULTIMO_MOVIMIENTO>)element2)
                    {

                        if (um.CLIENTE != null)
                        {
                            this.Mensaje2 += " El cliente: " + um.CLIENTE.CLIENTE1;
                            aux = true;
                        }

                        if (um.PROVEEDOR != null)
                        {
                            aux = true;
                            this.Mensaje2 += " El proveedor: " + um.PROVEEDOR.PROVEEDOR_NAME;
                        }

                        if (um.INFRAESTRUCTURA != null)
                        {
                            aux = true;
                            this.Mensaje2 += " La infraestructura: " + um.INFRAESTRUCTURA.INFRAESTRUCTURA_NAME;
                        }

                        if (um.ALMACEN != null)
                        {
                            aux = true;
                            this.Mensaje2 += " El almacén: " + um.ALMACEN.ALMACEN_NAME;
                        }
                    }

                    if (!aux)
                        this.Mensaje2 = "El artículo no se encuentra en ningún cliente o proveedor";
                }
            }
            catch (ArgumentException ae)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void loadItems(PROVEEDOR prov, CLIENTE cliente)
        {
            try
            {
                bool ban = false;
                this.Mensaje1 = "";
                this.Mensaje2 = "";
                object element = this._dataMapper.getElements_EntradasSalidasSerie(prov, cliente, this._serie, this._sku);
                FixupCollection<ItemModel> ic = new FixupCollection<ItemModel>();

                if (this.ItemModel.Count != 0)
                {
                    foreach (ItemModel elem in this.ItemModel)
                    {
                        if (elem.IsChecked)
                        {
                            ic.Add(elem);
                        }
                    }
                }

                this.ItemModel.Clear();

                if (element != null)
                {




                    foreach (ITEM elemento in (List<ITEM>)element)
                    {
                        ban = true;
                        ItemModel aux = new ItemModel(elemento);
                        UltimoMovimientoDataMapper ultimomovimientodm = new UltimoMovimientoDataMapper();
                        ULTIMO_MOVIMIENTO tmp = new ULTIMO_MOVIMIENTO();
                        if (cliente != null)
                            tmp = ultimomovimientodm.getCantidadItems(elemento, cliente);
                        else if(prov != null)
                            tmp = ultimomovimientodm.getCantidadItems(elemento, prov);
                        aux.CantidadDisponible = tmp.CANTIDAD;
                        aux.CantidadMovimiento = 1;
                        if (aux.CantidadDisponible > 0)
                        {
                            bool auxx = true;
                            foreach (ItemModel elem in ic)
                            {
                                if (elem.UNID_ITEM == aux.UNID_ITEM && elem.NUMERO_SERIE == aux.NUMERO_SERIE && elem.SKU == aux.SKU)
                                {
                                    auxx = false;
                                }
                            }

                            if (auxx)
                            {
                                aux.IsChecked = true;
                                ic.Add(aux);
                            }

                        }
                        else
                        {

                            this.Mensaje1 = "Este artículo no se encuentra en el lugar especificado";
                        }
                    }
                
                }
                if (ic != null)
                {
                    this.ItemModel = ic;
                }
                if (!ban)
                {
                    this.Mensaje1 = "Este artículo no se encuentra en el lugar especificado";
                    object element2 = this._dataMapper.getAlmacenDisponible(this._serie, this._sku);

                    bool aux = false;
                    this.Mensaje2 = "El artículo se encuentra en ";
                    foreach (ULTIMO_MOVIMIENTO um in (List<ULTIMO_MOVIMIENTO>)element2)
                    {

                        if (um.CLIENTE != null)
                        {
                            this.Mensaje2 += " El cliente: " + um.CLIENTE.CLIENTE1;
                            aux = true;
                        }

                        if (um.PROVEEDOR != null)
                        {
                            aux = true;
                            this.Mensaje2 += " El proveedor: " + um.PROVEEDOR.PROVEEDOR_NAME;
                        }

                        if (um.INFRAESTRUCTURA != null)
                        {
                            aux = true;
                            this.Mensaje2 += " La infraestructura: " + um.INFRAESTRUCTURA.INFRAESTRUCTURA_NAME;
                        }

                        if (um.ALMACEN != null)
                        {
                            aux = true;
                            this.Mensaje2 += " El almacén: " + um.ALMACEN.ALMACEN_NAME;
                        }
                    }

                    if (!aux)
                        this.Mensaje2 = "El artículo no se encuentra en ningún cliente o proveedor";
                }
            }
            catch (ArgumentException ae)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void loadItems(ALMACEN almacenDirecto, string Rafa)
        {
            try
            {
                bool ban = false;
                this.Mensaje1 = "";
                this.Mensaje2 = "";
                object element = this._dataMapper.getElements_EntradasSalidasSerie2(almacenDirecto, this._serie, this._sku);
                FixupCollection<ItemModel> ic = new FixupCollection<ItemModel>();

                if (this.ItemModel.Count != 0)
                {
                    foreach (ItemModel elem in this.ItemModel)
                    {
                        if (elem.IsChecked)
                        {
                            ic.Add(elem);
                        }
                    }                 
                }

                this.ItemModel.Clear();

                if (element != null)
                {
                                      

                    foreach (ITEM elemento in (List<ITEM>)element)
                    {
                        ban = true;
                        ItemModel aux = new ItemModel(elemento);
                        UltimoMovimientoDataMapper ultimomovimientodm = new UltimoMovimientoDataMapper();
                        ULTIMO_MOVIMIENTO tmp = new ULTIMO_MOVIMIENTO();
                        if (almacenDirecto != null)
                            tmp = ultimomovimientodm.getCantidadItems(elemento, almacenDirecto);
                        aux.CantidadDisponible = tmp.CANTIDAD;
                        aux.CantidadMovimiento = 1;
                        if (aux.CantidadDisponible > 0)
                        {
                            bool auxx = true;
                            foreach (ItemModel elem in ic)
                            {
                                if (elem.UNID_ITEM == aux.UNID_ITEM && elem.NUMERO_SERIE == aux.NUMERO_SERIE && elem.SKU == aux.SKU)
                                {
                                    auxx = false;
                                }
                            }

                            if (auxx)
                            {
                                aux.IsChecked = true;
                                ic.Add(aux);
                            }
                        }
                        else {

                            this.Mensaje1 = "Este artículo no se encuentra en el lugar especificado";
                        }
                    }
                }
                if (ic != null)
                {
                    this.ItemModel = ic;
                }
                if (!ban)
                {
                    this.Mensaje1 = "Este artículo no se encuentra en el almacén seleccionado";
                    object element2 = this._dataMapper.getAlmacenDisponible(this._serie, this._sku);

                    bool aux = false;
                    this.Mensaje2 = "El artículo se encuentra en: ";
                    foreach (ULTIMO_MOVIMIENTO um in (List<ULTIMO_MOVIMIENTO>)element2)
                    {
                        aux = true;
                        if (um.CLIENTE != null)
                        {
                            this.Mensaje2 += " El cliente: " + um.CLIENTE.CLIENTE1;
                            aux = true;
                        }

                        if (um.PROVEEDOR != null)
                        {
                            aux = true;
                            this.Mensaje2 += " El proveedor: " + um.PROVEEDOR.PROVEEDOR_NAME;
                        }

                        if (um.INFRAESTRUCTURA != null)
                        {
                            aux = true;
                            this.Mensaje2 += " La infraestructura: " + um.INFRAESTRUCTURA.INFRAESTRUCTURA_NAME;
                        }

                        if (um.ALMACEN != null)
                        {
                            aux = true;
                            this.Mensaje2 += " El almacén: " + um.ALMACEN.ALMACEN_NAME;
                        }
                    }

                    if (!aux)
                        this.Mensaje2 = "El artículo no se encuentra en ningún almacen";
                }
            }
            catch (ArgumentException ae)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatalogItemModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ItemDataMapper();
            this._itemModel = new FixupCollection<ItemModel>();
            this.Mensaje3 = "";
        }

        public ItemStatusModel ItemStatus
        {
            get { return _ItemStatus; }
            set
            {
                if (_ItemStatus != value)
                {
                    _ItemStatus = value;
                    OnPropertyChanged(ItemStatusPropertyName);
                }
            }
        }
        private ItemStatusModel _ItemStatus;
        public const string ItemStatusPropertyName = "ItemStatus";

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
