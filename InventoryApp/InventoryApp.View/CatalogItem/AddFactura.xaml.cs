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

namespace InventoryApp.View.CatalogItem
{
    /// <summary>
    /// Lógica de interacción para AddFactura.xaml
    /// </summary>
    public partial class AddFactura : Window
    {
        public AddFactura()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
