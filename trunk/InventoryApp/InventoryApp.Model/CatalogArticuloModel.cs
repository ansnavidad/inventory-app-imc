﻿using System;
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

        //public void loadItems(CategoriaModel categoria)
        //{
        //    List<ARTICULO> art=(List<ARTICULO>) (this._dataMapper as ArticuloDataMapper).getElements_EntradasSalidas(new CATEGORIA() { UNID_CATEGORIA=categoria.UnidCategoria });
        //    FixupCollection<DeleteArticulo> articulos = new FixupCollection<DeleteArticulo>();
        //    art.ForEach(o=>  articulos.Add(o) );
        //    this.Articulos = articulos;
        //}

        public void deleteArticulos()
        {
            foreach (DeleteArticulo item in _articulos)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
