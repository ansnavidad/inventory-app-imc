using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class PaisModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidPais;
        private string _pais;
        private string _iso;
        private PaisDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidPais
        {
            get
            {
                return _unidPais;
            }
            set
            {
                if (_unidPais != value)
                {
                    _unidPais = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidPais"));
                    }
                }
            }
        }

        public string Pais
        {
            get
            {
                return _pais;
            }
            set
            {
                if (_pais != value)
                {
                    _pais = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Pais"));
                    }
                }
            }
        }

        public string Iso
        {
            get
            {
                return _iso;
            }
            set
            {
                if (_iso != value)
                {
                    _iso = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Iso"));
                    }
                }
            }
        }

        #endregion

        public void savePais()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new PAI() {IS_ACTIVE=true, PAIS = this._pais, ISO = this._iso });
            }
        }

        public void updatePais()
        {
            this._dataMapper.udpateElement(new PAI() { PAIS = this._pais, ISO = this._iso, UNID_PAIS = this._unidPais });
        }

        #region Constructors
        public PaisModel(IDataMapper dataMapper)
        {
            if ((dataMapper as PaisDataMapper) != null)
            {
                this._dataMapper = dataMapper as PaisDataMapper;
            }

        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
