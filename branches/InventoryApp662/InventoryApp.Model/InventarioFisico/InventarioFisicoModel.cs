using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Model.InventarioFisico
{
    public class InventarioFisicoModel: ModelBase
    {
        public AlmacenModel Almacen
        {
            get { return _Almacen; }
            set
            {
                if (_Almacen != value)
                {
                    _Almacen = value;
                    OnPropertyChanged(AlmacenPropertyName);
                }
            }
        }
        private AlmacenModel _Almacen;
        public const string AlmacenPropertyName = "Almacen";

        public bool Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(StatusPropertyName);
                }
            }
        }
        private bool _Status;
        public const string StatusPropertyName = "Status";

        public DateTime FechaIni
        {
            get { return _FechaIni; }
            set
            {
                if (_FechaIni != value)
                {
                    _FechaIni = value;
                    OnPropertyChanged(FechaIniPropertyName);
                }
            }
        }
        private DateTime _FechaIni;
        public const string FechaIniPropertyName = "FechaIni";

        public DateTime FechaFin
        {
            get { return _FechaFin; }
            set
            {
                if (_FechaFin != value)
                {
                    _FechaFin = value;
                    OnPropertyChanged(FechaFinPropertyName);
                }
            }
        }
        private DateTime _FechaFin;
        public const string FechaFinPropertyName = "FechaFin";

        public Int32 Cantidad
        {
            get { return _Cantidad; }
            set
            {
                if (_Cantidad != value)
                {
                    _Cantidad = value;
                    OnPropertyChanged(CantidadPropertyName);
                }
            }
        }
        private Int32 _Cantidad;
        public const string CantidadPropertyName = "Cantidad";

        public InventarioFisico.InventarioFisicoDetalleModel Detalles
        {
            get { return _Detalles; }
            set
            {
                if (_Detalles != value)
                {
                    _Detalles = value;
                    OnPropertyChanged(DetallesPropertyName);
                }
            }
        }
        private InventarioFisico.InventarioFisicoDetalleModel _Detalles;
        public const string DetallesPropertyName = "Detalles";

        public long unid
        {
            get { return _unid; }
            set
            {
                if (_unid != value)
                {
                    _unid = value;
                    OnPropertyChanged(unidPropertyName);
                }
            }
        }
        private long _unid;
        public const string unidPropertyName = "unid";
    }
}
