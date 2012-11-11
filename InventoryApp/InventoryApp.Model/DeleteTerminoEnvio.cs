using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteTerminoEnvio : TERMINO_ENVIO, INotifyPropertyChanged
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

        public DeleteTerminoEnvio(TERMINO_ENVIO terminoEnvio)
        {
            this.UNID_TERMINO_ENVIO = terminoEnvio.UNID_TERMINO_ENVIO;
            this.CLAVE = terminoEnvio.CLAVE;
            this.GENERA_LOTES = terminoEnvio.GENERA_LOTES;
            this.SIGNIFICADO = terminoEnvio.SIGNIFICADO;
            this.TERMINO = terminoEnvio.TERMINO;
            this.IS_ACTIVE = terminoEnvio.IS_ACTIVE;
            this._isChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
