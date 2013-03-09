using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogPropiedadModel : INotifyPropertyChanged
    {
        private PROPIEDAD _selectedPropiedad;
        private FixupCollection<DeletePropiedad> _propiedad;
        private IDataMapper _dataMapper;

        public FixupCollection<DeletePropiedad> Propiedad
        {
            get
            {
                return _propiedad;
            }
            set
            {
                if (_propiedad != value)
                {
                    _propiedad = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Propiedad"));
                    }
                }
            }
        }
        public PROPIEDAD SelectedPropiedad
        {
            get
            {
                return _selectedPropiedad;
            }
            set
            {
                if (_selectedPropiedad != value)
                {
                    _selectedPropiedad = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedPropiedad"));
                    }
                }
            }
        }
        public CatalogPropiedadModel(IDataMapper dataMapper)
        {
            this._dataMapper = new PropiedadDataMapper();
            this._propiedad = new FixupCollection<DeletePropiedad>();
            this._selectedPropiedad = new PROPIEDAD();
            this.loadItems();
            
        }
        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeletePropiedad> ic = new FixupCollection<DeletePropiedad>();

            if (element != null)
            {
                if (((List<PROPIEDAD>)element).Count > 0)
                {
                    foreach (PROPIEDAD item in (List<PROPIEDAD>)element)
                    {
                        DeletePropiedad aux = new DeletePropiedad(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Propiedad = ic;
        }
        public void deletePropiedad(USUARIO u)
        {
            foreach (DeletePropiedad item in this._propiedad)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }
        }
    
public event PropertyChangedEventHandler  PropertyChanged;
}
}
