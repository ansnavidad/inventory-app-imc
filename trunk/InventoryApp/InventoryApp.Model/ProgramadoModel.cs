using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class ProgramadoModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidProgramado;
        private int? _programado;
        private ARTICULO _articulo;
        private CATEGORIA _categoria;
        private MARCA _marca;
        private MODELO _modelo;
        private EQUIPO _equipo;
        private ALMACEN _almacen;
        private ProgramadoDataMapper _dataMapper;
        #endregion

        #region Props

        public long UnidProgramado
        {
            get
            {
                return _unidProgramado;
            }
            set
            {
                if (_unidProgramado != value)
                {
                    _unidProgramado = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidProgramado"));
                    }
                }
            }
        }

       

        public int? Programado
        {
            get
            {
                return _programado;
            }
            set
            {
                if (_programado != value)
                {
                    _programado = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Programado"));
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

         public void saveProgramado()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new PROGRAMADO() { IS_ACTIVE = true, PROGRAMADO1=this._programado,UNID_ALMACEN=this._almacen.UNID_ALMACEN, UNID_ARTICULO=this._articulo.UNID_ARTICULO});
            }
        }

        public void updateProgramado()
        {
            _dataMapper.udpateElement(new PROGRAMADO() { IS_ACTIVE = true, UNID_PROGRAMADO=this._unidProgramado, PROGRAMADO1 = this._programado, UNID_ALMACEN = this._almacen.UNID_ALMACEN, UNID_ARTICULO = this._articulo.UNID_ARTICULO });
        }

        #region Constructors
        public ProgramadoModel(IDataMapper dataMapper)
        {
            if ((dataMapper as ProgramadoDataMapper) != null)
            {
                this._dataMapper = dataMapper as ProgramadoDataMapper;
            }

        }

        public ProgramadoModel()
        {
            this._dataMapper = new ProgramadoDataMapper();
        }
        
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
