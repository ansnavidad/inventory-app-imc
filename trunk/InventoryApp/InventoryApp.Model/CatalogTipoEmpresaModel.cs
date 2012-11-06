using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogTipoEmpresaModel : INotifyPropertyChanged
    {
        private FixupCollection<TIPO_EMPRESA> _tipoEmpresas;
        private TIPO_EMPRESA _selectedEmpresa;
        private IDataMapper _dataMapper;

        public FixupCollection<TIPO_EMPRESA> TipoEmpresas
        {
            get
            {
                return _tipoEmpresas;
            }
            set
            {
                if (_tipoEmpresas != value)
                {
                    _tipoEmpresas = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TipoEmpresas"));
                    }
                }
            }
        }

        public TIPO_EMPRESA SelectedEmpresa
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

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<TIPO_EMPRESA> ic = element as FixupCollection<TIPO_EMPRESA>; //element as FixupCollection<PROYECTO>;
            if (ic != null)
            {
                //this._itemStatus = ic;
                this.TipoEmpresas = ic;
            }
        }


        public CatalogTipoEmpresaModel(IDataMapper dataMapper)
        {
            this._dataMapper = new TipoEmpresaDataMapper();
            this._tipoEmpresas = new FixupCollection<TIPO_EMPRESA>();
            this._selectedEmpresa = new TIPO_EMPRESA();
            this.loadItems();
            //this.loadItems(new ItemDataMapper());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
