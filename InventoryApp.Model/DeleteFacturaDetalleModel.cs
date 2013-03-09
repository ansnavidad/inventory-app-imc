using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;


namespace InventoryApp.Model
{
    public class DeleteFacturaDetalleModel : FACTURA_DETALLE, INotifyPropertyChanged
    {
        private bool _isChecked;
        private ARTICULO _articulo;
        

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


        public DeleteFacturaDetalleModel(FACTURA_DETALLE facturaDetalle)
        {
            this.PRECIO_UNITARIO = facturaDetalle.PRECIO_UNITARIO;
            this.CANTIDAD = facturaDetalle.CANTIDAD;
            this.IMPUESTO_UNITARIO = facturaDetalle.IMPUESTO_UNITARIO;
            this.Articulo = facturaDetalle.ARTICULO;
            this.UNID_FACTURA_DETALE = facturaDetalle.UNID_FACTURA_DETALE;

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
