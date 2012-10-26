using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace InventoryApp.Model 
{
    public class Articulo : INotifyPropertyChanged
    {
        string _nombre;
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
                    _nombre=value;
                    if(PropertyChanged!=null)
                    {
                        PropertyChanged(this,new PropertyChangedEventArgs( "Nombre" ));
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

        public Articulo(string nombre, float peso, string color)
        {
            this._nombre = nombre;
            this._peso = peso;
            this._color = color;
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
