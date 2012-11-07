using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogDepartamentoModel : INotifyPropertyChanged
    {
        private DEPARTAMENTO _selectedDepartamento;
        private FixupCollection<DEPARTAMENTO> _departamento;
        private IDataMapper _dataMapper;

        public DEPARTAMENTO SelectedDepartamento
        {
            get { return _selectedDepartamento; }
            set {
                if (_selectedDepartamento != value)
                {
                    _selectedDepartamento = value;
                    if(PropertyChanged != null){
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedDepartamento"));
                    }
                }
            }
        }

        public FixupCollection<DEPARTAMENTO> Departamento
        {
            get { return _departamento; }
            set {
                if(_departamento != value){
                    _departamento = value;
                    if(PropertyChanged != null){
                        PropertyChanged(this, new PropertyChangedEventArgs("Departamento"));
                    }
                }
            }
        }

        public void loadDepartamentos()
        {
            object element = this._dataMapper.getElements();
            FixupCollection<DEPARTAMENTO> ic = element as FixupCollection<DEPARTAMENTO>;
            if (ic != null)
            {
                this.Departamento = ic;
            }
        }

        public CatalogDepartamentoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new DepartamentoDataMapper();
            this._departamento = new  FixupCollection<DEPARTAMENTO>();
            this._selectedDepartamento = new DEPARTAMENTO();
            this.loadDepartamentos();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
