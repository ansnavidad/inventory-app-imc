using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteMedioEnvio : MEDIO_ENVIO, INotifyPropertyChanged
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

        public DeleteMedioEnvio(MEDIO_ENVIO medioEnvio)
        {
            this.UNID_MEDIO_ENVIO = medioEnvio.UNID_MEDIO_ENVIO;
            this.MEDIO_ENVIO_NAME = medioEnvio.MEDIO_ENVIO_NAME;
            this.IS_ACTIVE = medioEnvio.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
