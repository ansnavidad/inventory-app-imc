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
        private float _impuesto;
        private long _idItem;

        public long IdItem
        {
            get { return _idItem; }
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

        public string SerialNbr
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

        public float Precio
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

        public float Impuesto
        {
            get { return _impuesto; }
            set
            {
                if (_impuesto != value)
                {
                    _impuesto = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Imputesto"));
                    }
                }
            }
        }

        public Item(Articulo articulo,long idItem, string sku, string serialNbr, float precio, float impuesto):base(articulo) {
            this._sku = sku;
            this._serialNbr = serialNbr;
            this._precio = precio;
            this._impuesto = impuesto;
            this._idItem = idItem;
        }

        public Item():base(new Articulo(){ Color="",Nombre="", Peso=0f, UnidArticulo=0})
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    
}
