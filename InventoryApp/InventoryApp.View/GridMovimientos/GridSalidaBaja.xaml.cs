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
    /// Lógica de interacción para GridSalidaBaja.xaml
    /// </summary>
    public partial class GridSalidaBaja : UserControl
    {
        public GridSalidaBaja()
        {
            InitializeComponent();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            SalidaBaja dlg = new SalidaBaja();
            MovimientoGridSalidaBajaViewModel viewModel = this.DataContext as MovimientoGridSalidaBajaViewModel;
            dlg.DataContext = new SalidaBajaViewModel(viewModel);
            dlg.ShowDialog();

        }
    }
}
