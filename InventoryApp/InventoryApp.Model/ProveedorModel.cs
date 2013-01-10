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
        private CIUDAD _ciudad;
        private PAI _pais;
        private string _codigoPostal;
        private string _RFC;
        private CATEGORIA _categoria;
        public List<long> _unidsCategorias;
        public List<long> _auxUnidsCategorias;
        public List<long> _unidsCuenta;
        public List<long> _auxUnidsCuenta;
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

        public CIUDAD Ciudad
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

        public PAI Pais
        {
            get
            {
                return _pais;
            }
            set
            {
                if (_pais != value)
                {
                    _pais = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Pais"));
                    }
                }
            }
        }

        public CATEGORIA Categoria
        {
            get
            {
                return _categoria;
            }
            set
            {
                if (_categoria != value)
                {
                    _categoria = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Categoria"));
                    }
                }
            }
        }
        
        #endregion

        public void saveProveedor()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertRelacion(new PROVEEDOR()
                {
                    UNID_PROVEEDOR = this.UnidProveedor,
                    IS_ACTIVE = true,
                    CALLE = this._calle,
                    CODIGO_POSTAL = this._codigoPostal,
                    CONTACTO = this._contacto,
                    MAIL = this._mail,
                    PROVEEDOR_NAME = this._proveedorName,
                    RFC = this._RFC,
                    TEL1 = this._tel1,
                    TEL2 = this._tel2,
                    UNID_CIUDAD = this._ciudad.UNID_CIUDAD,
                    UNID_PAIS = this._pais.UNID_PAIS
                }, this._unidsCategorias);
            }
        }

        public void updateProveedor(FixupCollection<DeleteProveedorCuenta> coll)
        {
            List<PROVEEDOR_CUENTA> listF = new List<PROVEEDOR_CUENTA>();
            foreach (DeleteProveedorCuenta dd in coll) {

                listF.Add(new PROVEEDOR_CUENTA { BENEFICIARIO = dd.BENEFICIARIO, CLABE = dd.CLABE, IS_ACTIVE = dd.IS_ACTIVE, IS_MODIFIED = dd.IS_MODIFIED, LAST_MODIFIED_DATE = dd.LAST_MODIFIED_DATE, NUMERO_CUENTA = dd.NUMERO_CUENTA, UNID_BANCO = dd.UNID_BANCO, UNID_PROVEEDOR_CUENTA = dd.UNID_PROVEEDOR_CUENTA, UNID_PROVEEDOR = dd.UNID_PROVEEDOR }); 
            }

            this._dataMapper.updateRelacion(new PROVEEDOR()
            {
                UNID_PROVEEDOR = this._unidProveedor,
                CALLE = this._calle,
                CODIGO_POSTAL = this._codigoPostal,
                CONTACTO = this._contacto,
                MAIL = this._mail,
                PROVEEDOR_NAME = this._proveedorName,
                RFC = this._RFC,
                TEL1 = this._tel1,
                TEL2 = this._tel2,
                UNID_CIUDAD = this._ciudad.UNID_CIUDAD,
                UNID_PAIS = this._pais.UNID_PAIS
            },this._unidsCategorias,
            this._auxUnidsCategorias,
            this._unidsCuenta,
            this._auxUnidsCuenta,
            listF
            );
        }

        public object GetProveedorCategoria(long obj)
        {
            return this._dataMapper.getElementProveedorCategoria(obj);   
        }

        public object GetProveedorCuenta(long obj)
        {
            return this._dataMapper.getElementProveedorCuenta(obj);
        }

        #region Constructors
        public ProveedorModel(IDataMapper dataMapper)
        {
            if ((dataMapper as ProveedorDataMapper) != null)
            {
                this._dataMapper = dataMapper as ProveedorDataMapper;
            }
            //varibles que guardan los ids de categorias
            this._unidsCategorias = new List<long>();
            this._auxUnidsCategorias = new List<long>();
            this._auxUnidsCuenta = new List<long>();
            this._unidsCuenta = new List<long>();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
