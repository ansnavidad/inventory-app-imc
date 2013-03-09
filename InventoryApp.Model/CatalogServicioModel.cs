using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogServicioModel : INotifyPropertyChanged
    {
        private SERVICIO _selectedServicio;
        private FixupCollection<DeleteServicio> _servicio;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteServicio> Servicio
        {
            get
            {
                return _servicio;
            }
            set
            {
                if (_servicio != value)
                {
                    _servicio = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Servicio"));
                    }
                }
            }
        }
        public SERVICIO SelectedServicio
        {
            get
            {
                return _selectedServicio;
            }
            set
            {
                if (_selectedServicio != value)
                {
                    _selectedServicio = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedServicio"));
                    }
                }
            }
        }
        public CatalogServicioModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ServicioDataMapper();
            this._servicio = new FixupCollection<DeleteServicio>();
            this._selectedServicio = new SERVICIO();
            this.loadItems();
            
        }
        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteServicio> ic = new FixupCollection<DeleteServicio>();

            if (element != null)
            {
                if (((List<SERVICIO>)element).Count > 0)
                {
                    foreach (SERVICIO item in (List<SERVICIO>)element)
                    {
                        DeleteServicio aux = new DeleteServicio(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Servicio = ic;
        }
        public void deleteServicio(USUARIO u)
        {
            foreach (DeleteServicio item in this._servicio)
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
