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

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    Entradas.EntradaDevolucionSoloLectura readOnly = new Entradas.EntradaDevolucionSoloLectura();
                    try
                    {
                        MovimientoGridEntradasDevolucionViewModel sololectura = new MovimientoGridEntradasDevolucionViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridEntradasDevolucionViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlyEntradaDevolucionViewModel();
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
