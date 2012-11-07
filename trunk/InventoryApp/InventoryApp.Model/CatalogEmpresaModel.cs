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
        private FixupCollection<EMPRESA> _empresa;
        private IDataMapper _dataMapper;

        public FixupCollection<EMPRESA> Empresa
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
            this._empresa = new FixupCollection<EMPRESA>();
            this._selectedEmpresa = new EMPRESA();
            //this._isChecked = false;
            this.loadEmpresa();
            
        }
        public void loadEmpresa()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<EMPRESA> ic = element as FixupCollection<EMPRESA>;
            if (ic != null)
            {
                this.Empresa = ic;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
