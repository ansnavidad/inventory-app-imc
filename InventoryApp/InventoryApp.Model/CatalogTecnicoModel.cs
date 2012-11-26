using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogTecnicoModel : INotifyPropertyChanged
    {
        private TECNICO _selectedTecnico;
        private FixupCollection<DeleteTecnico> _tecnico;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteTecnico> Tecnico
        {
            get { 
                return _tecnico; 
                }
            set
            {
                if (_tecnico !=value)
                {
                    _tecnico = value;
                        if (PropertyChanged !=null)
	                    {
                            PropertyChanged(this, new PropertyChangedEventArgs("Tecnico"));
	                    }
                }
            }
        }
        public TECNICO SelectedTecnico
        {
            get
            {
                return _selectedTecnico;
            }
            set
            {
                if (_selectedTecnico != value)
                {
                    _selectedTecnico = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedTecnico"));
                    }
                }
            }
        }

        public CatalogTecnicoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new TecnicoDataMapper();
            this._tecnico = new FixupCollection<DeleteTecnico>();
            this._selectedTecnico = new TECNICO();
            this.loadItems();
            
        }
        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<DeleteTecnico> ic = new FixupCollection<DeleteTecnico>();

            if (element != null)
            {
                if (((List<TECNICO>)element).Count > 0)
                {
                    foreach (TECNICO item in (List<TECNICO>)element)
                    {
                        DeleteTecnico aux = new DeleteTecnico(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Tecnico = ic;
        }

        public void deleteTecnico()
        {
            foreach (DeleteTecnico item in this._tecnico)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
