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
    public partial class RECIBO
    {
        #region Primitive Properties
    
        public virtual long UNID_RECIBO
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> FECHA_CREACION
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> FECHA_CIERRE
        {
            get;
            set;
        }
    
        public virtual Nullable<long> UNID_SOLICITANTE
        {
            get { return _uNID_SOLICITANTE; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_SOLICITANTE != value)
                    {
                        if (SOLICITANTE != null && SOLICITANTE.UNID_SOLICITANTE != value)
                        {
                            SOLICITANTE = null;
                        }
                        _uNID_SOLICITANTE = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_SOLICITANTE;
    
        public virtual string PO
        {
            get;
            set;
        }
    
        public virtual string TT
        {
            get;
            set;
        }
    
        public virtual string PEDIMIENTO_IMPO
        {
            get;
            set;
        }
    
        public virtual string PEDIMENTO_EXPO
        {
            get;
            set;
        }
    
        public virtual Nullable<long> UNID_RECIBO_STATUS
        {
            get { return _uNID_RECIBO_STATUS; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_RECIBO_STATUS != value)
                    {
                        if (RECIBO_STATUS != null && RECIBO_STATUS.UNID_RECIBO_STATUS != value)
                        {
                            RECIBO_STATUS = null;
                        }
                        _uNID_RECIBO_STATUS = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_RECIBO_STATUS;
    
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
    
        public virtual bool IS_ACTIVE
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<MASTER_INVENTARIOS> MASTER_INVENTARIOS
        {
            get
            {
                if (_mASTER_INVENTARIOS == null)
                {
                    var newCollection = new FixupCollection<MASTER_INVENTARIOS>();
                    newCollection.CollectionChanged += FixupMASTER_INVENTARIOS;
                    _mASTER_INVENTARIOS = newCollection;
                }
                return _mASTER_INVENTARIOS;
            }
            set
            {
                if (!ReferenceEquals(_mASTER_INVENTARIOS, value))
                {
                    var previousValue = _mASTER_INVENTARIOS as FixupCollection<MASTER_INVENTARIOS>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMASTER_INVENTARIOS;
                    }
                    _mASTER_INVENTARIOS = value;
                    var newValue = value as FixupCollection<MASTER_INVENTARIOS>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMASTER_INVENTARIOS;
                    }
                }
            }
        }
        private ICollection<MASTER_INVENTARIOS> _mASTER_INVENTARIOS;
    
        public virtual SOLICITANTE SOLICITANTE
        {
            get { return _sOLICITANTE; }
            set
            {
                if (!ReferenceEquals(_sOLICITANTE, value))
                {
                    var previousValue = _sOLICITANTE;
                    _sOLICITANTE = value;
                    FixupSOLICITANTE(previousValue);
                }
            }
        }
        private SOLICITANTE _sOLICITANTE;
    
        public virtual ICollection<RECIBO_MOVIMIENTO> RECIBO_MOVIMIENTO
        {
            get
            {
                if (_rECIBO_MOVIMIENTO == null)
                {
                    var newCollection = new FixupCollection<RECIBO_MOVIMIENTO>();
                    newCollection.CollectionChanged += FixupRECIBO_MOVIMIENTO;
                    _rECIBO_MOVIMIENTO = newCollection;
                }
                return _rECIBO_MOVIMIENTO;
            }
            set
            {
                if (!ReferenceEquals(_rECIBO_MOVIMIENTO, value))
                {
                    var previousValue = _rECIBO_MOVIMIENTO as FixupCollection<RECIBO_MOVIMIENTO>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupRECIBO_MOVIMIENTO;
                    }
                    _rECIBO_MOVIMIENTO = value;
                    var newValue = value as FixupCollection<RECIBO_MOVIMIENTO>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupRECIBO_MOVIMIENTO;
                    }
                }
            }
        }
        private ICollection<RECIBO_MOVIMIENTO> _rECIBO_MOVIMIENTO;
    
        public virtual RECIBO_STATUS RECIBO_STATUS
        {
            get { return _rECIBO_STATUS; }
            set
            {
                if (!ReferenceEquals(_rECIBO_STATUS, value))
                {
                    var previousValue = _rECIBO_STATUS;
                    _rECIBO_STATUS = value;
                    FixupRECIBO_STATUS(previousValue);
                }
            }
        }
        private RECIBO_STATUS _rECIBO_STATUS;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupSOLICITANTE(SOLICITANTE previousValue)
        {
            if (previousValue != null && previousValue.RECIBOes.Contains(this))
            {
                previousValue.RECIBOes.Remove(this);
            }
    
            if (SOLICITANTE != null)
            {
                if (!SOLICITANTE.RECIBOes.Contains(this))
                {
                    SOLICITANTE.RECIBOes.Add(this);
                }
                if (UNID_SOLICITANTE != SOLICITANTE.UNID_SOLICITANTE)
                {
                    UNID_SOLICITANTE = SOLICITANTE.UNID_SOLICITANTE;
                }
            }
            else if (!_settingFK)
            {
                UNID_SOLICITANTE = null;
            }
        }
    
        private void FixupRECIBO_STATUS(RECIBO_STATUS previousValue)
        {
            if (previousValue != null && previousValue.RECIBOes.Contains(this))
            {
                previousValue.RECIBOes.Remove(this);
            }
    
            if (RECIBO_STATUS != null)
            {
                if (!RECIBO_STATUS.RECIBOes.Contains(this))
                {
                    RECIBO_STATUS.RECIBOes.Add(this);
                }
                if (UNID_RECIBO_STATUS != RECIBO_STATUS.UNID_RECIBO_STATUS)
                {
                    UNID_RECIBO_STATUS = RECIBO_STATUS.UNID_RECIBO_STATUS;
                }
            }
            else if (!_settingFK)
            {
                UNID_RECIBO_STATUS = null;
            }
        }
    
        private void FixupMASTER_INVENTARIOS(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MASTER_INVENTARIOS item in e.NewItems)
                {
                    item.RECIBO = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MASTER_INVENTARIOS item in e.OldItems)
                {
                    if (ReferenceEquals(item.RECIBO, this))
                    {
                        item.RECIBO = null;
                    }
                }
            }
        }
    
        private void FixupRECIBO_MOVIMIENTO(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (RECIBO_MOVIMIENTO item in e.NewItems)
                {
                    item.RECIBO = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (RECIBO_MOVIMIENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.RECIBO, this))
                    {
                        item.RECIBO = null;
                    }
                }
            }
        }

        #endregion
    }
}
