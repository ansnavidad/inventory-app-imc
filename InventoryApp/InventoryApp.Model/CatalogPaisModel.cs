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
        private FixupCollection<DeletePais> _pais;
        private PAI _selectedPais;
        private IDataMapper _dataMapper;

        public FixupCollection<DeletePais> Pais
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

            FixupCollection<DeletePais> ic = new FixupCollection<DeletePais>();

            if (element != null)
            {
                if (((List<PAI>)element).Count > 0)
                {
                    foreach (PAI item in (List<PAI>)element)
                    {
                        DeletePais aux = new DeletePais(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Pais = ic;
        }

        public void deletePais(USUARIO u)
        {
            foreach (DeletePais item in this._pais)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }
        }

        public CatalogPaisModel(IDataMapper dataMapper)
        {
            this._dataMapper = new PaisDataMapper();
            this._pais = new FixupCollection<DeletePais>();
            this._selectedPais = new PAI();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
