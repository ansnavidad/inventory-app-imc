using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Model.Recibo
{
    public class TipoOrigenModel:ModelBase
    {
        public string TipoOrigenName
        {
            get { return _TipoOrigenName; }
            set
            {
                if (_TipoOrigenName != value)
                {
                    _TipoOrigenName = value;
                    OnPropertyChanged(TipoOrigenNamePropertyName);
                }
            }
        }
        private string _TipoOrigenName;
        public const string TipoOrigenNamePropertyName = "TipoOrigenName";

        public TipoOrigenModel():this("")
        {
        }

        public TipoOrigenModel(string tipoOrigenName)
        {
            this._TipoOrigenName = tipoOrigenName;
        }
    }
}
