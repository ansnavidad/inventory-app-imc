using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogTransporteModel : INotifyPropertyChanged
    {
        private TRANSPORTE _selectedTransporte;
        private IDataMapper _dataMapper;

        public CatalogTransporteModel(IDataMapper dataMapper)
        {
            //this._dataMapper = new TransporteDataMapper();
            //this._items = new ItemCollection();
            //this._selectedItem = new Item();
            //this.loadItems();
            
        }
        public void loadTransporte()
        {
            //object element = this._dataMapper.getElements();

            //ItemCollection ic = element as ItemCollection;
            //if (ic != null)
            //{
            //    this._items = ic;
            //}
        }

        public TRANSPORTE SelectedTransporte
        {
            get
            {
                return _selectedTransporte;
            }
            set
            {
                if (_selectedTransporte != value)
                {
                    _selectedTransporte = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedTransporte"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
