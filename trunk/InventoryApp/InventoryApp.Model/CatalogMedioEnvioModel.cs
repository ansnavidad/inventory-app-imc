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
        private FixupCollection<DeleteMedioEnvio> _medioEnvio;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteMedioEnvio> MedioEnvio
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
            this._medioEnvio = new FixupCollection<DeleteMedioEnvio>();
            this._selectedMedioEnvio = new MEDIO_ENVIO();
            //this._isChecked = false;
            this.loadItems();
            
        }

        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<DeleteMedioEnvio> ic = new FixupCollection<DeleteMedioEnvio>();

            if (element != null)
            {
                if (((List<MEDIO_ENVIO>)element).Count > 0)
                {
                    foreach (MEDIO_ENVIO item in (List<MEDIO_ENVIO>)element)
                    {
                        DeleteMedioEnvio aux = new DeleteMedioEnvio(item);
                        ic.Add(aux);
                    }
                }
            }
            this.MedioEnvio = ic;
        }

        public void deleteMedioEnvio(USUARIO u)
        {
            foreach (DeleteMedioEnvio item in this._medioEnvio)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
