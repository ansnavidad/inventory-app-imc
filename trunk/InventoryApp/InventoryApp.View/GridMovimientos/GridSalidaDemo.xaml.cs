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
    }
}
