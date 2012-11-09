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

        public ItemModel(ITEM item) 
        {
            this.ARTICULO = item.ARTICULO;
            this.COSTO_UNITARIO = item.COSTO_UNITARIO;
            this.FACTURA_DETALLE = item.FACTURA_DETALLE;
            this.IS_ACTIVE = item.IS_ACTIVE;
            this.ITEM_STATUS = item.ITEM_STATUS;
            this.MOVIMIENTO_DETALLE = item.MOVIMIENTO_DETALLE;
            this.NUMERO_SERIE = item.NUMERO_SERIE;
            this.UNID_ARTICULO = item.UNID_ARTICULO;
            this.UNID_EMPRESA = item.UNID_EMPRESA;
            this.UNID_FACTURA_DETALE = item.UNID_FACTURA_DETALE;
            this.UNID_ITEM = item.UNID_ITEM;
            this.UNID_ITEM_STATUS = item.UNID_ITEM_STATUS;

            this.IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
