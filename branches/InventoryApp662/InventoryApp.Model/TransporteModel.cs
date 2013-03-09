using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class TransporteModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidTransporte;
        private string _trasnporteName;
        private TIPO_EMPRESA _tipoEmpresa;
        private TransporteDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        public long UnidTransporte
        {
            get
            {
                return _unidTransporte;
            }
            set
            {
                if (_unidTransporte != value)
                {
                    _unidTransporte = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTransporte"));
                    }
                }
            }
        }

        public string TrasnporteName
        {
            get
            {
                return _trasnporteName;
            }
            set
            {
                if (_trasnporteName != value)
                {
                    _trasnporteName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TrasnporteName"));
                    }
                }
            }
        }

        public TIPO_EMPRESA TipoEmpresa
        {
            get
            {
                return _tipoEmpresa;
            }
            set
            {
                if (_tipoEmpresa != value)
                {
                    _tipoEmpresa = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoEmpresa"));
                    }
                }
            }
        }


        public void saveTransporte()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new TRANSPORTE() { IS_ACTIVE = true, TRANSPORTE_NAME = this._trasnporteName,  UNID_TIPO_EMPRESA= this._tipoEmpresa.UNID_TIPO_EMPRESA});
            }
        }

        public void updateArticulo()
        {
            this._dataMapper.udpateElement(new TRANSPORTE() {  UNID_TRANSPORTE = this._unidTransporte, TRANSPORTE_NAME = this._trasnporteName, UNID_TIPO_EMPRESA = this._tipoEmpresa.UNID_TIPO_EMPRESA });
        }

        #region Constructors
        public TransporteModel(IDataMapper dataMapper)
        {
            if ((dataMapper as TransporteDataMapper) != null)
            {
                this._dataMapper = dataMapper as TransporteDataMapper;
            }

        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
