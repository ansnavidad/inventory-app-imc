using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.ComponentModel;

namespace InventoryApp.ViewModel
{
    public class ItemACViewModel : INotifyPropertyChanged
    {
        ItemCollection _items;
        Item _selectedItem;
        CategoriaArticuloModel _categoriaArticulo;
        Articulo _selectedArticulo;
        string _descriptor;


        public ItemCollection Items 
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("_items"));
                    }
                }
            }
        }
        public Item SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    this.Descriptor = "se cambi{o el valor";
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
                    }
                }
            }
        }
        public CategoriaArticuloModel CategoriaArticulo
        {
            get
            {
                return _categoriaArticulo;
            }
            set
            {
                if (_categoriaArticulo != value)
                {
                    _categoriaArticulo = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CategoriaArticulo"));
                    }
                }
            }
        }
        public Articulo SelectedArticulo
        {
            get
            {
                return _selectedArticulo;
            }
            set
            {
                if (_selectedArticulo != value)
                {
                    _selectedArticulo = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedArticulo"));
                    }
                }
            }
        }
        public string Descriptor
        {
            get
            {
                return _descriptor;
            }
            set
            {
                    if (_descriptor != value)
                {
                    _descriptor = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Descriptor"));
                    }
                }
            }
        }

        public ItemACViewModel()
        {
            this._items = new ItemCollection();
            this._categoriaArticulo = new CategoriaArticuloModel(new Categoria(1,"Categoria 1"));
            this._descriptor = "prueba de texto";
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
