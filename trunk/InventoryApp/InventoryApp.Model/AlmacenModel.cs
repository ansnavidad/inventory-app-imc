using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class AlmacenModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidAlmacen;
        private string _almacenName;
        private string _contacto;
        private string _mail;
        private string _mailDefault;        
        private string _direccion;        
        private AlmacenDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidAlmacen
        {
            get
            {
                return _unidAlmacen;
            }
            set
            {
                if (_unidAlmacen != value)
                {
                    _unidAlmacen = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidAlmacen"));
                    }
                }
            }
        }

        public string AlmacenName
        {
            get
            {
                return _almacenName;
            }
            set
            {
                if (_almacenName != value)
                {
                    _almacenName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("AlmacenName"));
                    }
                }
            }
        }

        public string Contacto
        {
            get
            {
                return _contacto;
            }
            set
            {
                if (_contacto != value)
                {
                    _contacto = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Contacto"));
                    }
                }
            }
        }

        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                if (_mail != value)
                {
                    _mail = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Mail"));
                    }
                }
            }
        }

        public string MailDefault
        {
            get
            {
                return _mailDefault;
            }
            set
            {
                if (_mailDefault != value)
                {
                    _mailDefault = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("MailDefault"));
                    }
                }
            }
        }

        public string Direccion
        {
            get
            {
                return _direccion;
            }
            set
            {
                if (_direccion != value)
                {
                    _direccion = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Direccion"));
                    }
                }
            }
        }
        
        #endregion

        public void saveAlmacen()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new ALMACEN() {IS_ACTIVE=true,
                                                         ALMACEN_NAME= this._almacenName,
                                                         CONTACTO=this._contacto,
                                                         MAIL=this._mail,
                                                         MAIL_DEFAULT=this._mailDefault,
                                                         DIRECCION=this._direccion
                                                         });
            }
        }

        public void updateAlmacen()
        {
            this._dataMapper.udpateElement(new ALMACEN()
            {
                UNID_ALMACEN=this._unidAlmacen,
                ALMACEN_NAME = this._almacenName,
                CONTACTO = this._contacto,
                MAIL = this._mail,
                MAIL_DEFAULT = this._mailDefault,
                DIRECCION = this._direccion
            });
        }

        #region Constructors
        public AlmacenModel(IDataMapper dataMapper)
        {
            if ((dataMapper as AlmacenDataMapper) != null)
            {
                this._dataMapper = dataMapper as AlmacenDataMapper;
            }
            
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
