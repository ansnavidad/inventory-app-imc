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
using InventoryApp.ViewModel.CatalogItem;

namespace InventoryApp.View.CatalogItem
{
    /// <summary>
    /// Lógica de interacción para ModifyItem.xaml
    /// </summary>
    public partial class ModifyItem : UserControl
    {
        public ModifyItem()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            AddFactura addFactura = new AddFactura();
            ModifyItemViewModel viewModel = this.DataContext as ModifyItemViewModel;
            addFactura.DataContext = viewModel.CreateAgregarFacturaViewModel(); ;
            addFactura.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("El item ha sido modificado");
            
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            comboPropiedad.IsEnabled = true;
           
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            comboPropiedad.IsEnabled = false;
            comboPropiedad.SelectedIndex = -1;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            txtNomreStatus.IsEnabled = false;
            textBox1.IsEnabled = false;
            button1.IsEnabled = false;
            BotonNueva.IsEnabled = true;
        }

        private void BotonNueva_Click(object sender, RoutedEventArgs e)
        {
            txtNomreStatus.IsEnabled = true;
            textBox1.IsEnabled = true;
            button1.IsEnabled = true;
            BotonNueva.IsEnabled = false;
            
            textBlock2.Text = "";
            txtNomreStatus.Text = "";
            textBox1.Text = "";
        }          
    }
}
