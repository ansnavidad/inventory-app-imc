using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogProgramadoModel : INotifyPropertyChanged
    {
        private FixupCollection<DeleteProgramado> _programado;
        private DeleteProgramado _selectedProgramado;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteProgramado> Programado
        {
            get
            {
                return _programado;
            }
            set
            {
                if (_programado != value)
                {
                    _programado = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Programado"));
                    }
                }
            }
        }

        public DeleteProgramado SelectedProgramado
        {
            get
            {
                return _selectedProgramado;
            }
            set
            {
                if (_selectedProgramado != value)
                {
                    _selectedProgramado = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedProgramado"));
                    }
                }
            }
        }

        public void loadProgramado()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteProgramado> ic = new FixupCollection<DeleteProgramado>();

            if (element != null)
            {
                if (((List<PROGRAMADO>)element).Count > 0)
                {
                    foreach (PROGRAMADO item in (List<PROGRAMADO>)element)
                    {
                        DeleteProgramado aux = new DeleteProgramado(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Programado = ic;
        }

        public void deleteProgramado()
        {
            foreach (DeleteProgramado item in _programado)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }
        public CatalogProgramadoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ProgramadoDataMapper();
            this._programado = new FixupCollection<DeleteProgramado>();
            this.loadProgramado();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
