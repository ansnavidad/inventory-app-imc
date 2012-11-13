using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class PropiedadModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidPropiedad;
        private string _propiedadName;
        private PropiedadDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidPropiedad
        {
            get
            {
                return _unidPropiedad;
            }
            set
            {
                if (_unidPropiedad != value)
                {
                    _unidPropiedad = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidPropiedad"));
                    }
                }
            }
        }

        public string PropiedadName
        {
            get
            {
                return _propiedadName;
            }
            set
            {
                if (_propiedadName != value)
                {
                    _propiedadName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PropiedadName"));
                    }
                }
            }
        }
        #endregion

        public void savePropiedad()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new PROPIEDAD() { IS_ACTIVE = true,  PROPIEDAD1 = this._propiedadName });
            }
        }

        public void updatePropiedad()
        {
            this._dataMapper.udpateElement(new PROPIEDAD() {  UNID_PROPIEDAD=this._unidPropiedad, PROPIEDAD1=this._propiedadName });
        }

        #region Constructors
        public PropiedadModel(IDataMapper dataMapper)
        {
            if ((dataMapper as PropiedadDataMapper) != null)
            {
                this._dataMapper = dataMapper as PropiedadDataMapper;
            }
            
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
