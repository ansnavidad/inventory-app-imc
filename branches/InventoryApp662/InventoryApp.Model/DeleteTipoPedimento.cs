using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteTipoPedimento : TIPO_PEDIMENTO, INotifyPropertyChanged
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

        public DeleteTipoPedimento(TIPO_PEDIMENTO tipoPedimento)
        {
            this.UNID_TIPO_PEDIMENTO = tipoPedimento.UNID_TIPO_PEDIMENTO;
            this.CLAVE = tipoPedimento.CLAVE;
            this.NOTA = tipoPedimento.NOTA;
            this.REGIMEN = tipoPedimento.REGIMEN;
            this.TIPO_PEDIMENTO_NAME = tipoPedimento.TIPO_PEDIMENTO_NAME;
            this.IS_ACTIVE = tipoPedimento.IS_ACTIVE;
            this._isChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
