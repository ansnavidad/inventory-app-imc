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
    public partial class MAX_MIN
    {
        #region Primitive Properties
    
        public virtual long UNID_MAX_MIN
        {
            get { return _uNID_MAX_MIN; }
            set
            {
                if (_uNID_MAX_MIN != value)
                {
                    if (MAX_MIN2 != null && MAX_MIN2.UNID_MAX_MIN != value)
                    {
                        MAX_MIN2 = null;
                    }
                    _uNID_MAX_MIN = value;
                }
            }
        }
        private long _uNID_MAX_MIN;
    
        public virtual Nullable<int> MAX
        {
            get;
            set;
        }
    
        public virtual Nullable<int> MIN
        {
            get;
            set;
        }
    
        public virtual long UNID_ARTICULO
        {
            get { return _uNID_ARTICULO; }
            set
            {
                if (_uNID_ARTICULO != value)
                {
                    if (ARTICULO != null && ARTICULO.UNID_ARTICULO != value)
                    {
                        ARTICULO = null;
                    }
                    _uNID_ARTICULO = value;
                }
            }
        }
        private long _uNID_ARTICULO;
    
        public virtual long UNID_ALMACEN
        {
            get { return _uNID_ALMACEN; }
            set
            {
                if (_uNID_ALMACEN != value)
                {
                    if (ALMACEN != null && ALMACEN.UNID_ALMACEN != value)
                    {
                        ALMACEN = null;
                    }
                    _uNID_ALMACEN = value;
                }
            }
        }
        private long _uNID_ALMACEN;
    
        public virtual Nullable<bool> IS_ACTIVE
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> IS_MODIFIED
        {
            get;
            set;
        }
    
        public virtual Nullable<long> LAST_MODIFIED_DATE
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ARTICULO ARTICULO
        {
            get { return _aRTICULO; }
            set
            {
                if (!ReferenceEquals(_aRTICULO, value))
                {
                    var previousValue = _aRTICULO;
                    _aRTICULO = value;
                    FixupARTICULO(previousValue);
                }
            }
        }
        private ARTICULO _aRTICULO;
    
        public virtual ALMACEN ALMACEN
        {
            get { return _aLMACEN; }
            set
            {
                if (!ReferenceEquals(_aLMACEN, value))
                {
                    var previousValue = _aLMACEN;
                    _aLMACEN = value;
                    FixupALMACEN(previousValue);
                }
            }
        }
        private ALMACEN _aLMACEN;
    
        public virtual MAX_MIN MAX_MIN1
        {
            get { return _mAX_MIN1; }
            set
            {
                if (!ReferenceEquals(_mAX_MIN1, value))
                {
                    var previousValue = _mAX_MIN1;
                    _mAX_MIN1 = value;
                    FixupMAX_MIN1(previousValue);
                }
            }
        }
        private MAX_MIN _mAX_MIN1;
    
        public virtual MAX_MIN MAX_MIN2
        {
            get { return _mAX_MIN2; }
            set
            {
                if (!ReferenceEquals(_mAX_MIN2, value))
                {
                    var previousValue = _mAX_MIN2;
                    _mAX_MIN2 = value;
                    FixupMAX_MIN2(previousValue);
                }
            }
        }
        private MAX_MIN _mAX_MIN2;

        #endregion
        #region Association Fixup
    
        private void FixupARTICULO(ARTICULO previousValue)
        {
            if (previousValue != null && previousValue.MAX_MIN.Contains(this))
            {
                previousValue.MAX_MIN.Remove(this);
            }
    
            if (ARTICULO != null)
            {
                if (!ARTICULO.MAX_MIN.Contains(this))
                {
                    ARTICULO.MAX_MIN.Add(this);
                }
                if (UNID_ARTICULO != ARTICULO.UNID_ARTICULO)
                {
                    UNID_ARTICULO = ARTICULO.UNID_ARTICULO;
                }
            }
        }
    
        private void FixupALMACEN(ALMACEN previousValue)
        {
            if (previousValue != null && previousValue.MAX_MIN.Contains(this))
            {
                previousValue.MAX_MIN.Remove(this);
            }
    
            if (ALMACEN != null)
            {
                if (!ALMACEN.MAX_MIN.Contains(this))
                {
                    ALMACEN.MAX_MIN.Add(this);
                }
                if (UNID_ALMACEN != ALMACEN.UNID_ALMACEN)
                {
                    UNID_ALMACEN = ALMACEN.UNID_ALMACEN;
                }
            }
        }
    
        private void FixupMAX_MIN1(MAX_MIN previousValue)
        {
            if (previousValue != null && ReferenceEquals(previousValue.MAX_MIN2, this))
            {
                previousValue.MAX_MIN2 = null;
            }
    
            if (MAX_MIN1 != null)
            {
                MAX_MIN1.MAX_MIN2 = this;
            }
        }
    
        private void FixupMAX_MIN2(MAX_MIN previousValue)
        {
            if (previousValue != null && ReferenceEquals(previousValue.MAX_MIN1, this))
            {
                previousValue.MAX_MIN1 = null;
            }
    
            if (MAX_MIN2 != null)
            {
                MAX_MIN2.MAX_MIN1 = this;
                if (UNID_MAX_MIN != MAX_MIN2.UNID_MAX_MIN)
                {
                    UNID_MAX_MIN = MAX_MIN2.UNID_MAX_MIN;
                }
            }
        }

        #endregion
    }
}