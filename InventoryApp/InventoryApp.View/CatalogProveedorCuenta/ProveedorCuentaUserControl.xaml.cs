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
using InventoryApp.ViewModel.CatalogProveedorCuenta;

namespace InventoryApp.View.CatalogProveedorCuenta
{
    /// <summary>
    /// Lógica de interacción para ProveedorCuentaUserControl.xaml
    /// </summary>
    public partial class ProveedorCuentaUserControl : UserControl
    {
        public ProveedorCuentaUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaModificarProveedorCuenta alta = new AltaModificarProveedorCuenta();
            try
            {
                CatalogProveedorCuentaViewModel proveedorCuentaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogProveedorCuentaViewModel;
                alta.DataContext = proveedorCuentaViewModel.CreateAddProveedorCuentaViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dtGridItemStatus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyProveedorCuentaView dlgModify = new ModifyProveedorCuentaView();
                    try
                    {
                        CatalogProveedorCuentaViewModel proveedorViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogProveedorCuentaViewModel;
                        dlgModify.DataContext = proveedorViewModel.CreateModifyProveedorCuentaViewModel();
                        dlgModify.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
