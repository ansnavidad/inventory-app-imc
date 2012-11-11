using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteProveedor : PROVEEDOR, INotifyPropertyChanged
    {
        private bool _isChecked;
        private PAI _pais;
        private CIUDAD _ciudad;

        public PAI Pais
        {
            get { return this._pais; }
            set
            {
                if (value != this._pais)
                {
                    this._pais = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Pais"));
                }
            }
        }

        public CIUDAD Ciudad
        {
            get { return this._ciudad; }
            set
            {
                if (value != this._ciudad)
                {
                    this._ciudad = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Ciudad"));
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

       

        public DeleteProveedor( PROVEEDOR proveedor) 
        {
            this.UNID_PROVEEDOR = proveedor.UNID_PROVEEDOR;
            this.PROVEEDOR_NAME = proveedor.PROVEEDOR_NAME;
            this._pais = proveedor.PAI;
            this._ciudad = proveedor.CIUDAD;
            this.UNID_PAIS = proveedor.UNID_PAIS;
            this.UNID_CIUDAD = proveedor.UNID_CIUDAD;
            this.CONTACTO = proveedor.CONTACTO;
            this.TEL1 = proveedor.TEL1;
            this.TEL2 = proveedor.TEL2;
            this.MAIL = proveedor.MAIL;
            this.CALLE = proveedor.CALLE;
            this.CODIGO_POSTAL = proveedor.CODIGO_POSTAL;
            this.RFC = proveedor.RFC;
            this.IS_ACTIVE = proveedor.IS_ACTIVE;
            this.IsChecked = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
