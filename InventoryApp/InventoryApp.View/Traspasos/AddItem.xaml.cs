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

namespace InventoryApp.View.Traspasos
{
    /// <summary>
    /// Lógica de interacción para AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            this.txtNumSerie.Text = "";
            this.txtSKU.Text = "";

            this.txtSKU.IsEnabled = false;
            this.txtNumSerie.IsEnabled = true;
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            this.txtNumSerie.Text = "";
            this.txtSKU.Text = "";

            this.txtSKU.IsEnabled = true;
            this.txtNumSerie.IsEnabled = false;
        }
    }
}
