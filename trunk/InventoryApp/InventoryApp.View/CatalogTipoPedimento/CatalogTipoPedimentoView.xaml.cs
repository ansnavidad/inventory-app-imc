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
using System.Windows.Shapes;
using InventoryApp.View.CatalogTipoPedimento;

namespace InventoryApp.View.CatalogTipoPedimento
{
    /// <summary>
    /// Lógica de interacción para CatalogTipoPedimentoView.xaml
    /// </summary>
    public partial class CatalogTipoPedimentoView : Window
    {
        public CatalogTipoPedimentoView()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaModificarTipoPedimento alta = new AltaModificarTipoPedimento();
            alta.ShowDialog();
        }

        private void dtGridItemStatus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    (new ModifyTipoPedimentoView()).ShowDialog();
                }
            }
        }
    }
}
