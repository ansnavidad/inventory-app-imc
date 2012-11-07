using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogPaisModel : INotifyPropertyChanged
    {
        private FixupCollection<PAI> _pais;
        private PAI _selectedPais;
        private IDataMapper _dataMapper;

        public FixupCollection<PAI> Pais
        {
            get
            {
                return _pais;
            }
            set
            {
                if (_pais != value)
                {
                    _pais = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Pais"));
                    }
                }
            }
        }

        public PAI SelectedPais
        {
            get
            {
                return _selectedPais;
            }
            set
            {
                if (_selectedPais != value)
                {
                    _selectedPais = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedPais"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<PAI> ic = element as FixupCollection<PAI>;
            if (ic != null)
            {
                this.Pais = ic;
            }
        }

        public CatalogPaisModel(IDataMapper dataMapper)
        {
            this._dataMapper = new PaisDataMapper();
            this._pais = new FixupCollection<PAI>();
            this._selectedPais = new PAI();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
