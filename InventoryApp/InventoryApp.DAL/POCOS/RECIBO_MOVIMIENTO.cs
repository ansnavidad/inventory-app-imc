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
    public partial class RECIBO_MOVIMIENTO
    {
        #region Primitive Properties
    
        public virtual long UNID_RECIBO
        {
            get { return _uNID_RECIBO; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_RECIBO != value)
                    {
                        if (RECIBO != null && RECIBO.UNID_RECIBO != value)
                        {
                            RECIBO = null;
                        }
                        _uNID_RECIBO = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private long _uNID_RECIBO;
    
        public virtual long UNID_RECIBO_MOVIMIENTO
        {
            get;
            set;
        }
    
        public virtual Nullable<long> UNID_MOVIMIENTO
        {
            get { return _uNID_MOVIMIENTO; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_MOVIMIENTO != value)
                    {
                        if (MOVIMENTO != null && MOVIMENTO.UNID_MOVIMIENTO != value)
                        {
                            MOVIMENTO = null;
                        }
                        _uNID_MOVIMIENTO = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_MOVIMIENTO;
    
        public virtual Nullable<long> UNID_FACTURA
        {
            get { return _uNID_FACTURA; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_FACTURA != value)
                    {
                        if (FACTURA != null && FACTURA.UNID_FACTURA != value)
                        {
                            FACTURA = null;
                        }
                        _uNID_FACTURA = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_FACTURA;
    
        public virtual bool IS_MODIFIED
        {
            get;
            set;
        }
    
        public virtual long LAST_MODIFIED_DATE
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> IS_ACTIVE
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual MOVIMENTO MOVIMENTO
        {
            get { return _mOVIMENTO; }
            set
            {
                if (!ReferenceEquals(_mOVIMENTO, value))
                {
                    var previousValue = _mOVIMENTO;
                    _mOVIMENTO = value;
                    FixupMOVIMENTO(previousValue);
                }
            }
        }
        private MOVIMENTO _mOVIMENTO;
    
        public virtual RECIBO RECIBO
        {
            get { return _rECIBO; }
            set
            {
                if (!ReferenceEquals(_rECIBO, value))
                {
                    var previousValue = _rECIBO;
                    _rECIBO = value;
                    FixupRECIBO(previousValue);
                }
            }
        }
        private RECIBO _rECIBO;
    
        public virtual FACTURA FACTURA
        {
            get { return _fACTURA; }
            set
            {
                if (!ReferenceEquals(_fACTURA, value))
                {
                    var previousValue = _fACTURA;
                    _fACTURA = value;
                    FixupFACTURA(previousValue);
                }
            }
        }
        private FACTURA _fACTURA;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupMOVIMENTO(MOVIMENTO previousValue)
        {
            if (previousValue != null && previousValue.RECIBO_MOVIMIENTO.Contains(this))
            {
                previousValue.RECIBO_MOVIMIENTO.Remove(this);
            }
    
            if (MOVIMENTO != null)
            {
                if (!MOVIMENTO.RECIBO_MOVIMIENTO.Contains(this))
                {
                    MOVIMENTO.RECIBO_MOVIMIENTO.Add(this);
                }
                if (UNID_MOVIMIENTO != MOVIMENTO.UNID_MOVIMIENTO)
                {
                    UNID_MOVIMIENTO = MOVIMENTO.UNID_MOVIMIENTO;
                }
            }
            else if (!_settingFK)
            {
                UNID_MOVIMIENTO = null;
            }
        }
    
        private void FixupRECIBO(RECIBO previousValue)
        {
            if (previousValue != null && previousValue.RECIBO_MOVIMIENTO.Contains(this))
            {
                previousValue.RECIBO_MOVIMIENTO.Remove(this);
            }
    
            if (RECIBO != null)
            {
                if (!RECIBO.RECIBO_MOVIMIENTO.Contains(this))
                {
                    RECIBO.RECIBO_MOVIMIENTO.Add(this);
                }
                if (UNID_RECIBO != RECIBO.UNID_RECIBO)
                {
                    UNID_RECIBO = RECIBO.UNID_RECIBO;
                }
            }
        }
    
        private void FixupFACTURA(FACTURA previousValue)
        {
            if (previousValue != null && previousValue.RECIBO_MOVIMIENTO.Contains(this))
            {
                previousValue.RECIBO_MOVIMIENTO.Remove(this);
            }
    
            if (FACTURA != null)
            {
                if (!FACTURA.RECIBO_MOVIMIENTO.Contains(this))
                {
                    FACTURA.RECIBO_MOVIMIENTO.Add(this);
                }
                if (UNID_FACTURA != FACTURA.UNID_FACTURA)
                {
                    UNID_FACTURA = FACTURA.UNID_FACTURA;
                }
            }
            else if (!_settingFK)
            {
                UNID_FACTURA = null;
            }
        }

        #endregion
    }
}
