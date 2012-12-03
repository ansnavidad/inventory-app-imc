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
    public partial class FACTURA
    {
        #region Primitive Properties
    
        public virtual long UNID_FACTURA
        {
            get;
            set;
        }
    
        public virtual Nullable<long> UNID_LOTE
        {
            get { return _uNID_LOTE; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_LOTE != value)
                    {
                        if (LOTE != null && LOTE.UNID_LOTE != value)
                        {
                            LOTE = null;
                        }
                        _uNID_LOTE = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_LOTE;
    
        public virtual string FACTURA_NUMERO
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> FECHA_FACTURA
        {
            get;
            set;
        }
    
        public virtual Nullable<long> UNID_PROVEEDOR
        {
            get { return _uNID_PROVEEDOR; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_PROVEEDOR != value)
                    {
                        if (PROVEEDOR != null && PROVEEDOR.UNID_PROVEEDOR != value)
                        {
                            PROVEEDOR = null;
                        }
                        _uNID_PROVEEDOR = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_PROVEEDOR;
    
        public virtual Nullable<long> UNID_MONEDA
        {
            get { return _uNID_MONEDA; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_MONEDA != value)
                    {
                        if (MONEDA != null && MONEDA.UNID_MONEDA != value)
                        {
                            MONEDA = null;
                        }
                        _uNID_MONEDA = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_MONEDA;
    
        public virtual bool IS_ACTIVE
        {
            get;
            set;
        }
    
        public virtual string NUMERO_PEDIMENTO
        {
            get;
            set;
        }
    
        public virtual Nullable<double> IVA_POR
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual MONEDA MONEDA
        {
            get { return _mONEDA; }
            set
            {
                if (!ReferenceEquals(_mONEDA, value))
                {
                    var previousValue = _mONEDA;
                    _mONEDA = value;
                    FixupMONEDA(previousValue);
                }
            }
        }
        private MONEDA _mONEDA;
    
        public virtual PROVEEDOR PROVEEDOR
        {
            get { return _pROVEEDOR; }
            set
            {
                if (!ReferenceEquals(_pROVEEDOR, value))
                {
                    var previousValue = _pROVEEDOR;
                    _pROVEEDOR = value;
                    FixupPROVEEDOR(previousValue);
                }
            }
        }
        private PROVEEDOR _pROVEEDOR;
    
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
    
        public virtual ICollection<FACTURA_DETALLE> FACTURA_DETALLE
        {
            get
            {
                if (_fACTURA_DETALLE == null)
                {
                    var newCollection = new FixupCollection<FACTURA_DETALLE>();
                    newCollection.CollectionChanged += FixupFACTURA_DETALLE;
                    _fACTURA_DETALLE = newCollection;
                }
                return _fACTURA_DETALLE;
            }
            set
            {
                if (!ReferenceEquals(_fACTURA_DETALLE, value))
                {
                    var previousValue = _fACTURA_DETALLE as FixupCollection<FACTURA_DETALLE>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupFACTURA_DETALLE;
                    }
                    _fACTURA_DETALLE = value;
                    var newValue = value as FixupCollection<FACTURA_DETALLE>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupFACTURA_DETALLE;
                    }
                }
            }
        }
        private ICollection<FACTURA_DETALLE> _fACTURA_DETALLE;
    
        public virtual LOTE LOTE
        {
            get { return _lOTE; }
            set
            {
                if (!ReferenceEquals(_lOTE, value))
                {
                    var previousValue = _lOTE;
                    _lOTE = value;
                    FixupLOTE(previousValue);
                }
            }
        }
        private LOTE _lOTE;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupMONEDA(MONEDA previousValue)
        {
            if (previousValue != null && previousValue.FACTURAs.Contains(this))
            {
                previousValue.FACTURAs.Remove(this);
            }
    
            if (MONEDA != null)
            {
                if (!MONEDA.FACTURAs.Contains(this))
                {
                    MONEDA.FACTURAs.Add(this);
                }
                if (UNID_MONEDA != MONEDA.UNID_MONEDA)
                {
                    UNID_MONEDA = MONEDA.UNID_MONEDA;
                }
            }
            else if (!_settingFK)
            {
                UNID_MONEDA = null;
            }
        }
    
        private void FixupPROVEEDOR(PROVEEDOR previousValue)
        {
            if (previousValue != null && previousValue.FACTURAs.Contains(this))
            {
                previousValue.FACTURAs.Remove(this);
            }
    
            if (PROVEEDOR != null)
            {
                if (!PROVEEDOR.FACTURAs.Contains(this))
                {
                    PROVEEDOR.FACTURAs.Add(this);
                }
                if (UNID_PROVEEDOR != PROVEEDOR.UNID_PROVEEDOR)
                {
                    UNID_PROVEEDOR = PROVEEDOR.UNID_PROVEEDOR;
                }
            }
            else if (!_settingFK)
            {
                UNID_PROVEEDOR = null;
            }
        }
    
        private void FixupLOTE(LOTE previousValue)
        {
            if (previousValue != null && previousValue.FACTURAs.Contains(this))
            {
                previousValue.FACTURAs.Remove(this);
            }
    
            if (LOTE != null)
            {
                if (!LOTE.FACTURAs.Contains(this))
                {
                    LOTE.FACTURAs.Add(this);
                }
                if (UNID_LOTE != LOTE.UNID_LOTE)
                {
                    UNID_LOTE = LOTE.UNID_LOTE;
                }
            }
            else if (!_settingFK)
            {
                UNID_LOTE = null;
            }
        }
    
        private void FixupRECIBO_MOVIMIENTO(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (RECIBO_MOVIMIENTO item in e.NewItems)
                {
                    item.FACTURA = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (RECIBO_MOVIMIENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.FACTURA, this))
                    {
                        item.FACTURA = null;
                    }
                }
            }
        }
    
        private void FixupFACTURA_DETALLE(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (FACTURA_DETALLE item in e.NewItems)
                {
                    item.FACTURA = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (FACTURA_DETALLE item in e.OldItems)
                {
                    if (ReferenceEquals(item.FACTURA, this))
                    {
                        item.FACTURA = null;
                    }
                }
            }
        }

        #endregion
    }
}
