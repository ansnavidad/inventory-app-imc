using System;
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
        private FixupCollection<ALMACEN> _almacen;
        private ALMACEN _selectedAlmacen;
        private IDataMapper _dataMapper;

        public FixupCollection<ALMACEN> Almacen
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

            FixupCollection<ALMACEN> ic = new FixupCollection<ALMACEN>();
            if (element != null)
            {
                if (((List<ALMACEN>)element).Count > 0)
                {
                    foreach (ALMACEN item in (List<ALMACEN>)element)
                    {
                        ic.Add(item);
                    }
                }
            }            
            
            this.Almacen = ic;            
        }

        public CatalogAlmacenModel(IDataMapper dataMapper)
        {
            this._dataMapper = new AlmacenDataMapper();
            this._almacen = new FixupCollection<ALMACEN>();
            this._selectedAlmacen = new ALMACEN();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
