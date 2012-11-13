using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class ServicioModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidServicio;
        private string _servicioName;
        private ServicioDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidServicio
        {
            get
            {
                return _unidServicio;
            }
            set
            {
                if (_unidServicio != value)
                {
                    _unidServicio = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidServicio"));
                    }
                }
            }
        }

        public string ServicioName
        {
            get
            {
                return _servicioName;
            }
            set
            {
                if (_servicioName != value)
                {
                    _servicioName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ServicioName"));
                    }
                }
            }
        }
        #endregion

        public void saveServicio()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new SERVICIO() { IS_ACTIVE = true,  SERVICIO_NAME = this._servicioName });
            }
        }

        public void updateServicio()
        {
            this._dataMapper.udpateElement(new SERVICIO() {   UNID_SERVICIO=this._unidServicio, SERVICIO_NAME =this._servicioName });
        }

        #region Constructors
        public ServicioModel(IDataMapper dataMapper)
        {
            if ((dataMapper as ServicioDataMapper) != null)
            {
                this._dataMapper = dataMapper as ServicioDataMapper;
            }
            
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
