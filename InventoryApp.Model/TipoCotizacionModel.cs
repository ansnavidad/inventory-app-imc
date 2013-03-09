using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class TipoCotizacionModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidTipoCotizacion;
        private string _tipoCotizacionName;
        private TipoCotizacionDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion
        #region Props
        public long UnidTipoCotizacion
        {
            get
            {
                return _unidTipoCotizacion;
            }
            set
            {
                if (_unidTipoCotizacion != value)
                {
                    _unidTipoCotizacion = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTipoCotizacion"));
                    }
                }
            }
        }

        public string TipoCotizacionName
        {
            get
            {
                return _tipoCotizacionName;
            }
            set
            {
                if (_tipoCotizacionName != value)
                {
                    _tipoCotizacionName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoCotizacionName"));
                    }
                }
            }
        }
        #endregion

        public void saveTipoCotizacion()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new TIPO_COTIZACION() {IS_ACTIVE=true, TIPO_COTIZACION_NAME = this._tipoCotizacionName }, this.ActualUser);
            }
        }

        public void updateTipoCotizacion()
        {
            this._dataMapper.udpateElement(new TIPO_COTIZACION() { UNID_TIPO_COTIZACION = this._unidTipoCotizacion, TIPO_COTIZACION_NAME = this._tipoCotizacionName }, this.ActualUser);
        }
        #region Constructors
        public TipoCotizacionModel(IDataMapper dataMapper, USUARIO u)
        {
            if ((dataMapper as TipoCotizacionDataMapper) != null)
            {
                this._dataMapper = dataMapper as TipoCotizacionDataMapper;
            }
            this.ActualUser = u;
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
