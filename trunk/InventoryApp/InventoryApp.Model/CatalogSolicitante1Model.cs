using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogSolicitante1Model: INotifyPropertyChanged
    {
        private FixupCollection<SOLICITANTE1> _solicitante1;
        private SOLICITANTE1 _selectedSolicitante1;
        private IDataMapper _dataMapper;

        public FixupCollection<SOLICITANTE1> Solicitante1
        {
            get
            {
                return _solicitante1;
            }
            set
            {
                if (_solicitante1 != value)
                {
                    _solicitante1 = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Solicitante1"));
                    }
                }
            }
        }

        public SOLICITANTE1 SelectedSolicitante1
        {
            get
            {
                return _selectedSolicitante1;
            }
            set
            {
                if (_selectedSolicitante1 != value)
                {
                    _selectedSolicitante1 = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedSolicitante1"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<SOLICITANTE1> ic = new FixupCollection<SOLICITANTE1>();

            foreach (SOLICITANTE1 elemento in (List<SOLICITANTE1>)element)
            {
                ic.Add((SOLICITANTE1)elemento);
            }
            if (ic != null)
            {
                this.Solicitante1 = ic;
            }
        }

        public CatalogSolicitante1Model(IDataMapper dataMapper)
        {
            this._dataMapper = new Solicitante1DataMapper();
            this._solicitante1 = new FixupCollection<SOLICITANTE1>();
            this._selectedSolicitante1 = new SOLICITANTE1();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
