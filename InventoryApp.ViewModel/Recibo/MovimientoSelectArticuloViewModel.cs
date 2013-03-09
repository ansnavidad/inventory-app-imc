using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.Model.Recibo;

namespace InventoryApp.ViewModel.Recibo
{
    public class MovimientoSelectArticuloViewModel : ViewModelBase
    {
        private FacturaCompraModel _SelectedFactura;

        public ObservableCollection<FacturaCompraDetalleModel> FacturaDetalles
        {
            get { return _FacturaDetalles; }
            set
            {
                if (_FacturaDetalles != value)
                {
                    _FacturaDetalles = value;
                    OnPropertyChanged(FacturaDetallesPropertyName);
                }
            }
        }
        private ObservableCollection<FacturaCompraDetalleModel> _FacturaDetalles;
        public const string FacturaDetallesPropertyName = "FacturaDetalles";

        public MovimientoSelectArticuloViewModel(FacturaCompraModel SelectedFactura)
        {
            if (SelectedFactura != null)
            {
                this._SelectedFactura = SelectedFactura;
                this.FacturaDetalles = SelectedFactura.FacturaDetalle;
            }
        }
    }
}
