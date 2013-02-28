using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class TerminoEnvioModel
    {
        #region Fields
        private long _unidTerminoEnvio;
        private string _clave;
        private string _termino;
        private string _significado;
        private bool _generaLotes;
        private TerminoEnvioDataMapper _dataMapper;
        public USUARIO ActualUser;
        #endregion

        #region Props
        public long UnidTerminoEnvio
        {
            get
            {
                return _unidTerminoEnvio;
            }
            set
            {
                if (_unidTerminoEnvio != value)
                {
                    _unidTerminoEnvio = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTerminoEnvio"));
                    }
                }
            }
        }

        public string Clave
        {
            get
            {
                return _clave;
            }
            set
            {
                if (_clave != value)
                {
                    _clave = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Clave"));
                    }
                }
            }
        }

        public string Termino
        {
            get
            {
                return _termino;
            }
            set
            {
                if (_termino != value)
                {
                    _termino = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Termino"));
                    }
                }
            }
        }

        public string Significado
        {
            get
            {
                return _significado;
            }
            set
            {
                if (_significado != value)
                {
                    _significado = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Significado"));
                    }
                }
            }
        }

        public bool GeneraLotes
        {
            get
            {
                return _generaLotes;
            }
            set
            {
                if (_generaLotes != value)
                {
                    _generaLotes = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("GeneraLotes"));
                    }
                }
            }
        }
                
        #endregion

        public void saveTerminoEnvio()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new TERMINO_ENVIO() {IS_ACTIVE=true, GENERA_LOTES = this._generaLotes, CLAVE = this._clave, SIGNIFICADO = this._significado, TERMINO = this._termino });
            }
        }

        public void updateTerminoEnvio()
        {
            this._dataMapper.udpateElement(new TERMINO_ENVIO() { UNID_TERMINO_ENVIO = this._unidTerminoEnvio, GENERA_LOTES = this._generaLotes, CLAVE = this._clave, SIGNIFICADO = this._significado, TERMINO = this._termino });
        }

        #region Constructors
        public TerminoEnvioModel(IDataMapper dataMapper)
        {
            if ((dataMapper as TerminoEnvioDataMapper) != null)
            {
                this._dataMapper = dataMapper as TerminoEnvioDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
