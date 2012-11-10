using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class TipoMovimientoModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidTipoMovimiento;
        private string _tipoMovimientoName;
        private string _signoMovimiento;
        private TipoMovimientoDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidTipoMovimiento
        {
            get
            {
                return _unidTipoMovimiento;
            }
            set
            {
                if (_unidTipoMovimiento != value)
                {
                    _unidTipoMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTipoMovimiento"));
                    }
                }
            }
        }

        public string TipoMovimientoName
        {
            get
            {
                return _tipoMovimientoName;
            }
            set
            {
                if (_tipoMovimientoName != value)
                {
                    _tipoMovimientoName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoMovimientoName"));
                    }
                }
            }
        }

        public string SignoMovimiento
        {
            get
            {
                return _signoMovimiento;
            }
            set
            {
                if (_signoMovimiento != value)
                {
                    _signoMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("SignoMovimiento"));
                    }
                }
            }
        }
        #endregion

        public void saveTipoMovimiento()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new TIPO_MOVIMIENTO() {IS_ACTIVE=true, TIPO_MOVIMIENTO_NAME = this._tipoMovimientoName, SIGNO_MOVIMIENTO =this._signoMovimiento});
            }
        }

        public void updateTipoMovimiento()
        {
            this._dataMapper.udpateElement(new TIPO_MOVIMIENTO() {  UNID_TIPO_MOVIMIENTO=this._unidTipoMovimiento, TIPO_MOVIMIENTO_NAME = this._tipoMovimientoName, SIGNO_MOVIMIENTO =this._signoMovimiento });
        }

        #region Constructors
        public TipoMovimientoModel(IDataMapper dataMapper)
        {
            if ((dataMapper as TipoMovimientoDataMapper) != null)
            {
                this._dataMapper = dataMapper as TipoMovimientoDataMapper;
            }
            
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
