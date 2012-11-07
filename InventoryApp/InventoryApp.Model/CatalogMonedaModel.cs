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
        private FixupCollection<MONEDA> _moneda;
        private IDataMapper _dataMapper;

        public FixupCollection<MONEDA> Moneda
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
            FixupCollection<MONEDA> ic = element as FixupCollection<MONEDA>; 
            if (ic != null)
            {
                this.Moneda = ic;
            }
        }

        public CatalogMonedaModel(IDataMapper dataMapper)
        {
            this._dataMapper = new MonedaDataMapper();
            this._moneda = new  FixupCollection<MONEDA>();
            this._selectedMoneda = new MONEDA();
            this.loadMonedas();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
