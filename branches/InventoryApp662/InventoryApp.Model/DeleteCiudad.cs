using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteCiudad : CIUDAD, INotifyPropertyChanged
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
        public DeleteCiudad(CIUDAD ciudad) 
        {
            this.UNID_CIUDAD = ciudad.UNID_CIUDAD;
            this.CIUDAD1 = ciudad.CIUDAD1;
            this.ISO = ciudad.ISO;
            this.IS_ACTIVE = ciudad.IS_ACTIVE;
            this.IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
