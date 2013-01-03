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
    
        public virtual bool IS_ACTIVE
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<ROL_MENU> ROL_MENU
        {
            get
            {
                if (_rOL_MENU == null)
                {
                    var newCollection = new FixupCollection<ROL_MENU>();
                    newCollection.CollectionChanged += FixupROL_MENU;
                    _rOL_MENU = newCollection;
                }
                return _rOL_MENU;
            }
            set
            {
                if (!ReferenceEquals(_rOL_MENU, value))
                {
                    var previousValue = _rOL_MENU as FixupCollection<ROL_MENU>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupROL_MENU;
                    }
                    _rOL_MENU = value;
                    var newValue = value as FixupCollection<ROL_MENU>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupROL_MENU;
                    }
                }
            }
        }
        private ICollection<ROL_MENU> _rOL_MENU;
    
        public virtual ICollection<USUARIO_ROL> USUARIO_ROL
        {
            get
            {
                if (_uSUARIO_ROL == null)
                {
                    var newCollection = new FixupCollection<USUARIO_ROL>();
                    newCollection.CollectionChanged += FixupUSUARIO_ROL;
                    _uSUARIO_ROL = newCollection;
                }
                return _uSUARIO_ROL;
            }
            set
            {
                if (!ReferenceEquals(_uSUARIO_ROL, value))
                {
                    var previousValue = _uSUARIO_ROL as FixupCollection<USUARIO_ROL>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupUSUARIO_ROL;
                    }
                    _uSUARIO_ROL = value;
                    var newValue = value as FixupCollection<USUARIO_ROL>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupUSUARIO_ROL;
                    }
                }
            }
        }
        private ICollection<USUARIO_ROL> _uSUARIO_ROL;

        #endregion
        #region Association Fixup
    
        private void FixupROL_MENU(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ROL_MENU item in e.NewItems)
                {
                    item.ROL = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ROL_MENU item in e.OldItems)
                {
                    if (ReferenceEquals(item.ROL, this))
                    {
                        item.ROL = null;
                    }
                }
            }
        }
    
        private void FixupUSUARIO_ROL(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (USUARIO_ROL item in e.NewItems)
                {
                    item.ROL = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (USUARIO_ROL item in e.OldItems)
                {
                    if (ReferenceEquals(item.ROL, this))
                    {
                        item.ROL = null;
                    }
                }
            }
        }

        #endregion
    }
}
