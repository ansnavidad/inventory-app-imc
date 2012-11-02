using System;
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
        private TIPO_EMPRESA _newEmpresa;
        private IDataMapper _dataMapper;

        public TIPO_EMPRESA NewEmpresa
        {
            get
            {
                return _newEmpresa;
            }
            set
            {
                if (_newEmpresa != value)
                {
                    _newEmpresa = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedEmpresa"));
                    }
                }
            }
        }


        public InsertTipoEmpresaModel()
        {
            this._dataMapper = new TipoEmpresaDataMapper();
            this._newEmpresa = new TIPO_EMPRESA();
            _newEmpresa.TIPO_EMPRESA_NAME="JuanPablo";
            _newEmpresa.UNID_TIPO_EMPRESA=123564;
;        }

        public void insertItems()
        {
            this._dataMapper.insertElement(_newEmpresa);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
