using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Model.Recibo
{
    public class ReciboModel : ModelBase
    {
        public long UnidRecibo
        {
            get { return _UnidRecibo; }
            set
            {
                if (_UnidRecibo != value)
                {
                    _UnidRecibo = value;
                    OnPropertyChanged(UnidReciboPropertyName);
                }
            }
        }
        private long _UnidRecibo;
        public const string UnidReciboPropertyName = "UnidRecibo";

        public DateTime FechaCreacion
        {
            get { return _FechaCreacion; }
            set
            {
                if (_FechaCreacion != value)
                {
                    _FechaCreacion = value;
                    OnPropertyChanged(FechaCreacionPropertyName);
                }
            }
        }
        private DateTime _FechaCreacion;
        public const string FechaCreacionPropertyName = "FechaCreacion";

        public ReciboStatusModel ReciboStatus
        {
            get { return _ReciboStatus; }
            set
            {
                if (_ReciboStatus != value)
                {
                    _ReciboStatus = value;
                    OnPropertyChanged(ReciboStatusPropertyName);
                }
            }
        }
        private ReciboStatusModel _ReciboStatus;
        public const string ReciboStatusPropertyName = "ReciboStatus";
    }
}
