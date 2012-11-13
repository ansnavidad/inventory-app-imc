using System;
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
        private string _nombre;
        private long _unidItem;
        private ARTICULO _articulo;
        private FACTURA_DETALLE _facturaDetalle;
        private ITEM_STATUS _itemStatus;

        private MODELO _modelo;
        private MARCA _marca;
        private CATEGORIA _categoria;
        private EQUIPO _equipo;

        public MODELO Modelo
        {
            get { return this._modelo; }
            set
            {
                if (value != this._modelo)
                {
                    this._modelo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Modelo"));
                }
            }
        }

        public MARCA Marca
        {
            get { return this._marca; }
            set
            {
                if (value != this._marca)
                {
                    this._marca = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Marca"));
                }
            }
        }

        public CATEGORIA Categoria
        {
            get { return this._categoria; }
            set
            {
                if (value != this._categoria)
                {
                    this._categoria = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Categoria"));
                }
            }
        }

        public EQUIPO Equipo
        {
            get { return this._equipo; }
            set
            {
                if (value != this._equipo)
                {
                    this._equipo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Equipo"));
                }
            }
        }

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

        public long UnidItem
        {
            get { return this._unidItem; }
            set
            {
                if (value != this._unidItem)
                {
                    this._unidItem = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidItem"));
                }
            }
        }

        public string Nombre
        {
            get { return this._nombre; }
            set
            {
                if (value != this._nombre)
                {
                    this._nombre = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Nombre"));
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
            this._nombre = item.ARTICULO.ARTICULO1;
            this._unidItem = item.UNID_ITEM;
            this.SKU = item.SKU;
            this.NUMERO_SERIE = item.NUMERO_SERIE;
            this._itemStatus = item.ITEM_STATUS;
            this.COSTO_UNITARIO = item.COSTO_UNITARIO;
            //this._facturaDetalle = item.FACTURA_DETALLE;
            this.IS_ACTIVE = item.IS_ACTIVE;
            this.IsChecked = false;
            this._equipo = item.ARTICULO.EQUIPO;
            this._categoria = item.ARTICULO.CATEGORIA;
            this._marca = item.ARTICULO.MARCA;
            this._modelo = item.ARTICULO.MODELO;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
