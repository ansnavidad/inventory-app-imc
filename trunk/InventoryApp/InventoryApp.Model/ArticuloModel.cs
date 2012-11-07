﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;


namespace InventoryApp.Model 
{
    public class ArticuloModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidArticulo;
        private string _articuloName;
        private CATEGORIA _categoria;
        private MARCA _marca;
        private MODELO _modelo;
        private EQUIPO _equipo;
        private ArticuloDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidArticulo
        {
            get
            {
                return _unidArticulo;
            }
            set
            {
                if (_unidArticulo != value)
                {
                    _unidArticulo = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidArticulo"));
                    }
                }
            }
        }

        public string ArticuloName
        {
            get
            {
                return _articuloName;
            }
            set
            {
                if (_articuloName != value)
                {
                    _articuloName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ArticuloName"));
                    }
                }
            }
        }

        public CATEGORIA Categoria
        {
            get
            {
                return _categoria;
            }
            set
            {
                if (_categoria != value)
                {
                    _categoria = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Categoria"));
                    }
                }
            }
        }

        public MARCA Marca
        {
            get
            {
                return _marca;
            }
            set
            {
                if (_marca != value)
                {
                    _marca = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Marca"));
                    }
                }
            }
        }

        public EQUIPO Equipo
        {
            get
            {
                return _equipo;
            }
            set
            {
                if (_equipo != value)
                {
                    _equipo = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Equipo"));
                    }
                }
            }
        }

        public MODELO Modelo
        {
            get
            {
                return _modelo;
            }
            set
            {
                if (_modelo != value)
                {
                    _modelo = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Modelo"));
                    }
                }
            }
        }
        #endregion

        public void saveArticulo()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new ARTICULO() { ARTICULO1 = this._articuloName, UNID_CATEGORIA = this._categoria.UNID_CATEGORIA, UNID_EQUIPO = this._equipo.UNID_EQUIPO, UNID_MODELO = this._modelo.UNID_MODELO, UNID_MARCA =this._marca.UNID_MARCA });
            }
        }

        public void updateArticulo()
        {
            this._dataMapper.udpateElement(new ARTICULO() { UNID_ARTICULO=this._unidArticulo, ARTICULO1 = this._articuloName, UNID_CATEGORIA = this._categoria.UNID_CATEGORIA, UNID_EQUIPO = this._equipo.UNID_EQUIPO, UNID_MODELO = this._modelo.UNID_MODELO, UNID_MARCA =this._marca.UNID_MARCA });
        }

        #region Constructors
        public ArticuloModel(IDataMapper dataMapper)
        {
            if ((dataMapper as ArticuloDataMapper) != null)
            {
                this._dataMapper = dataMapper as ArticuloDataMapper;
            }

        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
    }
}