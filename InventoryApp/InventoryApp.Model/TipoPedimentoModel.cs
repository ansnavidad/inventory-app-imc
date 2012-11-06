using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class TipoPedimentoModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidTipoPedimento;
        private string _tipoPedimentoName;
        private string _clave;
        private string _regimen;
        private string _nota;
        private TipoPedimentoDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidTipoPedimento
        {
            get
            {
                return _unidTipoPedimento;
            }
            set
            {
                if (_unidTipoPedimento != value)
                {
                    _unidTipoPedimento = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidTipoPedimento"));
                    }
                }
            }
        }
        
        public string TipoPedimentoName
        {
            get
            {
                return _tipoPedimentoName;
            }
            set
            {
                if (_tipoPedimentoName != value)
                {
                    _tipoPedimentoName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoPedimentoName"));
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
        
        public string Regimen
        {
            get
            {
                return _regimen;
            }
            set
            {
                if (_regimen != value)
                {
                    _regimen = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Regimen"));
                    }
                }
            }
        }
        
        public string Nota
        {
            get
            {
                return _nota;
            }
            set
            {
                if (_nota != value)
                {
                    _nota = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Nota"));
                    }
                }
            }
        }
        #endregion

        public void saveTipoPedimento()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new TIPO_PEDIMENTO() { TIPO_PEDIMENTO_NAME = this._tipoPedimentoName, CLAVE = this._clave, NOTA = this._nota, REGIMEN = this._regimen });
            }
        }

        public void updateTipoPedimento()
        {
            this._dataMapper.udpateElement(new TIPO_PEDIMENTO() { UNID_TIPO_PEDIMENTO=this._unidTipoPedimento, TIPO_PEDIMENTO_NAME = this._tipoPedimentoName, CLAVE = this._clave, NOTA = this._nota, REGIMEN = this._regimen});
        }

        #region Constructors
        public TipoPedimentoModel(IDataMapper dataMapper)
        {
            if ((dataMapper as TipoPedimentoDataMapper) != null)
            {
                this._dataMapper = dataMapper as TipoPedimentoDataMapper;
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
