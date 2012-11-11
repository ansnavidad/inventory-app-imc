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
        private FixupCollection<DeleteTransporte> _transporte;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteTransporte> Transporte
        {
            get
            {
                return _transporte;
            }
            set
            {
                if (_transporte != value)
                {
                    _transporte = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Transporte"));
                    }
                }
            }
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

        public CatalogTransporteModel(IDataMapper dataMapper)
        {
            this._dataMapper = new TransporteDataMapper();
            this._transporte = new FixupCollection<DeleteTransporte>();
            this._selectedTransporte = new TRANSPORTE();
            this.loadItems();
            
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteTransporte> ic = new FixupCollection<DeleteTransporte>();

            if (element != null)
            {
                if (((List<TRANSPORTE>)element).Count > 0)
                {
                    foreach (TRANSPORTE item in (List<TRANSPORTE>)element)
                    {
                        DeleteTransporte aux = new DeleteTransporte(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Transporte = ic;
        }

        public void deleteTransporte()
        {
            foreach (DeleteTransporte item in this._transporte)
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
