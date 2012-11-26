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
    public partial class COTIZACION
    {
        #region Primitive Properties
    
        public virtual long UNID_COTIZACION
        {
            get;
            set;
        }
    
        public virtual int ID_EMPRESA
        {
            get;
            set;
        }
    
        public virtual long ID_USER
        {
            get;
            set;
        }
    
        public virtual Nullable<int> ID_STATUS
        {
            get;
            set;
        }
    
        public virtual Nullable<int> ID_CATEGORIA
        {
            get;
            set;
        }
    
        public virtual Nullable<int> ID_TIPO_COTIZACION
        {
            get;
            set;
        }
    
        public virtual Nullable<int> ID_PROYECTO
        {
            get;
            set;
        }
    
        public virtual System.DateTime FECHA_SOLICITUD
        {
            get;
            set;
        }
    
        public virtual System.DateTime FECHA_REQUERIMENTO
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> FECHA_COTIZACION
        {
            get;
            set;
        }
    
        public virtual string OBSERVACIONES_COMPRAS
        {
            get;
            set;
        }
    
        public virtual Nullable<int> DIAS_VIGENCIA
        {
            get;
            set;
        }
    
        public virtual string MOTIVO_CANCELACION
        {
            get;
            set;
        }
    
        public virtual Nullable<int> ID_ROL
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

        #endregion
        #region Association Fixup
    
        private void FixupPOMs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (POM item in e.NewItems)
                {
                    item.COTIZACION = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (POM item in e.OldItems)
                {
                    if (ReferenceEquals(item.COTIZACION, this))
                    {
                        item.COTIZACION = null;
                    }
                }
            }
        }

        #endregion
    }
}
