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
    public partial class DEPARTAMENTO
    {
        #region Primitive Properties
    
        public virtual long UNID_DEPARTAMENTO
        {
            get;
            set;
        }
    
        public virtual string DEPARTAMENTO_NAME
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

        #endregion
        #region Association Fixup
    
        private void FixupSOLICITANTEs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (SOLICITANTE item in e.NewItems)
                {
                    item.DEPARTAMENTO = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (SOLICITANTE item in e.OldItems)
                {
                    if (ReferenceEquals(item.DEPARTAMENTO, this))
                    {
                        item.DEPARTAMENTO = null;
                    }
                }
            }
        }

        #endregion
    }
}
