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
    /// Lógica de interacción para GridSalidaRevision.xaml
    /// </summary>
    public partial class GridSalidaRevision : UserControl
    {
        public GridSalidaRevision()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            SalidaRevision dlg = new SalidaRevision();
            MovimientoGridSalidaRevisionViewModel viewModel = this.DataContext as MovimientoGridSalidaRevisionViewModel;
            dlg.DataContext = new SalidaRevisionViewModel(viewModel);
            dlg.ShowDialog();
        }
    }
}
