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
        private FixupCollection<DeleteCiudad> _ciudad;
        private CIUDAD _selectedCiudad;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteCiudad> Ciudad
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

            FixupCollection<DeleteCiudad> ic = new FixupCollection<DeleteCiudad>();

            if (element != null)
            {
                if (((List<CIUDAD>)element).Count > 0)
                {
                    foreach (CIUDAD item in (List<CIUDAD>)element)
                    {
                        DeleteCiudad aux = new DeleteCiudad(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Ciudad = ic;
        }

        public void deleteCiudad()
        {
            foreach (DeleteCiudad item in this._ciudad)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogCiudadModel(IDataMapper dataMapper)
        {
            this._dataMapper = new CiudadDataMapper();
            this._ciudad = new FixupCollection<DeleteCiudad>();
            this._selectedCiudad = new CIUDAD();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
