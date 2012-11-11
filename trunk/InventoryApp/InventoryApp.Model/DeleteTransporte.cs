using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteTransporte : TRANSPORTE, INotifyPropertyChanged
    {
        private bool _isChecked;
        private TIPO_EMPRESA _tipoEmpresa;

        public TIPO_EMPRESA TipoEmpresa
        {
            get { return this._tipoEmpresa; }
            set
            {
                if (value != this._tipoEmpresa)
                {
                    this._tipoEmpresa = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoEmpresa"));
                }
            }
        }
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


        public DeleteTransporte(TRANSPORTE transporte)
        {
            this.UNID_TRANSPORTE = transporte.UNID_TRANSPORTE;
            this.TRANSPORTE_NAME = transporte.TRANSPORTE_NAME;
            this._tipoEmpresa = transporte.TIPO_EMPRESA;
            this.IS_ACTIVE = transporte.IS_ACTIVE;
            this._isChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
