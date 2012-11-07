using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogCiudadModel : INotifyPropertyChanged
    {
        private FixupCollection<CIUDAD> _ciudad;
        private CIUDAD _selectedCiudad;
        private IDataMapper _dataMapper;

        public FixupCollection<CIUDAD> Ciudad
        {
            get
            {
                return _ciudad;
            }
            set
            {
                if (_ciudad != value)
                {
                    _ciudad = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Ciudad"));
                    }
                }
            }
        }

        public CIUDAD SelectedCiudad
        {
            get
            {
                return _selectedCiudad;
            }
            set
            {
                if (_selectedCiudad != value)
                {
                    _selectedCiudad = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedCiudad"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<CIUDAD> ic = element as FixupCollection<CIUDAD>;
            if (ic != null)
            {
                this.Ciudad = ic;
            }
        }

        public CatalogCiudadModel(IDataMapper dataMapper)
        {
            this._dataMapper = new CiudadDataMapper();
            this._ciudad = new FixupCollection<CIUDAD>();
            this._selectedCiudad = new CIUDAD();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
