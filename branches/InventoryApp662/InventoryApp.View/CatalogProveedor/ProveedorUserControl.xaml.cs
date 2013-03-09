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
using InventoryApp.ViewModel.CatalogProveedor;

namespace InventoryApp.View.CatalogProveedor
{
    /// <summary>
    /// Lógica de interacción para ProveedorUserControl.xaml
    /// </summary>
    public partial class ProveedorUserControl : UserControl
    {
        public ProveedorUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaModificarProveedor alta = new AltaModificarProveedor();
            try
            {
                CatalogProveedorViewModel proveedorViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogProveedorViewModel;
                alta.DataContext = proveedorViewModel.CreateAddProveedorViewModel();
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
                    ModifyProveedorView dlgModify = new ModifyProveedorView();
                    try
                    {
                        CatalogProveedorViewModel proveedorViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogProveedorViewModel;
                        dlgModify.DataContext = proveedorViewModel.CreateModifyProveedorViewModel();
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
