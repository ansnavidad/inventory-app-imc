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
        private FixupCollection<TIPO_MOVIMIENTO> _tipoMovimiento;
        private IDataMapper _dataMapper;

        public FixupCollection<TIPO_MOVIMIENTO> TipoMovimiento
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
            this._tipoMovimiento = new FixupCollection<TIPO_MOVIMIENTO>();
            this._selectedTipoMovimiento = new TIPO_MOVIMIENTO();
            this.loadItems();
            
        }
        
        public void loadItems()
        {
            object element = this._dataMapper.getElements();


            FixupCollection<TIPO_MOVIMIENTO> ic = element as FixupCollection<TIPO_MOVIMIENTO>; //element as FixupCollection<PROYECTO>;
            if (ic != null)
            {
                this.TipoMovimiento = ic;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
