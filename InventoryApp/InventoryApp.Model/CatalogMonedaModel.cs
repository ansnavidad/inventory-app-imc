using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogMonedaModel : INotifyPropertyChanged
    {
        private MONEDA _selectedMoneda;
        private FixupCollection<DeleteMoneda> _moneda;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteMoneda> Moneda
        {
            get { 
                return _moneda; 
                }
            set
            {
                if (_moneda !=value)
                {
                    _moneda = value;
                        if (PropertyChanged !=null)
	                    {
                            PropertyChanged(this, new PropertyChangedEventArgs("Moneda"));
	                    }
                }
            }
        }

        public MONEDA SelectedMoneda
        {
            get
            {
                return _selectedMoneda;
            }
            set
            {
                if (_selectedMoneda != value)
                {
                    _selectedMoneda = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedMoneda"));
                    }
                }
            }
        }

        public void loadMonedas()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteMoneda> ic = new FixupCollection<DeleteMoneda>();

            if (element != null)
            {
                if (((List<MONEDA>)element).Count > 0)
                {
                    foreach (MONEDA item in (List<MONEDA>)element)
                    {
                        DeleteMoneda aux = new DeleteMoneda(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Moneda = ic;
        }

        public void deleteMoneda()
        {
            foreach (DeleteMoneda item in this._moneda)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogMonedaModel(IDataMapper dataMapper)
        {
            this._dataMapper = new MonedaDataMapper();
            this._moneda = new FixupCollection<DeleteMoneda>();
            this._selectedMoneda = new MONEDA();
            this.loadMonedas();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
