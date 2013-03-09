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
using InventoryApp.ViewModel.CatalogInventario;

namespace InventoryApp.View.CatalogInventario
{
    /// <summary>
    /// Lógica de interacción para EntradasDesinstalacion.xaml
    /// </summary>
    public partial class CatalogInv : UserControl
    {
        public CatalogInv()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaInventario addMovimientoView = new AltaInventario();
            CatalogInvViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addMovimientoView.DataContext = viewModel.CreateAddMovimientoViewModel();
            addMovimientoView.ShowDialog();
        }

        private CatalogInvViewModel ConvertDataContext(object dataSource)
        {
            CatalogInvViewModel viewModel = dataSource as CatalogInvViewModel;
            return viewModel;
        }

        private void dtGridMovimiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModificacionInventario dlgModifyFacturaView = new ModificacionInventario();
                    CatalogInvViewModel viewModel = this.ConvertDataContext(this.DataContext);
                    ModifyInventarioViewModel mfvm = viewModel.CreateModifyInventarioViewModel();
                    if (mfvm != null)
                    {
                        dlgModifyFacturaView.DataContext = mfvm;
                        dlgModifyFacturaView.ShowDialog();
                    }
                }
            }
        }
    }
}
