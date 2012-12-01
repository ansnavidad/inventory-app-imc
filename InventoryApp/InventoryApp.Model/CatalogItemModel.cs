using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;


namespace InventoryApp.Model
{
    public class CatalogItemModel : INotifyPropertyChanged
    {
        private FixupCollection<ItemModel> _itemModel;
        private string _serie;
        private string _sku;
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

        //public void loadItems(ALMACEN almacenDirecto)
        //{
        //    try
        //    {
        //        object element = this._dataMapper.getElements_EntradasSalidasSerie(almacenDirecto, this._serie);

        //        if (element != null)
        //        {

        //            FixupCollection<ItemModel> ic = new FixupCollection<ItemModel>();

        //            foreach (ITEM elemento in (List<ITEM>)element)
        //            {
        //                ItemModel aux = new ItemModel(elemento);
        //                ic.Add(aux);
        //            }
        //            if (ic != null)
        //            {
        //                this.ItemModel = ic;
        //            }
        //        }
        //    }
        //    catch (ArgumentException ae)
        //    {

        //        ;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void loadItems(ALMACEN almacenDirecto)
        {
            try
            {
                object element = this._dataMapper.getElements_EntradasSalidasSerie(almacenDirecto, this._serie, this._sku);

                if (element != null)
                {

                    FixupCollection<ItemModel> ic = new FixupCollection<ItemModel>();

                    foreach (ITEM elemento in (List<ITEM>)element)
                    {
                        ItemModel aux = new ItemModel(elemento);
                        ic.Add(aux);
                    }
                    if (ic != null)
                    {
                        this.ItemModel = ic;
                    }
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
                object element = this._dataMapper.getElements_EntradasSalidasSerie2(almacenDirecto, this._serie, this._sku);

                if (element != null)
                {

                    FixupCollection<ItemModel> ic = new FixupCollection<ItemModel>();

                    foreach (ITEM elemento in (List<ITEM>)element)
                    {
                        ItemModel aux = new ItemModel(elemento);
                        ic.Add(aux);
                    }
                    if (ic != null)
                    {
                        this.ItemModel = ic;
                    }
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

        }       

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
