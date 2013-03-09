using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class ClienteModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidCliente;
        private string _clienteName;
        private ClienteDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidCliente
        {
            get
            {
                return _unidCliente;
            }
            set
            {
                if (_unidCliente != value)
                {
                    _unidCliente = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidCliente"));
                    }
                }
            }
        }

        public string ClienteName
        {
            get
            {
                return _clienteName;
            }
            set
            {
                if (_clienteName != value)
                {
                    _clienteName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ClienteName"));
                    }
                }
            }
        }
        #endregion

        public void saveCliente()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new CLIENTE() { IS_ACTIVE = true, CLIENTE1 = this._clienteName }, this.ActualUser);
            }
        }

        public void updateCliente()
        {
            this._dataMapper.udpateElement(new CLIENTE() { UNID_CLIENTE=this._unidCliente, CLIENTE1=this._clienteName }, this.ActualUser);
        }

        #region Constructors
        public ClienteModel(IDataMapper dataMapper, USUARIO u)
        {
            if ((dataMapper as ClienteDataMapper) != null)
            {
                this._dataMapper = dataMapper as ClienteDataMapper;
            }
            this.ActualUser = u;
        }

        public ClienteModel(IDataMapper dataMapper)
        {
            if ((dataMapper as ClienteDataMapper) != null)
            {
                this._dataMapper = dataMapper as ClienteDataMapper;
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
