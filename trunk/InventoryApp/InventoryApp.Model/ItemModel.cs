﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class ItemModel : ITEM, INotifyPropertyChanged
    {
        private bool _isChecked;
        private ARTICULO _articulo;
        private FACTURA_DETALLE _facturaDetalle;
        private ITEM_STATUS _itemStatus;

        public bool IsChecked
        {
            get { return this._isChecked; }
            set
            {
                if (value != this._isChecked)
                {
                    this._isChecked = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }

        public ARTICULO Articulo
        {
            get { return this._articulo; }
            set
            {
                if (value != this._articulo)
                {
                    this._articulo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Articulo"));
                }
            }
        }

        public FACTURA_DETALLE FacturaDetalle
        {
            get { return this._facturaDetalle; }
            set
            {
                if (value != this._facturaDetalle)
                {
                    this._facturaDetalle = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("FacturaDetalle"));
                }
            }
        }

        public ITEM_STATUS ItemStatus
        {
            get { return this._itemStatus; }
            set
            {
                if (value != this._itemStatus)
                {
                    this._itemStatus = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ItemStatus"));
                }
            }
        }

        public ItemModel(ITEM item) 
        {
            this._articulo = item.ARTICULO;
            this.SKU = item.SKU;
            this.NUMERO_SERIE = item.NUMERO_SERIE;
            this._itemStatus = item.ITEM_STATUS;
            this.COSTO_UNITARIO = item.COSTO_UNITARIO;
            this._facturaDetalle = item.FACTURA_DETALLE;
            this.IS_ACTIVE = item.IS_ACTIVE;
            this.IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}