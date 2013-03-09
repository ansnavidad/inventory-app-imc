using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteProveedorCuenta : PROVEEDOR_CUENTA, INotifyPropertyChanged
    {
        private bool _isChecked;
        private BANCO _banco;
        private PROVEEDOR _proveedor;


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

        public BANCO Banco
        {
            get
            {
                return this._banco;
            }
            set
            {
                if (value != this._banco)
                {
                    this._banco = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Banco"));
                }
            }
        }

        public PROVEEDOR Proveedor
        {
            get
            {
                return this._proveedor;
            }
            set
            {
                if (value != this._proveedor)
                {
                    this._proveedor = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Proveedor"));
                }
            }
        }


        public DeleteProveedorCuenta( PROVEEDOR_CUENTA proveedorCuenta)
        {
            this.UNID_PROVEEDOR_CUENTA = proveedorCuenta.UNID_PROVEEDOR_CUENTA;
            this.UNID_BANCO = proveedorCuenta.UNID_BANCO;
            this.UNID_PROVEEDOR = proveedorCuenta.UNID_PROVEEDOR;
            this.NUMERO_CUENTA = proveedorCuenta.NUMERO_CUENTA;
            this.CLABE = proveedorCuenta.CLABE;
            this.BENEFICIARIO = proveedorCuenta.BENEFICIARIO;
            this._banco = proveedorCuenta.BANCO;
            this._proveedor = proveedorCuenta.PROVEEDOR;
            this.IS_ACTIVE = proveedorCuenta.IS_ACTIVE;
            this.IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
