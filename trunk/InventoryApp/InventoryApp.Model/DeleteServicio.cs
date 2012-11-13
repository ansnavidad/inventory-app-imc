using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteServicio : SERVICIO, INotifyPropertyChanged
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

        public DeleteServicio(SERVICIO servicio)
        {
            this.UNID_SERVICIO = servicio.UNID_SERVICIO;
            this.SERVICIO_NAME = servicio.SERVICIO_NAME;
            this.IS_ACTIVE = servicio.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
