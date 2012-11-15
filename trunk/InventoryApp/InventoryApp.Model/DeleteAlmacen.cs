using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteAlmacen : ALMACEN, INotifyPropertyChanged
    {
        private bool _isChecked;
        private CIUDAD _ciudad;

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

        public CIUDAD Ciudad
        {
            get
            {
                return _ciudad;
            }
            set
            {
                if (_ciudad != value)
                {
                    _ciudad = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Ciudad"));
                    }
                }
            }
        }
        public DeleteAlmacen(ALMACEN almacen)
        {
            this.UNID_ALMACEN = almacen.UNID_ALMACEN;
            this.ALMACEN_NAME = almacen.ALMACEN_NAME;
            this.CONTACTO = almacen.CONTACTO;
            this.DIRECCION = almacen.DIRECCION;
            this.TECNICO = almacen.TECNICO;
            this._ciudad = almacen.CIUDAD;
            this.IS_ACTIVE = almacen.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
