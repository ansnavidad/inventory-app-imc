using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class EquipoModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidEquipo;
        private string _equipoName;
        private EquipoDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidEquipo
        {
            get
            {
                return _unidEquipo;
            }
            set
            {
                if (_unidEquipo != value)
                {
                    _unidEquipo = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidEquipo"));
                    }
                }
            }
        }

        public string EquipoName
        {
            get
            {
                return _equipoName;
            }
            set
            {
                if (_equipoName != value)
                {
                    _equipoName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("EquipoName"));
                    }
                }
            }
        }
        #endregion

        public void saveEquipo()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new EQUIPO() { IS_ACTIVE=true, EQUIPO_NAME = this._equipoName }, this.ActualUser);
            }
        }

        public void updateEquipo()
        {
            this._dataMapper.udpateElement(new EQUIPO() { UNID_EQUIPO = this._unidEquipo, EQUIPO_NAME = this._equipoName }, this.ActualUser);
        }

        #region Constructors
        public EquipoModel(IDataMapper dataMapper, USUARIO u)
        {
            if ((dataMapper as EquipoDataMapper) != null)
            {
                this._dataMapper = dataMapper as EquipoDataMapper;
            }
            this.ActualUser = u;
        }

        public EquipoModel(IDataMapper dataMapper)
        {
            if ((dataMapper as EquipoDataMapper) != null)
            {
                this._dataMapper = dataMapper as EquipoDataMapper;
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
