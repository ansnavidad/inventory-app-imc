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
        
        public DeleteAlmacen(ALMACEN almacen)
        {
            this.UNID_ALMACEN = almacen.UNID_ALMACEN;
            this.ALMACEN_NAME = almacen.ALMACEN_NAME;
            this.CONTACTO = almacen.CONTACTO;
            this.MAIL = almacen.MAIL;
            this.MAIL_DEFAULT = almacen.MAIL_DEFAULT;
            this.DIRECCION = almacen.DIRECCION;
            this.IS_ACTIVE = almacen.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
