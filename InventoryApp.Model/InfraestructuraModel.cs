using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;


namespace InventoryApp.Model
{
    public class InfraestructuraModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidInfraestructura;
        private string _infraestructuraName;
        private InfraestructuraDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidInfraestructura
        {
            get
            {
                return _unidInfraestructura;
            }
            set
            {
                if (_unidInfraestructura != value)
                {
                    _unidInfraestructura = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidInfraestructura"));
                    }
                }
            }
        }

        public string InfraestructuraName
        {
            get
            {
                return _infraestructuraName;
            }
            set
            {
                if (_infraestructuraName != value)
                {
                    _infraestructuraName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("InfraestructuraName"));
                    }
                }
            }
        }
        #endregion

        public void saveInfraestructura()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new INFRAESTRUCTURA() { IS_ACTIVE = true, INFRAESTRUCTURA_NAME = this._infraestructuraName }, this.ActualUser);
            }
        }

        public void updateInfraestructura()
        {
            this._dataMapper.udpateElement(new INFRAESTRUCTURA() { UNID_INFRAESTRUCTURA = this._unidInfraestructura, INFRAESTRUCTURA_NAME = this._infraestructuraName }, this.ActualUser);
        }

        #region Constructors
        public InfraestructuraModel(IDataMapper dataMapper, USUARIO u)
        {
            if ((dataMapper as InfraestructuraDataMapper) != null)
            {
                this._dataMapper = dataMapper as InfraestructuraDataMapper;
            }
            this.ActualUser = u;
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
