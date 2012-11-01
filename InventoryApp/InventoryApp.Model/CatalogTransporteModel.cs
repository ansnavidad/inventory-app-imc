using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogTransporteModel : INotifyPropertyChanged
    {
        private FixupCollection<TRANSPORTE> _transportes;
        private TRANSPORTE _selectedtransporte;
        private IDataMapper _dataMapper;

        public FixupCollection<TRANSPORTE> Transportes 
        {
            get 
            {
                return _transportes;
            }
            set
            {
                if (_transportes != value)
                {
                    _transportes = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Transportes"));
                    }
                }
            }
        }

        public TRANSPORTE SelectedItem
        {
            get
            {
                return _selectedtransporte;
            }
            set
            {
                if (_selectedtransporte != value)
                {
                    _selectedtransporte = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedTransporte"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<TRANSPORTE> ic = new FixupCollection<TRANSPORTE>();

            foreach (TRANSPORTE elemento in (List<TRANSPORTE>)element)
            {
                ic.Add((TRANSPORTE)elemento);
            }
            if (ic != null)
            {
                this._transportes = ic;
            }
        }

        public CatalogTransporteModel(IDataMapper dataMapper)
        {
            this._dataMapper = new TransporteDataMapper();
            this._transportes = new FixupCollection<TRANSPORTE>();
            this._selectedtransporte = new TRANSPORTE();
            this.loadItems();
            //this.loadItems(new ItemDataMapper());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
