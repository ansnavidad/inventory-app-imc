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
        private TECNICO _tecnico;
        public List<long> _unidsTecnicos;
        public List<long> _auxUnidsTecnicos;
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

        public TECNICO Tecnico
        {
            get
            {
                return _tecnico;
            }
            set
            {
                if (_tecnico != value)
                {
                    _tecnico = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Tecnico"));
                    }
                }
            }
        }
        
        #endregion

        public void saveAlmacen()
        {
            if (_dataMapper != null)
            {               
              _dataMapper.insertRelacion(new ALMACEN()
                {
                    IS_ACTIVE = true,
                    ALMACEN_NAME = this._almacenName,
                    CONTACTO = this._contacto,
                    MAIL = this._mail,
                    MAIL_DEFAULT = this._mailDefault,
                    DIRECCION = this._direccion,
                    UNID_ALMACEN = this.UnidAlmacen

                },this._unidsTecnicos);
            }
        }

        public void updateAlmacen(string s)
        {            
            this._dataMapper.updateRelacion(new ALMACEN()
            {
                UNID_ALMACEN = this._unidAlmacen,
                ALMACEN_NAME = this._almacenName,
                CONTACTO = this._contacto,
                MAIL = this._mail,
                MAIL_DEFAULT = this._mailDefault,
                DIRECCION = this._direccion
            },this._unidsTecnicos,this._auxUnidsTecnicos, "s");
        }

        public void updateAlmacen()
        {
            this._dataMapper.updateRelacion(new ALMACEN()
            {
                UNID_ALMACEN = this._unidAlmacen,
                ALMACEN_NAME = this._almacenName,
                CONTACTO = this._contacto,
                MAIL = this._mail,
                MAIL_DEFAULT = this._mailDefault,
                DIRECCION = this._direccion
            },this._unidsTecnicos,this._auxUnidsTecnicos);
        }
        public object GetAlmacenCategoria(long obj)
        {
            return this._dataMapper.getElementAlmacenTecnico(obj);
        }
        #region Constructors
        public AlmacenModel(IDataMapper dataMapper)
        {
            if ((dataMapper as AlmacenDataMapper) != null)
            {
                this._dataMapper = dataMapper as AlmacenDataMapper;
            }
            //varibles que guardan los ids de tecnicos
            this._unidsTecnicos = new List<long>();
            this._auxUnidsTecnicos = new List<long>();
            
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
