using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class CategoriaModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidCategoria;
        private string _categoriaName;
        private CategoriaDataMapper _dataMapper;
        #endregion
        #region Props
        public long UnidCategoria
        {
            get
            {
                return _unidCategoria;
            }
            set
            {
                if (_unidCategoria != value)
                {
                    _unidCategoria = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidCategoria"));
                    }
                }
            }
        }

        public string CategoriaName
        {
            get
            {
                return _categoriaName;
            }
            set
            {
                if (_categoriaName != value)
                {
                    _categoriaName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("CategoriaName"));
                    }
                }
            }
        }
        #endregion
        public void saveCategoria()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new CATEGORIA() { CATEGORIA_NAME= this._categoriaName });
            }
        }
        public void updateCategoria()
        {
            this._dataMapper.udpateElement(new CATEGORIA() { UNID_CATEGORIA= this._unidCategoria, CATEGORIA_NAME = this._categoriaName});
        }

        #region Constructors
        public CategoriaModel(IDataMapper dataMapper)
        {
            if ((dataMapper as CategoriaDataMapper) != null)
            {
                this._dataMapper = dataMapper as CategoriaDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
