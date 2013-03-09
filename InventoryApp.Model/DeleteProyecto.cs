using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteProyecto : PROYECTO, INotifyPropertyChanged
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

        public DeleteProyecto(PROYECTO proyecto)
        {
            this.UNID_PROYECTO = proyecto.UNID_PROYECTO;
            this.PROYECTO_NAME = proyecto.PROYECTO_NAME;
            this.IS_ACTIVE = proyecto.IS_ACTIVE;
            this.IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
