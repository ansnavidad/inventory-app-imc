using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class DepartamentoModel 
    {
        #region Fields
        private long _unidDepartamento;
        private string _departamentoName;
        private DepartamentoDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidDepartamento
        {
            get
            {
                return _unidDepartamento;
            }
            set
            {
                if (_unidDepartamento != value)
                {
                    _unidDepartamento = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidDepartamento"));
                    }
                }
            }
        }

        public string DepartamentoName
        {
            get
            {
                return _departamentoName;
            }
            set
            {
                if (_departamentoName != value)
                {
                    _departamentoName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("DepartamentoName"));
                    }
                }
            }
        }
        #endregion

        public void saveDeparatamento()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new DEPARTAMENTO() { IS_ACTIVE = true, DEPARTAMENTO_NAME = this._departamentoName }, this.ActualUser);
            }
        }

        public void updateDepartamento()
        {
            this._dataMapper.udpateElement(new DEPARTAMENTO() { UNID_DEPARTAMENTO=this._unidDepartamento,DEPARTAMENTO_NAME=this._departamentoName }, this.ActualUser);
        }

        #region Constructors
        public DepartamentoModel(IDataMapper dataMapper, USUARIO u)
        {
            if ((dataMapper as DepartamentoDataMapper) != null)
            {
                this._dataMapper = dataMapper as DepartamentoDataMapper;
            }
            this.ActualUser = u;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
