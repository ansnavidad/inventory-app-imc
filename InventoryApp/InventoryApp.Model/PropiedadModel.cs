﻿using System;
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
        public USUARIO ActualUser;
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
                _dataMapper.insertElement(new PROPIEDAD() { IS_ACTIVE = true,  PROPIEDAD1 = this._propiedadName }, this.ActualUser);
            }
        }

        public void updatePropiedad()
        {
            this._dataMapper.udpateElement(new PROPIEDAD() { UNID_PROPIEDAD = this._unidPropiedad, PROPIEDAD1 = this._propiedadName }, this.ActualUser);
        }

        #region Constructors
        public PropiedadModel(IDataMapper dataMapper, USUARIO u)
        {
            if ((dataMapper as PropiedadDataMapper) != null)
            {
                this._dataMapper = dataMapper as PropiedadDataMapper;
            }
            this.ActualUser = u;
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
