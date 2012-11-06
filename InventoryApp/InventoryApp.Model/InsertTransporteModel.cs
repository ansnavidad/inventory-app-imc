using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class InsertTransporteModel : INotifyPropertyChanged
    {

        #region Fields
        private long _unidTransporte;
        private string _transporteName;
        private TIPO_EMPRESA _tipoEmpresa;
        private TransporteDataMapper _dataMapper;
        #endregion

        #region Props
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

        public string TransporteName
        {
            get
            {
                return _transporteName;
            }
            set
            {
                if (_transporteName != value)
                {
                    _transporteName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TransporteName"));
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
        #endregion

        public void saveTransporte()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new TRANSPORTE() { TRANSPORTE_NAME = this._transporteName, UNID_TIPO_EMPRESA = this._tipoEmpresa.UNID_TIPO_EMPRESA });
            }
        }

        public void updateTransporte()
        {
            this._dataMapper.udpateElement(new TRANSPORTE() { TRANSPORTE_NAME = this._transporteName, UNID_TRANSPORTE = this._unidTransporte, UNID_TIPO_EMPRESA = this._tipoEmpresa.UNID_TIPO_EMPRESA });
        }

        #region Constructors
        public InsertTransporteModel(IDataMapper dataMapper)
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
