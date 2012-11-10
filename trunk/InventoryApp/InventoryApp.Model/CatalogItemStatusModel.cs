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
        private FixupCollection<DeleteItemStatus> _itemStatus;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteItemStatus> ItemStatus
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
            this._dataMapper = new ItemStatusDataMapper();
            this._itemStatus = new FixupCollection<DeleteItemStatus>();
            this._selectedItemStatus = new ITEM_STATUS();
            this.loadItems();
            
        }

        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<DeleteItemStatus> ic = new FixupCollection<DeleteItemStatus>();
            if (element != null)
            {
                if (((List<ITEM_STATUS>)element).Count > 0)
                {
                    foreach (ITEM_STATUS item in (List<ITEM_STATUS>)element)
                    {
                        DeleteItemStatus aux = new DeleteItemStatus(item);
                        ic.Add(aux);
                    }
                }
            }
            this.ItemStatus = ic;
            
        }

        public void deleteItem()
        {
            foreach (DeleteItemStatus item in this._itemStatus)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
