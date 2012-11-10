using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteMoneda : MONEDA, INotifyPropertyChanged
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

        public DeleteMoneda( MONEDA moneda)
        {
            this.UNID_MONEDA = moneda.UNID_MONEDA;
            this.MONEDA_NAME = moneda.MONEDA_NAME;
            this.MONEDA_ABR = moneda.MONEDA_ABR;
            this.IS_ACTIVE = moneda.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
