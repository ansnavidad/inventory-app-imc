using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class CatalogTipoCotizacionModel : INotifyPropertyChanged
    {
        private TIPO_COTIZACION _selectedTipoCotizacion;
        private FixupCollection<TIPO_COTIZACION> _tipoCotizacion;
        private IDataMapper _dataMapper;

        public FixupCollection<TIPO_COTIZACION> TipoCotizacion
        {
            get
            {
                return _tipoCotizacion;
            }
            set
            {
                if (_tipoCotizacion != value)
                {
                    _tipoCotizacion = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TipoCotizacion"));
                    }
                }
            }
        }

        public TIPO_COTIZACION SelectedTipoCotizacion
        {
            get
            {
                return _selectedTipoCotizacion;
            }
            set
            {
                if (_selectedTipoCotizacion != value)
                {
                    _selectedTipoCotizacion = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedTipoCotizacion"));
                    }
                }
            }
        }

        public CatalogTipoCotizacionModel(IDataMapper dataMapper)
        {
            this._dataMapper = new TipoCotizacionDataMapper();
            this._tipoCotizacion = new FixupCollection<TIPO_COTIZACION>();
            this._selectedTipoCotizacion = new TIPO_COTIZACION();
            this.loadItems();
            
        }

        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<TIPO_COTIZACION> ic = element as FixupCollection<TIPO_COTIZACION>; //element as FixupCollection<PROYECTO>;
            if (ic != null)
            {
                this._tipoCotizacion = ic;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
