using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

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
        private EquipoModel _equipoModel;
        private ALMACEN _almacen;
        private bool _isChecked;
        private MaxMinDataMapper _dataMapper;
        private string _mensajeError;
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

        public EquipoModel EquipoModel
        {
            get
            {
                return _equipoModel;
            }
            set
            {
                if (_equipoModel != value)
                {
                    _equipoModel = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("EquipoModel"));
                    }
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

        public string MensajeError
        {
            get
            {
                return _mensajeError;
            }
            set
            {
                if (_mensajeError != value)
                {
                    _mensajeError = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("MensajeError"));
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

        public void DeleteMaxMin()
        {
            if (_dataMapper != null)
            {
                _dataMapper.deleteElement(new MAX_MIN() { UNID_MAX_MIN=this.UnidMaxMin});
            }
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
