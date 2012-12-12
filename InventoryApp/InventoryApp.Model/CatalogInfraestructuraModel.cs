using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogInfraestructuraModel : INotifyPropertyChanged
    {
        private FixupCollection<DeleteInfraestructura> _infraestructuras;
        private INFRAESTRUCTURA _selectedInfraestructura;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteInfraestructura> Infraestructuras
        {
            get
            {
                return _infraestructuras;
            }
            set
            {
                if (_infraestructuras != value)
                {
                    _infraestructuras = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Infraestructuras"));
                    }
                }
            }
        }

        public INFRAESTRUCTURA SelectedInfraestructura
        {
            get
            {
                return _selectedInfraestructura;
            }
            set
            {
                if (_selectedInfraestructura != value)
                {
                    _selectedInfraestructura = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedInfraestructura"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteInfraestructura> ic = new FixupCollection<DeleteInfraestructura>();

            if (element != null)
            {
                if (((List<INFRAESTRUCTURA>)element).Count > 0)
                {
                    foreach (INFRAESTRUCTURA item in (List<INFRAESTRUCTURA>)element)
                    {
                        DeleteInfraestructura aux = new DeleteInfraestructura(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Infraestructuras = ic;
        }

        public void deleteInfraestructura()
        {
            foreach (DeleteInfraestructura item in this._infraestructuras)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogInfraestructuraModel(IDataMapper dataMapper)
        {
            this._dataMapper = new InfraestructuraDataMapper();
            this._infraestructuras = new FixupCollection<DeleteInfraestructura>();
            this._selectedInfraestructura = new INFRAESTRUCTURA();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
