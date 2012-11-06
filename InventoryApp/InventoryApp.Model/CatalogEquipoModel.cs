using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model 
{
    public class CatalogEquipoModel: INotifyPropertyChanged 
    {
        private FixupCollection<EQUIPO> _equipos;
        private EQUIPO _selectedEquipo;
        private IDataMapper _dataMapper;

        public FixupCollection<EQUIPO> Equipos
        {
            get
            {
                return _equipos;
            }
            set
            {
                if (_equipos != value)
                {
                    _equipos = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Equipos"));
                    }
                }
            }
        }

        public EQUIPO SelectedEquipo
        {
            get
            {
                return _selectedEquipo;
            }
            set
            {
                if (_selectedEquipo != value)
                {
                    _selectedEquipo = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedEquipo"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<EQUIPO> ic = element as FixupCollection<EQUIPO>; //element as FixupCollection<PROYECTO>;
            if (ic != null)
            {
                //this._itemStatus = ic;
                this.Equipos = ic;
            }
        }


        public CatalogEquipoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new EquipoDataMapper();
            this._equipos = new FixupCollection<EQUIPO>();
            this._selectedEquipo = new EQUIPO();
            this.loadItems();

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
