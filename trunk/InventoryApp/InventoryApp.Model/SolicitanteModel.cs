using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class SolicitanteModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidSolicitante;
        //private long _unidDepartamento;
        private EMPRESA _empresa;
        private DEPARTAMENTO _departamento;
        //private long _unidEmpresa;
        private string _solicitanteName;
        private string _email;
        private string _validador;
        private bool _valideEmpresa;
        private string _error;
        private SolicitanteDataMapper _dataMapper;
        #endregion

        #region Props
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {

                if (_error != value)
                {
                    _error = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Error"));
                    }
                }

            }
        }

        public bool ValideEmpresa
        {
            get
            {
                return _valideEmpresa;
            }
            set
            {
                if (_valideEmpresa != value)
                {
                    _valideEmpresa = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ValideEmpresa"));
                    }
                }
            }
        }

        public long UnidSolicitante
        {
            get
            {
                return _unidSolicitante;
            }
            set
            {
                if (_unidSolicitante != value)
                {
                    _unidSolicitante = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidSolicitante"));
                    }
                }
            }
        }

        public EMPRESA Empresa
        {
            get
            {
                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Empresa"));
                    }
                }
            }
        }

        public DEPARTAMENTO Departamento
        {
            get
            {
                return _departamento;
            }
            set
            {
                if (_departamento != value)
                {
                    _departamento = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Departamento"));
                    }
                }
            }
        }

        public string SolicitanteName
        {
            get
            {
                return _solicitanteName;
            }
            set
            {
                
                if (_solicitanteName != value)
                {
                    _solicitanteName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("SolicitanteName"));
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

        public string Validador
        {
            get
            {
                return _validador;
            }
            set
            {
                if (_validador != value)
                {
                    _validador = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Validador"));
                    }
                }
            }
        }
        #endregion

        public void saveSolicitante()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new SOLICITANTE() {IS_ACTIVE=true,
                                                             SOLICITANTE_NAME= this._solicitanteName,
                                                             UNID_DEPARTAMENTO=this._departamento.UNID_DEPARTAMENTO,
                                                             UNID_EMPRESA=this._empresa.UNID_EMPRESA,
                                                             EMAIL=this._email,
                                                             VALIDADOR=this._validador});
            }
        }

        public void updateSolicitante()
        {
            this._dataMapper.udpateElement(new SOLICITANTE()
            {
                UNID_SOLICITANTE= this._unidSolicitante,
                SOLICITANTE_NAME = this._solicitanteName,
                UNID_DEPARTAMENTO = this._departamento.UNID_DEPARTAMENTO,
                UNID_EMPRESA = this._empresa.UNID_EMPRESA,
                EMAIL = this._email,
                VALIDADOR = this._validador
            });
        }

        #region Constructors
        public SolicitanteModel(IDataMapper dataMapper)
        {
            if ((dataMapper as SolicitanteDataMapper) != null)
            {
                this._dataMapper = dataMapper as SolicitanteDataMapper;
            }
            this.ValideEmpresa = false;
        }
        #endregion

        #region metodos

        public bool ValidateBusiness()
        {
            bool validar = false;
            SOLICITANTE solicitante = new SOLICITANTE();
            SolicitanteDataMapper dataMapper = new SolicitanteDataMapper();
            solicitante.SOLICITANTE_NAME = this._solicitanteName;
            solicitante.UNID_EMPRESA = this._empresa.UNID_EMPRESA;
            validar = dataMapper.ValideSolicitanteName(solicitante);
            if (validar)
                validar = true;
            else
                validar = false;
            return validar;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
