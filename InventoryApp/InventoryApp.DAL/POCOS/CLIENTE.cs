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
    public partial class CLIENTE
    {
        #region Primitive Properties
    
        public virtual long UNID_CLIENTE
        {
            get;
            set;
        }
    
        public virtual string CLIENTE1
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

        #endregion
        #region Association Fixup
    
        private void FixupMOVIMENTOes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MOVIMENTO item in e.NewItems)
                {
                    item.CLIENTE = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MOVIMENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.CLIENTE, this))
                    {
                        item.CLIENTE = null;
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
                    item.CLIENTE1 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MOVIMENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.CLIENTE1, this))
                    {
                        item.CLIENTE1 = null;
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
                    item.CLIENTE2 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MOVIMENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.CLIENTE2, this))
                    {
                        item.CLIENTE2 = null;
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
                    item.CLIENTE = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ULTIMO_MOVIMIENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.CLIENTE, this))
                    {
                        item.CLIENTE = null;
                    }
                }
            }
        }

        #endregion
    }
}
