using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteTecnico: TECNICO, INotifyPropertyChanged
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

        public DeleteTecnico(TECNICO tecnico)
        {
            this.CIUDAD = tecnico.CIUDAD;
            this.IS_ACTIVE = tecnico.IS_ACTIVE;
            this.MAIL = tecnico.MAIL;
            this.TECNICO_NAME = tecnico.TECNICO_NAME;
            this.UNID_CIUDAD = tecnico.UNID_CIUDAD;
            this.UNID_TECNICO = tecnico.UNID_TECNICO;

            this._isChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
