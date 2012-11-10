using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteModelo : MODELO, INotifyPropertyChanged
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

        public DeleteModelo(MODELO modelo)
        {
            this.UNID_MODELO = modelo.UNID_MODELO;
            this.MODELO_NAME = modelo.MODELO_NAME;
            this.IS_ACTIVE = modelo.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler  PropertyChanged;
    }
}
