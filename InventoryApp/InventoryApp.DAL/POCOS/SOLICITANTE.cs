//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace InventoryApp.DAL.POCOS
{
    public partial class SOLICITANTE
    {
        #region Primitive Properties
    
        public virtual long UNID_SOLICITANTE
        {
            get;
            set;
        }
    
        public virtual string SOLICITANTE_NAME
        {
            get;
            set;
        }
    
        public virtual long UNID_EMPRESA
        {
            get { return _uNID_EMPRESA; }
            set
            {
                if (_uNID_EMPRESA != value)
                {
                    if (EMPRESA != null && EMPRESA.UNID_EMPRESA != value)
                    {
                        EMPRESA = null;
                    }
                    _uNID_EMPRESA = value;
                }
            }
        }
        private long _uNID_EMPRESA;
    
        public virtual long UNID_DEPARTAMENTO
        {
            get { return _uNID_DEPARTAMENTO; }
            set
            {
                if (_uNID_DEPARTAMENTO != value)
                {
                    if (DEPARTAMENTO != null && DEPARTAMENTO.UNID_DEPARTAMENTO != value)
                    {
                        DEPARTAMENTO = null;
                    }
                    _uNID_DEPARTAMENTO = value;
                }
            }
        }
        private long _uNID_DEPARTAMENTO;
    
        public virtual string EMAIL
        {
            get;
            set;
        }
    
        public virtual string VALIDADOR
        {
            get;
            set;
        }
    
        public virtual bool IS_ACTIVE
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual DEPARTAMENTO DEPARTAMENTO
        {
            get { return _dEPARTAMENTO; }
            set
            {
                if (!ReferenceEquals(_dEPARTAMENTO, value))
                {
                    var previousValue = _dEPARTAMENTO;
                    _dEPARTAMENTO = value;
                    FixupDEPARTAMENTO(previousValue);
                }
            }
        }
        private DEPARTAMENTO _dEPARTAMENTO;
    
        public virtual EMPRESA EMPRESA
        {
            get { return _eMPRESA; }
            set
            {
                if (!ReferenceEquals(_eMPRESA, value))
                {
                    var previousValue = _eMPRESA;
                    _eMPRESA = value;
                    FixupEMPRESA(previousValue);
                }
            }
        }
        private EMPRESA _eMPRESA;

        #endregion
        #region Association Fixup
    
        private void FixupDEPARTAMENTO(DEPARTAMENTO previousValue)
        {
            if (previousValue != null && previousValue.SOLICITANTEs.Contains(this))
            {
                previousValue.SOLICITANTEs.Remove(this);
            }
    
            if (DEPARTAMENTO != null)
            {
                if (!DEPARTAMENTO.SOLICITANTEs.Contains(this))
                {
                    DEPARTAMENTO.SOLICITANTEs.Add(this);
                }
                if (UNID_DEPARTAMENTO != DEPARTAMENTO.UNID_DEPARTAMENTO)
                {
                    UNID_DEPARTAMENTO = DEPARTAMENTO.UNID_DEPARTAMENTO;
                }
            }
        }
    
        private void FixupEMPRESA(EMPRESA previousValue)
        {
            if (previousValue != null && previousValue.SOLICITANTEs.Contains(this))
            {
                previousValue.SOLICITANTEs.Remove(this);
            }
    
            if (EMPRESA != null)
            {
                if (!EMPRESA.SOLICITANTEs.Contains(this))
                {
                    EMPRESA.SOLICITANTEs.Add(this);
                }
                if (UNID_EMPRESA != EMPRESA.UNID_EMPRESA)
                {
                    UNID_EMPRESA = EMPRESA.UNID_EMPRESA;
                }
            }
        }

        #endregion
    }
}
