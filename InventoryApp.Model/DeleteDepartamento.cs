using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteDepartamento : DEPARTAMENTO, INotifyPropertyChanged
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

        public DeleteDepartamento(DEPARTAMENTO departamento)
        {
            this.UNID_DEPARTAMENTO = departamento.UNID_DEPARTAMENTO;
            this.DEPARTAMENTO_NAME = departamento.DEPARTAMENTO_NAME;
            this.IS_ACTIVE = departamento.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
