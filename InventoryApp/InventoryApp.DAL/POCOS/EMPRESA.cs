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
    public partial class EMPRESA
    {
        #region Primitive Properties
    
        public virtual long UNID_EMPRESA
        {
            get;
            set;
        }
    
        public virtual string EMPRESA_NAME
        {
            get;
            set;
        }
    
        public virtual string RAZON_SOCIAL
        {
            get;
            set;
        }
    
        public virtual string RFC
        {
            get;
            set;
        }
    
        public virtual string DIRECCION
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
    
        public virtual ICollection<SOLICITANTE> SOLICITANTEs
        {
            get
            {
                if (_sOLICITANTEs == null)
                {
                    var newCollection = new FixupCollection<SOLICITANTE>();
                    newCollection.CollectionChanged += FixupSOLICITANTEs;
                    _sOLICITANTEs = newCollection;
                }
                return _sOLICITANTEs;
            }
            set
            {
                if (!ReferenceEquals(_sOLICITANTEs, value))
                {
                    var previousValue = _sOLICITANTEs as FixupCollection<SOLICITANTE>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupSOLICITANTEs;
                    }
                    _sOLICITANTEs = value;
                    var newValue = value as FixupCollection<SOLICITANTE>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupSOLICITANTEs;
                    }
                }
            }
        }
        private ICollection<SOLICITANTE> _sOLICITANTEs;
    
        public virtual ICollection<POM> POMs
        {
            get
            {
                if (_pOMs == null)
                {
                    var newCollection = new FixupCollection<POM>();
                    newCollection.CollectionChanged += FixupPOMs;
                    _pOMs = newCollection;
                }
                return _pOMs;
            }
            set
            {
                if (!ReferenceEquals(_pOMs, value))
                {
                    var previousValue = _pOMs as FixupCollection<POM>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupPOMs;
                    }
                    _pOMs = value;
                    var newValue = value as FixupCollection<POM>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupPOMs;
                    }
                }
            }
        }
        private ICollection<POM> _pOMs;
    
        public virtual ICollection<ITEM_STATUS> ITEM_STATUS
        {
            get
            {
                if (_iTEM_STATUS == null)
                {
                    var newCollection = new FixupCollection<ITEM_STATUS>();
                    newCollection.CollectionChanged += FixupITEM_STATUS;
                    _iTEM_STATUS = newCollection;
                }
                return _iTEM_STATUS;
            }
            set
            {
                if (!ReferenceEquals(_iTEM_STATUS, value))
                {
                    var previousValue = _iTEM_STATUS as FixupCollection<ITEM_STATUS>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupITEM_STATUS;
                    }
                    _iTEM_STATUS = value;
                    var newValue = value as FixupCollection<ITEM_STATUS>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupITEM_STATUS;
                    }
                }
            }
        }
        private ICollection<ITEM_STATUS> _iTEM_STATUS;

        #endregion
        #region Association Fixup
    
        private void FixupSOLICITANTEs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (SOLICITANTE item in e.NewItems)
                {
                    item.EMPRESA = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (SOLICITANTE item in e.OldItems)
                {
                    if (ReferenceEquals(item.EMPRESA, this))
                    {
                        item.EMPRESA = null;
                    }
                }
            }
        }
    
        private void FixupPOMs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (POM item in e.NewItems)
                {
                    item.EMPRESA = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (POM item in e.OldItems)
                {
                    if (ReferenceEquals(item.EMPRESA, this))
                    {
                        item.EMPRESA = null;
                    }
                }
            }
        }
    
        private void FixupITEM_STATUS(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ITEM_STATUS item in e.NewItems)
                {
                    item.EMPRESA = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ITEM_STATUS item in e.OldItems)
                {
                    if (ReferenceEquals(item.EMPRESA, this))
                    {
                        item.EMPRESA = null;
                    }
                }
            }
        }

        #endregion
    }
}
