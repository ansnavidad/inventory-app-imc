using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class MarcaModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidMarca;
        private string _marcaName;
        private MarcaDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidMarca
        {
            get
            {
                return _unidMarca;
            }
            set
            {
                if (_unidMarca != value)
                {
                    _unidMarca = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidMarca"));
                    }
                }
            }
        }

        public string MarcaName
        {
            get
            {
                return _marcaName;
            }
            set
            {
                if (_marcaName != value)
                {
                    _marcaName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("MarcaName"));
                    }
                }
            }
        }
        #endregion

        public void saveMarca()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new MARCA() {IS_ACTIVE=true, MARCA_NAME = this._marcaName });
            }
        }

        public void updateMarca()
        {
            this._dataMapper.udpateElement(new MARCA() { UNID_MARCA=this._unidMarca, MARCA_NAME=this._marcaName });
        }

        #region Constructors
        public MarcaModel(IDataMapper dataMapper)
        {
            if ((dataMapper as MarcaDataMapper) != null)
            {
                this._dataMapper = dataMapper as MarcaDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
