using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class UnidadModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidUnidad;
        private string _unidadName;
        private UnidadDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidUnidad
        {
            get
            {
                return _unidUnidad;
            }
            set
            {
                if (_unidUnidad != value)
                {
                    _unidUnidad = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidUnidad"));
                    }
                }
            }
        }

        public string UnidadName
        {
            get
            {
                return _unidadName;
            }
            set
            {
                if (_unidadName != value)
                {
                    _unidadName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidadName"));
                    }
                }
            }
        }
        #endregion

        public void saveUnidad()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new UNIDAD() { IS_ACTIVE = true,  UNIDAD1 = this._unidadName });
            }
        }

        public void updateUnidad()
        {
            this._dataMapper.udpateElement(new UNIDAD() {  UNID_UNIDAD=this._unidUnidad, UNIDAD1=this._unidadName });
        }

        #region Constructors
        public UnidadModel(IDataMapper dataMapper)
        {
            if ((dataMapper as UnidadDataMapper) != null)
            {
                this._dataMapper = dataMapper as UnidadDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
