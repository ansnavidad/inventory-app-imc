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
    public partial class ITEM
    {
        #region Primitive Properties
    
        public virtual long UNID_ITEM
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
    
        public virtual string SKU
        {
            get;
            set;
        }
    
        public virtual string NUMERO_SERIE
        {
            get;
            set;
        }
    
        public virtual long UNID_ITEM_STATUS
        {
            get { return _uNID_ITEM_STATUS; }
            set
            {
                if (_uNID_ITEM_STATUS != value)
                {
                    if (ITEM_STATUS != null && ITEM_STATUS.UNID_ITEM_STATUS != value)
                    {
                        ITEM_STATUS = null;
                    }
                    _uNID_ITEM_STATUS = value;
                }
            }
        }
        private long _uNID_ITEM_STATUS;
    
        public virtual double COSTO_UNITARIO
        {
            get;
            set;
        }
    
        public virtual long UNID_FACTURA_DETALE
        {
            get { return _uNID_FACTURA_DETALE; }
            set
            {
                if (_uNID_FACTURA_DETALE != value)
                {
                    if (FACTURA_DETALLE != null && FACTURA_DETALLE.UNID_FACTURA_DETALE != value)
                    {
                        FACTURA_DETALLE = null;
                    }
                    _uNID_FACTURA_DETALE = value;
                }
            }
        }
        private long _uNID_FACTURA_DETALE;
    
        public virtual bool IS_ACTIVE
        {
            get;
            set;
        }
    
        public virtual Nullable<long> UNID_EMPRESA
        {
            get;
            set;
        }
    
        public virtual string STATUS
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
    
        public virtual ITEM_STATUS ITEM_STATUS
        {
            get { return _iTEM_STATUS; }
            set
            {
                if (!ReferenceEquals(_iTEM_STATUS, value))
                {
                    var previousValue = _iTEM_STATUS;
                    _iTEM_STATUS = value;
                    FixupITEM_STATUS(previousValue);
                }
            }
        }
        private ITEM_STATUS _iTEM_STATUS;
    
        public virtual ICollection<MOVIMIENTO_DETALLE> MOVIMIENTO_DETALLE
        {
            get
            {
                if (_mOVIMIENTO_DETALLE == null)
                {
                    var newCollection = new FixupCollection<MOVIMIENTO_DETALLE>();
                    newCollection.CollectionChanged += FixupMOVIMIENTO_DETALLE;
                    _mOVIMIENTO_DETALLE = newCollection;
                }
                return _mOVIMIENTO_DETALLE;
            }
            set
            {
                if (!ReferenceEquals(_mOVIMIENTO_DETALLE, value))
                {
                    var previousValue = _mOVIMIENTO_DETALLE as FixupCollection<MOVIMIENTO_DETALLE>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMOVIMIENTO_DETALLE;
                    }
                    _mOVIMIENTO_DETALLE = value;
                    var newValue = value as FixupCollection<MOVIMIENTO_DETALLE>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMOVIMIENTO_DETALLE;
                    }
                }
            }
        }
        private ICollection<MOVIMIENTO_DETALLE> _mOVIMIENTO_DETALLE;
    
        public virtual ULTIMO_MOVIMIENTO ULTIMO_MOVIMIENTO
        {
            get { return _uLTIMO_MOVIMIENTO; }
            set
            {
                if (!ReferenceEquals(_uLTIMO_MOVIMIENTO, value))
                {
                    var previousValue = _uLTIMO_MOVIMIENTO;
                    _uLTIMO_MOVIMIENTO = value;
                    FixupULTIMO_MOVIMIENTO(previousValue);
                }
            }
        }
        private ULTIMO_MOVIMIENTO _uLTIMO_MOVIMIENTO;
    
        public virtual FACTURA_DETALLE FACTURA_DETALLE
        {
            get { return _fACTURA_DETALLE; }
            set
            {
                if (!ReferenceEquals(_fACTURA_DETALLE, value))
                {
                    var previousValue = _fACTURA_DETALLE;
                    _fACTURA_DETALLE = value;
                    FixupFACTURA_DETALLE(previousValue);
                }
            }
        }
        private FACTURA_DETALLE _fACTURA_DETALLE;

        #endregion
        #region Association Fixup
    
        private void FixupARTICULO(ARTICULO previousValue)
        {
            if (previousValue != null && previousValue.ITEMs.Contains(this))
            {
                previousValue.ITEMs.Remove(this);
            }
    
            if (ARTICULO != null)
            {
                if (!ARTICULO.ITEMs.Contains(this))
                {
                    ARTICULO.ITEMs.Add(this);
                }
                if (UNID_ARTICULO != ARTICULO.UNID_ARTICULO)
                {
                    UNID_ARTICULO = ARTICULO.UNID_ARTICULO;
                }
            }
        }
    
        private void FixupITEM_STATUS(ITEM_STATUS previousValue)
        {
            if (previousValue != null && previousValue.ITEMs.Contains(this))
            {
                previousValue.ITEMs.Remove(this);
            }
    
            if (ITEM_STATUS != null)
            {
                if (!ITEM_STATUS.ITEMs.Contains(this))
                {
                    ITEM_STATUS.ITEMs.Add(this);
                }
                if (UNID_ITEM_STATUS != ITEM_STATUS.UNID_ITEM_STATUS)
                {
                    UNID_ITEM_STATUS = ITEM_STATUS.UNID_ITEM_STATUS;
                }
            }
        }
    
        private void FixupULTIMO_MOVIMIENTO(ULTIMO_MOVIMIENTO previousValue)
        {
            if (previousValue != null && ReferenceEquals(previousValue.ITEM, this))
            {
                previousValue.ITEM = null;
            }
    
            if (ULTIMO_MOVIMIENTO != null)
            {
                ULTIMO_MOVIMIENTO.ITEM = this;
            }
        }
    
        private void FixupFACTURA_DETALLE(FACTURA_DETALLE previousValue)
        {
            if (previousValue != null && previousValue.ITEMs.Contains(this))
            {
                previousValue.ITEMs.Remove(this);
            }
    
            if (FACTURA_DETALLE != null)
            {
                if (!FACTURA_DETALLE.ITEMs.Contains(this))
                {
                    FACTURA_DETALLE.ITEMs.Add(this);
                }
                if (UNID_FACTURA_DETALE != FACTURA_DETALLE.UNID_FACTURA_DETALE)
                {
                    UNID_FACTURA_DETALE = FACTURA_DETALLE.UNID_FACTURA_DETALE;
                }
            }
        }
    
        private void FixupMOVIMIENTO_DETALLE(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MOVIMIENTO_DETALLE item in e.NewItems)
                {
                    item.ITEM = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MOVIMIENTO_DETALLE item in e.OldItems)
                {
                    if (ReferenceEquals(item.ITEM, this))
                    {
                        item.ITEM = null;
                    }
                }
            }
        }

        #endregion
    }
}
