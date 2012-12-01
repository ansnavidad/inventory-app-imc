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
    /// Lógica de interacción para EntradasDevolucion.xaml
    /// </summary>
    public partial class EntradasDevolucion : UserControl
    {
        public EntradasDevolucion()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Entradas.EntradaDevolucion dlg = new Entradas.EntradaDevolucion();
            InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasDevolucionViewModel viewModel = this.DataContext as InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasDevolucionViewModel;
            dlg.DataContext = new InventoryApp.ViewModel.Entradas.EntradaDevolucionViewModel(viewModel);
            dlg.ShowDialog();
        }
    }
}
