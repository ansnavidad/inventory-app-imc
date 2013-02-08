using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteItemStatus : ITEM_STATUS , INotifyPropertyChanged
    {
        private bool _isChecked;

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
        public DeleteItemStatus(ITEM_STATUS itemStatus)
        {
            if (itemStatus != null)
            {
                this.ITEM_STATUS_NAME = itemStatus.ITEM_STATUS_NAME;
                this.UNID_ITEM_STATUS = itemStatus.UNID_ITEM_STATUS;
                this.IS_ACTIVE = itemStatus.IS_ACTIVE;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
