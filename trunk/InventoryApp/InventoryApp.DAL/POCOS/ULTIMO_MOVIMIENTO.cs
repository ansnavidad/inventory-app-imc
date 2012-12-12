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
    public partial class ULTIMO_MOVIMIENTO
    {
        #region Primitive Properties
    
        public virtual long UNID_ITEM
        {
            get { return _uNID_ITEM; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_ITEM != value)
                    {
                        if (ITEM != null && ITEM.UNID_ITEM != value)
                        {
                            ITEM = null;
                        }
                        _uNID_ITEM = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private long _uNID_ITEM;
    
        public virtual Nullable<long> UNID_ALMACEN
        {
            get { return _uNID_ALMACEN; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_ALMACEN != value)
                    {
                        if (ALMACEN != null && ALMACEN.UNID_ALMACEN != value)
                        {
                            ALMACEN = null;
                        }
                        _uNID_ALMACEN = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_ALMACEN;
    
        public virtual Nullable<long> UNID_CLIENTE
        {
            get { return _uNID_CLIENTE; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_CLIENTE != value)
                    {
                        if (CLIENTE != null && CLIENTE.UNID_CLIENTE != value)
                        {
                            CLIENTE = null;
                        }
                        _uNID_CLIENTE = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_CLIENTE;
    
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
    
        public virtual Nullable<long> UNID_MOVIMIENTO_DETALLE
        {
            get { return _uNID_MOVIMIENTO_DETALLE; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_MOVIMIENTO_DETALLE != value)
                    {
                        if (MOVIMIENTO_DETALLE != null && MOVIMIENTO_DETALLE.UNID_MOVIMIENTO_DETALLE != value)
                        {
                            MOVIMIENTO_DETALLE = null;
                        }
                        _uNID_MOVIMIENTO_DETALLE = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_MOVIMIENTO_DETALLE;
    
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
    
        public virtual CLIENTE CLIENTE
        {
            get { return _cLIENTE; }
            set
            {
                if (!ReferenceEquals(_cLIENTE, value))
                {
                    var previousValue = _cLIENTE;
                    _cLIENTE = value;
                    FixupCLIENTE(previousValue);
                }
            }
        }
        private CLIENTE _cLIENTE;
    
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
    
        public virtual MOVIMIENTO_DETALLE MOVIMIENTO_DETALLE
        {
            get { return _mOVIMIENTO_DETALLE; }
            set
            {
                if (!ReferenceEquals(_mOVIMIENTO_DETALLE, value))
                {
                    var previousValue = _mOVIMIENTO_DETALLE;
                    _mOVIMIENTO_DETALLE = value;
                    FixupMOVIMIENTO_DETALLE(previousValue);
                }
            }
        }
        private MOVIMIENTO_DETALLE _mOVIMIENTO_DETALLE;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupPROVEEDOR(PROVEEDOR previousValue)
        {
            if (previousValue != null && previousValue.ULTIMO_MOVIMIENTO.Contains(this))
            {
                previousValue.ULTIMO_MOVIMIENTO.Remove(this);
            }
    
            if (PROVEEDOR != null)
            {
                if (!PROVEEDOR.ULTIMO_MOVIMIENTO.Contains(this))
                {
                    PROVEEDOR.ULTIMO_MOVIMIENTO.Add(this);
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
    
        private void FixupALMACEN(ALMACEN previousValue)
        {
            if (previousValue != null && previousValue.ULTIMO_MOVIMIENTO.Contains(this))
            {
                previousValue.ULTIMO_MOVIMIENTO.Remove(this);
            }
    
            if (ALMACEN != null)
            {
                if (!ALMACEN.ULTIMO_MOVIMIENTO.Contains(this))
                {
                    ALMACEN.ULTIMO_MOVIMIENTO.Add(this);
                }
                if (UNID_ALMACEN != ALMACEN.UNID_ALMACEN)
                {
                    UNID_ALMACEN = ALMACEN.UNID_ALMACEN;
                }
            }
            else if (!_settingFK)
            {
                UNID_ALMACEN = null;
            }
        }
    
        private void FixupCLIENTE(CLIENTE previousValue)
        {
            if (previousValue != null && previousValue.ULTIMO_MOVIMIENTO.Contains(this))
            {
                previousValue.ULTIMO_MOVIMIENTO.Remove(this);
            }
    
            if (CLIENTE != null)
            {
                if (!CLIENTE.ULTIMO_MOVIMIENTO.Contains(this))
                {
                    CLIENTE.ULTIMO_MOVIMIENTO.Add(this);
                }
                if (UNID_CLIENTE != CLIENTE.UNID_CLIENTE)
                {
                    UNID_CLIENTE = CLIENTE.UNID_CLIENTE;
                }
            }
            else if (!_settingFK)
            {
                UNID_CLIENTE = null;
            }
        }
    
        private void FixupITEM(ITEM previousValue)
        {
            if (previousValue != null && ReferenceEquals(previousValue.ULTIMO_MOVIMIENTO, this))
            {
                previousValue.ULTIMO_MOVIMIENTO = null;
            }
    
            if (ITEM != null)
            {
                ITEM.ULTIMO_MOVIMIENTO = this;
                if (UNID_ITEM != ITEM.UNID_ITEM)
                {
                    UNID_ITEM = ITEM.UNID_ITEM;
                }
            }
        }
    
        private void FixupMOVIMIENTO_DETALLE(MOVIMIENTO_DETALLE previousValue)
        {
            if (previousValue != null && previousValue.ULTIMO_MOVIMIENTO.Contains(this))
            {
                previousValue.ULTIMO_MOVIMIENTO.Remove(this);
            }
    
            if (MOVIMIENTO_DETALLE != null)
            {
                if (!MOVIMIENTO_DETALLE.ULTIMO_MOVIMIENTO.Contains(this))
                {
                    MOVIMIENTO_DETALLE.ULTIMO_MOVIMIENTO.Add(this);
                }
                if (UNID_MOVIMIENTO_DETALLE != MOVIMIENTO_DETALLE.UNID_MOVIMIENTO_DETALLE)
                {
                    UNID_MOVIMIENTO_DETALLE = MOVIMIENTO_DETALLE.UNID_MOVIMIENTO_DETALLE;
                }
            }
            else if (!_settingFK)
            {
                UNID_MOVIMIENTO_DETALLE = null;
            }
        }

        #endregion
    }
}
