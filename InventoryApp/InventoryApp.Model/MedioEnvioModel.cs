using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class MedioEnvioModel: INotifyPropertyChanged
    {
        #region Fields
        private long _unidMedioEnvio;
        private string _medioEnvioName;
        private MedioEnvioDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidMedioEnvio
        {
            get
            {
                return _unidMedioEnvio;
            }
            set
            {
                if (_unidMedioEnvio != value)
                {
                    _unidMedioEnvio = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidMedioEnvio"));
                    }
                }
            }
        }

        public string MedioEnvioName
        {
            get
            {
                return _medioEnvioName;
            }
            set
            {
                if (_medioEnvioName != value)
                {
                    _medioEnvioName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("MedioEnvioName"));
                    }
                }
            }
        }
        #endregion

        public void saveMedioEnvio()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new MEDIO_ENVIO() {IS_ACTIVE=true, MEDIO_ENVIO_NAME = this._medioEnvioName });
            }
        }

        public void updateMedioEnvio()
        {
            this._dataMapper.udpateElement(new MEDIO_ENVIO() { UNID_MEDIO_ENVIO=this._unidMedioEnvio,MEDIO_ENVIO_NAME=this._medioEnvioName });
        }

        #region Constructors
        public MedioEnvioModel(IDataMapper dataMapper)
        {
            if ((dataMapper as MedioEnvioDataMapper) != null)
            {
                this._dataMapper = dataMapper as MedioEnvioDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
