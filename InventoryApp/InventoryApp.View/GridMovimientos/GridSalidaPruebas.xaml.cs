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
    /// Lógica de interacción para GridSalidaPruebas.xaml
    /// </summary>
    public partial class GridSalidaPruebas : UserControl
    {
        public GridSalidaPruebas()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            SalidaPruebas dlg = new SalidaPruebas();
            MovimientoGridSalidaPruebasViewModel viewModel = this.DataContext as MovimientoGridSalidaPruebasViewModel;
            dlg.DataContext = new SalidaPruebasViewModel(viewModel);
            dlg.ShowDialog();
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    SalidaPruebasSoloLectura readOnly = new SalidaPruebasSoloLectura();
                    try
                    {
                        MovimientoGridSalidaPruebasViewModel sololectura = new MovimientoGridSalidaPruebasViewModel("solo lectura");
                        sololectura = this.DataContext as MovimientoGridSalidaPruebasViewModel;
                        readOnly.DataContext = sololectura.CreateReadOnlySalidaPruebasViewModel();
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
