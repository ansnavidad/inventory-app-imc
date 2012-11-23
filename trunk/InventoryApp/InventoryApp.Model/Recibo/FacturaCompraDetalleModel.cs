using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model.Recibo
{
    public class FacturaCompraDetalleModel : ModelBase
    {
        #region Properties
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    OnPropertyChanged(IsSelectedPropertyName);
                }
            }
        }
        private bool _IsSelected;
        public const string IsSelectedPropertyName = "IsSelected";

        public long UnidFacturaCompraDetalle
        {
            get { return _UnidFacturaCompraDetalle; }
            set
            {
                if (_UnidFacturaCompraDetalle != value)
                {
                    _UnidFacturaCompraDetalle = value;
                    OnPropertyChanged(UnidFacturaCompraDetallePropertyName);
                }
            }
        }
        private long _UnidFacturaCompraDetalle;
        public const string UnidFacturaCompraDetallePropertyName = "UnidFacturaCompraDetalle";

        public FacturaCompraModel Factura
        {
            get { return _Factura; }
            set
            {
                if (_Factura != value)
                {
                    _Factura = value;
                    OnPropertyChanged(FacturaPropertyName);
                }
            }
        }
        private FacturaCompraModel _Factura;
        public const string FacturaPropertyName = "Factura";

        public ArticuloModel Articulo
        {
            get { return _Articulo; }
            set
            {
                if (_Articulo != value)
                {
                    _Articulo = value;
                    OnPropertyChanged(ArticuloPropertyName);
                }
            }
        }
        private ArticuloModel _Articulo;
        public const string ArticuloPropertyName = "Articulo";

        public UnidadModel Unidad
        {
            get { return _Unidad; }
            set
            {
                if (_Unidad != value)
                {
                    _Unidad = value;
                    OnPropertyChanged(UnidadPropertyName);
                }
            }
        }
        private UnidadModel _Unidad;
        public const string UnidadPropertyName = "Unidad";

        public int Cantidad
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
        private int _Cantidad;
        public const string CantidadPropertyName = "Cantidad";

        public int CantidadElegida
        {
            get { return _CantidadElegida; }
            set
            {
                if (_CantidadElegida != value)
                {
                    _CantidadElegida = value;
                    OnPropertyChanged(CantidadElegidaPropertyName);
                }
            }
        }
        private int _CantidadElegida;
        public const string CantidadElegidaPropertyName = "CantidadElegida";

        public double PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set
            {
                if (_PrecioUnitario != value)
                {
                    _PrecioUnitario = value;
                    OnPropertyChanged(PrecioUnitarioPropertyName);
                }
            }
        }
        private double _PrecioUnitario;
        public const string PrecioUnitarioPropertyName = "PrecioUnitario";

        public double ImpuestoUnitario
        {
            get { return _ImpuestoUnitario; }
            set
            {
                if (_ImpuestoUnitario != value)
                {
                    _ImpuestoUnitario = value;
                    OnPropertyChanged(ImpuestoUnitarioPropertyName);
                }
            }
        }
        private double _ImpuestoUnitario;
        public const string ImpuestoUnitarioPropertyName = "ImpuestoUnitario";

        public string Numero
        {
            get { return _Numero; }
            set
            {
                if (_Numero != value)
                {
                    _Numero = value;
                    OnPropertyChanged(NumeroPropertyName);
                }
            }
        }
        private string _Numero;
        public const string NumeroPropertyName = "Numero";

        public string Descripcion
        {
            get { return _Descripcion; }
            set
            {
                if (_Descripcion != value)
                {
                    _Descripcion = value;
                    OnPropertyChanged(DescripcionPropertyName);
                }
            }
        }
        private string _Descripcion;
        public const string DescripcionPropertyName = "Descripcion";

        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged(IsActivePropertyName);
                }
            }
        }
        private bool _IsActive;
        public const string IsActivePropertyName = "IsActive";
        #endregion

    }
}
