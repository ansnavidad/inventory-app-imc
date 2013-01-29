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

namespace InventoryApp.View.CatalogFactura
{
    /// <summary>
    /// Lógica de interacción para DlgAddFacturaArticuloView.xaml
    /// </summary>
    public partial class DlgAddFacturaArticuloView : Window
    {
        public DlgAddFacturaArticuloView()
        {
            InitializeComponent();
        }

        private void btnFacturaDel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnFacturaAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddFacturaArticulo_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
