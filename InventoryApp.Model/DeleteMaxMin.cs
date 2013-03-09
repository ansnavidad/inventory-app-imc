using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteMaxMin : MAX_MIN, INotifyPropertyChanged
    {
        private bool _isChecked;
        private ALMACEN _almacen;
        private ARTICULO _articulo;
        private CATEGORIA _categoria;

        private MARCA _marca;

        private MODELO _modelo;

        private EQUIPO _equipo;

        public EQUIPO Equipo
        {
            get
            {
                return this._equipo;
            }
            set
            {
                if (value != this._equipo)
                {
                    this._equipo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Equipo"));
                }
            }
        }

        public MODELO Modelo
        {
            get
            {
                return this._modelo;
            }
            set
            {
                if (value != this._modelo)
                {
                    this._modelo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Modelo"));
                }
            }
        }

        public MARCA Marca
        {
            get
            {
                return this._marca;
            }
            set
            {
                if (value != this._marca)
                {
                    this._marca = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Marca"));
                }
            }
        }

        public CATEGORIA Categoria
        {
            get
            {
                return this._categoria;
            }
            set
            {
                if (value != this._categoria)
                {
                    this._categoria = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Categoria"));
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

        public ALMACEN Almacen
        {
            get
            {
                return this._almacen;
            }
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

        public ARTICULO Articulo
        {
            get
            {
                return this._articulo;
            }
            set
            {
                if (value != this._articulo)
                {
                    this._articulo = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Articulo"));
                }
            }
        }

        public DeleteMaxMin(MAX_MIN maxMin) 
        {
            this.UNID_MAX_MIN = maxMin.UNID_MAX_MIN;
            this.MAX = maxMin.MAX;
            this.MIN = maxMin.MIN;
            this._almacen = maxMin.ALMACEN;
            this._articulo = maxMin.ARTICULO;
            this._categoria = maxMin.ARTICULO.CATEGORIA;
            this._equipo = maxMin.ARTICULO.EQUIPO;
            this._marca = maxMin.ARTICULO.MARCA;
            this._modelo = maxMin.ARTICULO.MODELO;
            this.IS_ACTIVE = maxMin.IS_ACTIVE;
            this.IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
