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
            alta.ShowDialog();
        }

        private void dtGridItemStatus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    (new ModifyProveedorView()).ShowDialog();
                }
            }
        }
    }
}
