using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteTipoCotizacion : TIPO_COTIZACION, INotifyPropertyChanged
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

        public DeleteTipoCotizacion(TIPO_COTIZACION tipoCotizacion)
        {
            this.UNID_TIPO_COTIZACION = tipoCotizacion.UNID_TIPO_COTIZACION;
            this.TIPO_COTIZACION_NAME = tipoCotizacion.TIPO_COTIZACION_NAME;
            this.IS_ACTIVE = tipoCotizacion.IS_ACTIVE;
            this._isChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
