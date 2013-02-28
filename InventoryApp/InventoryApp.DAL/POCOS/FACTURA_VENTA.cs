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
    public partial class FACTURA_VENTA
    {
        #region Primitive Properties
    
        public virtual long UNID_FACTURA_VENTA
        {
            get;
            set;
        }
    
        public virtual string FOLIO
        {
            get;
            set;
        }
    
        public virtual string TOTAL_DESC_FACTURA
        {
            get;
            set;
        }
    
        public virtual Nullable<double> TOTAL_FACTURA
        {
            get;
            set;
        }
    
        public virtual Nullable<double> POR_IVA
        {
            get;
            set;
        }
    
        public virtual Nullable<double> IVA_PESOS
        {
            get;
            set;
        }
    
        public virtual Nullable<double> IMPORTE_FACTURA
        {
            get;
            set;
        }
    
        public virtual Nullable<long> UNID_MONEDA
        {
            get { return _uNID_MONEDA; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uNID_MONEDA != value)
                    {
                        if (MONEDA != null && MONEDA.UNID_MONEDA != value)
                        {
                            MONEDA = null;
                        }
                        _uNID_MONEDA = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _uNID_MONEDA;
    
        public virtual Nullable<double> TIPO_CAMBIO
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
    
        public virtual MONEDA MONEDA
        {
            get { return _mONEDA; }
            set
            {
                if (!ReferenceEquals(_mONEDA, value))
                {
                    var previousValue = _mONEDA;
                    _mONEDA = value;
                    FixupMONEDA(previousValue);
                }
            }
        }
        private MONEDA _mONEDA;
    
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
    
        private bool _settingFK = false;
    
        private void FixupMONEDA(MONEDA previousValue)
        {
            if (previousValue != null && previousValue.FACTURA_VENTA.Contains(this))
            {
                previousValue.FACTURA_VENTA.Remove(this);
            }
    
            if (MONEDA != null)
            {
                if (!MONEDA.FACTURA_VENTA.Contains(this))
                {
                    MONEDA.FACTURA_VENTA.Add(this);
                }
                if (UNID_MONEDA != MONEDA.UNID_MONEDA)
                {
                    UNID_MONEDA = MONEDA.UNID_MONEDA;
                }
            }
            else if (!_settingFK)
            {
                UNID_MONEDA = null;
            }
        }
    
        private void FixupMASTER_INVENTARIOS(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MASTER_INVENTARIOS item in e.NewItems)
                {
                    item.FACTURA_VENTA = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MASTER_INVENTARIOS item in e.OldItems)
                {
                    if (ReferenceEquals(item.FACTURA_VENTA, this))
                    {
                        item.FACTURA_VENTA = null;
                    }
                }
            }
        }
    
        private void FixupMOVIMENTOes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MOVIMENTO item in e.NewItems)
                {
                    item.FACTURA_VENTA = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MOVIMENTO item in e.OldItems)
                {
                    if (ReferenceEquals(item.FACTURA_VENTA, this))
                    {
                        item.FACTURA_VENTA = null;
                    }
                }
            }
        }

        #endregion
    }
}
