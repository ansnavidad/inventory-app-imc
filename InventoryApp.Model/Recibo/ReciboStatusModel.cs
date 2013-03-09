using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Model.Recibo
{
    public class ReciboStatusModel:ModelBase
    {
        public long UnidReciboStatus
        {
            get { return _UnidReciboStatus; }
            set
            {
                if (_UnidReciboStatus != value)
                {
                    _UnidReciboStatus = value;
                    OnPropertyChanged(UnidReciboStatusPropertyName);
                }
            }
        }
        private long _UnidReciboStatus;
        public const string UnidReciboStatusPropertyName = "UnidReciboStatus";

        public string StatusName
        {
            get { return _StatusName; }
            set
            {
                if (_StatusName != value)
                {
                    _StatusName = value;
                    OnPropertyChanged(StatusNamePropertyName);
                }
            }
        }
        private string _StatusName;
        public const string StatusNamePropertyName = "StatusName";
    }
}
