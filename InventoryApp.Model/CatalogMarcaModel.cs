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
        private FixupCollection<DeleteMarca> _marcas;
        private MARCA _selectedmarca;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteMarca> Marcas
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

            FixupCollection<DeleteMarca> ic = new FixupCollection<DeleteMarca>();

            if (element != null)
            {
                if (((List<MARCA>)element).Count > 0)
                {
                    foreach (MARCA item in (List<MARCA>)element)
                    {
                        DeleteMarca aux = new DeleteMarca(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Marcas = ic;
        }

        public void deleteMarca(USUARIO u)
        {
            foreach (DeleteMarca item in this._marcas)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }
        }

        public CatalogMarcaModel(IDataMapper dataMapper)
        {
            this._dataMapper = new MarcaDataMapper();
            this._marcas = new FixupCollection<DeleteMarca>();
            this._selectedmarca = new MARCA();
            this.loadItems();

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
