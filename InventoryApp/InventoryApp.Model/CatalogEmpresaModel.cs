using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model 
{
    public class CatalogEmpresaModel : INotifyPropertyChanged
    {
        private EMPRESA _selectedEmpresa;
        private FixupCollection<DeleteEmpresa> _empresa;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteEmpresa> Empresa
        {
            get { 
                return _empresa; 
                }
            set
            {
                if (_empresa !=value)
                {
                    _empresa = value;
                        if (PropertyChanged !=null)
	                    {
                            PropertyChanged(this, new PropertyChangedEventArgs("Empresa"));
	                    }
                }
            }
        }
        public EMPRESA SelectedEmpresa
        {
            get
            {
                return _selectedEmpresa;
            }
            set
            {
                if (_selectedEmpresa != value)
                {
                    _selectedEmpresa = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedEmpresa"));
                    }
                }
            }
        }

        public CatalogEmpresaModel(IDataMapper dataMapper)
        {
            this._dataMapper = new EmpresaDataMapper();
            this._empresa = new FixupCollection<DeleteEmpresa>();
            this._selectedEmpresa = new EMPRESA();
            //this._isChecked = false;
            this.loadEmpresa();
            
        }

        public void loadEmpresa()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<DeleteEmpresa> ic = new FixupCollection<DeleteEmpresa>();

            if (element != null)
            {
                if (((List<EMPRESA>)element).Count > 0)
                {
                    foreach (EMPRESA item in (List<EMPRESA>)element)
                    {
                        DeleteEmpresa aux = new DeleteEmpresa(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Empresa = ic;
        }

        public void deleteEmpresa()
        {
            foreach (DeleteEmpresa item in this._empresa)
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
