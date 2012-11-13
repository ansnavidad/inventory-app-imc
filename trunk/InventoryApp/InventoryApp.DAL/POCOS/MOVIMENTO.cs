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
    public partial class MOVIMENTO
    {
        #region Primitive Properties
    
        public virtual long UNID_MOVIMIENTO
        {
            get;
            set;
        }
    
        public virtual System.DateTime FECHA_MOVIMIENTO
        {
            get;
            set;
        }
    
        public virtual long UNID_TIPO_MOVIMIENTO
        {
            get { return _uNID_TIPO_MOVIMIENTO; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_TIPO_MOVIMIENTO != value)
                    {
                        if (TIPO_MOVIMIENTO != null && TIPO_MOVIMIENTO.UNID_TIPO_MOVIMIENTO != value)
                        {
                            TIPO_MOVIMIENTO = null;
                        }
                        _uNID_TIPO_MOVIMIENTO = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private long _uNID_TIPO_MOVIMIENTO;
    
        public virtual Nullable<long> UNID_ALMACEN_DESTINO
        {
            get { return _uNID_ALMACEN_DESTINO; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_ALMACEN_DESTINO != value)
                    {
                        if (ALMACEN != null && ALMACEN.UNID_ALMACEN != value)
                        {
                            ALMACEN = null;
                        }
                        _uNID_ALMACEN_DESTINO = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_ALMACEN_DESTINO;
    
        public virtual Nullable<long> UNID_PROVEEDOR_DESTINO
        {
            get { return _uNID_PROVEEDOR_DESTINO; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_PROVEEDOR_DESTINO != value)
                    {
                        if (PROVEEDOR2 != null && PROVEEDOR2.UNID_PROVEEDOR != value)
                        {
                            PROVEEDOR2 = null;
                        }
                        _uNID_PROVEEDOR_DESTINO = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_PROVEEDOR_DESTINO;
    
        public virtual Nullable<long> UNID_CLIENTE_DESTINO
        {
            get { return _uNID_CLIENTE_DESTINO; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_CLIENTE_DESTINO != value)
                    {
                        if (CLIENTE2 != null && CLIENTE2.UNID_CLIENTE != value)
                        {
                            CLIENTE2 = null;
                        }
                        _uNID_CLIENTE_DESTINO = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_CLIENTE_DESTINO;
    
        public virtual Nullable<long> UNID_ALMACEN_PROCEDENCIA
        {
            get { return _uNID_ALMACEN_PROCEDENCIA; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_ALMACEN_PROCEDENCIA != value)
                    {
                        if (ALMACEN1 != null && ALMACEN1.UNID_ALMACEN != value)
                        {
                            ALMACEN1 = null;
                        }
                        _uNID_ALMACEN_PROCEDENCIA = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_ALMACEN_PROCEDENCIA;
    
        public virtual Nullable<long> UNID_CLIENTE_PROCEDENCIA
        {
            get { return _uNID_CLIENTE_PROCEDENCIA; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_CLIENTE_PROCEDENCIA != value)
                    {
                        if (CLIENTE != null && CLIENTE.UNID_CLIENTE != value)
                        {
                            CLIENTE = null;
                        }
                        _uNID_CLIENTE_PROCEDENCIA = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_CLIENTE_PROCEDENCIA;
    
        public virtual Nullable<long> UNID_PROVEEDOR_PROCEDENCIA
        {
            get { return _uNID_PROVEEDOR_PROCEDENCIA; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_PROVEEDOR_PROCEDENCIA != value)
                    {
                        if (PROVEEDOR != null && PROVEEDOR.UNID_PROVEEDOR != value)
                        {
                            PROVEEDOR = null;
                        }
                        _uNID_PROVEEDOR_PROCEDENCIA = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_PROVEEDOR_PROCEDENCIA;
    
        public virtual Nullable<long> UNID_SERVICIO
        {
            get { return _uNID_SERVICIO; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_SERVICIO != value)
                    {
                        if (SERVICIO != null && SERVICIO.UNID_SERVICIO != value)
                        {
                            SERVICIO = null;
                        }
                        _uNID_SERVICIO = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_SERVICIO;
    
        public virtual string TT
        {
            get;
            set;
        }
    
        public virtual string CONTACTO
        {
            get;
            set;
        }
    
        public virtual Nullable<long> UNID_TRANSPORTE
        {
            get { return _uNID_TRANSPORTE; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_TRANSPORTE != value)
                    {
                        if (TRANSPORTE != null && TRANSPORTE.UNID_TRANSPORTE != value)
                        {
                            TRANSPORTE = null;
                        }
                        _uNID_TRANSPORTE = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_TRANSPORTE;
    
        public virtual bool IS_ACTIVE
        {
            get;
            set;
        }
    
        public virtual string DIRECCION_ENVIO
        {
            get;
            set;
        }
    
        public virtual string SITIO_ENLACE
        {
            get;
            set;
        }
    
        public virtual string NOMBRE_SITIO
        {
            get;
            set;
        }
    
        public virtual string RECIBE
        {
            get;
            set;
        }
    
        public virtual string GUIA
        {
            get;
            set;
        }
    
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
                        if (CLIENTE1 != null && CLIENTE1.UNID_CLIENTE != value)
                        {
                            CLIENTE1 = null;
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
                        if (PROVEEDOR1 != null && PROVEEDOR1.UNID_PROVEEDOR != value)
                        {
                            PROVEEDOR1 = null;
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
    
        public virtual Nullable<long> UNID_FACTURA_VENTA
        {
            get { return _uNID_FACTURA_VENTA; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_FACTURA_VENTA != value)
                    {
                        if (FACTURA_VENTA != null && FACTURA_VENTA.UNID_FACTURA_VENTA != value)
                        {
                            FACTURA_VENTA = null;
                        }
                        _uNID_FACTURA_VENTA = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_FACTURA_VENTA;
    
        public virtual string PEDIMIENTO_IMPO
        {
            get;
            set;
        }
    
        public virtual string PEDIMIENTO_EXPO
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
    
        public virtual PROVEEDOR PROVEEDOR1
        {
            get { return _pROVEEDOR1; }
            set
            {
                if (!ReferenceEquals(_pROVEEDOR1, value))
                {
                    var previousValue = _pROVEEDOR1;
                    _pROVEEDOR1 = value;
                    FixupPROVEEDOR1(previousValue);
                }
            }
        }
        private PROVEEDOR _pROVEEDOR1;
    
        public virtual PROVEEDOR PROVEEDOR2
        {
            get { return _pROVEEDOR2; }
            set
            {
                if (!ReferenceEquals(_pROVEEDOR2, value))
                {
                    var previousValue = _pROVEEDOR2;
                    _pROVEEDOR2 = value;
                    FixupPROVEEDOR2(previousValue);
                }
            }
        }
        private PROVEEDOR _pROVEEDOR2;
    
        public virtual TRANSPORTE TRANSPORTE
        {
            get { return _tRANSPORTE; }
            set
            {
                if (!ReferenceEquals(_tRANSPORTE, value))
                {
                    var previousValue = _tRANSPORTE;
                    _tRANSPORTE = value;
                    FixupTRANSPORTE(previousValue);
                }
            }
        }
        private TRANSPORTE _tRANSPORTE;
    
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
    
        public virtual ALMACEN ALMACEN1
        {
            get { return _aLMACEN1; }
            set
            {
                if (!ReferenceEquals(_aLMACEN1, value))
                {
                    var previousValue = _aLMACEN1;
                    _aLMACEN1 = value;
                    FixupALMACEN1(previousValue);
                }
            }
        }
        private ALMACEN _aLMACEN1;
    
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
    
        public virtual CLIENTE CLIENTE1
        {
            get { return _cLIENTE1; }
            set
            {
                if (!ReferenceEquals(_cLIENTE1, value))
                {
                    var previousValue = _cLIENTE1;
                    _cLIENTE1 = value;
                    FixupCLIENTE1(previousValue);
                }
            }
        }
        private CLIENTE _cLIENTE1;
    
        public virtual CLIENTE CLIENTE2
        {
            get { return _cLIENTE2; }
            set
            {
                if (!ReferenceEquals(_cLIENTE2, value))
                {
                    var previousValue = _cLIENTE2;
                    _cLIENTE2 = value;
                    FixupCLIENTE2(previousValue);
                }
            }
        }
        private CLIENTE _cLIENTE2;
    
        public virtual SERVICIO SERVICIO
        {
            get { return _sERVICIO; }
            set
            {
                if (!ReferenceEquals(_sERVICIO, value))
                {
                    var previousValue = _sERVICIO;
                    _sERVICIO = value;
                    FixupSERVICIO(previousValue);
                }
            }
        }
        private SERVICIO _sERVICIO;
    
        public virtual TIPO_MOVIMIENTO TIPO_MOVIMIENTO
        {
            get { return _tIPO_MOVIMIENTO; }
            set
            {
                if (!ReferenceEquals(_tIPO_MOVIMIENTO, value))
                {
                    var previousValue = _tIPO_MOVIMIENTO;
                    _tIPO_MOVIMIENTO = value;
                    FixupTIPO_MOVIMIENTO(previousValue);
                }
            }
        }
        private TIPO_MOVIMIENTO _tIPO_MOVIMIENTO;
    
        public virtual FACTURA_VENTA FACTURA_VENTA
        {
            get { return _fACTURA_VENTA; }
            set
            {
                if (!ReferenceEquals(_fACTURA_VENTA, value))
                {
                    var previousValue = _fACTURA_VENTA;
                    _fACTURA_VENTA = value;
                    FixupFACTURA_VENTA(previousValue);
                }
            }
        }
        private FACTURA_VENTA _fACTURA_VENTA;
    
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

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupPROVEEDOR(PROVEEDOR previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes.Contains(this))
            {
                previousValue.MOVIMENTOes.Remove(this);
            }
    
            if (PROVEEDOR != null)
            {
                if (!PROVEEDOR.MOVIMENTOes.Contains(this))
                {
                    PROVEEDOR.MOVIMENTOes.Add(this);
                }
                if (UNID_PROVEEDOR_PROCEDENCIA != PROVEEDOR.UNID_PROVEEDOR)
                {
                    UNID_PROVEEDOR_PROCEDENCIA = PROVEEDOR.UNID_PROVEEDOR;
                }
            }
            else if (!_settingFK)
            {
                UNID_PROVEEDOR_PROCEDENCIA = null;
            }
        }
    
        private void FixupPROVEEDOR1(PROVEEDOR previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes1.Contains(this))
            {
                previousValue.MOVIMENTOes1.Remove(this);
            }
    
            if (PROVEEDOR1 != null)
            {
                if (!PROVEEDOR1.MOVIMENTOes1.Contains(this))
                {
                    PROVEEDOR1.MOVIMENTOes1.Add(this);
                }
                if (UNID_PROVEEDOR != PROVEEDOR1.UNID_PROVEEDOR)
                {
                    UNID_PROVEEDOR = PROVEEDOR1.UNID_PROVEEDOR;
                }
            }
            else if (!_settingFK)
            {
                UNID_PROVEEDOR = null;
            }
        }
    
        private void FixupPROVEEDOR2(PROVEEDOR previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes2.Contains(this))
            {
                previousValue.MOVIMENTOes2.Remove(this);
            }
    
            if (PROVEEDOR2 != null)
            {
                if (!PROVEEDOR2.MOVIMENTOes2.Contains(this))
                {
                    PROVEEDOR2.MOVIMENTOes2.Add(this);
                }
                if (UNID_PROVEEDOR_DESTINO != PROVEEDOR2.UNID_PROVEEDOR)
                {
                    UNID_PROVEEDOR_DESTINO = PROVEEDOR2.UNID_PROVEEDOR;
                }
            }
            else if (!_settingFK)
            {
                UNID_PROVEEDOR_DESTINO = null;
            }
        }
    
        private void FixupTRANSPORTE(TRANSPORTE previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes.Contains(this))
            {
                previousValue.MOVIMENTOes.Remove(this);
            }
    
            if (TRANSPORTE != null)
            {
                if (!TRANSPORTE.MOVIMENTOes.Contains(this))
                {
                    TRANSPORTE.MOVIMENTOes.Add(this);
                }
                if (UNID_TRANSPORTE != TRANSPORTE.UNID_TRANSPORTE)
                {
                    UNID_TRANSPORTE = TRANSPORTE.UNID_TRANSPORTE;
                }
            }
            else if (!_settingFK)
            {
                UNID_TRANSPORTE = null;
            }
        }
    
        private void FixupALMACEN(ALMACEN previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes.Contains(this))
            {
                previousValue.MOVIMENTOes.Remove(this);
            }
    
            if (ALMACEN != null)
            {
                if (!ALMACEN.MOVIMENTOes.Contains(this))
                {
                    ALMACEN.MOVIMENTOes.Add(this);
                }
                if (UNID_ALMACEN_DESTINO != ALMACEN.UNID_ALMACEN)
                {
                    UNID_ALMACEN_DESTINO = ALMACEN.UNID_ALMACEN;
                }
            }
            else if (!_settingFK)
            {
                UNID_ALMACEN_DESTINO = null;
            }
        }
    
        private void FixupALMACEN1(ALMACEN previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes1.Contains(this))
            {
                previousValue.MOVIMENTOes1.Remove(this);
            }
    
            if (ALMACEN1 != null)
            {
                if (!ALMACEN1.MOVIMENTOes1.Contains(this))
                {
                    ALMACEN1.MOVIMENTOes1.Add(this);
                }
                if (UNID_ALMACEN_PROCEDENCIA != ALMACEN1.UNID_ALMACEN)
                {
                    UNID_ALMACEN_PROCEDENCIA = ALMACEN1.UNID_ALMACEN;
                }
            }
            else if (!_settingFK)
            {
                UNID_ALMACEN_PROCEDENCIA = null;
            }
        }
    
        private void FixupCLIENTE(CLIENTE previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes.Contains(this))
            {
                previousValue.MOVIMENTOes.Remove(this);
            }
    
            if (CLIENTE != null)
            {
                if (!CLIENTE.MOVIMENTOes.Contains(this))
                {
                    CLIENTE.MOVIMENTOes.Add(this);
                }
                if (UNID_CLIENTE_PROCEDENCIA != CLIENTE.UNID_CLIENTE)
                {
                    UNID_CLIENTE_PROCEDENCIA = CLIENTE.UNID_CLIENTE;
                }
            }
            else if (!_settingFK)
            {
                UNID_CLIENTE_PROCEDENCIA = null;
            }
        }
    
        private void FixupCLIENTE1(CLIENTE previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes1.Contains(this))
            {
                previousValue.MOVIMENTOes1.Remove(this);
            }
    
            if (CLIENTE1 != null)
            {
                if (!CLIENTE1.MOVIMENTOes1.Contains(this))
                {
                    CLIENTE1.MOVIMENTOes1.Add(this);
                }
                if (UNID_CLIENTE != CLIENTE1.UNID_CLIENTE)
                {
                    UNID_CLIENTE = CLIENTE1.UNID_CLIENTE;
                }
            }
            else if (!_settingFK)
            {
                UNID_CLIENTE = null;
            }
        }
    
        private void FixupCLIENTE2(CLIENTE previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes2.Contains(this))
            {
                previousValue.MOVIMENTOes2.Remove(this);
            }
    
            if (CLIENTE2 != null)
            {
                if (!CLIENTE2.MOVIMENTOes2.Contains(this))
                {
                    CLIENTE2.MOVIMENTOes2.Add(this);
                }
                if (UNID_CLIENTE_DESTINO != CLIENTE2.UNID_CLIENTE)
                {
                    UNID_CLIENTE_DESTINO = CLIENTE2.UNID_CLIENTE;
                }
            }
            else if (!_settingFK)
            {
                UNID_CLIENTE_DESTINO = null;
            }
        }
    
        private void FixupSERVICIO(SERVICIO previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes.Contains(this))
            {
                previousValue.MOVIMENTOes.Remove(this);
            }
    
            if (SERVICIO != null)
            {
                if (!SERVICIO.MOVIMENTOes.Contains(this))
                {
                    SERVICIO.MOVIMENTOes.Add(this);
                }
                if (UNID_SERVICIO != SERVICIO.UNID_SERVICIO)
                {
                    UNID_SERVICIO = SERVICIO.UNID_SERVICIO;
                }
            }
            else if (!_settingFK)
            {
                UNID_SERVICIO = null;
            }
        }
    
        private void FixupTIPO_MOVIMIENTO(TIPO_MOVIMIENTO previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes.Contains(this))
            {
                previousValue.MOVIMENTOes.Remove(this);
            }
    
            if (TIPO_MOVIMIENTO != null)
            {
                if (!TIPO_MOVIMIENTO.MOVIMENTOes.Contains(this))
                {
                    TIPO_MOVIMIENTO.MOVIMENTOes.Add(this);
                }
                if (UNID_TIPO_MOVIMIENTO != TIPO_MOVIMIENTO.UNID_TIPO_MOVIMIENTO)
                {
                    UNID_TIPO_MOVIMIENTO = TIPO_MOVIMIENTO.UNID_TIPO_MOVIMIENTO;
                }
            }
        }
    
        private void FixupFACTURA_VENTA(FACTURA_VENTA previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes.Contains(this))
            {
                previousValue.MOVIMENTOes.Remove(this);
            }
    
            if (FACTURA_VENTA != null)
            {
                if (!FACTURA_VENTA.MOVIMENTOes.Contains(this))
                {
                    FACTURA_VENTA.MOVIMENTOes.Add(this);
                }
                if (UNID_FACTURA_VENTA != FACTURA_VENTA.UNID_FACTURA_VENTA)
                {
                    UNID_FACTURA_VENTA = FACTURA_VENTA.UNID_FACTURA_VENTA;
                }
            }
            else if (!_settingFK)
            {
                UNID_FACTURA_VENTA = null;
            }
        }
    
        private void FixupSOLICITANTE(SOLICITANTE previousValue)
        {
            if (previousValue != null && previousValue.MOVIMENTOes.Contains(this))
            {
                previousValue.MOVIMENTOes.Remove(this);
            }
    
            if (SOLICITANTE != null)
            {
                if (!SOLICITANTE.MOVIMENTOes.Contains(this))
                {
                    SOLICITANTE.MOVIMENTOes.Add(this);
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
    
        private void FixupMOVIMIENTO_DETALLE(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MOVIMIENTO_DETALLE item in e.NewItems)
                {
                    item.MOVIMENTO = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MOVIMIENTO_DETALLE item in e.OldItems)
                {
                    if (ReferenceEquals(item.MOVIMENTO, this))
                    {
                        item.MOVIMENTO = null;
                    }
                }
            }
        }

        #endregion
    }
}
