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
    public partial class ROL
    {
        #region Primitive Properties
    
        public virtual long UNID_ROL
        {
            get;
            set;
        }
    
        public virtual string ROL_NAME
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<MENU> MENUs
        {
            get
            {
                if (_mENUs == null)
                {
                    var newCollection = new FixupCollection<MENU>();
                    newCollection.CollectionChanged += FixupMENUs;
                    _mENUs = newCollection;
                }
                return _mENUs;
            }
            set
            {
                if (!ReferenceEquals(_mENUs, value))
                {
                    var previousValue = _mENUs as FixupCollection<MENU>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMENUs;
                    }
                    _mENUs = value;
                    var newValue = value as FixupCollection<MENU>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMENUs;
                    }
                }
            }
        }
        private ICollection<MENU> _mENUs;
    
        public virtual ICollection<USUARIO> USUARIOs
        {
            get
            {
                if (_uSUARIOs == null)
                {
                    var newCollection = new FixupCollection<USUARIO>();
                    newCollection.CollectionChanged += FixupUSUARIOs;
                    _uSUARIOs = newCollection;
                }
                return _uSUARIOs;
            }
            set
            {
                if (!ReferenceEquals(_uSUARIOs, value))
                {
                    var previousValue = _uSUARIOs as FixupCollection<USUARIO>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupUSUARIOs;
                    }
                    _uSUARIOs = value;
                    var newValue = value as FixupCollection<USUARIO>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupUSUARIOs;
                    }
                }
            }
        }
        private ICollection<USUARIO> _uSUARIOs;

        #endregion
        #region Association Fixup
    
        private void FixupMENUs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MENU item in e.NewItems)
                {
                    if (!item.ROLs.Contains(this))
                    {
                        item.ROLs.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MENU item in e.OldItems)
                {
                    if (item.ROLs.Contains(this))
                    {
                        item.ROLs.Remove(this);
                    }
                }
            }
        }
    
        private void FixupUSUARIOs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (USUARIO item in e.NewItems)
                {
                    if (!item.ROLs.Contains(this))
                    {
                        item.ROLs.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (USUARIO item in e.OldItems)
                {
                    if (item.ROLs.Contains(this))
                    {
                        item.ROLs.Remove(this);
                    }
                }
            }
        }

        #endregion
    }
}