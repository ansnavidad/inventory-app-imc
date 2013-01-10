﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class MaxMinModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidMaxMin;
        private int? _min;
        private int? _max;
        private ARTICULO _articulo;
        private CATEGORIA _categoria;
        private MARCA _marca;
        private MODELO _modelo;
        private EQUIPO _equipo;
        private ALMACEN _almacen;
        private MaxMinDataMapper _dataMapper;
        #endregion

        #region Props

        public long UnidMaxMin
        {
            get
            {
                return _unidMaxMin;
            }
            set
            {
                if (_unidMaxMin != value)
                {
                    _unidMaxMin = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidMaxMin"));
                    }
                }
            }
        }

        public int? Min
        {
            get
            {
                return _min;
            }
            set
            {
                if (_min != value)
                {
                    _min = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Min"));
                    }
                }
            }
        }

        public int? Max
        {
            get
            {
                return _max;
            }
            set
            {
                if (_max != value)
                {
                    _max = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Max"));
                    }
                }
            }
        }

        public ARTICULO Articulo
        {
            get
            {
                return _articulo;
            }
            set
            {
                if (_articulo != value)
                {
                    _articulo = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Articulo"));
                    }
                }
            }
        }

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

        public ALMACEN Almacen
        {
            get
            {
                return _almacen;
            }
            set
            {
                if (_almacen != value)
                {
                    _almacen = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Almacen"));
                    }
                }
            }
        }

        #endregion


        public void saveMaxMin()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new MAX_MIN() { IS_ACTIVE = true, MAX=this._max, MIN=this._min, UNID_ALMACEN=this._almacen.UNID_ALMACEN, UNID_ARTICULO=this._articulo.UNID_ARTICULO});
            }
        }

        public void updateMaxMin()
        {
            _dataMapper.udpateElement(new MAX_MIN() { IS_ACTIVE = true, UNID_MAX_MIN=this._unidMaxMin, MAX = this._max, MIN = this._min, UNID_ALMACEN = this._almacen.UNID_ALMACEN, UNID_ARTICULO = this._articulo.UNID_ARTICULO });
        }

        #region Constructors
        public MaxMinModel(IDataMapper dataMapper)
        {
            if ((dataMapper as MaxMinDataMapper) != null)
            {
                this._dataMapper = dataMapper as MaxMinDataMapper;
            }

        }

        public MaxMinModel()
        {
            this._dataMapper = new MaxMinDataMapper();
        }
        
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}