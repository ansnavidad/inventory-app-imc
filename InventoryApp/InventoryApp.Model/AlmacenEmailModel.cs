using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class AlmacenEmailModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidAlmacenEmail;
        private string _email;
        private bool _isDefault;
        private ALMACEN _almacen;
        private AlmacenEmailDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidAlmacenEmail
        {
            get
            {
                return _unidAlmacenEmail;
            }
            set
            {
                if (_unidAlmacenEmail != value)
                {
                    _unidAlmacenEmail = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidAlmacenEmail"));
                    }
                }
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Email"));
                    }
                }
            }
        }

        public bool IsDefault
        {
            get
            {
                return _isDefault;
            }
            set
            {
                if (_isDefault != value)
                {
                    _isDefault = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsDefault"));
                    }
                }
            }
        }

        public ALMACEN Almacen
        {
            get
            {
                return _almacen;
            }
            set
            {
                if (_almacen != value)
                {
                    _almacen = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Almacen"));
                    }
                }
            }
        }
        #endregion

        public void saveAlmacenEmail()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new ALMACEN_EMAIL()
                {
                 IS_ACTIVE = true,
                 EMAIL = this._email,
                 IS_DEFAULT = this._isDefault,
                 UNID_ALMACEN= this._almacen.UNID_ALMACEN
                });
            }
        }

        public void updateAlmacenEmail()
        {
            this._dataMapper.udpateElement(new ALMACEN_EMAIL()
            {
             UNID_ALMACEN_EMAIL = this._unidAlmacenEmail,
             EMAIL = this._email,
             IS_DEFAULT = this._isDefault,
             UNID_ALMACEN = this._almacen.UNID_ALMACEN
            });
        }

         #region Constructors
        public AlmacenEmailModel(IDataMapper dataMapper)
        {
            if ((dataMapper as AlmacenEmailDataMapper) != null)
            {
                this._dataMapper = dataMapper as AlmacenEmailDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
