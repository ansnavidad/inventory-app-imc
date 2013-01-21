using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class ItemStatusModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidItemStatus;
        private string _itemStatusName;
        private ItemStatusModel _itemStatusModel;
        
        private ItemStatusDataMapper _dataMapper;
        #endregion

        #region Props
        public ItemStatusModel ItemStatusMode
        {
            get
            {
                return _itemStatusModel;
            }
            set
            {
                if (_itemStatusModel != value)
                {
                    _itemStatusModel = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ItemStatusMode"));
                    }
                }
            }
        }
        public long UnidItemStatus
        {
            get
            {
                return _unidItemStatus;
            }
            set
            {
                if (_unidItemStatus != value)
                {
                    _unidItemStatus = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidItemStatus"));
                    }
                }
            }
        }

        public string ItemStatusName
        {
            get
            {
                return _itemStatusName;
            }
            set
            {
                if (_itemStatusName != value)
                {
                    _itemStatusName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ItemStatusName"));
                    }
                }
            }
        }
        #endregion

        public void saveItemStatus()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new ITEM_STATUS() { ITEM_STATUS_NAME = this._itemStatusName, IS_ACTIVE=true });
            }
        }

        public void updateItemStatus()
        {
            this._dataMapper.udpateElement(new ITEM_STATUS() { UNID_ITEM_STATUS=this._unidItemStatus,ITEM_STATUS_NAME=this._itemStatusName });
        }

        #region Constructors
        public ItemStatusModel(IDataMapper dataMapper)
        {
            if ((dataMapper as ItemStatusDataMapper) != null)
            {
                this._dataMapper = dataMapper as ItemStatusDataMapper;
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
