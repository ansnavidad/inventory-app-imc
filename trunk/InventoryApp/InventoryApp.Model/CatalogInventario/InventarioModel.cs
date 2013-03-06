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
    public class InventarioModel : ModelBase
    {
        #region Properties
        
        public struct Descriptor
        {
            public string Desc
            {
                get { return _Desc; }
                set
                {
                    if (_Desc != value)
                    {
                        _Desc = value;
                        OnPropertyChanged(DescPropertyName);
                    }
                }
            }
            private string _Desc;
            public const string DescPropertyName = "Desc";

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

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(string propertyName)
            {
                var eh = this.PropertyChanged;
                if (eh != null)
                {
                    eh(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

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

        public bool Finished
        {
            get { return _Finished; }
            set
            {
                if (_Finished != value)
                {
                    _Finished = value;
                    OnPropertyChanged(FinishedPropertyName);
                }
            }
        }
        private bool _Finished;
        public const string FinishedPropertyName = "Finished";
        
        public ObservableCollection<Descriptor> Descriptores
        {
            get { return _Descriptores; }
            set
            {
                if (_Descriptores != value)
                {
                    _Descriptores = value;                    
                    OnPropertyChanged(DescriptoresPropertyName);
                }
            }
        }
        private ObservableCollection<Descriptor> _Descriptores;
        public const string DescriptoresPropertyName = "Descriptores";
                        
        public long UnidSegmento
        {
            get { return _UnidSegmento; }
            set
            {
                if (_UnidSegmento != value)
                {
                    _UnidSegmento = value;
                    OnPropertyChanged(UnidSegmentoPropertyName);
                }
            }
        }
        private long _UnidSegmento;
        public const string UnidSegmentoPropertyName = "UnidSegmento";

        public DateTime Fecha
        {
            get { return _Fecha; }
            set
            {
                if (_Fecha != value)
                {
                    _Fecha = value;
                    OnPropertyChanged(FechaPropertyName);
                }
            }
        }
        private DateTime _Fecha;
        public const string FechaPropertyName = "Fecha";

        public long Cantidad
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
        private long _Cantidad;
        public const string CantidadPropertyName = "Cantidad";

        public ALMACEN SelectedAlmacen
        {
            get { return _SelectedAlmacen; }
            set
            {
                if (_SelectedAlmacen != value)
                {
                    _SelectedAlmacen = value;
                    OnPropertyChanged(SelectedAlmacenPropertyName);
                }
            }
        }
        private ALMACEN _SelectedAlmacen;
        public const string SelectedAlmacenPropertyName = "SelectedAlmacen";

        #endregion

        #region Constructors

        public InventarioModel()
        {            

        }

        #endregion

        #region Methods

        #endregion
    }
}
