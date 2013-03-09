using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class EmpresaModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidEmpresa;
        private string _empresaName;
        private string _razonSocial;
        private string _rfc;
        private string _direccion;
        private EmpresaDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props

        public long UnidEmpresa
        {
            get
            {
                return _unidEmpresa;
            }
            set
            {
                if (_unidEmpresa != value)
                {
                    _unidEmpresa = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidEmpresa"));
                    }
                }
            }
        }

        public string EmpresaName
        {
            get
            {
                return _empresaName;
            }
            set
            {
                if (_empresaName != value)
                {
                    _empresaName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("EmpresaName"));
                    }
                }
            }
        }

        public string RazonSocial
        {
            get
            {
                return _razonSocial;
            }
            set
            {
                if (_razonSocial != value)
                {
                    _razonSocial = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("RazonSocial"));
                    }
                }
            }
        }

        public string Rfc
        {
            get
            {
                return _rfc;
            }
            set
            {
                if (_rfc != value)
                {
                    _rfc = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Rfc"));
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

        public void saveEmpresa()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new EMPRESA() { IS_ACTIVE = true, EMPRESA_NAME = this._empresaName, DIRECCION = this._direccion, RAZON_SOCIAL = this._razonSocial, RFC = this._rfc }, this.ActualUser);
            }
        }

        public void updateEmpresa()
        {
            this._dataMapper.udpateElement(new EMPRESA() { UNID_EMPRESA = this._unidEmpresa, EMPRESA_NAME = this._empresaName, DIRECCION = this._direccion, RAZON_SOCIAL = this._razonSocial, RFC = this._rfc }, this.ActualUser);
        }

        #region Constructors
        public EmpresaModel(IDataMapper dataMapper, USUARIO u)
        {
            if ((dataMapper as EmpresaDataMapper) != null)
            {
                this._dataMapper = dataMapper as EmpresaDataMapper;
            }
            this.ActualUser = u;
        }

        public EmpresaModel(IDataMapper dataMapper)
        {
            if ((dataMapper as EmpresaDataMapper) != null)
            {
                this._dataMapper = dataMapper as EmpresaDataMapper;
            }            
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
