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
    /// Lógica de interacción para GridSalidaPrestamo.xaml
    /// </summary>
    public partial class GridSalidaPrestamo : UserControl
    {
        public GridSalidaPrestamo()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            SalidaPrestamo dlg = new SalidaPrestamo();
            MovimientoGridSalidaPrestamoViewModel viewModel = this.DataContext as MovimientoGridSalidaPrestamoViewModel;
            dlg.DataContext = new SalidaPrestamoViewModel(viewModel);
            dlg.ShowDialog();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    SalidaPrestamoSoloLectura readOnly = new SalidaPrestamoSoloLectura();
                    try
                    {
                        MovimientoGridSalidaPrestamoViewModel sololectura = new MovimientoGridSalidaPrestamoViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridSalidaPrestamoViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlySalidaPrestamoViewModel();
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
