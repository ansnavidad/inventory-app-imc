using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogArticuloModel : INotifyPropertyChanged
    {
        private FixupCollection<DeleteArticulo> _articulos;
        private DeleteArticulo _selectedarticulo;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteArticulo> Articulos
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

        public DeleteArticulo SelectedArticulo
        {
            get
            {
                return _selectedarticulo;
            }
            set
            {
                if (_selectedarticulo != value)
                {
                    _selectedarticulo = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedArticulo"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteArticulo> ic = new FixupCollection<DeleteArticulo>();

            if (element != null)
            {
                if (((List<ARTICULO>)element).Count > 0)
                {
                    foreach (ARTICULO item in (List<ARTICULO>)element)
                    {
                        DeleteArticulo aux = new DeleteArticulo(item);
                        //item.IsChecked = false;
                        ic.Add(aux);
                    }
                }
            }
            this.Articulos = ic;
            //FixupCollection<DeleteArticulo> ic = new FixupCollection<DeleteArticulo>();

            //foreach (ARTICULO elemento in (List<ARTICULO>)element)
            //{
            //    ic.Add((ARTICULO)elemento);
            //}
            //if (ic != null)
            //{
            //    this.Articulos = ic;
            //}
        }

        public void loadItems(CATEGORIA cc)
        {
            ArticuloDataMapper aa = new ArticuloDataMapper();
            object element = aa.getElementsByCategoria(cc);

            FixupCollection<DeleteArticulo> ic = new FixupCollection<DeleteArticulo>();

            if (element != null)
            {
                if (((List<ARTICULO>)element).Count > 0)
                {
                    foreach (ARTICULO item in (List<ARTICULO>)element)
                    {
                        DeleteArticulo aux = new DeleteArticulo(item);
                        //item.IsChecked = false;
                        ic.Add(aux);
                    }
                }
            }
            this.Articulos = ic;
        }


        public void deleteArticulos(USUARIO u)
        {
            foreach (DeleteArticulo item in _articulos)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }
        }

        public CatalogArticuloModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ArticuloDataMapper();
            this._articulos = new FixupCollection<DeleteArticulo>();
            //this._selectedarticulo = new DeleteArticulo();
            this.loadItems();
            //this.loadItems(new ItemDataMapper());
        }

        public CatalogArticuloModel(IDataMapper dataMapper, CATEGORIA c)
        {
            this._dataMapper = new ArticuloDataMapper();
            this._articulos = new FixupCollection<DeleteArticulo>();
            //this._selectedarticulo = new DeleteArticulo();
            this.loadItems(c);
            //this.loadItems(new ItemDataMapper());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
