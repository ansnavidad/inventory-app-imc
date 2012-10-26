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
        float _peso;
        string _color;
        Categoria categoria;

        
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

        public string Color
        {
            get
            {
                return _color;
            }

            set
            {
                if (_color != value)
                {
                    _color = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Color"));
                    }
                }
            }
        }

        public float Peso
        {
            get
            {
                return _peso;
            }

            set
            {
                if (_peso != value)
                {
                    _peso = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Peso"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

  
}
