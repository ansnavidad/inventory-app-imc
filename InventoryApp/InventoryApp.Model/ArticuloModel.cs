using System;
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
        private EquipoModel _equipoModel;
        private CATEGORIA _categoria;
        private MARCA _marca;
        private MODELO _modelo;
        private EQUIPO _equipo;
        private ArticuloDataMapper _dataMapper;
        #endregion

        #region Props
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
            CategoriaDataMapper catDM = new CategoriaDataMapper();
            MarcaDataMapper marDM = new MarcaDataMapper();
            ModeloDataMapper modDM = new ModeloDataMapper();
            EquipoDataMapper equDM = new EquipoDataMapper();

            catDM.insertElement(new CATEGORIA() { UNID_CATEGORIA = this._categoria.UNID_CATEGORIA, IS_MODIFIED = true, IS_ACTIVE = true, CATEGORIA_NAME = this._categoria.CATEGORIA_NAME});
            marDM.insertElement(new MARCA() { UNID_MARCA = this._marca.UNID_MARCA, IS_ACTIVE = true, IS_MODIFIED = true, MARCA_NAME = this._marca.MARCA_NAME });
            modDM.insertElement(new MODELO() { UNID_MODELO = this._modelo.UNID_MODELO, IS_ACTIVE = true, IS_MODIFIED = true, MODELO_NAME = this._modelo.MODELO_NAME });
            equDM.insertElement(new EQUIPO() { UNID_EQUIPO = this._equipo.UNID_EQUIPO, IS_ACTIVE = true, IS_MODIFIED = true, EQUIPO_NAME = this._equipo.EQUIPO_NAME });

            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new ARTICULO() { IS_ACTIVE=true ,ARTICULO1 = this._articuloName, UNID_CATEGORIA = this._categoria.UNID_CATEGORIA, UNID_EQUIPO = this._equipo.UNID_EQUIPO, UNID_MODELO = this._modelo.UNID_MODELO, UNID_MARCA =this._marca.UNID_MARCA });
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
        public ArticuloModel()
        {
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
