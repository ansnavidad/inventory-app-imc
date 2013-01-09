using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class AgregarItemDestinoModel : INotifyPropertyChanged 
    {
        private int _cantidad;
        private PROVEEDOR _proveedor;
        private CLIENTE _cliente;
        private ALMACEN _almacen;

        #region Props

        public PROVEEDOR Proveedor
        {
            get { return this._proveedor; }
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

        public int Cantidad
        {
            get { return this._cantidad; }
            set
            {
                if (value != this._cantidad)
                {
                    this._cantidad = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Cantidad"));
                }
            }
        }

        public CLIENTE Cliente
        {
            get { return this._cliente; }
            set
            {
                if (value != this._cliente)
                {
                    this._cliente = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Cliente"));
                }
            }
        }


        public ALMACEN Almacen
        {
            get { return this._almacen; }
            set
            {
                if (value != this._almacen)
                {
                    this._almacen = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Almacen"));
                }
            }
        }

        #endregion

        #region Constructores

        public AgregarItemDestinoModel()
        {
           
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
