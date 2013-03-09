using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteTipoEmpresa : TIPO_EMPRESA, INotifyPropertyChanged
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

        public DeleteTipoEmpresa(TIPO_EMPRESA tipoEmpresa)
        {
            this.UNID_TIPO_EMPRESA = tipoEmpresa.UNID_TIPO_EMPRESA;
            this.TIPO_EMPRESA_NAME = tipoEmpresa.TIPO_EMPRESA_NAME;
            this.IS_ACTIVE = tipoEmpresa.IS_ACTIVE;
            this._isChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
