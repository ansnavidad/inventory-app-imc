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
        private FixupCollection<ARTICULO> _articulos;
        private ARTICULO _selectedarticulo;
        private IDataMapper _dataMapper;

        public FixupCollection<ARTICULO> Articulos
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

        public ARTICULO SelectedArticulo
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

            FixupCollection<ARTICULO> ic = new FixupCollection<ARTICULO>();

            foreach (ARTICULO elemento in (List<ARTICULO>)element)
            {
                ic.Add((ARTICULO)elemento);
            }
            if (ic != null)
            {
                this.Articulos = ic;
            }
        }

        public CatalogArticuloModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ArticuloDataMapper();
            this._articulos = new FixupCollection<ARTICULO>();
            this._selectedarticulo = new ARTICULO();
            this.loadItems();
            //this.loadItems(new ItemDataMapper());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
