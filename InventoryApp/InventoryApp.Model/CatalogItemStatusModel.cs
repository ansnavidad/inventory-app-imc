using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogItemStatusModel : INotifyPropertyChanged
    {
        private ITEM_STATUS _selectedItemStatus;
        private FixupCollection<ITEM_STATUS> _itemStatus;
        private IDataMapper _dataMapper;
        //private bool _isChecked;

        //public bool IsChecked
        //{
        //    get { return this._isChecked; }
        //    set
        //    {
        //        if (value != this._isChecked)
        //        {
        //            this._isChecked = value;
        //            if (this.PropertyChanged != null)
        //                this.PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
        //        }
        //    }
        //}

        public FixupCollection<ITEM_STATUS> ItemStatus
        {
            get { 
                return _itemStatus; 
                }
            set
            {
                if (_itemStatus !=value)
                {
                    _itemStatus = value;
                        if (PropertyChanged !=null)
	                    {
                            PropertyChanged(this, new PropertyChangedEventArgs("ItemStatus"));
	                    }
                }
            }
        }
        public ITEM_STATUS SelectedItemStatus
        {
            get
            {
                return _selectedItemStatus;
            }
            set
            {
                if (_selectedItemStatus != value)
                {
                    _selectedItemStatus = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedItemStatus"));
                    }
                }
            }
        }

        public CatalogItemStatusModel(IDataMapper dataMapper)
        {
            this._dataMapper = new EstatusDataMapper();
            this._itemStatus = new  FixupCollection<ITEM_STATUS>();
            this._selectedItemStatus = new ITEM_STATUS();
            //this._isChecked = false;
            this.loadItems();
            
        }
        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<ITEM_STATUS> ic = element as FixupCollection<ITEM_STATUS>;
            if (ic != null)
            {
                this.ItemStatus = ic;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
