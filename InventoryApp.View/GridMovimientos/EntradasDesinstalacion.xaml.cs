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

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    Entradas.EntradaDesinstalacionSoloLectura readOnly = new Entradas.EntradaDesinstalacionSoloLectura();
                    try
                    {
                        MovimientoGridEntradasDesinstalacionViewModel sololectura = new MovimientoGridEntradasDesinstalacionViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridEntradasDesinstalacionViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlyEntradaDesistalacionViewModel();
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
