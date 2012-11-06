using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogTerminoEnvioModel : INotifyPropertyChanged
    {
        private FixupCollection<TERMINO_ENVIO> _terminoEnvio;
        private TERMINO_ENVIO _selectedTerminoEnvio;
        private IDataMapper _dataMapper;

        public FixupCollection<TERMINO_ENVIO> TerminoEnvio
        {
            get
            {
                return _terminoEnvio;
            }
            set
            {
                if (_terminoEnvio != value)
                {
                    _terminoEnvio = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TerminoEnvio"));
                    }
                }
            }
        }

        public TERMINO_ENVIO SelectedTerminoEnvio
        {
            get
            {
                return _selectedTerminoEnvio;
            }
            set
            {
                if (_selectedTerminoEnvio != value)
                {
                    _selectedTerminoEnvio = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedTerminoEnvio"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<TERMINO_ENVIO> ic = element as FixupCollection<TERMINO_ENVIO>;
            if (ic != null)
            {
                this.TerminoEnvio = ic;
            }
        }

        public CatalogTerminoEnvioModel(IDataMapper dataMapper)
        {
            this._dataMapper = new TerminoEnvioDataMapper();
            this._terminoEnvio = new FixupCollection<TERMINO_ENVIO>();
            this._selectedTerminoEnvio = new TERMINO_ENVIO();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
