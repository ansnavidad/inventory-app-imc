using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteTipoMovimiento : TIPO_MOVIMIENTO, INotifyPropertyChanged
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

        public DeleteTipoMovimiento(TIPO_MOVIMIENTO tipoMovimiento)
        {
            this.UNID_TIPO_MOVIMIENTO = tipoMovimiento.UNID_TIPO_MOVIMIENTO;
            this.TIPO_MOVIMIENTO_NAME = tipoMovimiento.TIPO_MOVIMIENTO_NAME;
            this.SIGNO_MOVIMIENTO = tipoMovimiento.SIGNO_MOVIMIENTO;
            this.IS_ACTIVE = tipoMovimiento.IS_ACTIVE;
            this._isChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
