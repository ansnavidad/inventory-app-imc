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
    /// Lógica de interacción para GridEntradas.xaml
    /// </summary>
    public partial class GridEntradas : UserControl
    {
        public GridEntradas()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Entradas.EntradaValidacionRevision dlg = new Entradas.EntradaValidacionRevision();
            InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel viewModel = this.DataContext as InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel;
            dlg.DataContext = new InventoryApp.ViewModel.Entradas.EntradaPorValidacionViewModel(viewModel);
            dlg.ShowDialog();

            //Entradas.EntradaPrestamo dlg = new Entradas.EntradaPrestamo();
            //InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel viewModel = this.DataContext as InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel;
            //dlg.DataContext = new InventoryApp.ViewModel.Entradas.EntradaPrestamoViewModel(viewModel);
            //dlg.ShowDialog();

            //Entradas.EntradaDesinstalacion dlg = new Entradas.EntradaDesinstalacion();
            //InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel viewModel = this.DataContext as InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel;
            //dlg.DataContext = new InventoryApp.ViewModel.Entradas.EntradaDesinstalacionViewModel(viewModel);
            //dlg.ShowDialog();

            //Entradas.EntradaDevolucion dlg = new Entradas.EntradaDevolucion();
            //InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel viewModel = this.DataContext as InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel;
            //dlg.DataContext = new InventoryApp.ViewModel.Entradas.EntradaDevolucionViewModel(viewModel);
            //dlg.ShowDialog();
        }
    }
}
