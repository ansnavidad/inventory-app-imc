using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventoryApp.View.Traspasos;
using InventoryApp.ViewModel.GridMovimientos;
using InventoryApp.ViewModel.Traspasos;

namespace InventoryApp.View.GridMovimientos
{
    /// <summary>
    /// Lógica de interacción para GridTraspasoStock.xaml
    /// </summary>
    public partial class GridTraspasoStock : UserControl
    {
        public GridTraspasoStock()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            TraspasoStock dlg = new TraspasoStock();
            MovimientoGridTraspasoStockViewModel viewModel = this.DataContext as MovimientoGridTraspasoStockViewModel;
            dlg.DataContext = new TraspasoStockViewModel(viewModel);
            dlg.ShowDialog();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    TraspasoStockSoloLectura readOnly = new TraspasoStockSoloLectura();
                    try
                    {
                        MovimientoGridTraspasoStockViewModel sololectura = new MovimientoGridTraspasoStockViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridTraspasoStockViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlyTraspasoStockViewModel();
                        readOnly.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }
    }
}
