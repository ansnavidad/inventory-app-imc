using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.Data.Objects;
using System.Data;

namespace InventoryApp.Model
{
    public class CatalogProyectoModel : INotifyPropertyChanged
    {

        private PROYECTO _selectedProyecto;
        private FixupCollection<PROYECTO> _proyecto;
        //private ProyectoICollection _proyecto;
        private IDataMapper _dataMapper;

        public FixupCollection<PROYECTO> Proyecto
        {
            get
            {
                return _proyecto;
            }
            set
            {
                if (_proyecto != value)
                {
                    _proyecto = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Proyecto"));
                    }
                }
            }
        }
        public PROYECTO SelectedProyecto
        {
            get
            {
                return _selectedProyecto;
            }
            set
            {
                if (_selectedProyecto != value)
                {
                    _selectedProyecto = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedProyecto"));
                    }
                }
            }
        }
        public CatalogProyectoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ProyectoDataMapper();
            this._proyecto = new FixupCollection<PROYECTO>();
            this._selectedProyecto = new PROYECTO();
            this.loadItems();
            
        }
        public void loadItems()
        {
            object element = this._dataMapper.getElements();


            FixupCollection<PROYECTO> ic = element as FixupCollection<PROYECTO>; //element as FixupCollection<PROYECTO>;
            if (ic != null)
            {
                this._proyecto = ic;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
