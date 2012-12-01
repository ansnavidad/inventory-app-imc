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

namespace InventoryApp.View.GridMovimientos
{
    /// <summary>
    /// Lógica de interacción para EntradasDesinstalacion.xaml
    /// </summary>
    public partial class EntradasDesinstalacion : UserControl
    {
        public EntradasDesinstalacion()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Entradas.EntradaDesinstalacion dlg = new Entradas.EntradaDesinstalacion();
            InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasDesinstalacionViewModel viewModel = this.DataContext as InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasDesinstalacionViewModel;
            dlg.DataContext = new InventoryApp.ViewModel.Entradas.EntradaDesinstalacionViewModel(viewModel);
            dlg.ShowDialog();
        }
    }
}
