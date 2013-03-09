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

           
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    Entradas.EntradaValidacionRevisionSoloLectura readOnly = new Entradas.EntradaValidacionRevisionSoloLectura();
                    try
                    {
                        MovimientoGridEntradasViewModel sololectura = new MovimientoGridEntradasViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridEntradasViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlyEntradaPorValidacionViewModel();
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
