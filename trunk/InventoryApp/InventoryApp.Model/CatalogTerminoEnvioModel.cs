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
        private FixupCollection<DeleteTerminoEnvio> _terminoEnvio;
        private TERMINO_ENVIO _selectedTerminoEnvio;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteTerminoEnvio> TerminoEnvio
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

            FixupCollection<DeleteTerminoEnvio> ic = new FixupCollection<DeleteTerminoEnvio>();

            if (element != null)
            {
                if (((List<TERMINO_ENVIO>)element).Count > 0)
                {
                    foreach (TERMINO_ENVIO item in (List<TERMINO_ENVIO>)element)
                    {
                        DeleteTerminoEnvio aux = new DeleteTerminoEnvio(item);
                        ic.Add(aux);
                    }
                }
            }
            this.TerminoEnvio = ic;
        }

        public void deleteTerminoEnvio(USUARIO u)
        {
            foreach (DeleteTerminoEnvio item in this._terminoEnvio)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }
        }

        public CatalogTerminoEnvioModel(IDataMapper dataMapper)
        {
            this._dataMapper = new TerminoEnvioDataMapper();
            this._terminoEnvio = new FixupCollection<DeleteTerminoEnvio>();
            this._selectedTerminoEnvio = new TERMINO_ENVIO();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
