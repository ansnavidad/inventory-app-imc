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
    public partial class SERVICIO
    {
        #region Primitive Properties
    
        public virtual long UNID_SERVICIO
        {
            get;
            set;
        }
    
        public virtual string SERVICIO_NAME
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

        #endregion
        #region Association Fixup
    
        private void FixupMOVIMENTOes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MOVIMENTO item in e.NewItems)
                {
                    item.SERVICIO = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MOVIMENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.SERVICIO, this))
                    {
                        item.SERVICIO = null;
                    }
                }
            }
        }

        #endregion
    }
}
