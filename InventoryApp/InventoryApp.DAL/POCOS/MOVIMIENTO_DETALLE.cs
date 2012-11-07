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
    public partial class MOVIMIENTO_DETALLE
    {
        #region Primitive Properties
    
        public virtual long UNID_MOVIMIENTO_DETALLE
        {
            get;
            set;
        }
    
        public virtual long UNID_ITEM
        {
            get { return _uNID_ITEM; }
            set
            {
                if (_uNID_ITEM != value)
                {
                    if (ITEM != null && ITEM.UNID_ITEM != value)
                    {
                        ITEM = null;
                    }
                    _uNID_ITEM = value;
                }
            }
        }
        private long _uNID_ITEM;
    
        public virtual long UNID_MOVIMIENTO
        {
            get { return _uNID_MOVIMIENTO; }
            set
            {
                if (_uNID_MOVIMIENTO != value)
                {
                    if (MOVIMENTO != null && MOVIMENTO.UNID_MOVIMIENTO != value)
                    {
                        MOVIMENTO = null;
                    }
                    _uNID_MOVIMIENTO = value;
                }
            }
        }
        private long _uNID_MOVIMIENTO;

        #endregion
        #region Navigation Properties
    
        public virtual ITEM ITEM
        {
            get { return _iTEM; }
            set
            {
                if (!ReferenceEquals(_iTEM, value))
                {
                    var previousValue = _iTEM;
                    _iTEM = value;
                    FixupITEM(previousValue);
                }
            }
        }
        private ITEM _iTEM;
    
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

        #endregion
        #region Association Fixup
    
        private void FixupITEM(ITEM previousValue)
        {
            if (previousValue != null && previousValue.MOVIMIENTO_DETALLE.Contains(this))
            {
                previousValue.MOVIMIENTO_DETALLE.Remove(this);
            }
    
            if (ITEM != null)
            {
                if (!ITEM.MOVIMIENTO_DETALLE.Contains(this))
                {
                    ITEM.MOVIMIENTO_DETALLE.Add(this);
                }
                if (UNID_ITEM != ITEM.UNID_ITEM)
                {
                    UNID_ITEM = ITEM.UNID_ITEM;
                }
            }
        }
    
        private void FixupMOVIMENTO(MOVIMENTO previousValue)
        {
            if (previousValue != null && previousValue.MOVIMIENTO_DETALLE.Contains(this))
            {
                previousValue.MOVIMIENTO_DETALLE.Remove(this);
            }
    
            if (MOVIMENTO != null)
            {
                if (!MOVIMENTO.MOVIMIENTO_DETALLE.Contains(this))
                {
                    MOVIMENTO.MOVIMIENTO_DETALLE.Add(this);
                }
                if (UNID_MOVIMIENTO != MOVIMENTO.UNID_MOVIMIENTO)
                {
                    UNID_MOVIMIENTO = MOVIMENTO.UNID_MOVIMIENTO;
                }
            }
        }

        #endregion
    }
}
