using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogItemModel : INotifyPropertyChanged
    {
        //private ItemCollection _items;
        //private Item _selectedItem;
        //private IDataMapper _dataMapper;

        //public ItemCollection Items 
        //{
        //    get 
        //    {
        //        return _items;
        //    }
        //    set
        //    {
        //        if (_items != value)
        //        {
        //            _items = value;
        //            if (PropertyChanged != null)
        //            {
        //                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
        //            }
        //        }
        //    }
        //}

        //public Item SelectedItem
        //{
        //    get
        //    {
        //        return _selectedItem;
        //    }
        //    set
        //    {
        //        if (_selectedItem != value)
        //        {
        //            _selectedItem = value;
        //            if (PropertyChanged != null)
        //            {
        //                PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
        //            }
        //        }
        //    }
        //}

        //public void loadItems()
        //{
        //    object element = this._dataMapper.getElements();

        //    ItemCollection ic = element as ItemCollection;
        //    if (ic != null)
        //    {
        //        this._items = ic;
        //    }
        //}

        //public CatalogItemModel(IDataMapper dataMapper)
        //{
        //    this._dataMapper = new ItemDataMapper();
        //    this._items = new ItemCollection();
        //    this._selectedItem = new Item();
        //    this.loadItems();
        //    //this.loadItems(new ItemDataMapper());
        //}

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
