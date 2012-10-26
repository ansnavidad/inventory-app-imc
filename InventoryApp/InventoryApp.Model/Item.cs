using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class Item : INotifyPropertyChanged
    {
        int _idItem;
        string _sku;
        string _numeroSerie;


        public int IdItem
        {
            get
            {
                return _idItem;
            }
            set
            {
                if (_idItem != value)
                {
                    _idItem = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IdItem"));
                    }
                }
            }
        }

        public string Sku
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
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Sku"));
                    }
                }
            }
        }

        public Item(int IdItem, string sku)
        {
            this._idItem = IdItem;
            this._sku = sku;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
