using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.Model.Recibo;
using System.Collections.ObjectModel;

namespace InventoryApp.ViewModel.CatalogFactura
{
    public interface IFacturaViewModel
    {
        ProveedorModel SelectedProveedor { get; }
        ObservableCollection<FacturaCompraDetalleModel> FacturaDetalles{get;}
        IFacturaArticuloViewModel CreateFacturaArticuloViewModel();

        DateTime FechaFactura { get; }
        MonedaModel SelectedMoneda { get; }
        String NumeroFactura { get; }
        string NumeroPedimento { get; }
        double PorIva { get; }
        
        long UnidFactura { get; }
        
        
        
        TipoPedimentoModel SelectedTipoPedimento { get; }
        
    }
}
