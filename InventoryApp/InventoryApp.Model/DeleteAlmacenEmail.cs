using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteAlmacenEmail : ALMACEN_EMAIL, INotifyPropertyChanged
    {
        private bool _isChecked;
        private ALMACEN _almacen;

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

        public ALMACEN Almacen
        {
            get
            {
                return _almacen;
            }
            set
            {
                if (_almacen != value)
                {
                    _almacen = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Almacen"));
                    }
                }
            }
        }

        public DeleteAlmacenEmail(ALMACEN_EMAIL almacenEmail)
        {
            this.UNID_ALMACEN_EMAIL = almacenEmail.UNID_ALMACEN_EMAIL;
            this.EMAIL = almacenEmail.EMAIL;
            this._almacen = almacenEmail.ALMACEN;
            this.IS_DEFAULT = almacenEmail.IS_DEFAULT;
            this.IS_ACTIVE = almacenEmail.IS_ACTIVE;
            this.IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
