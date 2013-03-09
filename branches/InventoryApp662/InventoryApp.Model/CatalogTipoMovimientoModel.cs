using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class CatalogTipoMovimientoModel : INotifyPropertyChanged
    {
        private TIPO_MOVIMIENTO _selectedTipoMovimiento;
        private FixupCollection<DeleteTipoMovimiento> _tipoMovimiento;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteTipoMovimiento> TipoMovimiento
        {
            get
            {
                return _tipoMovimiento;
            }
            set
            {
                if (_tipoMovimiento != value)
                {
                    _tipoMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TipoMovimiento"));
                    }
                }
            }
        }

        public TIPO_MOVIMIENTO SelectedTipoMovimiento
        {
            get
            {
                return _selectedTipoMovimiento;
            }
            set
            {
                if (_selectedTipoMovimiento != value)
                {
                    _selectedTipoMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedTipoMovimiento"));
                    }
                }
            }
        }

        public CatalogTipoMovimientoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new TipoMovimientoDataMapper();
            this._tipoMovimiento = new FixupCollection<DeleteTipoMovimiento>();
            this._selectedTipoMovimiento = new TIPO_MOVIMIENTO();
            this.loadItems();
            
        }
        
        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteTipoMovimiento> ic = new FixupCollection<DeleteTipoMovimiento>();

            if (element != null)
            {
                if (((List<TIPO_MOVIMIENTO>)element).Count > 0)
                {
                    foreach (TIPO_MOVIMIENTO item in (List<TIPO_MOVIMIENTO>)element)
                    {
                        DeleteTipoMovimiento aux = new DeleteTipoMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.TipoMovimiento = ic;
        }

        public void deleteTipoMovimiento()
        {
            foreach (DeleteTipoMovimiento item in this._tipoMovimiento)
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
