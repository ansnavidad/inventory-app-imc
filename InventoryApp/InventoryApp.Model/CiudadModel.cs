using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CiudadModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidCiudad;
        private string _ciudad;
        private string _iso;
        private long _unidPais;
        private CiudadDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidCiudad
        {
            get
            {
                return _unidCiudad;
            }
            set
            {
                if (_unidCiudad != value)
                {
                    _unidCiudad = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidCiudad"));
                    }
                }
            }
        }
        
        public string Ciudad
        {
            get
            {
                return _ciudad;
            }
            set
            {
                if (_ciudad != value)
                {
                    _ciudad = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Ciudad"));
                    }
                }
            }
        }
        
        public string Iso
        {
            get
            {
                return _iso;
            }
            set
            {
                if (_iso != value)
                {
                    _iso = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Iso"));
                    }
                }
            }
        }    
        
        public long UnidPais
        {
            get
            {
                return _unidPais;
            }
            set
            {
                if (_unidPais != value)
                {
                    _unidPais = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidPais"));
                    }
                }
            }
        }        
        
        #endregion

        public void saveCiudad()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new CIUDAD() { IS_ACTIVE = true, CIUDAD1 = this._ciudad, ISO = this._iso });
            }
        }

        public void updateCiudad()
        {
            this._dataMapper.udpateElement(new CIUDAD() {  UNID_CIUDAD = this._unidCiudad, CIUDAD1 = this._ciudad, ISO = this._iso });
        }

        #region Constructors
        public CiudadModel(IDataMapper dataMapper)
        {
            if ((dataMapper as CiudadDataMapper) != null)
            {
                this._dataMapper = dataMapper as CiudadDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
