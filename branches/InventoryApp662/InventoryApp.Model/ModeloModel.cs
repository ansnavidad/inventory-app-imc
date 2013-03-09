using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class ModeloModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidModelo;
        private string _modeloName;
        private ModeloDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidModelo
        {
            get
            {
                return _unidModelo;
            }
            set
            {
                if (_unidModelo != value)
                {
                    _unidModelo = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidModelo"));
                    }
                }
            }
        }

        public string ModeloName
        {
            get
            {
                return _modeloName;
            }
            set
            {
                if (_modeloName != value)
                {
                    _modeloName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ModeloName"));
                    }
                }
            }
        }
        #endregion

        public void saveModelo()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new MODELO() { IS_ACTIVE = true, MODELO_NAME = this._modeloName }, this.ActualUser);
            }
        }

        public void updateModelo()
        {
            this._dataMapper.udpateElement(new MODELO() { UNID_MODELO = this._unidModelo, MODELO_NAME = this._modeloName }, this.ActualUser);
        }

        #region Constructors
        public ModeloModel(IDataMapper dataMapper, USUARIO u)
        {
            if ((dataMapper as ModeloDataMapper) != null)
            {
                this._dataMapper = dataMapper as ModeloDataMapper;
            }
            this.ActualUser = u;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
