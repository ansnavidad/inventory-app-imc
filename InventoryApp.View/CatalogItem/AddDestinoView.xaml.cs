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
    /// Lógica de interacción para AddDestinoView.xaml
    /// </summary>
    public partial class AddDestinoView : Window
    {
        public AddDestinoView()
        {
            InitializeComponent();
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            comboAlmacen.IsEnabled = true;
            comboAlmacen.SelectedIndex = 0;
            ComboProveedor.IsEnabled = false;
            ComboProveedor.SelectedIndex = -1;
            comboCliente.IsEnabled = false;
            comboCliente.SelectedIndex = -1;
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            comboAlmacen.IsEnabled = false;
            comboAlmacen.SelectedIndex = -1;
            ComboProveedor.IsEnabled = false;
            ComboProveedor.SelectedIndex = -1;
            comboCliente.IsEnabled = true;
            comboCliente.SelectedIndex = 0;

        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            comboAlmacen.IsEnabled = false;
            comboAlmacen.SelectedIndex = -1;
            ComboProveedor.IsEnabled = true;
            ComboProveedor.SelectedIndex = 0;
            comboCliente.IsEnabled = false;
            comboCliente.SelectedIndex = -1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
