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
        private FixupCollection<DeleteTipoEmpresa> _tipoEmpresas;
        private TIPO_EMPRESA _selectedEmpresa;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteTipoEmpresa> TipoEmpresas
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

            FixupCollection<DeleteTipoEmpresa> ic = new FixupCollection<DeleteTipoEmpresa>();

            if (element != null)
            {
                if (((List<TIPO_EMPRESA>)element).Count > 0)
                {
                    foreach (TIPO_EMPRESA item in (List<TIPO_EMPRESA>)element)
                    {
                        DeleteTipoEmpresa aux = new DeleteTipoEmpresa(item);
                        ic.Add(aux);
                    }
                }
            }
            this.TipoEmpresas = ic;
        }

        public void deleteTipoEmpresa()
        {
            foreach (DeleteTipoEmpresa item in this._tipoEmpresas)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogTipoEmpresaModel(IDataMapper dataMapper)
        {
            this._dataMapper = new TipoEmpresaDataMapper();
            this._tipoEmpresas = new FixupCollection<DeleteTipoEmpresa>();
            this._selectedEmpresa = new TIPO_EMPRESA();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
