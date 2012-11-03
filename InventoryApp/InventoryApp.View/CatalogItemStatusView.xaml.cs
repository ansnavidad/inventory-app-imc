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
using InventoryApp.ViewModel;

namespace InventoryApp.View
{
    /// <summary>
    /// Lógica de interacción para CatalogItemStatusView.xaml
    /// </summary>
    public partial class CatalogItemStatusView : Window
    {
        public CatalogItemStatusView()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaItemStatus alta = new AltaItemStatus();
            alta.ShowDialog();
        }
    }
}
