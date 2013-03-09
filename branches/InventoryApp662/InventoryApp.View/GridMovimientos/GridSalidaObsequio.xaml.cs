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
    /// Lógica de interacción para GridSalidaObsequio.xaml
    /// </summary>
    public partial class GridSalidaObsequio : UserControl
    {
        public GridSalidaObsequio()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            SalidaObsequio dlg = new SalidaObsequio();
            MovimientoGridSalidaObsequioViewModel viewModel = this.DataContext as MovimientoGridSalidaObsequioViewModel;
            dlg.DataContext = new SalidaObsequioViewModel(viewModel);
            dlg.ShowDialog();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    SalidaObsequioSoloLectura readOnly = new SalidaObsequioSoloLectura();
                    try
                    {
                        MovimientoGridSalidaObsequioViewModel sololectura = new MovimientoGridSalidaObsequioViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridSalidaObsequioViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlySalidaObsequioViewModel();
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
