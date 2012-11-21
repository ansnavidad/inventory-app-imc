using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogMovimiento : MOVIMENTO, INotifyPropertyChanged
    {
        private int _totalItems;
        private DateTime _fecha;
        private string _destino;
        private string _procedencia;
        private string _tipoMovimiento;

        public int TotalItems
        {
            get { return this._totalItems; }
            set
            {
                if (value != this._totalItems)
                {
                    this._totalItems = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TotalItems"));
                }
            }
        }
        public string Destino
        {
            get { return this._destino; }
            set
            {
                if (value != this._destino)
                {
                    this._destino = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Destino"));
                }
            }
        }
        public string Procedencia
        {
            get { return this._procedencia; }
            set
            {
                if (value != this._procedencia)
                {
                    this._procedencia = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Procedencia"));
                }
            }
        }
        public string TipoMovimiento
        {
            get { return this._tipoMovimiento; }
            set
            {
                if (value != this._tipoMovimiento)
                {
                    this._tipoMovimiento = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoMovimiento"));
                }
            }
        }
        public DateTime Fecha
        {
            get { return this._fecha; }
            set
            {
                if (value != this._fecha)
                {
                    this._fecha = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Fecha"));
                }
            }
        }

        public CatalogMovimiento(MOVIMENTO m)
        {            
            this._totalItems = m.MOVIMIENTO_DETALLE.Count;
            this._fecha = m.FECHA_MOVIMIENTO;

            if (m.ALMACEN != null)
                this._destino = m.ALMACEN.ALMACEN_NAME;
            else if (m.PROVEEDOR2 != null)
                this._destino = m.PROVEEDOR2.PROVEEDOR_NAME;
            else if (m.CLIENTE2 != null)
                this._destino = m.CLIENTE2.CLIENTE1;
            else
                this._destino = "";

            if (m.ALMACEN1 != null)
                this._procedencia = m.ALMACEN1.ALMACEN_NAME;
            else if (m.PROVEEDOR != null)
                this._procedencia = m.PROVEEDOR.PROVEEDOR_NAME;
            else if (m.CLIENTE != null)
                this._procedencia = m.CLIENTE.CLIENTE1;
            else
                this._procedencia = "";

            this._tipoMovimiento = m.TIPO_MOVIMIENTO.TIPO_MOVIMIENTO_NAME;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
