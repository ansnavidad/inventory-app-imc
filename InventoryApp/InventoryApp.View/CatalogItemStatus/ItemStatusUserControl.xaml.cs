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

namespace InventoryApp.View.CatalogItemStatus
{
    /// <summary>
    /// Lógica de interacción para ItemStatusUserControl.xaml
    /// </summary>
    public partial class ItemStatusUserControl : UserControl
    {
        public ItemStatusUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaItemStatus alta = new AltaItemStatus();
            alta.ShowDialog();
        }

        private void dtGridItemStatus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    (new ModifyItemStatusView()).ShowDialog();
                }
            }
        }
    }
}
