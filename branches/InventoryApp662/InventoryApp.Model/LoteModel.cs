using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class LoteModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidLote;
        private long _unidPOM;
        private LoteDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidLote
        {
            get
            {
                return _unidLote;
            }
            set
            {
                if (_unidLote != value)
                {
                    _unidLote = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidLote"));
                    }
                }
            }
        }

        public long UnidPOM
        {
            get
            {
                return _unidPOM;
            }
            set
            {
                if (_unidPOM != value)
                {
                    _unidPOM = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidPOM"));
                    }
                }
            }
        }

        #endregion

        public void saveLote()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new LOTE() { IS_ACTIVE = true, UNID_LOTE = this._unidLote, UNID_POM= this._unidPOM });
            }
        }

        #region Constructors
        public LoteModel(IDataMapper dataMapper)
        {
            if ((dataMapper as LoteDataMapper) != null)
            {
                this._dataMapper = dataMapper as LoteDataMapper;
            }
            
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
