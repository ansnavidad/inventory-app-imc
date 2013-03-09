using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteCategoria : CATEGORIA, INotifyPropertyChanged
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
        public DeleteCategoria( CATEGORIA categoria) 
        {
            this.UNID_CATEGORIA = categoria.UNID_CATEGORIA;
            this.CATEGORIA_NAME = categoria.CATEGORIA_NAME;
            this.IS_ACTIVE = categoria.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
