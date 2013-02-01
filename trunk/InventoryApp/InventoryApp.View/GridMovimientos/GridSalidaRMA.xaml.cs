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
    /// Lógica de interacción para GridSalidaRMA.xaml
    /// </summary>
    public partial class GridSalidaRMA : UserControl
    {
        public GridSalidaRMA()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            SalidaRMA dlg = new SalidaRMA();
            MovimientoGridSalidaRMAViewModel viewModel = this.DataContext as MovimientoGridSalidaRMAViewModel;
            dlg.DataContext = new SalidaRMAViewModel(viewModel);
            dlg.ShowDialog();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    SalidaRMASoloLectura readOnly = new SalidaRMASoloLectura();
                    try
                    {
                        MovimientoGridSalidaRMAViewModel sololectura = new MovimientoGridSalidaRMAViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridSalidaRMAViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlySalidaRMAViewModel();
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
