using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogMaxMinModel : INotifyPropertyChanged
    {
        private FixupCollection<DeleteMaxMin> _maxMin;
        private DeleteMaxMin _selectedMaxMin;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteMaxMin> MaxMin
        {
            get
            {
                return _maxMin;
            }
            set
            {
                if (_maxMin != value)
                {
                    _maxMin = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("MaxMin"));
                    }
                }
            }
        }

        public DeleteMaxMin SelectedMaxMin
        {
            get
            {
                return _selectedMaxMin;
            }
            set
            {
                if (_selectedMaxMin != value)
                {
                    _selectedMaxMin = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedMaxMin"));
                    }
                }
            }
        }

        public void loadMaxMin()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteMaxMin> ic = new FixupCollection<DeleteMaxMin>();

            if (element != null)
            {
                if (((List<MAX_MIN>)element).Count > 0)
                {
                    foreach (MAX_MIN item in (List<MAX_MIN>)element)
                    {
                        DeleteMaxMin aux = new DeleteMaxMin(item);
                        ic.Add(aux);
                    }
                }
            }
            this.MaxMin = ic;
        }

        public void deleteMaxMin()
        {
            foreach (DeleteMaxMin item in _maxMin)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }
        public CatalogMaxMinModel(IDataMapper dataMapper)
        {
            this._dataMapper = new MaxMinDataMapper();
            this._maxMin = new FixupCollection<DeleteMaxMin>();
            this.loadMaxMin();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
