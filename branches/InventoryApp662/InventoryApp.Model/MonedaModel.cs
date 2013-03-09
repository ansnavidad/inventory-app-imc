using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class MonedaModel
    {
        #region Fields
        private long _unidMoneda;
        private string _monedaName;
        private string _monedaAbr;
        private MonedaDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidMoneda
        {
            get
            {
                return _unidMoneda;
            }
            set
            {
                if (_unidMoneda != value)
                {
                    _unidMoneda = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidMoneda"));
                    }
                }
            }
        }

        public string MonedaName
        {
            get
            {
                return _monedaName;
            }
            set
            {
                if (_monedaName != value)
                {
                    _monedaName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("MonedaName"));
                    }
                }
            }
        }

        public string MonedaAbr
        {
            get { return _monedaAbr; }
            set { 
                if(_monedaAbr != value){
                    _monedaAbr = value;
                    if(PropertyChanged != null){
                        PropertyChanged(this, new PropertyChangedEventArgs("MonedaAbr"));
                    }
                }
            }
        }

        #endregion

        public void saveMoneda()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new MONEDA() { IS_ACTIVE = true, MONEDA_NAME = this._monedaName, MONEDA_ABR = this._monedaAbr }, this.ActualUser);
            }
        }

        public void updateMoneda()
        {
            this._dataMapper.udpateElement(new MONEDA() { UNID_MONEDA = this._unidMoneda, MONEDA_NAME = this._monedaName, MONEDA_ABR = this._monedaAbr }, this.ActualUser);
        }

        #region Constructors
        public MonedaModel(IDataMapper dataMapper, USUARIO u)
        {
            if ((dataMapper as MonedaDataMapper) != null)
            {
                this._dataMapper = dataMapper as MonedaDataMapper;
            }
            this.ActualUser = u;
        }
        public MonedaModel(IDataMapper dataMapper)
        {
            if ((dataMapper as MonedaDataMapper) != null)
            {
                this._dataMapper = dataMapper as MonedaDataMapper;
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
