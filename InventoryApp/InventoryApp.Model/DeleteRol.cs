using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{

    public class DeleteRol:ROL, INotifyPropertyChanged
    {
        private bool _isCheckedEliminar;
                
        public bool IsCheckedEliminar
        {
            get { return this._isCheckedEliminar; }
            set
            {
                if (value != this._isCheckedEliminar)
                {
                    this._isCheckedEliminar = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsCheckedEliminar"));
                }
            }
        }

        public DeleteRol(ROL rol)
        {
            this._isCheckedEliminar = false;
            this.IS_ACTIVE = rol.IS_ACTIVE;
            this.IS_SYSTEM_ROOL = rol.IS_SYSTEM_ROOL;
            this.RECIBIR_MAILS = rol.RECIBIR_MAILS;
            this.ROL_NAME = rol.ROL_NAME;
            this.UNID_ROL = rol.UNID_ROL;

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
