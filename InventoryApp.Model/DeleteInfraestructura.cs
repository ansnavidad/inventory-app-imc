using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;


namespace InventoryApp.Model
{
    public class DeleteInfraestructura : INFRAESTRUCTURA, INotifyPropertyChanged
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

        public DeleteInfraestructura(INFRAESTRUCTURA infraestructura)
        {
            this.UNID_INFRAESTRUCTURA = infraestructura.UNID_INFRAESTRUCTURA;
            this.INFRAESTRUCTURA_NAME = infraestructura.INFRAESTRUCTURA_NAME;
            this.IS_ACTIVE = infraestructura.IS_ACTIVE;
            this._isChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
