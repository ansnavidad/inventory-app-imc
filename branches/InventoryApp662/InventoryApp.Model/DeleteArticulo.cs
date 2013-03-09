using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteArticulo : ARTICULO, INotifyPropertyChanged
    {
        private bool _isChecked;

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
            get { 
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


        //public DeleteArticulo(object articulo) 
        public DeleteArticulo(ARTICULO articulo) 
        {
            this.UNID_ARTICULO = articulo.UNID_ARTICULO;
            this.ARTICULO1 = articulo.ARTICULO1;
            this._categoria = articulo.CATEGORIA;
            this._marca = articulo.MARCA;
            this._equipo = articulo.EQUIPO;
            this._modelo =articulo.MODELO;
            this.IS_ACTIVE = articulo.IS_ACTIVE;
            this.IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
