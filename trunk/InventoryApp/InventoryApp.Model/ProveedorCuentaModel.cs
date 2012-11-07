using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class ProveedorCuentaModel
    {
        #region Fields

        private long _unidProveedorCuenta;
        private string _numeroCuenta;
        private string _clabe;
        private string _beneficiario;
        private BANCO _banco;
        private PROVEEDOR _proveedor;
        private ProveedorDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidProveedorCuenta
        {
            get
            {
                return _unidProveedorCuenta;
            }
            set
            {
                if (_unidProveedorCuenta != value)
                {
                    _unidProveedorCuenta = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidProveedorCuenta"));
                    }
                }
            }
        }
        
        public string NumeroCuenta
        {
            get
            {
                return _numeroCuenta;
            }
            set
            {
                if (_numeroCuenta != value)
                {
                    _numeroCuenta = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("NumeroCuenta"));
                    }
                }
            }
        }
        
        public string Clabe
        {
            get
            {
                return _clabe;
            }
            set
            {
                if (_clabe != value)
                {
                    _clabe = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Clabe"));
                    }
                }
            }
        }
        
        public string Beneficiario
        {
            get
            {
                return _beneficiario;
            }
            set
            {
                if (_beneficiario != value)
                {
                    _beneficiario = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Beneficiario"));
                    }
                }
            }
        }
                
        public BANCO Banco
        {
            get
            {
                return _banco;
            }
            set
            {
                if (_banco != value)
                {
                    _banco = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Banco"));
                    }
                }
            }
        }

        public PROVEEDOR Proveedor
        {
            get
            {
                return _proveedor;
            }
            set
            {
                if (_proveedor != value)
                {
                    _proveedor = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Proveedor"));
                    }
                }
            }
        }
        
        #endregion

        public void saveProveedorCuenta()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new PROVEEDOR_CUENTA() { UNID_PROVEEDOR = this._proveedor.UNID_PROVEEDOR, UNID_BANCO = this._banco.UNID_BANCO, NUMERO_CUENTA = this._numeroCuenta, CLABE = this._clabe, BENEFICIARIO = this._beneficiario });
            }
        }

        public void updateProveedorCuenta()
        {
            this._dataMapper.udpateElement(new PROVEEDOR_CUENTA() { UNID_PROVEEDOR_CUENTA = this._unidProveedorCuenta, UNID_PROVEEDOR = this._proveedor.UNID_PROVEEDOR, UNID_BANCO = this._banco.UNID_BANCO, NUMERO_CUENTA = this._numeroCuenta, CLABE = this._clabe, BENEFICIARIO = this._beneficiario });
        }

        #region Constructors
        public ProveedorCuentaModel(IDataMapper dataMapper)
        {
            if ((dataMapper as ProveedorCuentaDataMapper) != null)
            {
                this._dataMapper = dataMapper as ProveedorCuentaDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
