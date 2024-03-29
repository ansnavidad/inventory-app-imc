﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;


namespace InventoryApp.Model
{
    public class InsertTipoEmpresaModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidTipoEmpresa;
        private string _tipoEmpresaName;
        private TipoEmpresaDataMapper _dataMapper;
        private USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidTipoEmpresa
        {
            get
            {
                return _unidTipoEmpresa;
            }
            set
            {
                if (_unidTipoEmpresa != value)
                {
                    _unidTipoEmpresa = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTipoEmpresa"));
                    }
                }
            }
        }

        public string TipoEmpresaName
        {
            get
            {
                return _tipoEmpresaName;
            }
            set
            {
                if (_tipoEmpresaName != value)
                {
                    _tipoEmpresaName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoEmpresaName"));
                    }
                }
            }
        }
        #endregion

        public void saveTipoEmpresa()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new TIPO_EMPRESA() { IS_ACTIVE = true, TIPO_EMPRESA_NAME = this._tipoEmpresaName }, this.ActualUser);
            }
        }

        public void updateTipoEmpresa()
        {
            this._dataMapper.udpateElement(new TIPO_EMPRESA() { UNID_TIPO_EMPRESA = this._unidTipoEmpresa, TIPO_EMPRESA_NAME = this._tipoEmpresaName }, this.ActualUser);
        }

        #region Constructors
        public InsertTipoEmpresaModel(IDataMapper dataMapper, USUARIO u)
        {
            if ((dataMapper as TipoEmpresaDataMapper) != null)
            {
                this._dataMapper = dataMapper as TipoEmpresaDataMapper;
            }
            this.ActualUser = u;
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
