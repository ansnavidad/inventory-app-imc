using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteBanco : BANCO, INotifyPropertyChanged
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

        public DeleteBanco( BANCO banco) 
        {
            this.UNID_BANCO = banco.UNID_BANCO;
            this.BANCO_NAME = banco.BANCO_NAME;
            this.IS_ACTIVE = banco.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
