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
    public partial class TIPO_EMPRESA
    {
        #region Primitive Properties
    
        public virtual long UNID_TIPO_EMPRESA
        {
            get;
            set;
        }
    
        public virtual string TIPO_EMPRESA_NAME
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
    
        public virtual ICollection<TRANSPORTE> TRANSPORTEs
        {
            get
            {
                if (_tRANSPORTEs == null)
                {
                    var newCollection = new FixupCollection<TRANSPORTE>();
                    newCollection.CollectionChanged += FixupTRANSPORTEs;
                    _tRANSPORTEs = newCollection;
                }
                return _tRANSPORTEs;
            }
            set
            {
                if (!ReferenceEquals(_tRANSPORTEs, value))
                {
                    var previousValue = _tRANSPORTEs as FixupCollection<TRANSPORTE>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTRANSPORTEs;
                    }
                    _tRANSPORTEs = value;
                    var newValue = value as FixupCollection<TRANSPORTE>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTRANSPORTEs;
                    }
                }
            }
        }
        private ICollection<TRANSPORTE> _tRANSPORTEs;

        #endregion
        #region Association Fixup
    
        private void FixupMASTER_INVENTARIOS(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MASTER_INVENTARIOS item in e.NewItems)
                {
                    item.TIPO_EMPRESA = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MASTER_INVENTARIOS item in e.OldItems)
                {
                    if (ReferenceEquals(item.TIPO_EMPRESA, this))
                    {
                        item.TIPO_EMPRESA = null;
                    }
                }
            }
        }
    
        private void FixupTRANSPORTEs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TRANSPORTE item in e.NewItems)
                {
                    item.TIPO_EMPRESA = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TRANSPORTE item in e.OldItems)
                {
                    if (ReferenceEquals(item.TIPO_EMPRESA, this))
                    {
                        item.TIPO_EMPRESA = null;
                    }
                }
            }
        }

        #endregion
    }
}
