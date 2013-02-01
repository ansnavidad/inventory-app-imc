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
using InventoryApp.View.Salidas;
using InventoryApp.ViewModel.GridMovimientos;
using InventoryApp.ViewModel.Salidas;

namespace InventoryApp.View.GridMovimientos
{
    /// <summary>
    /// Lógica de interacción para GridSalidaVenta.xaml
    /// </summary>
    public partial class GridSalidaVenta : UserControl
    {
        public GridSalidaVenta()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            SalidaVenta dlg = new SalidaVenta();
            MovimientoGridSalidaVentaViewModel viewModel = this.DataContext as MovimientoGridSalidaVentaViewModel;
            dlg.DataContext = new SalidaVentaViewModel(viewModel);
            dlg.ShowDialog();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    SalidaVentaSoloLectura readOnly = new SalidaVentaSoloLectura();
                    try
                    {
                        MovimientoGridSalidaVentaViewModel sololectura = new MovimientoGridSalidaVentaViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridSalidaVentaViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlySalidaVentaViewModel();
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
