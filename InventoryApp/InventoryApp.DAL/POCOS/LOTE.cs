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
    public partial class LOTE
    {
        #region Primitive Properties
    
        public virtual long UNID_LOTE
        {
            get;
            set;
        }
    
        public virtual long UNID_POM
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
    
        public virtual ICollection<PEDIMENTO> PEDIMENTOes
        {
            get
            {
                if (_pEDIMENTOes == null)
                {
                    var newCollection = new FixupCollection<PEDIMENTO>();
                    newCollection.CollectionChanged += FixupPEDIMENTOes;
                    _pEDIMENTOes = newCollection;
                }
                return _pEDIMENTOes;
            }
            set
            {
                if (!ReferenceEquals(_pEDIMENTOes, value))
                {
                    var previousValue = _pEDIMENTOes as FixupCollection<PEDIMENTO>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupPEDIMENTOes;
                    }
                    _pEDIMENTOes = value;
                    var newValue = value as FixupCollection<PEDIMENTO>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupPEDIMENTOes;
                    }
                }
            }
        }
        private ICollection<PEDIMENTO> _pEDIMENTOes;

        #endregion
        #region Association Fixup
    
        private void FixupMASTER_INVENTARIOS(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MASTER_INVENTARIOS item in e.NewItems)
                {
                    item.LOTE = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MASTER_INVENTARIOS item in e.OldItems)
                {
                    if (ReferenceEquals(item.LOTE, this))
                    {
                        item.LOTE = null;
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
                    item.LOTE = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (FACTURA item in e.OldItems)
                {
                    if (ReferenceEquals(item.LOTE, this))
                    {
                        item.LOTE = null;
                    }
                }
            }
        }
    
        private void FixupPEDIMENTOes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (PEDIMENTO item in e.NewItems)
                {
                    item.LOTE = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (PEDIMENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.LOTE, this))
                    {
                        item.LOTE = null;
                    }
                }
            }
        }

        #endregion
    }
}
