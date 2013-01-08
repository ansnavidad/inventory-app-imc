using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class TecnicoModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidTecnico;
        private string _mail;
        private string _tecnicoName;
        private CIUDAD _ciudad;        
        private TecnicoDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidTecnico
        {
            get
            {
                return _unidTecnico;
            }
            set
            {
                if (_unidTecnico != value)
                {
                    _unidTecnico = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTecnico"));
                    }
                }
            }
        }
        
        public string TecnicoName
        {
            get
            {
                return _tecnicoName;
            }
            set
            {
                if (_tecnicoName != value)
                {
                    _tecnicoName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TecnicoName"));
                    }
                }
            }
        }
        
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                if (_mail != value)
                {
                    _mail = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Mail"));
                    }
                }
            }
        }
        
        public CIUDAD Ciudad
        {
            get
            {
                return _ciudad;
            }
            set
            {
                if (_ciudad != value)
                {
                    _ciudad = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Ciudad"));
                    }
                }
            }
        }       
        
        #endregion

        public void saveTecnico()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new TECNICO() { IS_ACTIVE = true,
                                                          MAIL = this._mail,
                                                          TECNICO_NAME = this._tecnicoName,
                                                          UNID_CIUDAD = this._ciudad.UNID_CIUDAD,
                                                          UNID_TECNICO = this.UnidTecnico
                                                         });
            }
        }

        public void updateTipoPedimento()
        {
            this._dataMapper.udpateElement(new TECNICO() { UNID_TECNICO = this._unidTecnico,
                                                           IS_ACTIVE = true,
                                                           MAIL = this._mail,
                                                           TECNICO_NAME = this._tecnicoName,
                                                           UNID_CIUDAD = this._ciudad.UNID_CIUDAD
                                                          });
        }

        #region Constructors
        public TecnicoModel(IDataMapper dataMapper)
        {
            if ((dataMapper as TecnicoDataMapper) != null)
            {
                this._dataMapper = dataMapper as TecnicoDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
