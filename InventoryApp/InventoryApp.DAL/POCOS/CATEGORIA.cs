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
    public partial class CATEGORIA
    {
        #region Primitive Properties
    
        public virtual long UNID_CATEGORIA
        {
            get;
            set;
        }
    
        public virtual string CATEGORIA_NAME
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
    
        public virtual ICollection<ARTICULO> ARTICULOes
        {
            get
            {
                if (_aRTICULOes == null)
                {
                    var newCollection = new FixupCollection<ARTICULO>();
                    newCollection.CollectionChanged += FixupARTICULOes;
                    _aRTICULOes = newCollection;
                }
                return _aRTICULOes;
            }
            set
            {
                if (!ReferenceEquals(_aRTICULOes, value))
                {
                    var previousValue = _aRTICULOes as FixupCollection<ARTICULO>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupARTICULOes;
                    }
                    _aRTICULOes = value;
                    var newValue = value as FixupCollection<ARTICULO>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupARTICULOes;
                    }
                }
            }
        }
        private ICollection<ARTICULO> _aRTICULOes;

        #endregion
        #region Association Fixup
    
        private void FixupARTICULOes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ARTICULO item in e.NewItems)
                {
                    item.CATEGORIA = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ARTICULO item in e.OldItems)
                {
                    if (ReferenceEquals(item.CATEGORIA, this))
                    {
                        item.CATEGORIA = null;
                    }
                }
            }
        }

        #endregion
    }
}
