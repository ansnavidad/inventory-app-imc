using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteEmpresa : EMPRESA, INotifyPropertyChanged
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
        public DeleteEmpresa( EMPRESA empresa)
        {
            this.UNID_EMPRESA = empresa.UNID_EMPRESA;
            this.DIRECCION = empresa.DIRECCION;
            this.EMPRESA_NAME = empresa.EMPRESA_NAME;
            this.RAZON_SOCIAL = empresa.RAZON_SOCIAL;
            this.RFC = empresa.RFC;
            this.IS_ACTIVE = empresa.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
