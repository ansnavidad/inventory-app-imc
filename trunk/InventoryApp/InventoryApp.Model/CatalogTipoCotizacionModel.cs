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
        private FixupCollection<DeleteTipoCotizacion> _tipoCotizacion;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteTipoCotizacion> TipoCotizacion
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
            this._tipoCotizacion = new FixupCollection<DeleteTipoCotizacion>();
            this._selectedTipoCotizacion = new TIPO_COTIZACION();
            this.loadItems();
            
        }

        public void loadItems()
        {

            object element = this._dataMapper.getElements();

            FixupCollection<DeleteTipoCotizacion> ic = new FixupCollection<DeleteTipoCotizacion>();

            if (element != null)
            {
                if (((List<TIPO_COTIZACION>)element).Count > 0)
                {
                    foreach (TIPO_COTIZACION item in (List<TIPO_COTIZACION>)element)
                    {
                        DeleteTipoCotizacion aux = new DeleteTipoCotizacion(item);
                        ic.Add(aux);
                    }
                }
            }
            this.TipoCotizacion = ic;
        }

        public void deleteTipoCotizacion(USUARIO u)
        {
            foreach (DeleteTipoCotizacion item in this._tipoCotizacion)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
