using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace InventoryApp.DAL
{
    public class Categoria : INotifyPropertyChanged
    {
        private long _unidCategoria;
        private string _nombre;

        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Nombre"));
                    }
                }
            }
        }

        public long UnidCategoria
        {
            get
            {
                return _unidCategoria;
            }
            set
            {
                if (_unidCategoria != value)
                {
                    _unidCategoria = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("UnidCategoria"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
