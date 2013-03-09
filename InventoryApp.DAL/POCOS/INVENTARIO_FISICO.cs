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
    public partial class INVENTARIO_FISICO
    {
        #region Primitive Properties
    
        public virtual long UNID_INVENTARIO_FISICO
        {
            get;
            set;
        }
    
        public virtual long UNID_ALMACEN
        {
            get { return _uNID_ALMACEN; }
            set
            {
                if (_uNID_ALMACEN != value)
                {
                    if (ALMACEN != null && ALMACEN.UNID_ALMACEN != value)
                    {
                        ALMACEN = null;
                    }
                    _uNID_ALMACEN = value;
                }
            }
        }
        private long _uNID_ALMACEN;
    
        public virtual bool IS_FINALIZADO
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> IS_MODIFIED
        {
            get;
            set;
        }
    
        public virtual Nullable<long> LAST_MODIFIED_DATE
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> FECHA_INICIO
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> FECHA_FIN
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> IS_ACTIVE
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ALMACEN ALMACEN
        {
            get { return _aLMACEN; }
            set
            {
                if (!ReferenceEquals(_aLMACEN, value))
                {
                    var previousValue = _aLMACEN;
                    _aLMACEN = value;
                    FixupALMACEN(previousValue);
                }
            }
        }
        private ALMACEN _aLMACEN;
    
        public virtual ICollection<INVENTARIO_FISICO_DET> INVENTARIO_FISICO_DET
        {
            get
            {
                if (_iNVENTARIO_FISICO_DET == null)
                {
                    var newCollection = new FixupCollection<INVENTARIO_FISICO_DET>();
                    newCollection.CollectionChanged += FixupINVENTARIO_FISICO_DET;
                    _iNVENTARIO_FISICO_DET = newCollection;
                }
                return _iNVENTARIO_FISICO_DET;
            }
            set
            {
                if (!ReferenceEquals(_iNVENTARIO_FISICO_DET, value))
                {
                    var previousValue = _iNVENTARIO_FISICO_DET as FixupCollection<INVENTARIO_FISICO_DET>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupINVENTARIO_FISICO_DET;
                    }
                    _iNVENTARIO_FISICO_DET = value;
                    var newValue = value as FixupCollection<INVENTARIO_FISICO_DET>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupINVENTARIO_FISICO_DET;
                    }
                }
            }
        }
        private ICollection<INVENTARIO_FISICO_DET> _iNVENTARIO_FISICO_DET;

        #endregion
        #region Association Fixup
    
        private void FixupALMACEN(ALMACEN previousValue)
        {
            if (previousValue != null && previousValue.INVENTARIO_FISICO.Contains(this))
            {
                previousValue.INVENTARIO_FISICO.Remove(this);
            }
    
            if (ALMACEN != null)
            {
                if (!ALMACEN.INVENTARIO_FISICO.Contains(this))
                {
                    ALMACEN.INVENTARIO_FISICO.Add(this);
                }
                if (UNID_ALMACEN != ALMACEN.UNID_ALMACEN)
                {
                    UNID_ALMACEN = ALMACEN.UNID_ALMACEN;
                }
            }
        }
    
        private void FixupINVENTARIO_FISICO_DET(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (INVENTARIO_FISICO_DET item in e.NewItems)
                {
                    item.INVENTARIO_FISICO = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (INVENTARIO_FISICO_DET item in e.OldItems)
                {
                    if (ReferenceEquals(item.INVENTARIO_FISICO, this))
                    {
                        item.INVENTARIO_FISICO = null;
                    }
                }
            }
        }

        #endregion
    }
}
