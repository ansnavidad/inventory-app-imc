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
    /// Lógica de interacción para GridSalidaDemo.xaml
    /// </summary>
    public partial class GridSalidaDemo : UserControl
    {
        public GridSalidaDemo()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            SalidaDemo dlg = new SalidaDemo();
            MovimientoGridSalidaDemoViewModel viewModel = this.DataContext as MovimientoGridSalidaDemoViewModel;
            dlg.DataContext = new SalidaDemoViewModel(viewModel);
            dlg.ShowDialog();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    SalidaDemoSoloLectura readOnly = new SalidaDemoSoloLectura();
                    try
                    {
                        MovimientoGridSalidaDemoViewModel sololectura = new MovimientoGridSalidaDemoViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridSalidaDemoViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlySalidaDemoViewModel();
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
