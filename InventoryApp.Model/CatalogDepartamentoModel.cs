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
        private FixupCollection<DeleteDepartamento> _departamento;
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

        public FixupCollection<DeleteDepartamento> Departamento
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

            FixupCollection<DeleteDepartamento> ic = new FixupCollection<DeleteDepartamento>();

            if (element != null)
            {
                if (((List<DEPARTAMENTO>)element).Count > 0)
                {
                    foreach (DEPARTAMENTO item in (List<DEPARTAMENTO>)element)
                    {
                        DeleteDepartamento aux = new DeleteDepartamento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Departamento = ic;
        }

        public void deleteDepartamentos(USUARIO u)
        {
            foreach (DeleteDepartamento item in this._departamento)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }           
        }

        public CatalogDepartamentoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new DepartamentoDataMapper();
            this._departamento = new FixupCollection<DeleteDepartamento>();
            this._selectedDepartamento = new DEPARTAMENTO();
            this.loadDepartamentos();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
