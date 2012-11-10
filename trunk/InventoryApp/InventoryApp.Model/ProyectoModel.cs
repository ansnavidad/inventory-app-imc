using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class ProyectoModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidProyecto;
        private string _proyectoName;
        private ProyectoDataMapper _dataMapper;
        #endregion
        #region Props
        public long UnidProyecto
        {
            get
            {
                return _unidProyecto;
            }
            set
            {
                if (_unidProyecto != value)
                {
                    _unidProyecto = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidProyecto"));
                    }
                }
            }
        }

        public string ProyectoName
        {
            get
            {
                return _proyectoName;
            }
            set
            {
                if (_proyectoName != value)
                {
                    _proyectoName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ProyectoName"));
                    }
                }
            }
        }
        #endregion

        public void saveProyecto()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new PROYECTO() {IS_ACTIVE=true, PROYECTO_NAME = this._proyectoName });
            }
        }
        public void updateProyecto()
        {
            this._dataMapper.udpateElement(new PROYECTO() { UNID_PROYECTO = this._unidProyecto, PROYECTO_NAME = this._proyectoName });
        }

        #region Constructors
        public ProyectoModel(IDataMapper dataMapper)
        {
            if ((dataMapper as ProyectoDataMapper) != null)
            {
                this._dataMapper = dataMapper as ProyectoDataMapper;
            }
            
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
