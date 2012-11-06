using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogMarcaModel : INotifyPropertyChanged
    {
        private FixupCollection<MARCA> _marcas;
        private MARCA _selectedmarca;
        private IDataMapper _dataMapper;

        public FixupCollection<MARCA> Marcas
        {
            get
            {
                return _marcas;
            }
            set
            {
                if (_marcas != value)
                {
                    _marcas = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Marcas"));
                    }
                }
            }
        }

        public MARCA SelectedMarca
        {
            get
            {
                return _selectedmarca;
            }
            set
            {
                if (_selectedmarca != value)
                {
                    _selectedmarca = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedMarca"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<MARCA> ic = element as FixupCollection<MARCA>; //element as FixupCollection<PROYECTO>;
            if (ic != null)
            {
                //this._itemStatus = ic;
                this.Marcas = ic;
            }
        }


        public CatalogMarcaModel(IDataMapper dataMapper)
        {
            this._dataMapper = new MarcaDataMapper();
            this._marcas = new FixupCollection<MARCA>();
            this._selectedmarca = new MARCA();
            this.loadItems();

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
