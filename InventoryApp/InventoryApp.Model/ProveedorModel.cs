using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class ProveedorModel
    {
        #region Fields
        private long _unidProveedor;
        private string _proveedorName;
        private string _contacto;
        private string _tel1;
        private string _tel2;
        private string _mail;
        private string _calle;
        private long _unidCiudad;
        private long _unidPais;
        private string _codigoPostal;
        private string _RFC;
        private ProveedorDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidProveedor
        {
            get
            {
                return _unidProveedor;
            }
            set
            {
                if (_unidProveedor != value)
                {
                    _unidProveedor = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidProveedor"));
                    }
                }
            }
        }
        
        public string ProveedorName
        {
            get
            {
                return _proveedorName;
            }
            set
            {
                if (_proveedorName != value)
                {
                    _proveedorName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProveedorName"));
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
        
        public string Tel1
        {
            get
            {
                return _tel1;
            }
            set
            {
                if (_tel1 != value)
                {
                    _tel1 = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Tel1"));
                    }
                }
            }
        }
        
        public string Tel2
        {
            get
            {
                return _tel2;
            }
            set
            {
                if (_tel2 != value)
                {
                    _tel2 = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Tel2"));
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
        
        public string Calle
        {
            get
            {
                return _calle;
            }
            set
            {
                if (_calle != value)
                {
                    _calle = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Calle"));
                    }
                }
            }
        }

        public string CodigoPostal
        {
            get
            {
                return _codigoPostal;
            }
            set
            {
                if (_codigoPostal != value)
                {
                    _codigoPostal = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("CodigoPostal"));
                    }
                }
            }
        }

        public string RFC
        {
            get
            {
                return _RFC;
            }
            set
            {
                if (_RFC != value)
                {
                    _RFC = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("RFC"));
                    }
                }
            }
        }

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

        public void saveProveedor()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new PROVEEDOR() { CALLE = this._calle, CODIGO_POSTAL = this._codigoPostal, CONTACTO = this._contacto, MAIL = this._mail, PROVEEDOR_NAME = this._proveedorName, RFC = this._RFC, TEL1 = this._tel1, TEL2 = this._tel2, UNID_CIUDAD = this._unidCiudad, UNID_PAIS = this._unidPais });
            }
        }

        public void updateProveedor()
        {
            this._dataMapper.udpateElement(new PROVEEDOR() { UNID_PROVEEDOR = this._unidProveedor, CALLE = this._calle, CODIGO_POSTAL = this._codigoPostal, CONTACTO = this._contacto, MAIL = this._mail, PROVEEDOR_NAME = this._proveedorName, RFC = this._RFC, TEL1 = this._tel1, TEL2 = this._tel2, UNID_CIUDAD = this._unidCiudad, UNID_PAIS = this._unidPais });
        }

        #region Constructors
        public ProveedorModel(IDataMapper dataMapper)
        {
            if ((dataMapper as ProveedorDataMapper) != null)
            {
                this._dataMapper = dataMapper as ProveedorDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
