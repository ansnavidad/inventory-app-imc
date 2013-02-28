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
        private FixupCollection<DeleteProyecto> _proyecto;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteProyecto> Proyecto
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
            this._proyecto = new FixupCollection<DeleteProyecto>();
            this._selectedProyecto = new PROYECTO();
            this.loadItems();
            
        }
        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteProyecto> ic = new FixupCollection<DeleteProyecto>();

            if (element != null)
            {
                if (((List<PROYECTO>)element).Count > 0)
                {
                    foreach (PROYECTO item in (List<PROYECTO>)element)
                    {
                        DeleteProyecto aux = new DeleteProyecto(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Proyecto = ic;
        }
        public void deleteProyecto(USUARIO u)
        {
            foreach (DeleteProyecto item in this._proyecto)
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
