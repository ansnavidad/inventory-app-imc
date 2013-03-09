using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Model.Recibo
{
    public class AlmacenModel:ModelBase,IOrigenModel
    {
        public long UnidAlmacen
        {
            get { return _UnidAlmacen; }
            set
            {
                if (_UnidAlmacen != value)
                {
                    _UnidAlmacen = value;
                    OnPropertyChanged(UnidAlmacenPropertyName);
                }
            }
        }
        private long _UnidAlmacen;
        public const string UnidAlmacenPropertyName = "UnidAlmacen";

        public string AlmacenName
        {
            get { return _AlmacenName; }
            set
            {
                if (_AlmacenName != value)
                {
                    _AlmacenName = value;
                    OnPropertyChanged(AlmacenNamePropertyName);
                }
            }
        }
        private string _AlmacenName;
        public const string AlmacenNamePropertyName = "AlmacenName";

        public string Recibe
        {
            get { return _Recibe; }
            set
            {
                if (_Recibe != value)
                {
                    _Recibe = value;
                    OnPropertyChanged(RecibePropertyName);
                }
            }
        }
        private string _Recibe;
        public const string RecibePropertyName = "Recibe";

        public AlmacenModel()
        {
        }

        public string OrigenName
        {
            get { return this._AlmacenName; }
        }
    }
}
