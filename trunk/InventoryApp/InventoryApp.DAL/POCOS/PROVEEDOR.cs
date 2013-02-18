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
    public partial class PROVEEDOR
    {
        #region Primitive Properties
    
        public virtual long UNID_PROVEEDOR
        {
            get;
            set;
        }
    
        public virtual string PROVEEDOR_NAME
        {
            get;
            set;
        }
    
        public virtual string CONTACTO
        {
            get;
            set;
        }
    
        public virtual string TEL1
        {
            get;
            set;
        }
    
        public virtual string TEL2
        {
            get;
            set;
        }
    
        public virtual string MAIL
        {
            get;
            set;
        }
    
        public virtual string CALLE
        {
            get;
            set;
        }
    
        public virtual long UNID_PAIS
        {
            get { return _uNID_PAIS; }
            set
            {
                if (_uNID_PAIS != value)
                {
                    if (PAI != null && PAI.UNID_PAIS != value)
                    {
                        PAI = null;
                    }
                    _uNID_PAIS = value;
                }
            }
        }
        private long _uNID_PAIS;
    
        public virtual long UNID_CIUDAD
        {
            get { return _uNID_CIUDAD; }
            set
            {
                if (_uNID_CIUDAD != value)
                {
                    if (CIUDAD != null && CIUDAD.UNID_CIUDAD != value)
                    {
                        CIUDAD = null;
                    }
                    _uNID_CIUDAD = value;
                }
            }
        }
        private long _uNID_CIUDAD;
    
        public virtual string CODIGO_POSTAL
        {
            get;
            set;
        }
    
        public virtual string RFC
        {
            get;
            set;
        }
    
        public virtual bool IS_ACTIVE
        {
            get;
            set;
        }
    
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

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<PROVEEDOR_CUENTA> PROVEEDOR_CUENTA
        {
            get
            {
                if (_pROVEEDOR_CUENTA == null)
                {
                    var newCollection = new FixupCollection<PROVEEDOR_CUENTA>();
                    newCollection.CollectionChanged += FixupPROVEEDOR_CUENTA;
                    _pROVEEDOR_CUENTA = newCollection;
                }
                return _pROVEEDOR_CUENTA;
            }
            set
            {
                if (!ReferenceEquals(_pROVEEDOR_CUENTA, value))
                {
                    var previousValue = _pROVEEDOR_CUENTA as FixupCollection<PROVEEDOR_CUENTA>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupPROVEEDOR_CUENTA;
                    }
                    _pROVEEDOR_CUENTA = value;
                    var newValue = value as FixupCollection<PROVEEDOR_CUENTA>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupPROVEEDOR_CUENTA;
                    }
                }
            }
        }
        private ICollection<PROVEEDOR_CUENTA> _pROVEEDOR_CUENTA;
    
        public virtual ICollection<MOVIMENTO> MOVIMENTOes
        {
            get
            {
                if (_mOVIMENTOes == null)
                {
                    var newCollection = new FixupCollection<MOVIMENTO>();
                    newCollection.CollectionChanged += FixupMOVIMENTOes;
                    _mOVIMENTOes = newCollection;
                }
                return _mOVIMENTOes;
            }
            set
            {
                if (!ReferenceEquals(_mOVIMENTOes, value))
                {
                    var previousValue = _mOVIMENTOes as FixupCollection<MOVIMENTO>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMOVIMENTOes;
                    }
                    _mOVIMENTOes = value;
                    var newValue = value as FixupCollection<MOVIMENTO>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMOVIMENTOes;
                    }
                }
            }
        }
        private ICollection<MOVIMENTO> _mOVIMENTOes;
    
        public virtual ICollection<MOVIMENTO> MOVIMENTOes1
        {
            get
            {
                if (_mOVIMENTOes1 == null)
                {
                    var newCollection = new FixupCollection<MOVIMENTO>();
                    newCollection.CollectionChanged += FixupMOVIMENTOes1;
                    _mOVIMENTOes1 = newCollection;
                }
                return _mOVIMENTOes1;
            }
            set
            {
                if (!ReferenceEquals(_mOVIMENTOes1, value))
                {
                    var previousValue = _mOVIMENTOes1 as FixupCollection<MOVIMENTO>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMOVIMENTOes1;
                    }
                    _mOVIMENTOes1 = value;
                    var newValue = value as FixupCollection<MOVIMENTO>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMOVIMENTOes1;
                    }
                }
            }
        }
        private ICollection<MOVIMENTO> _mOVIMENTOes1;
    
        public virtual ICollection<MOVIMENTO> MOVIMENTOes2
        {
            get
            {
                if (_mOVIMENTOes2 == null)
                {
                    var newCollection = new FixupCollection<MOVIMENTO>();
                    newCollection.CollectionChanged += FixupMOVIMENTOes2;
                    _mOVIMENTOes2 = newCollection;
                }
                return _mOVIMENTOes2;
            }
            set
            {
                if (!ReferenceEquals(_mOVIMENTOes2, value))
                {
                    var previousValue = _mOVIMENTOes2 as FixupCollection<MOVIMENTO>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMOVIMENTOes2;
                    }
                    _mOVIMENTOes2 = value;
                    var newValue = value as FixupCollection<MOVIMENTO>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMOVIMENTOes2;
                    }
                }
            }
        }
        private ICollection<MOVIMENTO> _mOVIMENTOes2;
    
        public virtual ICollection<ULTIMO_MOVIMIENTO> ULTIMO_MOVIMIENTO
        {
            get
            {
                if (_uLTIMO_MOVIMIENTO == null)
                {
                    var newCollection = new FixupCollection<ULTIMO_MOVIMIENTO>();
                    newCollection.CollectionChanged += FixupULTIMO_MOVIMIENTO;
                    _uLTIMO_MOVIMIENTO = newCollection;
                }
                return _uLTIMO_MOVIMIENTO;
            }
            set
            {
                if (!ReferenceEquals(_uLTIMO_MOVIMIENTO, value))
                {
                    var previousValue = _uLTIMO_MOVIMIENTO as FixupCollection<ULTIMO_MOVIMIENTO>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupULTIMO_MOVIMIENTO;
                    }
                    _uLTIMO_MOVIMIENTO = value;
                    var newValue = value as FixupCollection<ULTIMO_MOVIMIENTO>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupULTIMO_MOVIMIENTO;
                    }
                }
            }
        }
        private ICollection<ULTIMO_MOVIMIENTO> _uLTIMO_MOVIMIENTO;
    
        public virtual ICollection<FACTURA> FACTURAs
        {
            get
            {
                if (_fACTURAs == null)
                {
                    var newCollection = new FixupCollection<FACTURA>();
                    newCollection.CollectionChanged += FixupFACTURAs;
                    _fACTURAs = newCollection;
                }
                return _fACTURAs;
            }
            set
            {
                if (!ReferenceEquals(_fACTURAs, value))
                {
                    var previousValue = _fACTURAs as FixupCollection<FACTURA>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupFACTURAs;
                    }
                    _fACTURAs = value;
                    var newValue = value as FixupCollection<FACTURA>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupFACTURAs;
                    }
                }
            }
        }
        private ICollection<FACTURA> _fACTURAs;
    
        public virtual CIUDAD CIUDAD
        {
            get { return _cIUDAD; }
            set
            {
                if (!ReferenceEquals(_cIUDAD, value))
                {
                    var previousValue = _cIUDAD;
                    _cIUDAD = value;
                    FixupCIUDAD(previousValue);
                }
            }
        }
        private CIUDAD _cIUDAD;
    
        public virtual PAI PAI
        {
            get { return _pAI; }
            set
            {
                if (!ReferenceEquals(_pAI, value))
                {
                    var previousValue = _pAI;
                    _pAI = value;
                    FixupPAI(previousValue);
                }
            }
        }
        private PAI _pAI;

        #endregion
        #region Association Fixup
    
        private void FixupCIUDAD(CIUDAD previousValue)
        {
            if (previousValue != null && previousValue.PROVEEDORs.Contains(this))
            {
                previousValue.PROVEEDORs.Remove(this);
            }
    
            if (CIUDAD != null)
            {
                if (!CIUDAD.PROVEEDORs.Contains(this))
                {
                    CIUDAD.PROVEEDORs.Add(this);
                }
                if (UNID_CIUDAD != CIUDAD.UNID_CIUDAD)
                {
                    UNID_CIUDAD = CIUDAD.UNID_CIUDAD;
                }
            }
        }
    
        private void FixupPAI(PAI previousValue)
        {
            if (previousValue != null && previousValue.PROVEEDORs.Contains(this))
            {
                previousValue.PROVEEDORs.Remove(this);
            }
    
            if (PAI != null)
            {
                if (!PAI.PROVEEDORs.Contains(this))
                {
                    PAI.PROVEEDORs.Add(this);
                }
                if (UNID_PAIS != PAI.UNID_PAIS)
                {
                    UNID_PAIS = PAI.UNID_PAIS;
                }
            }
        }
    
        private void FixupPROVEEDOR_CUENTA(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (PROVEEDOR_CUENTA item in e.NewItems)
                {
                    item.PROVEEDOR = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (PROVEEDOR_CUENTA item in e.OldItems)
                {
                    if (ReferenceEquals(item.PROVEEDOR, this))
                    {
                        item.PROVEEDOR = null;
                    }
                }
            }
        }
    
        private void FixupMOVIMENTOes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MOVIMENTO item in e.NewItems)
                {
                    item.PROVEEDOR = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MOVIMENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.PROVEEDOR, this))
                    {
                        item.PROVEEDOR = null;
                    }
                }
            }
        }
    
        private void FixupMOVIMENTOes1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MOVIMENTO item in e.NewItems)
                {
                    item.PROVEEDOR1 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MOVIMENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.PROVEEDOR1, this))
                    {
                        item.PROVEEDOR1 = null;
                    }
                }
            }
        }
    
        private void FixupMOVIMENTOes2(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MOVIMENTO item in e.NewItems)
                {
                    item.PROVEEDOR2 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MOVIMENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.PROVEEDOR2, this))
                    {
                        item.PROVEEDOR2 = null;
                    }
                }
            }
        }
    
        private void FixupULTIMO_MOVIMIENTO(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ULTIMO_MOVIMIENTO item in e.NewItems)
                {
                    item.PROVEEDOR = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ULTIMO_MOVIMIENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.PROVEEDOR, this))
                    {
                        item.PROVEEDOR = null;
                    }
                }
            }
        }
    
        private void FixupFACTURAs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (FACTURA item in e.NewItems)
                {
                    item.PROVEEDOR = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (FACTURA item in e.OldItems)
                {
                    if (ReferenceEquals(item.PROVEEDOR, this))
                    {
                        item.PROVEEDOR = null;
                    }
                }
            }
        }

        #endregion
    }
}
