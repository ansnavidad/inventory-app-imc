using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace InventoryApp.DAL
{
    
    public class Articulo : INotifyPropertyChanged
    {
        private long _unidArticulo;
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

        public long UnidArticulo
        {
            get
            {
                return _unidArticulo;
            }

            set
            {
                if (_unidArticulo != value)
                {
                    _unidArticulo = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("UnidArticulo"));
                    }
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }

  
}
