using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteUnidad : UNIDAD, INotifyPropertyChanged
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

        public DeleteUnidad(UNIDAD unidad)
        {
            this.UNID_UNIDAD = unidad.UNID_UNIDAD;
            this.UNIDAD1 = unidad.UNIDAD1;
            this.IS_ACTIVE = unidad.IS_ACTIVE;
            this.IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
