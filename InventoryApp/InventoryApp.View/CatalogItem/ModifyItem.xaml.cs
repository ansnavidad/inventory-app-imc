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
using InventoryApp.View.Recibo;

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
                     
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            txtNomreStatus.IsEnabled = false;
            textBoxGuía.IsEnabled = false;
            button1.IsEnabled = false;
            BotonNueva.IsEnabled = true;
        }

        private void BotonNueva_Click(object sender, RoutedEventArgs e)
        {
            checkBox1.IsChecked = false;

            txtNomreStatus.IsEnabled = true;
            textBoxGuía.IsEnabled = true;
            textBox1.IsEnabled = true;
            button1.IsEnabled = true;
            BotonNueva.IsEnabled = false;
            
            textBlock2.Text = "";
            txtNomreStatus.Text = "";
            textBox1.Text = "";

            ModifyItemViewModel m = new ModifyItemViewModel();
            this.DataContext = m;
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            DlgAddFacturaView addFactura = new DlgAddFacturaView();
            AgregarItemViewModel viewModel = new AgregarItemViewModel(null);
            addFactura.DataContext = viewModel.CreateAddFacturaViewModel();
            addFactura.ShowDialog();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            FacturaCatalogAgregarItem addFactura = new FacturaCatalogAgregarItem();
            ModifyItemViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateModifyFacturaViewModel();
            addFactura.ShowDialog();
        }

        private ModifyItemViewModel ConvertDataContext(object dataSource)
        {
            ModifyItemViewModel viewModel = dataSource as ModifyItemViewModel;
            return viewModel;
        }
    }
}
