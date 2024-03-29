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
    public partial class TERMINO_ENVIO
    {
        #region Primitive Properties
    
        public virtual long UNID_TERMINO_ENVIO
        {
            get;
            set;
        }
    
        public virtual string CLAVE
        {
            get;
            set;
        }
    
        public virtual string TERMINO
        {
            get;
            set;
        }
    
        public virtual string SIGNIFICADO
        {
            get;
            set;
        }
    
        public virtual bool GENERA_LOTES
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

        #endregion
        #region Association Fixup
    
        private void FixupMASTER_INVENTARIOS(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MASTER_INVENTARIOS item in e.NewItems)
                {
                    item.TERMINO_ENVIO = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MASTER_INVENTARIOS item in e.OldItems)
                {
                    if (ReferenceEquals(item.TERMINO_ENVIO, this))
                    {
                        item.TERMINO_ENVIO = null;
                    }
                }
            }
        }

        #endregion
    }
}
