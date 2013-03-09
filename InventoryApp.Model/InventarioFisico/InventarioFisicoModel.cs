using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.DAL;

namespace InventoryApp.Model.InventarioFisico
{
    public class InventarioFisicoModel: ModelBase,IModelDataConverter
    {
        private IDataMapper _DataMapper;

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
            get {
                int _Cantidad = 0;
                try
                {
                    var res = (from o in this._Detalles
                               select o.Cantidad).Sum();
                    _Cantidad = res;
                }
                catch (Exception)
                {

                }

                return _Cantidad;
            }
        }
        public const string CantidadPropertyName = "Cantidad";

        public ObservableCollection<InventarioFisicoDetalleModel> Detalles
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
        private ObservableCollection<InventarioFisicoDetalleModel> _Detalles;
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

        public void AddDetalle(InventarioFisicoDetalleModel detalle)
        {
            if(this.Detalles!=null)
            {
                //Si ya existe el detalle
                var res=(from o in this.Detalles
                        where o.Item.UnidItem==detalle.Item.UnidItem
                        select o).First();

                //El elemento ya existe. Hay que sumar a la cantidad
                if(res!=null)
                {
                    res.Cantidad+=detalle.Cantidad;
                }
                else
                {
                    this.Detalles.Add(detalle);
                }
            }
        }

        #region Constructor Logic
        public InventarioFisicoModel()
        {
            this.init();
        }

        public void init()
        {
            this._Detalles = new ObservableCollection<InventarioFisicoDetalleModel>();
            this._unid = UNID.getNewUNID();
            this._FechaIni = DateTime.Now;
            this._Status = false;
        }
        #endregion

        #region DataAccessFunctions
        public void Load(long unid)
        {
        }

        public void Save()
        {
        } 
        #endregion

        #region IModelDataConverter
        public object ConvertTo()
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object data)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
