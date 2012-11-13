﻿using System;
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
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Serie"));
                    }
                }
            }
        }


        public void loadItems()
        {
            try
            {
                object element = this._dataMapper.getElements_EntradasSalidasSerie(this._serie);

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