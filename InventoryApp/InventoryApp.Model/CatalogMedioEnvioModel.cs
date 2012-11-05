using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogMedioEnvioModel : INotifyPropertyChanged
    {
        private MEDIO_ENVIO _selectedMedioEnvio;
        private FixupCollection<MEDIO_ENVIO> _medioEnvio;
        private IDataMapper _dataMapper;
        //private bool _isChecked;

        //public bool IsChecked
        //{
        //    get { return this._isChecked; }
        //    set
        //    {
        //        if (value != this._isChecked)
        //        {
        //            this._isChecked = value;
        //            if (this.PropertyChanged != null)
        //                this.PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
        //        }
        //    }
        //}

        public FixupCollection<MEDIO_ENVIO> MedioEnvio
        {
            get { 
                return _medioEnvio; 
                }
            set
            {
                if (_medioEnvio !=value)
                {
                    _medioEnvio = value;
                        if (PropertyChanged !=null)
	                    {
                            PropertyChanged(this, new PropertyChangedEventArgs("MedioEnvio"));
	                    }
                }
            }
        }
        public MEDIO_ENVIO SelectedMedioEnvio
        {
            get
            {
                return _selectedMedioEnvio;
            }
            set
            {
                if (_selectedMedioEnvio != value)
                {
                    _selectedMedioEnvio = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedMedioEnvio"));
                    }
                }
            }
        }

        public CatalogMedioEnvioModel(IDataMapper dataMapper)
        {
            this._dataMapper = new MedioEnvioDataMapper();
            this._medioEnvio = new FixupCollection<MEDIO_ENVIO>();
            this._selectedMedioEnvio = new MEDIO_ENVIO();
            //this._isChecked = false;
            this.loadItems();
            
        }
        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<MEDIO_ENVIO> ic = element as FixupCollection<MEDIO_ENVIO>; //element as FixupCollection<PROYECTO>;
            if (ic != null)
            {
                //this._itemStatus = ic;
                this.MedioEnvio = ic;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
