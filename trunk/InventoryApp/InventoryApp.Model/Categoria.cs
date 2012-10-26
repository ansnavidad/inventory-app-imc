using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class Categoria : INotifyPropertyChanged
    {
        int _idCategoria;
        string _categoriaName;

        public int IdCategoria
        {
            get
            {
                return _idCategoria;
            }
            set
            {
                if (_idCategoria != value)
                {
                    _idCategoria = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IdCategoria"));
                    }
                }
            }
        }

        public string CategoriaName
        {
            get
            {
                return _categoriaName;
            }
            set
            {
                if (_categoriaName != value)
                {
                    _categoriaName = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CategoriaName"));
                    }
                }
            }
        }

        public Categoria(int idCategoria,string categoriaName)
        {
            this._categoriaName = categoriaName;
            this._idCategoria = idCategoria;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
