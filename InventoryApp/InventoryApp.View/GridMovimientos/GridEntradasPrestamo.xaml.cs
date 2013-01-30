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
using InventoryApp.ViewModel.GridMovimientos;

namespace InventoryApp.View.GridMovimientos
{
    /// <summary>
    /// Lógica de interacción para GridEntradasPrestamo.xaml
    /// </summary>
    public partial class GridEntradasPrestamo : UserControl
    {
        public GridEntradasPrestamo()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Entradas.EntradaPrestamo dlg = new Entradas.EntradaPrestamo();
            InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasPrestamoViewModel viewModel = this.DataContext as InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasPrestamoViewModel;
            dlg.DataContext = new InventoryApp.ViewModel.Entradas.EntradaPrestamoViewModel(viewModel);
            dlg.ShowDialog();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    Entradas.EntradaPrestamoSoloLectura readOnly = new Entradas.EntradaPrestamoSoloLectura();
                    try
                    {
                        MovimientoGridEntradasPrestamoViewModel sololectura = new MovimientoGridEntradasPrestamoViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridEntradasPrestamoViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlyEntradaPrestamoViewModel();
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
