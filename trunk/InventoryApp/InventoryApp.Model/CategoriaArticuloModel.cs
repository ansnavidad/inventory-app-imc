using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CategoriaArticuloModel : INotifyPropertyChanged
    {
        Categoria _categoria;
        ArticuloCollection _articulos;

        public Categoria Categoria
        {
            get
            {
                return _categoria;
            }
            set
            {
                if (_categoria != value)
                {
                    _categoria = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Categoria"));
                    }
                }
            }
        }

        public ArticuloCollection Articulos
        {
            get
            {
                return _articulos;
            }
            set
            {
                if (_articulos != value)
                {
                    _articulos = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Articulos"));
                    }
                }
            }
        }

        public CategoriaArticuloModel(Categoria categoria)
        {
            if (categoria != null)
            {
                this._categoria = categoria;
                this._articulos = this.getArticulos(categoria);
            }
            else
            {
                this._categoria = categoria;
                this._articulos = new ArticuloCollection();
                this._articulos.Clear();
            }
        }

        private ArticuloCollection getArticulos(Categoria categoria)
        {
            ArticuloCollection ac = new ArticuloCollection();
            if (categoria != null && categoria.IdCategoria==1)
            {
                ac=new ArticuloCollection() { new Articulo("Articulo 1", 12.2f, "Pink"), new Articulo("Articulo 2", 12.2f, "Red") };
            }
            if (categoria != null && categoria.IdCategoria==2)
            {
                ac = new ArticuloCollection() { new Articulo("Articulo 3", 12.2f, "Pink"), new Articulo("Articulo 4", 12.2f, "Red") };
            }

            return ac;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
