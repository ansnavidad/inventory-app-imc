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
    }
}
