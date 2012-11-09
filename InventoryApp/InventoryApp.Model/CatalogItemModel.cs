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
        private ItemModel _selectedItemModel;
        private IDataMapper _dataMapper;

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

        public ItemModel SelectedItemModel
        {
            get
            {
                return _selectedItemModel;
            }
            set
            {
                if (_selectedItemModel != value)
                {
                    _selectedItemModel = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedItemModel"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<ItemModel> ic = new FixupCollection<ItemModel>();

            foreach (ItemModel elemento in (List<ItemModel>)element)
            {
                ic.Add((ItemModel)elemento);
            }
            if (ic != null)
            {
                this.ItemModel = ic;
            }
        }

        public CatalogItemModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ItemDataMapper();
            this._itemModel = new FixupCollection<ItemModel>();
            //this._selectedItemModel = new ItemModel();
            this.loadItems();
        }       

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
