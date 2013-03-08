using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.DAL.Recibo;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model.CatalogInventario
{
    public class Descriptor: ModelBase
    {
        #region Properties

        public string DescriptorName
        {
            get { return _DescriptorName; }
            set
            {
                if (_DescriptorName != value)
                {
                    _DescriptorName = value;
                    OnPropertyChanged(DescriptorNamePropertyName);
                }
            }
        }
        private string _DescriptorName;
        public const string DescriptorNamePropertyName = "DescriptorName";

        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    OnPropertyChanged(IsCheckedPropertyName);
                }
            }
        }
        private bool _IsChecked;
        public const string IsCheckedPropertyName = "IsChecked";

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

        #endregion

        #region Constructors


        #endregion

        #region Methods

        #endregion
    }
}
