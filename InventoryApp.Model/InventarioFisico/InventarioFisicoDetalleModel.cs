using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Model.InventarioFisico
{
    public class InventarioFisicoDetalleModel : ModelBase
    {
        public Recibo.ReciboItemModel Item
        {
            get { return _Item; }
            set
            {
                if (_Item != value)
                {
                    _Item = value;
                    OnPropertyChanged(ItemPropertyName);
                }
            }
        }
        private Recibo.ReciboItemModel _Item;
        public const string ItemPropertyName = "Item";

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

        public String SKU
        {
            get { return _SKU; }
            set
            {
                if (_SKU != value)
                {
                    _SKU = value;
                    OnPropertyChanged(SKUPropertyName);
                }
            }
        }
        private String _SKU;
        public const string SKUPropertyName = "SKU";

        public String NumSerie
        {
            get { return _NumSerie; }
            set
            {
                if (_NumSerie != value)
                {
                    _NumSerie = value;
                    OnPropertyChanged(NumSeriePropertyName);
                }
            }
        }
        private String _NumSerie;
        public const string NumSeriePropertyName = "NumSerie";

        public long UnidInvFis
        {
            get { return _UnidInvFis; }
            set
            {
                if (_UnidInvFis != value)
                {
                    _UnidInvFis = value;
                    OnPropertyChanged(UnidInvFisPropertyName);
                }
            }
        }
        private long _UnidInvFis;
        public const string UnidInvFisPropertyName = "UnidInvFis";

        public bool IsModified
        {
            get { return _IsModified; }
            set
            {
                if (_IsModified != value)
                {
                    _IsModified = value;
                    OnPropertyChanged(IsModifiedPropertyName);
                }
            }
        }
        private bool _IsModified;
        public const string IsModifiedPropertyName = "IsModified";

        public bool IsNew
        {
            get { return _IsNew; }
            set
            {
                if (_IsNew != value)
                {
                    _IsNew = value;
                    OnPropertyChanged(IsNewPropertyName);
                }
            }
        }
        private bool _IsNew;
        public const string IsNewPropertyName = "IsNew";

        public bool Is_Active
        {
            get { return _Is_Active; }
            set
            {
                if (_Is_Active != value)
                {
                    _Is_Active = value;
                    OnPropertyChanged(Is_ActivePropertyName);
                }
            }
        }
        private bool _Is_Active;
        public const string Is_ActivePropertyName = "Is_Active";
    }
}
