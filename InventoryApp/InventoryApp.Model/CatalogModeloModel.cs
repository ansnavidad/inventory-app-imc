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
        private FixupCollection<MODELO> _modelos;
        private MODELO _selectedmodelo;
        private IDataMapper _dataMapper;

        public FixupCollection<MODELO> Modelos
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

            FixupCollection<MODELO> ic = element as FixupCollection<MODELO>; //element as FixupCollection<PROYECTO>;
            if (ic != null)
            {
                //this._itemStatus = ic;
                this.Modelos = ic;
            }
        }


        public CatalogModeloModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ModeloDataMapper();
            this._modelos = new FixupCollection<MODELO>();
            this._selectedmodelo = new MODELO();
            this.loadItems();

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
