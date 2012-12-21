﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Model.Recibo
{
    public class ReciboItemModel : ModelBase
    {
        public long UnidItem
        {
            get { return _UnidItem; }
            set
            {
                if (_UnidItem != value)
                {
                    _UnidItem = value;
                    OnPropertyChanged(UnidItemPropertyName);
                }
            }
        }
        private long _UnidItem;
        public const string UnidItemPropertyName = "UnidItem";

        public string Sku
        {
            get { return _Sku; }
            set
            {
                if (_Sku != value)
                {
                    _Sku = value;
                    OnPropertyChanged(SkuPropertyName);
                }
            }
        }
        private string _Sku;
        public const string SkuPropertyName = "Sku";

        public string NumeroSerie
        {
            get { return _NumeroSerie; }
            set
            {
                if (_NumeroSerie != value)
                {
                    _NumeroSerie = value;
                    OnPropertyChanged(NumeroSeriePropertyName);
                }
            }
        }
        private string _NumeroSerie;
        public const string NumeroSeriePropertyName = "NumeroSerie";

        public string Status
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
        private string _Status;
        public const string StatusPropertyName = "Status";

        public double CostoUnitario
        {
            get { return _CostoUnitario; }
            set
            {
                if (_CostoUnitario != value)
                {
                    _CostoUnitario = value;
                    OnPropertyChanged(CostoUnitarioPropertyName);
                }
            }
        }
        private double _CostoUnitario;
        public const string CostoUnitarioPropertyName = "CostoUnitario";

        public FacturaCompraDetalleModel FacturaDetalle
        {
            get { return _FacturaDetalle; }
            set
            {
                if (_FacturaDetalle != value)
                {
                    _FacturaDetalle = value;
                    OnPropertyChanged(FacturaDetallePropertyName);
                }
            }
        }
        private FacturaCompraDetalleModel _FacturaDetalle;
        public const string FacturaDetallePropertyName = "FacturaDetalle";

        public EmpresaModel Empresa
        {
            get { return _Empresa; }
            set
            {
                if (_Empresa != value)
                {
                    _Empresa = value;
                    OnPropertyChanged(EmpresaPropertyName);
                }
            }
        }
        private EmpresaModel _Empresa;
        public const string EmpresaPropertyName = "Empresa";

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

        public long UnidMovimiento
        {
            get { return _UnidMovimiento; }
            set
            {
                if (_UnidMovimiento != value)
                {
                    _UnidMovimiento = value;
                    OnPropertyChanged(UnidMovimientoPropertyName);
                }
            }
        }
        private long _UnidMovimiento;
        public const string UnidMovimientoPropertyName = "UnidMovimiento";

        public long UnidMovimientoDetalle
        {
            get { return _UnidMovimientoDetalle; }
            set
            {
                if (_UnidMovimientoDetalle != value)
                {
                    _UnidMovimientoDetalle = value;
                    OnPropertyChanged(UnidMovimientoDetallePropertyName);
                }
            }
        }
        private long _UnidMovimientoDetalle;
        public const string UnidMovimientoDetallePropertyName = "UnidMovimientoDetalle";
    }
}