﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogAlmacenModel : INotifyPropertyChanged
    {
        private FixupCollection<DeleteAlmacen> _almacen;
        private ALMACEN _selectedAlmacen;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteAlmacen> Almacen
        {
            get
            {
                return _almacen;
            }
            set
            {
                if (_almacen != value)
                {
                    _almacen = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Almacen"));
                    }
                }
            }
        }

        public ALMACEN SelectedAlmacen
        {
            get
            {
                return _selectedAlmacen;
            }
            set
            {
                if (_selectedAlmacen != value)
                {
                    _selectedAlmacen = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedAlmacen"));
                    }
                }
            }
        }

        public void loadItems()
        {         
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteAlmacen> ic = new FixupCollection<DeleteAlmacen>();

            if (element != null)
            {
                if (((List<ALMACEN>)element).Count > 0)
                {
                    foreach (ALMACEN item in (List<ALMACEN>)element)
                    {
                        DeleteAlmacen aux = new DeleteAlmacen(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Almacen = ic;
        }

        public void deleteAlamacen()
        {
            foreach (DeleteAlmacen item in this._almacen)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogAlmacenModel(IDataMapper dataMapper)
        {
            this._dataMapper = new AlmacenDataMapper();
            this._almacen = new FixupCollection<DeleteAlmacen>();
            this._selectedAlmacen = new ALMACEN();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
