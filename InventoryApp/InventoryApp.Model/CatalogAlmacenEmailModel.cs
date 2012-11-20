using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogAlmacenEmailModel : INotifyPropertyChanged
    {
        private FixupCollection<DeleteAlmacenEmail> _almacenEmail;
        private DeleteAlmacenEmail _selectedAlmacenEmail;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteAlmacenEmail> AlmacenEmail
        {
            get
            {
                return _almacenEmail;
            }
            set
            {
                if (_almacenEmail != value)
                {
                    _almacenEmail = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("AlmacenEmail"));
                    }
                }
            }
        }

        public DeleteAlmacenEmail SelectedAlmacenEmail
        {
            get
            {
                return _selectedAlmacenEmail;
            }
            set
            {
                if (_selectedAlmacenEmail != value)
                {
                    _selectedAlmacenEmail = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedAlmacenEmail"));
                    }
                }
            }
        }

        public void loadItems()
        {         
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteAlmacenEmail> ic = new FixupCollection<DeleteAlmacenEmail>();

            if (element != null)
            {
                if (((List<ALMACEN_EMAIL>)element).Count > 0)
                {
                    foreach (ALMACEN_EMAIL item in (List<ALMACEN_EMAIL>)element)
                    {
                        DeleteAlmacenEmail aux = new DeleteAlmacenEmail(item);
                        ic.Add(aux);
                    }
                }
            }
            this.AlmacenEmail = ic;
        }

        public void deleteAlamacenEmail()
        {
            foreach (DeleteAlmacenEmail item in this._almacenEmail)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogAlmacenEmailModel(IDataMapper dataMapper)
        {
            this._dataMapper = new AlmacenEmailDataMapper();
            this._almacenEmail = new FixupCollection<DeleteAlmacenEmail>();
            //this._selectedAlmacenEmail = new ALMACEN_EMAIL();
            this.loadItems();
        }
    
public event PropertyChangedEventHandler  PropertyChanged;
}
}
