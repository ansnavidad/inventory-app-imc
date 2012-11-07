using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class BancoModel
    {
        #region Fields
        private long _unidBanco;
        private string _bancoName;
        private BancoDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidBanco
        {
            get
            {
                return _unidBanco;
            }
            set
            {
                if (_unidBanco != value)
                {
                    _unidBanco = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidBanco"));
                    }
                }
            }
        }

        public string BancoName
        {
            get
            {
                return _bancoName;
            }
            set
            {
                if (_bancoName != value)
                {
                    _bancoName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("BancoName"));
                    }
                }
            }
        }
        #endregion

        public void saveBanco()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new BANCO() { BANCO_NAME = this._bancoName });
            }
        }

        public void updateBanco()
        {
            this._dataMapper.udpateElement(new BANCO() { UNID_BANCO=this._unidBanco,BANCO_NAME=this._bancoName });
        }

        #region Constructors
        public BancoModel(IDataMapper dataMapper)
        {
            if ((dataMapper as BancoDataMapper) != null)
            {
                this._dataMapper = dataMapper as BancoDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
