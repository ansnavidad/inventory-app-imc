using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace InventoryApp.DAL
{
    public class Item : Articulo, INotifyPropertyChanged
    {
        private string _sku;
        private string _serialNbr;
        private float _precio;
        private float _imputesto;

        private string Sku
        {
            get { return _sku; }
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

        private string SerialNbr
        {
            get { return _serialNbr; }
            set
            {
                if (_serialNbr != value)
                {
                    _serialNbr = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SerialNbr"));
                    }
                }
            }
        }

        private float Precio
        {
            get { return _precio; }
            set
            {
                if (_precio != value)
                {
                    _precio = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Precio"));
                    }
                }
            }
        }

        private float Imputesto
        {
            get { return _imputesto; }
            set
            {
                if (_imputesto != value)
                {
                    _imputesto = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Imputesto"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    
}
