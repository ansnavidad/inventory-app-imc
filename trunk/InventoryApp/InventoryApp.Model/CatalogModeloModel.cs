using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogModeloModel : INotifyPropertyChanged
    {
        private FixupCollection<DeleteModelo> _modelos;
        private MODELO _selectedmodelo;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteModelo> Modelos
        {
            get
            {
                return _modelos;
            }
            set
            {
                if (_modelos != value)
                {
                    _modelos = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Modelos"));
                    }
                }
            }
        }

        public MODELO SelectedModelo
        {
            get
            {
                return _selectedmodelo;
            }
            set
            {
                if (_selectedmodelo != value)
                {
                    _selectedmodelo = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedModelo"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteModelo> ic = new FixupCollection<DeleteModelo>();

            if (element != null)
            {
                if (((List<MODELO>)element).Count > 0)
                {
                    foreach (MODELO item in (List<MODELO>)element)
                    {
                        DeleteModelo aux = new DeleteModelo(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Modelos = ic;
        }

        public void deleteModelo()
        {
            foreach (DeleteModelo item in this._modelos)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogModeloModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ModeloDataMapper();
            this._modelos = new FixupCollection<DeleteModelo>();
            this._selectedmodelo = new MODELO();
            this.loadItems();

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
