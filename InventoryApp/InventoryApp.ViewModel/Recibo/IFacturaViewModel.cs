using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.Model.Recibo;
using System.Collections.ObjectModel;

namespace InventoryApp.ViewModel.Recibo
{
    public interface IFacturaViewModel
    {
        ProveedorModel SelectedProveedor { get; }
        ObservableCollection<FacturaCompraDetalleModel> FacturaDetalles{get;}
        double PorIva { get; set; }
    }
}
