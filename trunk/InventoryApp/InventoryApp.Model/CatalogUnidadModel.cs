using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogUnidadModel : INotifyPropertyChanged
    {
        private UNIDAD _selectedUnidad;
        private FixupCollection<DeleteUnidad> _unidad;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteUnidad> Unidad
        {
            get
            {
                return _unidad;
            }
            set
            {
                if (_unidad != value)
                {
                    _unidad = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Unidad"));
                    }
                }
            }
        }
        public UNIDAD SelectedUnidad
        {
            get
            {
                return _selectedUnidad;
            }
            set
            {
                if (_selectedUnidad != value)
                {
                    _selectedUnidad = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedUnidad"));
                    }
                }
            }
        }
        public CatalogUnidadModel(IDataMapper dataMapper)
        {
            this._dataMapper = new UnidadDataMapper();
            this._unidad = new FixupCollection<DeleteUnidad>();
            this._selectedUnidad = new UNIDAD();
            this.loadItems();
            
        }
        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteUnidad> ic = new FixupCollection<DeleteUnidad>();

            if (element != null)
            {
                if (((List<UNIDAD>)element).Count > 0)
                {
                    foreach (UNIDAD item in (List<UNIDAD>)element)
                    {
                        DeleteUnidad aux = new DeleteUnidad(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Unidad = ic;
        }
        public void deleteUnidad()
        {
            foreach (DeleteUnidad item in this._unidad)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
